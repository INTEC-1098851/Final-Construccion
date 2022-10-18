using GenericRepository.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GenericRepository.Repositories
{
    public interface IContextRepository<TContext>
      where TContext : DbContext
    {
        Task<IEnumerable<SqlParameter>> ExecuteSqlAsync(string procedureName = null, List<SqlParameter> sqlParameters = null);
        Task<List<TEntity>> ExecuteFromSqlRaw<TEntity>(string procedureName, List<SqlParameter> sqlParameters = null) where TEntity:class;
    }
    public class ContextRepository<TContext> : IContextRepository<TContext>
        where TContext : DbContext
    {
        private readonly TContext _context;
        public ContextRepository(TContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<SqlParameter>> ExecuteSqlAsync(string procedureName = null, List<SqlParameter> sqlParameters = null)
        {
            string sqlRaw = $"{procedureName} {sqlParameters.JoinSqlParameters()}";
            //int response;
            if (sqlParameters == null)
                await _context.Database.ExecuteSqlRawAsync(sqlRaw);
            else
                await _context.Database.ExecuteSqlRawAsync(sqlRaw, sqlParameters);
            return sqlParameters.Where(x => x.Direction == ParameterDirection.Output);
        }
        public virtual Task<List<TEntity>> ExecuteFromSqlRaw<TEntity>(string procedureName, List<SqlParameter> sqlParameters = null) where TEntity : class
        {
            DbSet<TEntity> collections = _context.Set<TEntity>();
            List<TEntity> result;
            if (sqlParameters == null)
                result = collections.FromSqlRaw($"exec {procedureName}").ToList();
            else
                result = collections.FromSqlRaw($"exec {procedureName} {sqlParameters.JoinSqlParameters()}", sqlParameters?.ToArray())
                  .ToList();

            return Task.FromResult(result);
        }
    }
}