using ABCBrasil.Core.Data;
using ABCBrasil.OpenBanking.Pagamento.Core.Common;
using ABCBrasil.OpenBanking.Pagamento.Core.Interfaces.Repository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.Pagamento.Infra.Repository
{
    public class DapperRepository : IDapperRepository
    {
        string _connectionString;
        readonly IConnectDataBase _connectDataBase;

        public DapperRepository(IConnectDataBase connectDataBase)
        {
            _connectDataBase = connectDataBase;
        }
        string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                    _connectionString = _connectDataBase.GetConnectString(Shared.Configuration.ABC_CS_NAME);
                return _connectionString;
            }
        }

        public void ExecuteStoredProcedure(string pSql, object dynamicParameters)
        {
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                connection.Execute(pSql, dynamicParameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }
        public T QueryFirstOrDefault<T>(string pSql, object dynamicParameters)
        {
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                return connection.Query<T>(pSql, dynamicParameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }
        public IEnumerable<T> Query<T>(string pSql, object dynamicParameters)
        {
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                return connection.Query<T>(pSql, dynamicParameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }


        public async Task<IList<T>> QueryAsync<T>(string sql, object param = null, CommandType? commandType = CommandType.StoredProcedure)
        {
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                return (IList<T>) await connection.QueryAsync<T>(sql, param, commandType: commandType);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }
        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, CommandType? commandType = CommandType.StoredProcedure, string splitOn = "Id")
        {
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                return await connection.QueryAsync<TFirst, TSecond, TReturn>(
                    sql,
                    map: map,
                    param: param,
                    commandType: commandType,
                    splitOn: splitOn);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }
        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, CommandType? commandType = CommandType.StoredProcedure)
        {
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                return await connection.QueryFirstOrDefaultAsync<T>(sql, param, commandType: commandType);
            }

            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }
        public async Task<int> ExecuteAsync(string sql, object param = null, CommandType? commandType = CommandType.StoredProcedure)
        {
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                return await connection.ExecuteAsync(sql, param, commandType: commandType);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }


        public async Task<int> ExecuteAsync(string sql, DynamicParameters param = null, CommandType? commandType = CommandType.StoredProcedure)
        {
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                return await connection.ExecuteAsync(sql, param, commandType: commandType);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }
}
