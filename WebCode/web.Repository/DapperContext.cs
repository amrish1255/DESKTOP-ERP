using System.Data;
using System.Data.Common;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using web.Interface.Repository;

namespace web.Repository
{
    public class DapperContext : IDapperContext
    {

        private readonly ILogger<DapperContext> _logger;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration, ILogger<DapperContext> logger)
        {
            _connectionString = configuration.GetConnectionString("DbCon");
            _logger = logger;
        }

        public DbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
        public async Task<dynamic> ExecuteStoredProcedure(string storedProcedureName, object parameters = null)
        {
            await using var _dbConnection = CreateConnection();
            try
            {
                await _dbConnection.OpenAsync();
                var result = await _dbConnection.QueryAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
            finally
            {
                _dbConnection?.Close();
            }
        }

        public async Task<IEnumerable<T>> ExecuteStoredProcedureAsync<T>(string storedProcedureName, object parameters = null)
        {
            if (string.IsNullOrWhiteSpace(storedProcedureName))
                throw new ArgumentException("Stored procedure name cannot be null or empty.", nameof(storedProcedureName));
            await using var _dbConnection = CreateConnection();
            try
            {
                if (_dbConnection.State != ConnectionState.Open)
                    await _dbConnection.OpenAsync();

                return await _dbConnection.QueryAsync<T>(
                    storedProcedureName,
                    parameters,
                    commandType: CommandType.StoredProcedure,
                    commandTimeout: 60 // Increase timeout to 60 seconds
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while executing stored procedure: {StoredProcedureName}", storedProcedureName);
                throw; // Rethrow the exception to ensure the caller is aware of the failure
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                    await _dbConnection.CloseAsync();
            }
        }

        public async Task<T> ExecuteQueryFirstOrDefaultAsync<T>(string storedProcedureName, object parameters = null)
        {
            if (string.IsNullOrWhiteSpace(storedProcedureName))
                throw new ArgumentException("Stored procedure name cannot be null or empty.", nameof(storedProcedureName));
            await using var _dbConnection = CreateConnection();
            try
            {
                if (_dbConnection.State != ConnectionState.Open)
                    await _dbConnection.OpenAsync();

                return await _dbConnection.QueryFirstOrDefaultAsync<T>(
                    storedProcedureName,
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while executing stored procedure: {StoredProcedureName}", storedProcedureName);
                throw; // Rethrow the exception to ensure the caller is aware of the failure
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                    await _dbConnection.CloseAsync();
            }
        }

        public async Task<T> ExecuteStoredProcedureWithOutputAsync<T>(
            string storedProcedureName,
            DynamicParameters parameters,
            string outputParameterName
        )
        {
            if (string.IsNullOrWhiteSpace(storedProcedureName))
                throw new ArgumentException("Stored procedure name cannot be null or empty.", nameof(storedProcedureName));

            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters), "Parameters cannot be null.");

            await using var _dbConnection = CreateConnection();

            try
            {
                if (_dbConnection.State != ConnectionState.Open)
                    await _dbConnection.OpenAsync();

                await _dbConnection.ExecuteAsync(
                    storedProcedureName,
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return parameters.Get<T>(outputParameterName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while executing stored procedure with output: {StoredProcedureName}", storedProcedureName);
                throw; // Rethrow the exception to ensure the caller is aware of the failure
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                    await _dbConnection.CloseAsync();
            }
        }
    }
}
