using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_core_practice.Services.Redis
{
    public interface IRedisService
    {
        /// <summary>
        /// 取 Redis Connection String From: appSettings.json
        /// </summary>
        string GetRedisConnString();

        /// <summary>
        /// 取得資料
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="useDb">資料存放的DB: 0~15</param>
        /// <returns>value</returns>
        string Get(string key, int useDb = 2);

        /// <summary>
        /// 取得資料-測試是否連線
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="useDb">資料存放的DB: 0~15</param>
        /// <returns>value</returns>
        string Get_test(string key, int useDb = 2);

        /// <summary>
        /// 移除key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="useDb">資料存放的DB: 0~15</param>
        /// <returns>是否成功刪除</returns>
        bool Remove(string key, int useDb = 2);

        /// <summary>
        /// 取得CLASS物件資料
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="useDb">資料存放的DB: 0~15</param>
        /// <returns>CLASS 物件</returns>
        T GetObject<T>(string key, int useDb = 2);

        /// <summary>
        /// 儲存字串資料
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <param name="expiresAtMinutes">n分鐘後redis自動刪除資料</param>
        /// <param name="useDb">資料存放的DB: 0~15</param>
        void Set(string key, string value, int expiresAtMinutes, int useDb = 2);

        /// <summary>
        /// 儲存CLASS物件資料 (會轉成JSON STRING再儲存)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">CLASS 物件</param>
        /// <param name="expiresAtMinutes">n分鐘後redis自動刪除資料</param>
        /// <param name="useDb">資料存放的DB: 0~15</param>
        void SetObject(string key, object value, int expiresAtMinutes, int useDb = 2);
    }
}
