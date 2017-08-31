using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
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

        protected int Execute(string sql, object parameters = null, CommandType? type = null)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                return connection.Execute(sql, parameters, commandType: type);
            }
        }
        
        protected IEnumerable<TParent> OneToOneTableQuery<TParent, TChild>(
            string sql, 
            Func<TParent, TChild> key,
            object param = null, 
            string splitOn = "Id", 
            CommandType? type = null,
            string splitTableName = ""
            )
        {
            using (var connection = CreateConnection())
            {
                var data = connection.Query<TParent, TChild, TParent>(sql, (p, c) =>
                {
                    PropertyInfo propertyInfo = p.GetType().GetProperty(splitTableName) ??
                                                p.GetType().GetProperty(typeof(TChild).Name);
                    if (propertyInfo != null)
                        propertyInfo.SetValue(p, c);
                    return p;
                }, param, splitOn: splitOn, commandType: type);

                return data;
            }
        }
        
        protected IEnumerable<TParent> OneToManyTablesQuery<TParent, TChild, TParentKey>(
            string sql,
            Func<TParent, TParentKey> parentKeySelector, 
            Func<TParent, IList<TChild>> childSelector, 
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
                        cache.Add(parentKeySelector(parent), parent);

                    TParent cachedParent = cache[parentKeySelector(parent)];
                    IList<TChild> children = childSelector(cachedParent);
                    children.Add(child);
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