using System;
using System.Runtime.InteropServices;

namespace HisoutenSupportTools.AddressUpdater.Lib.Api
{
    /// <summary>
    /// User32
    /// </summary>
    public class User32
    {
        /// <summary>
        /// 1 つまたは複数のウィンドウへ、指定されたメッセージを送信します。
        /// この関数は、指定されたウィンドウのウィンドウプロシージャを呼び出し、
        /// そのウィンドウプロシージャがメッセージを処理し終わった後で、制御を返します。
        /// </summary>
        /// <param name="hWnd">送信先ウィンドウのハンドル</param>
        /// <param name="Msg">メッセージ</param>
        /// <param name="wParam">メッセージの最初のパラメータ</param>
        /// <param name="lParam">メッセージの 2 番目のパラメータ</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);

        /// <summary></summary>
        public const uint EM_LINESCROLL = 0x00B6;

        /// <summary></summary>
        public const uint WM_HSCROLL = 0x0114;

        /// <summary></summary>
        public const int SB_HORZ = 0x0;
        /// <summary></summary>
        public const int SB_VERT = 0x1;

        /// <summary>左へ1単位スクロール</summary>
        public const uint SB_LINELEFT = 0;
        /// <summary>右へ1単位スクロール</summary>
        public const uint SB_LINERIGHT = 1;
        /// <summary>左へウィンドウの幅だけスクロール</summary>
        public const uint SB_PAGELEFT = 2;
        /// <summary>右へウィンドウの幅だけスクロール</summary>
        public const uint SB_PAGERIGHT = 3;
        /// <summary>スクロールボックスを操作した</summary>
        public const uint SB_THUMBPOSITION = 4;
        /// <summary>スクロールボックスを操作中</summary>
        public const uint SB_THUMTRACK = 5;
        /// <summary>左にスクロール</summary>
        public const uint SB_LEFT = 6;
        /// <summary>右にスクロール</summary>
        public const uint SB_RIGHT = 7;
        /// <summary>スクロールを終了</summary>
        public const uint SB_ENDSCROLL = 8;

        /// <summary>
        /// 指定したスクロールバーの中のスクロールボックス（つまみ）の現在の位置を取得します。
        /// 現在の位置とは、現在のスクロール範囲に基づく相対的な値のことです。
        /// たとえば、スクロール範囲が 0～100 で、スクロールボックスがスクロールバーの中央に存在する場合、現在の位置は 50 になります。
        /// </summary>
        /// <param name="hWnd">ウィンドウのハンドル</param>
        /// <param name="nBar">スクロールバーのオプション</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int GetScrollPos(IntPtr hWnd, int nBar);

        /// <summary>
        /// 指定したスクロールバーの中で、スクロールボックス（つまみ）の位置を設定します。
        /// また、要求に応じて、スクロールボックスの新しい位置を反映するためにスクロールバーを再描画します。
        /// </summary>
        /// <param name="hWnd">ウィンドウのハンドル</param>
        /// <param name="nBar">スクロールバー</param>
        /// <param name="nPos">スクロールボックスの新しい位置</param>
        /// <param name="bRedraw">再描画フラグ</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);

        /// <summary>
        /// 指定された文字列と一致するクラス名とウィンドウ名を持つトップレベルウィンドウ（親を持たないウィンドウ）のハンドルを返します。
        /// この関数は、子ウィンドウは探しません。検索では、大文字小文字は区別されません。
        /// </summary>
        /// <param name="lpClassName">クラス名</param>
        /// <param name="lpWindowName">ウィンドウ名</param>
        /// <returns>指定したクラス名とウィンドウ名を持つウィンドウのハンドル</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        /// <summary>
        /// 指定された文字列と一致するキャプションを持つトップレベルウィンドウ（親を持たないウィンドウ）のハンドルを返します。
        /// この関数は、子ウィンドウは探しません。検索では、大文字小文字は区別されません。
        /// </summary>
        /// <param name="caption">キャプション</param>
        /// <returns>指定したキャプションを持つウィンドウのハンドル</returns>
        public static IntPtr FindWindow(string caption)
        {
            return FindWindow(null, caption);
        }

        /// <summary>
        /// 指定されたウィンドウを作成したスレッドの ID を取得します。
        /// 必要であれば、ウィンドウを作成したプロセスの ID も取得できます。
        /// </summary>
        /// <param name="hWnd">ウィンドウのハンドル</param>
        /// <param name="lpdwProcessId">プロセスID</param>
        /// <returns>ウィンドウを作成したスレッドのID</returns>
        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        /// <summary>
        /// 指定されたウィンドウの左上端と右下端の座標をスクリーン座標で取得します。
        /// スクリーン座標は、表示画面の左上端が (0,0) となります。
        /// </summary>
        /// <param name="hWnd">ウィンドウのハンドル</param>
        /// <param name="lpRect">ウィンドウの座標値</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        /// <summary>
        /// 指定されたクライアント領域を確保するために必要なウィンドウ座標を計算します。
        /// </summary>
        /// <param name="lpRect">クライアント矩形領域の左上端と右下端の座標を入れた RECT 構造体へのポインタを指定します。指定したクライアント領域に対応するウィンドウの左上端と右下端の座標が、この構造体に入れて返されます。</param>
        /// <param name="dwStyle">サイズを計算するウィンドウのウィンドウスタイル を指定します。ただし、WS_OVERLAPPED は指定できません。</param>
        /// <param name="bMenu">ウィンドウがメニューを持つかどうかを指定します。</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool AdjustWindowRect(out RECT lpRect, WindowStyles dwStyle, bool bMenu);

        /// <summary>
        /// 子ウィンドウ、ポップアップウィンドウ、またはトップレベルウィンドウのサイズ、位置、および Z オーダーを変更します。
        /// これらのウィンドウは、その画面上での表示に従って順序が決められます。
        /// 最前面にあるウィンドウは最も高いランクを与えられ、Z オーダーの先頭に置かれます。
        /// </summary>
        /// <param name="hWnd">ウィンドウのハンドル</param>
        /// <param name="hWndInsertAfter">配置順序のハンドル</param>
        /// <param name="X">横方向の位置</param>
        /// <param name="Y">縦方向の位置</param>
        /// <param name="cx">幅</param>
        /// <param name="cy">高さ</param>
        /// <param name="uFlags">ウィンドウ位置のオプション</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SWP uFlags);
    }
}
