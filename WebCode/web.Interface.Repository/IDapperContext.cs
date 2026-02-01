using Dapper;
using System.Data.Common;

namespace web.Interface.Repository
{
    public interface IDapperContext
    {
        public DbConnection CreateConnection();
        public Task<dynamic> ExecuteStoredProcedure(string storedProcedureName, object parameters);
        public Task<IEnumerable<T>> ExecuteStoredProcedureAsync<T>(string storedProcedureName, object parameters);
        public Task<T> ExecuteQueryFirstOrDefaultAsync<T>(string storedProcedureName, object parameters);
        public Task<T> ExecuteStoredProcedureWithOutputAsync<T>(
            string storedProcedureName,
            DynamicParameters parameters,
            string outputParameterName
        );


    }
}
