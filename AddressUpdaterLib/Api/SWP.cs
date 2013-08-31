using System;

namespace HisoutenSupportTools.AddressUpdater.Lib.Api
{
    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum SWP : uint
    {
        /// <summary></summary>
        SWP_NOSIZE = 0x0001,
        /// <summary></summary>
        SWP_NOMOVE = 0x0002,
        /// <summary></summary>
        SWP_NOZORDER = 0x0004,
        /// <summary></summary>
        SWP_NOREDRAW = 0x0008,
        /// <summary></summary>
        SWP_NOACTIVATE = 0x0010,
        /// <summary></summary>
        SWP_FRAMECHANGED = 0x0020,
        /// <summary></summary>
        SWP_SHOWWINDOW = 0x0040,
        /// <summary></summary>
        SWP_HIDEWINDOW = 0x0080,
        /// <summary></summary>
        SWP_NOCOPYBITS = 0x0100,
        /// <summary></summary>
        SWP_NOOWNERZORDER = 0x0200,
        /// <summary></summary>
        SWP_NOSENDCHANGING = 0x0400,
        /// <summary></summary>
        SWP_DRAWFRAME = SWP_FRAMECHANGED,
        /// <summary></summary>
        SWP_NOREPOSITION = SWP_NOOWNERZORDER,
        /// <summary></summary>
        SWP_DEFERERASE = 0x2000,
        /// <summary></summary>
        SWP_ASYNCWINDOWPOS = 0x4000
    }
}
