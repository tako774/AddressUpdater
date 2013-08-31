using System;
using System.Diagnostics;
using System.Reflection;

namespace HisoutenSupportTools.AddressUpdater.Lib
{
    /// <summary>パラメータも戻り値も持たないメソッドをカプセル化します。</summary>
    public delegate void Action();

    /// <summary>パラメータを受け取らずに、TResult パラメータに指定された型の値を返すメソッドをカプセル化します。</summary>
    /// <typeparam name="TResult">このデリゲートによってカプセル化されるメソッドの戻り値の型。</typeparam>
    /// <returns>このデリゲートによってカプセル化されるメソッドの戻り値。</returns>
    public delegate TResult Func<TResult>();

    /// <summary>
    /// 単一の読み取り専用フィールドを持つイベント引数
    /// </summary>
    /// <typeparam name="T">フィールドの型</typeparam>
    public class EventArgs<T> : EventArgs
    {
        /// <summary>フィールド</summary>
        public readonly T Field;

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="field">フィールド</param>
        public EventArgs(T field)
        {
            Field = field;
        }
    }

    /// <summary>
    /// 時間計測用
    /// </summary>
    public class TimeMeasure
    {
        private readonly string _name;
        private readonly DateTime _startTime;

        /// <summary>時間計測用</summary>
        public TimeMeasure()
        {
            _startTime = DateTime.Now;
        }

        /// <summary>時間計測用</summary>
        /// <param name="name">識別用の名前</param>
        public TimeMeasure(string name)
        {
            _startTime = DateTime.Now;
            _name = name;
            Debug.WriteLine(string.Format("{0} start {1:HH:mm:ss.fffffff}", _name, _startTime));
        }

        /// <summary>
        /// 終了時間を出力
        /// </summary>
        public void WriteFinishTime()
        {
            var finishTime = DateTime.Now;
            if (string.IsNullOrEmpty(_name))
                Debug.WriteLine(finishTime - _startTime);
            else
                Debug.WriteLine(string.Format("{0} finish {1:HH:mm:ss.fffffff} {2}", _name, finishTime, finishTime - _startTime));
        }
    }

    /// <summary>enumにつけるテキスト属性</summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public sealed class EnumTextAttribute : Attribute
    {
        /// <summary>テキスト</summary>
        public readonly string Text;

        /// <summary></summary>
        public EnumTextAttribute()
            : this(string.Empty)
        { }

        /// <summary></summary>
        /// <param name="text"></param>
        public EnumTextAttribute(string text)
        {
            Text = text;
        }

        /// <summary></summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetText(Enum value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);

            return GetText(type.GetField(name));
        }

        /// <summary></summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public static string GetText(MemberInfo memberInfo)
        {
            var attributes = memberInfo.GetCustomAttributes(typeof(EnumTextAttribute), true);

            foreach (var attribute in attributes)
                if (attribute is EnumTextAttribute)
                    return ((EnumTextAttribute)attribute).Text;

            return null;
        }
    }
}
