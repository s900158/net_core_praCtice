using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_core_practice.Enums
{
    public enum ErrCodeEnum
    {
        /// <summary>
        /// 資料傳輸正常
        /// </summary>
        ACK = 0,
        /// <summary>
        /// 資料格式驗證錯誤
        /// </summary>
        InvalidParameters = 1,
        /// <summary>
        /// 無效的操作與設置
        /// </summary>
        InvalidOperationAndSetting = 2,
        /// <summary>
        /// 執行時發生錯誤
        /// </summary>
        ErrorOccurredDuringExecution = 3,
        /// <summary>
        /// 由後端程式邏輯判斷後拋出的錯誤
        /// </summary>
        BackendErrorOccurred = 4
    }
}
