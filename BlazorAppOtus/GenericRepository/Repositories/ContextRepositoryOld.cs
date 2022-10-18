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
    public interface IContextRepositoryOld<TEntity, TContext>
        where TEntity : class
        where TContext : DbContext
    {
        Task<IEnumerable<SqlParameter>> ExecuteSqlAsync(string procedureName = null, List<SqlParameter> sqlParameters = null);
        Task<List<TEntity>> ExecuteFromSqlRaw(string procedureName, List<SqlParameter> sqlParameters = null);
        //Task<List<T>> ExecuteFromSqlRaw<T>(string procedureName, List<SqlParameter> sqlParameters = null);
    }
    public class ContextRepositoryOld<TEntity, TContext>: IContextRepositoryOld<TEntity,TContext>
        where TEntity : class
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly DbSet<TEntity> _collections;
        public ContextRepositoryOld(TContext context)
        {
            this._context = context;
            this._collections = context.Set<TEntity>();
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
        public virtual Task<List<TEntity>> ExecuteFromSqlRaw(string procedureName, List<SqlParameter> sqlParameters = null)
        {
            List<TEntity> result;
            if (sqlParameters == null)
                result = _collections.FromSqlRaw($"exec {procedureName}").ToList();
            else
                result = _collections.FromSqlRaw($"exec {procedureName} {sqlParameters.JoinSqlParameters()}", sqlParameters?.ToArray())
                  .ToList();

            return Task.FromResult(result);
        }
        //public virtual Task<List<T>> ExecuteFromSqlRaw<T>(string procedureName, List<SqlParameter> sqlParameters = null)
        //{
        //    DbSet<T> collections = _context.Set<T>();
        //    List<T> result;
        //    if (sqlParameters == null)
        //        result = collections.FromSqlRaw($"exec {procedureName}").ToList();
        //    else
        //        result = collections.FromSqlRaw($"exec {procedureName} {sqlParameters.JoinSqlParameters()}", sqlParameters?.ToArray())
        //          .ToList();

        //    return Task.FromResult(result);
        //}

        //public virtual Task<List<object>> ExecuteFromSqlRaw(string typeName,string procedureName, List<SqlParameter> sqlParameters = null)
        //{
        //    var myClassInstance = CreateByTypeName("MyClass");
        //    var dbSet = _context.Set<myClassInstance>();
        //    List<object> result;
        //    if (sqlParameters == null)
        //        result = _collections.FromSqlRaw($"exec {procedureName}").ToList();
        //    else
        //        result = _collections.FromSqlRaw($"exec {procedureName} {sqlParameters.JoinSqlParameters()}", sqlParameters?.ToArray())
        //          .ToList();

        //    return Task.FromResult(result);
        //}

        private static object CreateByTypeName(string typeName)
        {
            // scan for the class type
            var type = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                        from t in assembly.GetTypes()
                        where t.Name == typeName  // you could use the t.FullName as well
                        select t).FirstOrDefault();

            if (type == null)
                throw new InvalidOperationException("Type not found");

            return Activator.CreateInstance(type);
        }
    }
}
