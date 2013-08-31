namespace HisoutenSupportTools.AddressUpdater.Lib.Model
{
    /// <summary>登録モード</summary>
    public enum RegisterMode : int
    {
        /// <summary>通常ホスト</summary>
        [EnumText("通常ホスト")]
        Normal,
        /// <summary>Tenco!</summary>
        [EnumText("Tenco!")]
        Tenco,
        /// <summary>大会</summary>
        [EnumText("大会")]
        Tournament,
    }
}
