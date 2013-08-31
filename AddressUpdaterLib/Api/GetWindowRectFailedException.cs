using System;

namespace HisoutenSupportTools.AddressUpdater.Lib.Api
{
    /// <summary>
    /// 座標の取得に失敗した時にスローされる例外
    /// </summary>
    public class GetWindowRectFailedException : ApplicationException
    {
        private const string MESSAGE = "座標の取得に失敗しました。";
        /// <summary></summary>
        public GetWindowRectFailedException()
            : this(MESSAGE)
        { }
        /// <summary></summary>
        public GetWindowRectFailedException(Exception innerException)
            : this(MESSAGE, innerException)
        { }
        /// <summary></summary>
        public GetWindowRectFailedException(string message)
            : base(message)
        { }
        /// <summary></summary>
        public GetWindowRectFailedException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
