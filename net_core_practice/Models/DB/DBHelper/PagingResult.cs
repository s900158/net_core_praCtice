using System.Collections.Generic;

namespace net_core_practice.Models.DB.DBHelper
{
    public class PagingResult<T>
    {
        /// <summary>
        /// 頁次
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 每頁筆數
        /// </summary>
        public int page_size { get; set; }

        /// <summary>
        /// 總頁次
        /// </summary>
        public int page_count { get; set; }

        /// <summary>
        /// 總筆數
        /// </summary>
        public int count { get; set; }

        ///// <summary>
        ///// 計算總筆數的sql語法 - debug模式才會輸出
        ///// </summary>
        //public string sqlCount { get; set; }

        ///// <summary>
        ///// 取資料的sql語法 - debug模式才會輸出
        ///// </summary>
        //public string sql { get; set; }

        /// <summary>
        /// 資料集
        /// </summary>
        public List<T> rows { get; set; } = new List<T>();
    }
}
