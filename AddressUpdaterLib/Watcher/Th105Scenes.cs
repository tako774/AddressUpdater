namespace HisoutenSupportTools.AddressUpdater.Lib.Watcher
{
    /// <summary></summary>
    public enum Th105Scenes : byte
    {
        /// <summary>ロゴ</summary>
        Logo = 0,
        /// <summary>オープニング</summary>
        Opening = 1,
        /// <summary>タイトル</summary>
        Title = 2,
        /// <summary>キャラ選択</summary>
        Select = 3,
        /// <summary>対戦中</summary>
        Battle = 5,
        /// <summary>少女祈祷中</summary>
        Loading = 6,
        /// <summary>キャラ選択（ネット対戦サーバー側）</summary>
        SelectServer = 8,
        /// <summary>キャラ選択（ネット対戦クライアント側）</summary>
        SelectClient = 9,
        /// <summary>少女祈祷中（ネット対戦サーバー側）</summary>
        LoadingServer = 10,
        /// <summary>少女祈祷中（ネット対戦クライアント側）</summary>
        LoadingClient = 11,
        /// <summary>少女祈祷中（ネット対戦観戦中）</summary>
        LoadingWatch = 12,
        /// <summary>対戦中（ネット対戦サーバー側）</summary>
        BattleServer = 13,
        /// <summary>対戦中（ネット対戦クライアント側）</summary>
        BattleClient = 14,
        /// <summary>観戦中</summary>
        BattleWatch = 15,
        /// <summary>シナリオ選択</summary>
        ScenarioSelect = 16,
        /// <summary>エンディング</summary>
        Ending = 20,
    }
}
