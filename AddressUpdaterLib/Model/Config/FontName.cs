using System;
using System.Drawing;
using System.Windows.Forms;

namespace HisoutenSupportTools.AddressUpdater.Lib.Model.Config
{
    /// <summary>
    /// フォント名
    /// </summary>
    [Serializable]
    public class FontName
    {
        /// <summary>フォント名</summary>
        public string Name { get; set; }

        /// <summary>
        /// フォントをセット
        /// </summary>
        /// <param name="control"></param>
        public void SetFont(Control control)
        {
            try { control.Font = new Font(Name, control.Font.SizeInPoints); }
            catch (ArgumentException) { }
        }
    }
}
