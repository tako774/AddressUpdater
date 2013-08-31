using System;

namespace HisoutenSupportTools.AddressUpdater.Lib.Util
{
    /// <summary>
    /// 配列のいろいろ
    /// </summary>
    public static class ArrayExtention
    {
        /// <summary>
        /// 配列の要素毎にEqualsで比較する
        /// </summary>
        /// <param name="arrayA"></param>
        /// <param name="arrayB"></param>
        /// <returns></returns>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static bool ElementsEquals<T>(T[] arrayA, T[] arrayB)
        {
            if (arrayA == null) throw new ArgumentNullException("arrayA");
            if (arrayB == null) throw new ArgumentNullException("arrayB");

            if (object.ReferenceEquals(arrayA, arrayB)) return true;
            if (arrayA.Length != arrayB.Length) return false;

            for (var i = 0; i < arrayA.Length; i++)
            {
                if (!arrayA[i].Equals(arrayB[i]))
                    return false;
            }

            return true;
        }
    }
}
