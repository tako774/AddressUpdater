using System;

namespace HisoutenSupportTools.AddressUpdater.Lib.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class HWND
    {
        /// <summary></summary>
        public static readonly IntPtr HWND_BROADCAST = new IntPtr(0xffff);
        /// <summary></summary>
        public static readonly IntPtr HWND_TOP = new IntPtr(0);
        /// <summary></summary>
        public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        /// <summary></summary>
        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        /// <summary></summary>
        public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        /// <summary></summary>
        public static readonly IntPtr HWND_MESSAGE = new IntPtr(-3);
    }
}
