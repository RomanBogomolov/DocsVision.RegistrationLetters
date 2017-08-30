using System;
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
        
        protected IEnumerable<TResult> Query<TFirst, TSecond, TResult>(string sql, Func<TFirst, TSecond, TResult> func = null, object parameters = null, CommandType? type = null)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                return connection.Query(sql, func, parameters, commandType: type);
            }
        }

        protected int Execute(string sql, object parameters = null, CommandType? type = null)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                return connection.Execute(sql, parameters, commandType: type);
            }
        }


        /*
         * Подумать!!!
         */
        protected IEnumerable<TParent> MultipleTablesQuery<TParent, TChild, TParentKey>(
            string sql,
            Func<TParent, TParentKey> parentKeySelector, 
            Func<TParent, TChild> childSelector, 
            object param = null, 
            string splitOn = "Id",
            CommandType? commandType = null)
        {
            IDictionary<TParentKey, TParent> cache = new Dictionary<TParentKey, TParent>();

            using (var connection = CreateConnection())
            {
                connection.Open();

                connection.Query<TParent, TChild, TParent>(sql, (parent, child) =>
                {
                    if (!cache.ContainsKey(parentKeySelector(parent)))
                    {
                        cache.Add(parentKeySelector(parent), parent);
                    }
                    TParent cachedParent = cache[parentKeySelector(parent)];
                    //TChild children = child;
                    child = childSelector(cachedParent);

                    return cachedParent;

                }, param, splitOn: splitOn, commandType: commandType);

                return cache.Values;
            }
        }

        private IDbConnection CreateConnection()
        {
            var connection = new SqlConnection(_connectionString);
            return connection;
        }
    }
}