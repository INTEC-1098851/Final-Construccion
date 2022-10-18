using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericRepository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GenericRepository.Extensions
{
    public static class GenericRepositoryExtensions
    {
        public static void ConfigureGenericContextRepository(this IServiceCollection services)
        {
            //services.AddScoped(typeof(IContextRepositoryOld<,>), typeof(ContextRepositoryOld<,>));
            services.AddScoped(typeof(IContextRepository<>), typeof(ContextRepository<>));
        }
        public static async Task<IEnumerable<SqlParameter>> ExecuteSqlAsync(this DbContext context,string procedureName = null, List<SqlParameter> sqlParameters = null)
        {
            string sqlRaw = $"{procedureName} {sqlParameters.JoinSqlParameters()}";
            //int response;
            if (sqlParameters == null)
                await context.Database.ExecuteSqlRawAsync(sqlRaw);
            else
                await context.Database.ExecuteSqlRawAsync(sqlRaw, sqlParameters);
            return sqlParameters.Where(x => x.Direction == ParameterDirection.Output);
        }
        public static string JoinSqlParameters(this List<SqlParameter> sqlParameters)
        {
            List<string> parameters = new();
            if (sqlParameters != null && sqlParameters.Any())
                foreach (var param in sqlParameters)
                {
                    var parameterName = param.ParameterName;
                    parameterName += $"{((param.Direction == ParameterDirection.Output) ? " Output" : null)}";
                    parameters.Add(parameterName);
                }
            return string.Join(",", parameters);
            //return string.Join(",", parameters);
        }

      

   
    }
}
