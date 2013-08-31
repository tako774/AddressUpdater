using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace HisoutenSupportTools.AddressUpdater.Lib.Api
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        /// <summary></summary>
        public int Left;
        /// <summary></summary>
        public int Top;
        /// <summary></summary>
        public int Right;
        /// <summary></summary>
        public int Bottom;

        /// <summary></summary>
        public RECT(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        /// <summary></summary>
        public int Height { get { return Bottom - Top; } }
        /// <summary></summary>
        public int Width { get { return Right - Left; } }
        /// <summary></summary>
        public Size Size { get { return new Size(Width, Height); } }

        /// <summary></summary>
        public Point Location { get { return new Point(Left, Top); } }

        /// <summary></summary>
        // Handy method for converting to a System.Drawing.Rectangle
        public Rectangle ToRectangle()
        {
            return Rectangle.FromLTRB(Left, Top, Right, Bottom);
        }

        /// <summary></summary>
        public static RECT FromRectangle(Rectangle rectangle)
        {
            return new RECT(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
        }

        /// <summary></summary>
        public override int GetHashCode()
        {
            return Left ^ ((Top << 13) | (Top >> 0x13))
              ^ ((Width << 0x1a) | (Width >> 6))
              ^ ((Height << 7) | (Height >> 0x19));
        }

        #region Operator overloads

        /// <summary></summary>
        public static implicit operator Rectangle(RECT rect)
        {
            return rect.ToRectangle();
        }

        /// <summary></summary>
        public static implicit operator RECT(Rectangle rect)
        {
            return FromRectangle(rect);
        }

        #endregion
    }
}
