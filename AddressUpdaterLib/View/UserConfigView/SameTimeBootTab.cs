using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Config;

namespace HisoutenSupportTools.AddressUpdater.Lib.View.UserConfigView
{
    /// <summary>
    /// 同時起動タブ
    /// </summary>
    public partial class SameTimeBootTab : TabBase
    {
        /// <summary>
        /// 同時起動ソフトリストの取得
        /// </summary>
        /// <returns>同時起動ソフトリスト</returns>
        public Collection<SoftwareInformation> GetSoftwareInformations()
        {
            var softwareInformations = new Collection<SoftwareInformation>();
            foreach (ListViewItem item in bootSameTimeListView.Items)
            {
                softwareInformations.Add(new SoftwareInformation(item.Text, item.SubItems[1].Text, item.Checked));
            }
            return softwareInformations;
        }


        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public SameTimeBootTab()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 表示言語の反映
        /// </summary>
        /// <param name="language"></param>
        public void ReflectLanguage(Language language)
        {
            try { dragdropLabel.Text = language["SameTimeBootTab_DragDrop"]; }
            catch (KeyNotFoundException) { }
            try { addButton.Text = language["SameTimeBootTab_Add"]; }
            catch (KeyNotFoundException) { }
            try { deleteButton.Text = language["SameTimeBootTab_Delete"]; }
            catch (KeyNotFoundException) { }
        }

        /// <summary>
        /// 同時起動ソフトの起動
        /// </summary>
        public void Boot()
        {
            // AUが無限に起動し続けないよう確認
            var addressUpdaters = Process.GetProcessesByName(Application.ProductName);
            if (10 <= addressUpdaters.Length)
            {
                MessageBox.Show(
                    ParentForm,
                    "AddressUpdaterが多数起動されています。" +
                    Environment.NewLine + Environment.NewLine +
                    "同時起動ソフトの指定に問題のある可能性があるため" + Environment.NewLine +
                    "起動を中止します。",
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            foreach (var softInfo in UserConfig.BootSameTimeSofts)
            {
                if (!softInfo.Boot || !softInfo.Exists)
                    continue;


                // 既に起動しているっぽかったらやめておく
                try
                {
                    var processes = Process.GetProcessesByName(softInfo.Name);
                    if (0 < processes.Length)
                        continue;
                }
                catch (InvalidOperationException) { }

                // 起動
                using (var process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo(softInfo.Path);
                    try
                    {
                        if (!process.Start())
                            continue;
                    }
                    catch (Win32Exception) { continue; }

                    // 起動するまで待機（MAX5秒くらい）
                    try
                    {
                        var count = 0;
                        while (!process.Responding && count <= 5)
                        {
                            System.Threading.Thread.Sleep(1000);
                            count++;
                            System.Diagnostics.Debug.WriteLine(count);
                        }

                        // 起動（もしくはタイムアウト）してからも少し待つ
                        System.Threading.Thread.Sleep(500);
                    }
                    catch (PlatformNotSupportedException) { continue; }
                    catch (InvalidOperationException) { continue; }
                    catch (NotSupportedException) { continue; }
                }
            }

            ParentForm.Activate();
        }

        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SameTimeBootTab_Load(object sender, EventArgs e)
        {
            if (UserConfig == null)
                return;

            bootSameTimeListView.Items.Clear();
            foreach (var softwareInformation in UserConfig.BootSameTimeSofts)
            {
                var icon = Icon.ExtractAssociatedIcon(softwareInformation.Path);
                imageList.Images.Add(icon);

                var item = new ListViewItem(new string[] { softwareInformation.Name, softwareInformation.Path }, imageList.Images.Count - 1);
                item.Checked = softwareInformation.Boot;
                bootSameTimeListView.Items.Add(item);
            }
        }

        /// <summary>
        /// テーマの反映
        /// </summary>
        public override void ReflectTheme()
        {
            BackColor = Theme.ToolBackColor.ToColor();
            addButton.BackColor = SystemColors.Control;
            addButton.UseVisualStyleBackColor = true;
            deleteButton.BackColor = SystemColors.Control;
            deleteButton.UseVisualStyleBackColor = true;


            dragdropLabel.ForeColor = Theme.GeneralTextColor.ToColor();

            bootSameTimeListView.ForeColor = Theme.GeneralTextColor.ToColor();
            bootSameTimeListView.BackColor = Theme.ToolBackColor.ToColor();
        }

        /// <summary>
        /// 同時起動ソフト一覧DragEnter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bootSameTimeListView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var containsExeOrLnkFile = false;
                foreach (var fileName in (string[])e.Data.GetData(DataFormats.FileDrop))
                {
                    var file = new FileInfo(fileName);
                    if (!file.Exists)
                        continue;
                    if (file.Extension.ToLower() != ".exe" && file.Extension.ToLower() != ".lnk")
                        continue;

                    containsExeOrLnkFile = true;
                    break;
                }

                if (containsExeOrLnkFile)
                    e.Effect = DragDropEffects.Copy;
            }
        }

        /// <summary>
        /// 同時起動ソフト一覧DragLeave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bootSameTimeListView_DragLeave(object sender, EventArgs e)
        {
            // 一度exeファイル等をドラッグしたあとドロップしないでおくと
            // 次にドラッグしてきたファイルが何であっても+表示されるのでこんな処理
            bootSameTimeListView.AllowDrop = false;
            bootSameTimeListView.AllowDrop = true;
        }

        /// <summary>
        /// 同時起動ソフト一覧DragDrop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bootSameTimeListView_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                foreach (var fileName in (string[])e.Data.GetData(DataFormats.FileDrop))
                {
                    var file = new FileInfo(fileName);
                    if (!file.Exists)
                        continue;
                    if (file.Extension.ToLower() != ".exe" && file.Extension.ToLower() != ".lnk")
                        continue;

                    var name = file.Name.Replace(file.Extension, "");

                    if (name == Application.ProductName)
                        continue;

                    var icon = Icon.ExtractAssociatedIcon(file.FullName);
                    imageList.Images.Add(icon);

                    var item = new ListViewItem(new string[] { name, fileName }, imageList.Images.Count - 1);
                    item.Checked = true;
                    bootSameTimeListView.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// 同時起動ソフト追加ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                var file = new FileInfo(openFileDialog.FileName);
                var filename = file.Name.Replace(file.Extension, "");

                if (filename == Application.ProductName)
                    return;

                var icon = Icon.ExtractAssociatedIcon(file.FullName);
                imageList.Images.Add(icon);

                var item = new ListViewItem(new string[] { filename, openFileDialog.FileName }, imageList.Images.Count - 1);
                item.Checked = true;
                bootSameTimeListView.Items.Add(item);
            }
        }

        /// <summary>
        /// 同時起動ソフト削除ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (bootSameTimeListView.SelectedItems.Count <= 0)
                return;

            foreach (ListViewItem item in bootSameTimeListView.SelectedItems)
            {
                bootSameTimeListView.Items.Remove(item);
            }
        }
    }
}
