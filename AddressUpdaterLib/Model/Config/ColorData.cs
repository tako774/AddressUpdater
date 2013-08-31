using System;
using System.Drawing;
using System.Xml.Serialization;

namespace HisoutenSupportTools.AddressUpdater.Lib.Model.Config
{
    /// <summary>
    /// 色データ
    /// </summary>
    [Serializable]
    [XmlInclude(typeof(NullColorData))]
    public class ColorData : IEquatable<ColorData>
    {
        /// <summary>Alpha</summary>
        public byte A { get; set; }
        /// <summary>Red</summary>
        public byte R { get; set; }
        /// <summary>Green</summary>
        public byte G { get; set; }
        /// <summary>Blue</summary>
        public byte B { get; set; }


        /// <summary></summary>
        public ColorData()
            : this(byte.MaxValue, Color.Transparent.R, Color.Transparent.G, Color.Transparent.B)
        { }

        /// <summary></summary>
        /// <param name="r">Red</param>
        /// <param name="g">Green</param>
        /// <param name="b">Blue</param>
        public ColorData(byte r, byte g, byte b)
            : this(byte.MaxValue, r, g, b)
        { }

        /// <summary></summary>
        /// <param name="a">Alpha</param>
        /// <param name="r">Red</param>
        /// <param name="g">Green</param>
        /// <param name="b">Blue</param>
        public ColorData(byte a, byte r, byte g, byte b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        /// <summary></summary>
        /// <returns></returns>
        public Color ToColor()
        {
            return Color.FromArgb(A, R, G, B);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public Color ToNotTransparentColor()
        //{
        //    return Color.FromArgb(byte.MaxValue, R, G, B);
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public ColorData ToNotTransparentColorData()
        //{
        //    return new ColorData(byte.MaxValue, R, G, B);
        //}

        /// <summary></summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static ColorData FromColor(Color color)
        {
            return new ColorData(color.A, color.R, color.G, color.B);
        }

        /// <summary></summary>
        /// <param name="colorData"></param>
        /// <returns></returns>
        public static explicit operator Color(ColorData colorData)
        {
            return Color.FromArgb(colorData.A, colorData.R, colorData.G, colorData.B);
        }

        #region IEquatable<ColorData> メンバ

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ColorData other)
        {
            if (other == null) return false;
            if (object.ReferenceEquals(this, other)) return true;

            return
                A == other.A &&
                R == other.R &&
                G == other.G &&
                B == other.B;
        }

        #endregion
    }
}
