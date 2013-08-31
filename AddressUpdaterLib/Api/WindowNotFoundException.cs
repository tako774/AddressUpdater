using System;

namespace HisoutenSupportTools.AddressUpdater.Lib.Api
{
    /// <summary>
    /// ウィンドウが見つからなかった時にスローされる例外
    /// </summary>
    public class WindowNotFoundException : ApplicationException
    {
        private const string MESSAGE = "ウィンドウが見つかりませんでした。";
        /// <summary></summary>
        public WindowNotFoundException()
            : this(MESSAGE)
        { }
        /// <summary></summary>
        public WindowNotFoundException(Exception innerException)
            : this(MESSAGE, innerException)
        { }
        /// <summary></summary>
        public WindowNotFoundException(string message)
            : base(message)
        { }
        /// <summary></summary>
        public WindowNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
