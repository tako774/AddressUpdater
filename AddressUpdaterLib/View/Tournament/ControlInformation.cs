using System;
using System.Windows.Forms;

namespace HisoutenSupportTools.AddressUpdater.Lib.View.Tournament
{
    /// <summary>
    /// コントロール配置情報
    /// </summary>
    public class ControlInformation : IEquatable<ControlInformation>
    {
        /// <summary>コントロール</summary>
        public Control Control { get; private set; }
        /// <summary>X座標</summary>
        public int X { get; private set; }
        /// <summary>Y座標</summary>
        public int Y { get; private set; }
        /// <summary></summary>
        public int ColumnSpan { get; set; }
        /// <summary></summary>
        public int RowSpan { get; set; }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="control">コントロール</param>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        public ControlInformation(Control control, int x, int y)
            : this(control, x, y, 0, 0)
        { }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="control">コントロール</param>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        /// <param name="columnSpan"></param>
        /// <param name="rowSpan"></param>
        public ControlInformation(Control control, int x, int y, int columnSpan, int rowSpan)
        {
            Control = control;
            X = x;
            Y = y;
            ColumnSpan = columnSpan;
            RowSpan = rowSpan;
        }

        #region IEquatable<ControlInformation> メンバ

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ControlInformation other)
        {
            if (other == null) return false;
            if (object.ReferenceEquals(this, other)) return true;

            if (X != other.X) return false;
            if (Y != other.Y) return false;
            if (ColumnSpan != other.ColumnSpan) return false;
            if (RowSpan != other.RowSpan) return false;

            if (Control is PlayerLinkLabel)
            {
                if (other.Control is PlayerLinkLabel)
                    return ((PlayerLinkLabel)Control).Player.Equals(((PlayerLinkLabel)other.Control).Player);
                else
                    return false;
            }
            else if (Control is Label)
            {
                if (other.Control is PlayerLinkLabel)
                    return false;
                else if (other.Control is Label)
                    return Control.Text == other.Control.Text;
                else
                    return false;
            }
            else if (Control is ResultEditor && other.Control is ResultEditor)
                return ((ResultEditor)Control).Equals((ResultEditor)other.Control);

            return false;
        }

        #endregion
    }
}
