using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HisoutenSupportTools.AddressUpdater.Lib.Util
{
    class PtrUtil
    {
        public static IntPtr BytesToIntPtr(Byte[] bytes, int offset = 0)
        {
            IntPtr ptr;
            String address = "0x" + Convert.ToString((BitConverter.ToInt32(bytes, 0) + offset), 16);
            
            if (IntPtr.Size == sizeof(long))
                ptr = new IntPtr(Convert.ToInt64(address, 16));
            else
                ptr = new IntPtr(Convert.ToInt32(address, 16));

            return ptr;
        }
    }
}
