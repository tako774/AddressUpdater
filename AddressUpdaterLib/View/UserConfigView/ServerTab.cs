using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using HisoutenSupportTools.AddressUpdater.Lib.Model.Config;

namespace HisoutenSupportTools.AddressUpdater.Lib.View.UserConfigView
{
    /// <summary>
    /// サーバータブ
    /// </summary>
    public partial class ServerTab : TabBase
    {
        /// <summary>
        /// サーバー一覧の取得
        /// </summary>
        /// <returns>サーバー一覧</returns>
        public Collection<ServerInformation> GetServerInformations()
        {
            var list = new Collection<ServerInformation>();
            foreach (DataGridViewRow row in serverInformationsGridView.Rows)
            {
                if (row.Cells[1].Value != null && row.Cells[1].Value.ToString() != string.Empty)
                    list.Add(new ServerInformation(row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), (bool)row.Cells[0].Value));
            }

            return list;
        }


        /// <summary>
        /// インスタンスの生成
        /// </summary>
        public ServerTab()
        {
            InitializeComponent();

            serverInformationsGridView.Columns[0].CellTemplate.ValueType = typeof(bool);
            serverInformationsGridView.Columns[1].CellTemplate.ValueType = typeof(string);
            serverInformationsGridView.Columns[2].CellTemplate.ValueType = typeof(string);
        }


        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServerTab_Load(object sender, EventArgs e)
        {
            if (UserConfig == null)
                return;

            foreach (var serverInformation in UserConfig.ServerInformations)
            {
                serverInformationsGridView.Rows.Add(new object[]
                {
                    serverInformation.Visible,
                    serverInformation.Name,
                    serverInformation.Uri,
                });
            }
        }

        /// <summary>
        /// 表示言語の反映
        /// </summary>
        /// <param name="language"></param>
        public void ReflectLanguage(Language language)
        {
            try { visivilityColumn.HeaderText = language["ServerTab_IsVisible"]; }
            catch (KeyNotFoundException) { }
            try { serverNameColumn.HeaderText = language["ServerTab_ServerName"]; }
            catch (KeyNotFoundException) { }
            try { urlColumn.HeaderText = language["ServerTab_ServerAddress"]; }
            catch (KeyNotFoundException) { }
            try { importButton.Text = language["ServerTab_Import"]; }
            catch (KeyNotFoundException) { }
            try { exportButton.Text = language["ServerTab_Export"]; }
            catch (KeyNotFoundException) { }
            try { addButton.Text = language["ServerTab_Add"]; }
            catch (KeyNotFoundException) { }
            try { deleteButton.Text = language["ServerTab_Delete"]; }
            catch (KeyNotFoundException) { }
            try { upButton.Text = language["ServerTab_Up"]; }
            catch (KeyNotFoundException) { }
            try { downButton.Text = language["ServerTab_Down"]; }
            catch (KeyNotFoundException) { }
        }

        /// <summary>
        /// 色設定の反映
        /// </summary>
        public override void ReflectTheme()
        {
            BackColor = Theme.ToolBackColor.ToColor();
            importButton.BackColor = SystemColors.Control;
            importButton.UseVisualStyleBackColor = true;
            exportButton.BackColor = SystemColors.Control;
            exportButton.UseVisualStyleBackColor = true;
            addButton.BackColor = SystemColors.Control;
            addButton.UseVisualStyleBackColor = true;
            deleteButton.BackColor = SystemColors.Control;
            deleteButton.UseVisualStyleBackColor = true;
            upButton.BackColor = SystemColors.Control;
            upButton.UseVisualStyleBackColor = true;
            downButton.BackColor = SystemColors.Control;
            downButton.UseVisualStyleBackColor = true;

            //serverInformationsGridView.ForeColor = Theme.ChatForeColor.ToColor();
            serverInformationsGridView.BackgroundColor = Theme.ChatBackColor.ToColor();
            serverInformationsGridView.DefaultCellStyle.ForeColor = Theme.ChatForeColor.ToColor();
            serverInformationsGridView.DefaultCellStyle.BackColor = Theme.ChatBackColor.ToColor();
        }

        /// <summary>
        /// インポートボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void importServerButton_Click(object sender, EventArgs e)
        {
            var message1 = "auservers.txtの内容をインポートします。";
            var message2 = "よろしいですか?";

            try { message1 = Language["ServerTab_ImportConfirm_1"]; }
            catch (KeyNotFoundException) { }
            try { message2 = Language["ServerTab_ImportConfirm_2"]; }
            catch (KeyNotFoundException) { }

            if (MessageBox.Show(
                ParentForm,
                message1 + Environment.NewLine + message2,
                Application.ProductName,
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    var serverList = serverListLoader.Load();
                    serverInformationsGridView.Rows.Clear();
                    foreach (var serverInformation in serverList)
                    {
                        serverInformationsGridView.Rows.Add(new object[]
                        {
                            serverInformation.Visible,
                            serverInformation.Name,
                            serverInformation.Uri,
                        });
                    }
                }
                catch (FileNotFoundException)
                {
                    var message = "auservers.txtが見つかりませんでした。";
                    try { message = Language["ServerTab_Import_FileNotFound"]; }
                    catch (KeyNotFoundException) { }

                    MessageBox.Show(ParentForm,
                        message,
                        Application.ProductName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }
        }

        /// <summary>
        /// エクスポートボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportServerButton_Click(object sender, EventArgs e)
        {
            var message1 = "現在の内容をauservers.txtにエクスポートします。";
            var message2 = "よろしいですか?";
            try { message1 = Language["ServerTab_ExportConfirm_1"]; }
            catch (KeyNotFoundException) { }
            try { message2 = Language["ServerTab_ExportConfirm_2"]; }
            catch (KeyNotFoundException) { }

            if (MessageBox.Show(
                ParentForm,
                message1 + Environment.NewLine + message2,
                Application.ProductName,
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var serverInformations = new Collection<ServerInformation>();
                foreach (DataGridViewRow row in serverInformationsGridView.Rows)
                {
                    var serverName = row.Cells[1].Value.ToString();
                    var uri = row.Cells[2].Value.ToString();
                    var visible = (bool)row.Cells[0].Value;
                    if (serverName != null && serverName != string.Empty)
                        serverInformations.Add(new ServerInformation(serverName, uri, visible));
                }

                try { serverListLoader.Save(serverInformations); }
                catch (UnauthorizedAccessException ex)
                {
                    var message = "エクスポートに失敗しました。";
                    try { message = Language["ServerTab_ExportFailed"]; }
                    catch (KeyNotFoundException) { }
                    MessageBox.Show(
                        ParentForm, message + Environment.NewLine + Environment.NewLine + ex.Message,
                        Application.ProductName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                catch (IOException ex)
                {
                    var message = "エクスポートに失敗しました。";
                    try { message = Language["ServerTab_ExportFailed"]; }
                    catch (KeyNotFoundException) { }
                    MessageBox.Show(
                        ParentForm, message + Environment.NewLine + Environment.NewLine + ex.Message,
                        Application.ProductName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// サーバー追加ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addServerButton_Click(object sender, EventArgs e)
        {
            var selectedRows = GetSelectedRows();

            var row = new DataGridViewRow();
            row.CreateCells(serverInformationsGridView, new object[] { true, string.Empty, string.Empty });
            if (selectedRows.Count <= 0)
            {
                serverInformationsGridView.Rows.Add(row);
                row.Selected = true;
            }
            else
            {
                serverInformationsGridView.Rows.Insert(selectedRows[0].Index, row);
                UnselectAll();
                row.Selected = true;
            }
        }

        /// <summary>
        /// サーバー削除ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeServerButton_Click(object sender, EventArgs e)
        {
            var selectedRows = new List<DataGridViewRow>(GetSelectedRows());
            if (selectedRows.Count <= 0)
                return;

            foreach (DataGridViewRow row in selectedRows)
                serverInformationsGridView.Rows.Remove(row);
        }

        /// <summary>
        /// サーバー▲ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void upServerButton_Click(object sender, EventArgs e)
        {
            var selectedRows = new List<DataGridViewRow>(GetSelectedRows());
            if (selectedRows.Count <= 0)
                return;

            foreach (DataGridViewRow row in selectedRows)
            {
                var index = row.Index;
                if (index == 0)
                    continue;

                if (selectedRows.Contains(serverInformationsGridView.Rows[index - 1]))
                    continue;

                serverInformationsGridView.Rows.Remove(row);
                serverInformationsGridView.Rows.Insert(index - 1, row);
            }

            foreach (DataGridViewRow row in serverInformationsGridView.Rows)
                row.Selected = false;

            foreach (DataGridViewRow row in selectedRows)
                row.Selected = true;
        }

        /// <summary>
        /// サーバー▼ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downServerButton_Click(object sender, EventArgs e)
        {
            var selectedRows = new List<DataGridViewRow>(GetSelectedRows());
            if (selectedRows.Count <= 0)
                return;

            selectedRows.Reverse();

            foreach (DataGridViewRow row in selectedRows)
            {
                var index = row.Index;
                if (index == serverInformationsGridView.Rows.Count -1 )
                    continue;

                if (selectedRows.Contains(serverInformationsGridView.Rows[index + 1]))
                    continue;

                serverInformationsGridView.Rows.Remove(row);
                serverInformationsGridView.Rows.Insert(index + 1, row);
            }

            foreach (DataGridViewRow row in serverInformationsGridView.Rows)
                row.Selected = false;

            foreach (DataGridViewRow row in selectedRows)
                row.Selected = true;
        }

        /// <summary>
        /// 選択されている行を取得
        /// </summary>
        /// <returns></returns>
        private Collection<DataGridViewRow> GetSelectedRows()
        {
            var selectedRows = new Collection<DataGridViewRow>();
            foreach (DataGridViewRow row in serverInformationsGridView.Rows)
            {
                if (row.Selected)
                {
                    selectedRows.Add(row);
                    continue;
                }

                var cellSelected = false;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Selected)
                    {
                        cellSelected = true;
                        break;
                    }
                }
                if(cellSelected)
                    selectedRows.Add(row);
            }

            return selectedRows;
        }

        /// <summary>
        /// 選択全解除
        /// </summary>
        private void UnselectAll()
        {
            foreach (DataGridViewRow row in serverInformationsGridView.Rows)
            {
                row.Selected = false;
                foreach (DataGridViewCell cell in row.Cells)
                    cell.Selected = false;
            }
        }

        private void serverInformationsGridView_MouseDown(object sender, MouseEventArgs e)
        {
            var info = serverInformationsGridView.HitTest(e.X, e.Y);
            if (info.RowIndex == -1)
                UnselectAll();
        }
    }
}
