using Dapper;
using net_core_practice.Enums;
using net_core_practice.Utilis.ExceptionOption;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace net_core_practice.Services.DB
{
    public class DBService : IDBService
    {
        public int Execute(string sql, object parameters = null)
        {
            try
            {
                using (IDbConnection connection = CreateConnection())
                {

                    return connection.Execute(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                throw new ErrorOperationException(ex.Message) { Errcode = ErrCodeEnum.ErrorOccurredDuringExecution };
            }
        }

        public async Task<int> ExecuteAsync(string sql, object parameters = null)
        {
            try
            {
                using (IDbConnection connection = CreateConnection())
                {
                    return await connection.ExecuteAsync(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                throw new ErrorOperationException(ex.Message) { Errcode = ErrCodeEnum.ErrorOccurredDuringExecution };
            }
        }

        public T ExecuteScalar<T>(string sql, object parameters = null)
        {
            try
            {
                using (IDbConnection connection = CreateConnection())
                {
                    return connection.ExecuteScalar<T>(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                throw new ErrorOperationException(ex.Message) { Errcode = ErrCodeEnum.ErrorOccurredDuringExecution };
            }
        }

        public async Task<T> ExecuteScalarAsync<T>(string sql, object parameters = null)
        {
            try
            {
                using (IDbConnection connection = CreateConnection())
                {
                    return await connection.ExecuteScalarAsync<T>(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                throw new ErrorOperationException(ex.Message) { Errcode = ErrCodeEnum.ErrorOccurredDuringExecution };
            }
        }

        public List<T> Query<T>(string sql, object parameters = null)
        {
            try
            {
                using (IDbConnection connection = CreateConnection())
                {
                    return connection.Query<T>(sql, parameters, commandTimeout: 10).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ErrorOperationException(ex.Message) { Errcode = ErrCodeEnum.ErrorOccurredDuringExecution };
            }
        }

        public List<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, string splitOn, object parameters = null)
        {
            try
            {
                using (IDbConnection connection = CreateConnection())
                {
                    return connection.Query<TFirst, TSecond, TReturn>(sql, map, parameters, null, true, splitOn, 6000).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ErrorOperationException(ex.Message) { Errcode = ErrCodeEnum.ErrorOccurredDuringExecution };
            }
        }

        public async Task<List<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, string splitOn, object parameters = null)
        {
            try
            {
                using (IDbConnection connection = CreateConnection())
                {
                    return (await connection.QueryAsync<TFirst, TSecond, TReturn>(sql: sql, map: map, param: parameters, splitOn: splitOn, commandTimeout: 6000)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ErrorOperationException(ex.Message) { Errcode = ErrCodeEnum.ErrorOccurredDuringExecution };
            }
        }

        public async Task<List<T>> QueryAsync<T>(string sql, object parameters = null)
        {
            try
            {
                using (IDbConnection connection = CreateConnection())
                {
                    return (await connection.QueryAsync<T>(sql, parameters)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ErrorOperationException(ex.Message) { Errcode = ErrCodeEnum.ErrorOccurredDuringExecution };
            }
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object parameters = null)
        {
            try
            {
                using (IDbConnection connection = CreateConnection())
                {

                    return await connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                throw new ErrorOperationException(ex.Message) { Errcode = ErrCodeEnum.ErrorOccurredDuringExecution };
            }
        }

        public async Task<T> QueryMultipleAsync<T>(Func<SqlMapper.GridReader, T> logicDelegate, string sql, object parameters = null)
        {
            try
            {
                using IDbConnection connection = CreateConnection();
                SqlMapper.GridReader grid = await connection.QueryMultipleAsync(sql, parameters);
                //委派執行function
                return logicDelegate(grid);
            }
            catch (Exception ex)
            {
                throw new ErrorOperationException(ex.Message) { Errcode = ErrCodeEnum.ErrorOccurredDuringExecution };
            }
        }

        public async Task<bool> QueryMultipleAsync<T>(Delegate logicDelegate, string sql, object parameters = null)
        {
            try
            {
                using IDbConnection connection = CreateConnection();
                SqlMapper.GridReader grid = await connection.QueryMultipleAsync(sql, parameters);
                logicDelegate.DynamicInvoke(grid);
                return true;
            }
            catch (Exception ex)
            {
                throw new ErrorOperationException(ex.Message) { Errcode = ErrCodeEnum.ErrorOccurredDuringExecution };
            }
        }

        protected virtual IDbConnection CreateConnection()
        {

            return new SqlConnection("");
        }


    }
}
