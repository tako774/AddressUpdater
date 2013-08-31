using System;

namespace HisoutenSupportTools.AddressUpdater.Lib.Network
{
    /// <summary>
    /// 通信失敗時にスローされる例外
    /// </summary>
    public class CommunicationFailedException : ApplicationException
    {
        private const string MESSAGE = "サーバーとの通信に失敗しました。";
        /// <summary></summary>
        public CommunicationFailedException()
            : this(MESSAGE)
        { }
        /// <summary></summary>
        public CommunicationFailedException(Exception innerException)
            : this(MESSAGE, innerException)
        { }
        /// <summary></summary>
        public CommunicationFailedException(string message)
            : base(message)
        { }
        /// <summary></summary>
        public CommunicationFailedException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
