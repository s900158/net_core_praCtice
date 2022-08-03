using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;

namespace net_core_practice.Services.DB
{
    public interface IDBService
    {
        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object parameters = null);
        List<T> Query<T>(string sql, object parameters = null);
        List<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, string splitOn, object parameters = null);
        Task<List<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, string splitOn, object parameters = null);
        Task<List<T>> QueryAsync<T>(string sql, object parameters = null);
        int Execute(string sql, object parameters = null);
        Task<int> ExecuteAsync(string sql, object parameters = null);
        T ExecuteScalar<T>(string sql, object parameters = null);
        Task<T> ExecuteScalarAsync<T>(string sql, object parameters = null);
        Task<T> QueryMultipleAsync<T>(Func<SqlMapper.GridReader, T> logicDelegate, string sql, object parameters = null);
        Task<bool> QueryMultipleAsync<T>(Delegate logicDelegate, string sql, object parameters = null);
    }
}
