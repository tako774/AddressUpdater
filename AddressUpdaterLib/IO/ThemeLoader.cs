using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Config;

namespace HisoutenSupportTools.AddressUpdater.Lib.IO
{
    /// <summary>
    /// テーマ読み込み
    /// </summary>
    public partial class ThemeLoader : Component
    {
        /// <summary>テーマフォルダ名</summary>
        private const string THEME_FOLDER_NAME = "AUTheme";
        /// <summary>保存リトライ回数</summary>
        private const int RETRY_COUNT = 3;


        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public ThemeLoader()
        {
            InitializeComponent();
        }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="container"></param>
        public ThemeLoader(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }


        /// <summary>
        /// 読み込み
        /// </summary>
        /// <returns>テーマリスト</returns>
        public Dictionary<string, Theme> Load()
        {
            var themes = new Dictionary<string, Theme>();
            var directory = new DirectoryInfo(THEME_FOLDER_NAME);
            if (!directory.Exists)
                return themes;

            var themeFiles = directory.GetFiles("*.xml");
            foreach (var file in themeFiles)
            {
                var themeName = file.Name;
                if (!string.IsNullOrEmpty(file.Extension))
                    themeName = themeName.Replace(file.Extension, string.Empty);

                try
                {
                    var xmlConfig = new XmlConfigFile<Theme>(file.FullName);
                    var theme = xmlConfig.Deserialize();
                    themes.Add(themeName, theme);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    continue;
                }
            }

            return themes;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="themes">テーマリスト</param>
        /// <exception cref="System.UnauthorizedAccessException">呼び出し元に、必要なアクセス許可がありません。</exception>
        /// <exception cref="System.IO.IOException">I/O エラーが発生しました。</exception>
        public void Save(Dictionary<string, Theme> themes)
        {
            var directory = new DirectoryInfo(THEME_FOLDER_NAME);
            if (!directory.Exists)
                directory.Create();

            foreach (var theme in themes)
            {
                var xmlConfig = new XmlConfigFile<Theme>(THEME_FOLDER_NAME + "/" + theme.Key + ".xml");
                xmlConfig.Serialize(theme.Value);
            }
        }

        /// <summary>
        /// 保存(単体)
        /// </summary>
        /// <param name="name">テーマ名</param>
        /// <param name="theme">テーマ</param>
        /// <exception cref="System.UnauthorizedAccessException">呼び出し元に、必要なアクセス許可がありません。</exception>
        /// <exception cref="System.IO.IOException">I/O エラーが発生しました。</exception>
        public void Save(string name, Theme theme)
        {
            var directory = new DirectoryInfo(THEME_FOLDER_NAME);
            if (!directory.Exists)
                directory.Create();

            var xmlConfig = new XmlConfigFile<Theme>(THEME_FOLDER_NAME + "/" + name + ".xml");
            xmlConfig.Serialize(theme);
        }
    }
}
