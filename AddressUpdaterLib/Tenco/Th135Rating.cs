using System;
namespace HisoutenSupportTools.AddressUpdater.Lib.Tenco
{
    /// <summary>
    /// レーティング
    /// </summary>
    public class Th135Rating
    {
        /// <summary>
        /// キャラ
        /// </summary>
        public readonly Th135Characters Character;
        /// <summary>
        /// レーティング
        /// </summary>
        public readonly int Value;
        /// <summary>
        /// 偏差
        /// </summary>
        public readonly int Deviation;
        /// <summary>
        /// 対戦人数
        /// </summary>
        public readonly int MatchAccounts;
        /// <summary>
        /// 対戦数
        /// </summary>
        public readonly int MatchCount;

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="character">キャラ</param>
        /// <param name="value">レート値</param>
        /// <param name="deviation">偏差</param>
        /// <param name="matchAccounts">対戦人数</param>
        /// <param name="matchCount">対戦数</param>
        public Th135Rating(Th135Characters character, int value, int deviation, int matchAccounts, int matchCount)
        {
            Character = character;
            Value = value;
            Deviation = deviation;
            MatchAccounts = matchAccounts;
            MatchCount = matchCount;
        }
    }
}
