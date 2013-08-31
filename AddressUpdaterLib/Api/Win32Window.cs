using System;
using System.Drawing;

namespace HisoutenSupportTools.AddressUpdater.Lib.Api
{
    /// <summary>
    /// ウィンドウ
    /// </summary>
    public class Win32Window
    {
        /// <summary>ウィンドウハンドル</summary>
        private IntPtr _windowHandle = IntPtr.Zero;
        /// <summary>ウィンドウハンドルの取得</summary>
        public IntPtr WindowHandle { get { return _windowHandle; } }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="windowHandle">ウィンドウハンドル</param>
        public Win32Window(IntPtr windowHandle)
        {
            _windowHandle = windowHandle;
        }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="caption">キャプション</param>
        public Win32Window(string caption)
            : this(null, caption)
        { }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="className">クラス名</param>
        /// <param name="caption">キャプション</param>
        public Win32Window(string className, string caption)
            : this(User32.FindWindow(className, caption))
        { }

        /// <summary>
        /// ウィンドウの左上と右下の座標を取得
        /// </summary>
        /// <param name="windowStyle">サイズを計算するウィンドウのウィンドウスタイル を指定します。ただし、WS_OVERLAPPED は指定できません。</param>
        /// <param name="hasMenu">ウィンドウがメニューを持つかどうか</param>
        /// <returns>ウィンドウの左上と右下の座標</returns>
        /// <exception cref="WindowNotFoundException">ウィンドウが見つからなかった時に発生します。</exception>
        /// <exception cref="GetWindowRectFailedException">座標の取得に失敗した時に発生します。</exception>
        /// <exception cref="AdjustWindowRectFailedException">指定されたクライアント領域を確保するために必要なウィンドウ座標の計算に失敗した時に発生します。</exception>
        public Rectangle GetWindowRect(WindowStyles windowStyle, bool hasMenu)
        {
            if (windowStyle == WindowStyles.WS_OVERLAPPED)
                throw new ArgumentException("WS_OVERLAPPED は指定できません。", "windowStyle");

            if (_windowHandle == IntPtr.Zero)
                throw new WindowNotFoundException();

            var rect = new RECT();
            if (!User32.GetWindowRect(_windowHandle, out rect))
                throw new GetWindowRectFailedException();

            var adRect = new RECT(rect.Left, rect.Top, rect.Right, rect.Bottom);
            if (!User32.AdjustWindowRect(out adRect, windowStyle, hasMenu))
                throw new AdjustWindowRectFailedException();

            var rectangle = rect.ToRectangle();
            rectangle.Width -= (rect.Left - adRect.Left) * 2;
            rectangle.Height -= (rect.Top - adRect.Top) + (adRect.Bottom - rect.Bottom);
            return rectangle;
        }

        /// <summary>
        /// ウィンドウのサイズ、位置、順序を設定します。
        /// </summary>
        /// <param name="hWndInsertAfter">配置順序のハンドル</param>
        /// <param name="windowStyle">サイズを計算するウィンドウのウィンドウスタイル を指定します。ただし、WS_OVERLAPPED は指定できません。</param>
        /// <param name="hasMenu">ウィンドウがメニューを持つかどうか</param>
        /// <param name="uFlags">ウィンドウ位置のオプション</param>
        /// <param name="x">横方向の位置</param>
        /// <param name="y">縦方向の位置</param>
        /// <param name="width">幅</param>
        /// <param name="height">高さ</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="WindowNotFoundException">ウィンドウが見つからなかった時に発生します。</exception>
        /// <exception cref="AdjustWindowRectFailedException">指定されたクライアント領域を確保するために必要なウィンドウ座標の計算に失敗した時に発生します。</exception>
        public void SetWindowPos(IntPtr hWndInsertAfter, WindowStyles windowStyle, bool hasMenu, SWP uFlags, int x, int y, int width, int height)
        {
            SetWindowPos(hWndInsertAfter, windowStyle, hasMenu, uFlags, new Rectangle(x, y, width, height));
        }

        /// <summary>
        /// ウィンドウのサイズ、位置、順序を設定します。
        /// </summary>
        /// <param name="hWndInsertAfter">配置順序のハンドル</param>
        /// <param name="windowStyle">サイズを計算するウィンドウのウィンドウスタイル を指定します。ただし、WS_OVERLAPPED は指定できません。</param>
        /// <param name="hasMenu">ウィンドウがメニューを持つかどうか</param>
        /// <param name="uFlags">ウィンドウ位置のオプション</param>
        /// <param name="rectangle">座標・サイズ</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="WindowNotFoundException">ウィンドウが見つからなかった時に発生します。</exception>
        /// <exception cref="AdjustWindowRectFailedException">指定されたクライアント領域を確保するために必要なウィンドウ座標の計算に失敗した時に発生します。</exception>
        public void SetWindowPos(IntPtr hWndInsertAfter, WindowStyles windowStyle, bool hasMenu, SWP uFlags, Rectangle rectangle)
        {
            if (windowStyle == WindowStyles.WS_OVERLAPPED)
                throw new ArgumentException("WS_OVERLAPPED は指定できません。", "windowStyle");

            if (_windowHandle == IntPtr.Zero)
                throw new WindowNotFoundException();

            var rect = RECT.FromRectangle(rectangle);
            if (!User32.AdjustWindowRect(out rect, windowStyle, hasMenu))
                throw new AdjustWindowRectFailedException();

            User32.SetWindowPos(
                    _windowHandle,
                    hWndInsertAfter,
                    rect.Left + (rectangle.Left - rect.Left),
                    rect.Top + (rectangle.Top - rect.Top),
                    rect.Width,
                    rect.Height,
                    uFlags);
        }
    }
}
