using net_core_practice.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_core_practice.Utilis.ExceptionOption
{
    public class ErrorOperationException : Exception
    {
        /// <summary>
        /// 狀態: ok , error
        /// </summary>
        public ErrCodeEnum Errcode { get; set; } = ErrCodeEnum.BackendErrorOccurred;

        public ErrorOperationException() : base()
        {
        }

        public ErrorOperationException(string message) : base(message)
        {
        }

        public ErrorOperationException(ErrCodeEnum errcode, string message) : base(message)
        {
            Errcode = errcode;
        }
    }
}
