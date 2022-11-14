using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalaryService.Domain;

namespace SalaryService.DataAccess
{
    public static class QueryableExtensions
    {
        public static IQueryable<TEntity> QueryableAsNoTracking<TEntity>(this DbContext context)
            where TEntity : class
        {
            return context.Queryable<TEntity>().AsNoTracking();
        }

        public static IQueryable<TEntity> Queryable<TEntity>(this DbContext context)
            where TEntity : class
        {
            return context.Set<TEntity>();
        }

        public static async Task<TEntity> GetByIdAsync<TEntity>(this IQueryable<TEntity> queryable, long id)
            where TEntity : class, IIdentityEntity
        {
            var entity = await queryable.SingleOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                throw new Exception("Entity does not exits");
            }

            return entity;
        }
    }
}
