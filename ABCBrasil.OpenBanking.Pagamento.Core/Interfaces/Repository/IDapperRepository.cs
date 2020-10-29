using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Interfaces.Repository
{
    public interface IDapperRepository
    {
        void ExecuteStoredProcedure(string pSql, object dynamicParameters);
        T QueryFirstOrDefault<T>(string pSql, object dynamicParameters);
        IEnumerable<T> Query<T>(string pSql, object dynamicParameters);
        Task<IList<T>> QueryAsync<T>(string sql, object param = null, CommandType? commandType = CommandType.StoredProcedure);
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, CommandType? commandType = CommandType.StoredProcedure, string splitOn = "Id");
        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, CommandType? commandType = CommandType.StoredProcedure);
        Task<int> ExecuteAsync(string sql, object param = null, CommandType? commandType = CommandType.StoredProcedure);
        Task<int> ExecuteAsync(string sql, DynamicParameters param = null, CommandType? commandType = CommandType.StoredProcedure);
    }
}
