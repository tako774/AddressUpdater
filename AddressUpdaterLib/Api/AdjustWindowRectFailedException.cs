using System;

namespace HisoutenSupportTools.AddressUpdater.Lib.Api
{
    /// <summary>
    /// 指定されたクライアント領域を確保するために必要なウィンドウ座標の計算に失敗した時にスローされる例外
    /// </summary>
    public class AdjustWindowRectFailedException : ApplicationException
    {
        private const string MESSAGE = "指定されたクライアント領域を確保するために必要なウィンドウ座標の計算に失敗しました。";
        /// <summary></summary>
        public AdjustWindowRectFailedException()
            : this(MESSAGE)
        { }
        /// <summary></summary>
        public AdjustWindowRectFailedException(Exception innerException)
            : this(MESSAGE, innerException)
        { }
        /// <summary></summary>
        public AdjustWindowRectFailedException(string message)
            : base(message)
        { }
        /// <summary></summary>
        public AdjustWindowRectFailedException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}

