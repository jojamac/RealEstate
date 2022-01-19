using Application.Interfaces.Contexts;
using Application.Interfaces.Shared;
using AspNetCoreHero.Abstractions.Domain;
using AspNetCoreHero.EntityFrameworkCore.AuditTrail;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.DbContexts
{
    public class ApplicationDbContext : AuditableContext, IApplicationDbContext
    {
        private IAuthenticatedUserService _authenticatedUser;
        private IDateTimeService _dateTime;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IAuthenticatedUserService authenticatedUser, IDateTimeService dateTime) : base(options)
        {
            _authenticatedUser = authenticatedUser;
            _dateTime = dateTime;
        }

        public DbSet<Property> Properties { get; set; }

        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<PropertyTrace> PropertyTraces { get; set; }

        public IDbConnection Connection => Database.GetDbConnection();

        public bool HasChanges => ChangeTracker.HasChanges();

        

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        entry.Entity.CreatedOn = _dateTime.NowUtc;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                        entry.Entity.LastModifiedOn = _dateTime.NowUtc;
                        break;
                }
            }
            if(_authenticatedUser.UserId == null)
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            else
            {
                return await base.SaveChangesAsync(_authenticatedUser.UserId);
            }
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach(var property in builder.Model.GetEntityTypes()
                .SelectMany(t =>t.GetProperties())
                .Where(p=>p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }

            base.OnModelCreating(builder);
        }

    }
}
