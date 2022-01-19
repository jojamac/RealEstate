using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.Interfaces.Contexts;
using Infrastructure.DbContexts;
using AutoMapper;
using Application.Interfaces.Repositories;
using Infrastructure.Repositories;
using Application.Interfaces.CacheRepositories;
using Infrastructure.CacheRepositories;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistence (this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            #region Repositories
            
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            services.AddTransient<IPropertyRepository, PropertyRepository>();
            services.AddTransient<IPropertyCacheRepository, PropertyCacheRepository>();
            services.AddTransient<IOwnerRepository, OwnerRepository>();
            services.AddTransient<IOwnerCacheRepository, OwnerCacheRepository>();
            services.AddTransient<IPropertyImageRepository, PropertyImageRepository>();
            services.AddTransient<IPropertyImageCacheRepository, PropertyImageCacheRepository>();
            services.AddTransient<IPropertyTraceRepository, PropertyTraceRepository>();
            services.AddTransient<IPropertyTraceCacheRepository, PropertyTraceCacheRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            #endregion Repositories
        }
    }
}
