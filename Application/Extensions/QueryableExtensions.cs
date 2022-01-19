﻿using AspNetCoreHero.Results;
using AspNetCoreHero.ThrowR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNumber , int pageSize) where T : class
        {
            Throw.Exception.IfNull(source, nameof(source));
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            pageSize = pageSize == 0 ? 1 : pageSize;
            long count = await source.LongCountAsync();
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            List<T> items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return PaginatedResult<T>.Success(items, count, pageNumber, pageSize);
        }
    }
}
