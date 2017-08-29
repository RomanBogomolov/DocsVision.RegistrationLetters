using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DocsVision.RegistrationLetters.Log;

namespace DocsVision.RegistrationLetters.DataAccess.Sql
{
    public class BaseRepository
    {
        private readonly string _connectionString;
        protected readonly ILogger _logger;
        public BaseRepository(string connectionString, ILogger logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        protected T QueryFirstOrDefault<T>(string sql, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                return connection.QueryFirstOrDefault<T>(sql, parameters);
            }
        }

        protected IEnumerable<T> Query<T>(string sql, object parameters = null, CommandType? type = null)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                return connection.Query<T>(sql, parameters, commandType: type).ToList();
            }
        }
        
        /* СДЕЛАТЬ!!! */
        protected IEnumerable<T> Query<T,TSecond, TResult>(string sql, ( object parameters = null, CommandType? type = null)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                return connection.Query<T>(sql, parameters, commandType: type);
            }
        }

        protected int Execute(string sql, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                return connection.Execute(sql, parameters);
            }
        }
        private IDbConnection CreateConnection()
        {
            var connection = new SqlConnection(_connectionString);

            return connection;
        }
    }
}