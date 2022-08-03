using System.Threading.Tasks;
using net_core_practice.Models.DB.DBHelper;

namespace net_core_practice.Services.DB
{
    public interface IDBHelperService
    {
        /// <summary>
        /// 跳頁 - 會自動計算總筆數
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">需包含orderby欄位</param>
        /// <param name="orderby">預設: cno asc</param>
        /// <param name="page">頁次</param>
        /// <param name="pagesize">每頁筆數</param>
        /// <param name="sqldebug">true = 輸出 sql 條件</param>
        /// <returns>PagingResult</returns>
        /// public PagingResult Paging<T>(string sql, ref string sqlcomplete, string orderby = "cno asc", int page = 1, int pagesize = 10)
        Task<PagingResult<T>> Paging<T>(string sql, string orderby = "cno asc", int page = 1, int pagesize = 10, bool sqldebug = false);
    }
}
