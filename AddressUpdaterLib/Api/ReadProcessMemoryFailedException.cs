using System;
using System.Collections.Generic;
using System.Text;

namespace HisoutenSupportTools.AddressUpdater.Lib.Api
{
    /// <summary></summary>
    public class ReadProcessMemoryFailedException : ApplicationException
    {
        private const string MESSAGE = "メモリ領域からのデータ読み取りに失敗しました。";
        /// <summary></summary>
        public ReadProcessMemoryFailedException()
            : this(MESSAGE)
        { }
        /// <summary></summary>
        public ReadProcessMemoryFailedException(Exception innerException)
            : this(MESSAGE, innerException)
        { }
        /// <summary></summary>
        public ReadProcessMemoryFailedException(string message)
            : base(message)
        { }
        /// <summary></summary>
        public ReadProcessMemoryFailedException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
