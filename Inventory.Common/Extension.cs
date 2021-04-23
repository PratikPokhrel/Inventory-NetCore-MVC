using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Common
{
    public static class Extension
    {

        public static IQueryable<TEntity> OrderResult<TEntity>(this IQueryable<TEntity> source,
                                                    string orderByProperty, bool desc)
        {
            string command = desc ? "OrderByDescending" : "OrderBy";
            var type = typeof(TEntity);
            var property = type.GetProperty(orderByProperty);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command,
                                                   new[] { type, property.PropertyType },
                                                   source.AsQueryable().Expression,
                                                   Expression.Quote(orderByExpression));
            return source.AsQueryable().Provider.CreateQuery<TEntity>(resultExpression);
        }


        public static async Task<List<T>> GetPagedResult<T>(this IQueryable<T> query, string sortColumn, string sortOrder, int getStartingRecord, int getEndingRecord)
        {
            return await query.OrderResult(sortColumn, sortOrder == "Desc" ? true : false)
                                                     .Skip(getStartingRecord)
                                                     .Take(getEndingRecord).ToListAsync();
        }

    }
}
