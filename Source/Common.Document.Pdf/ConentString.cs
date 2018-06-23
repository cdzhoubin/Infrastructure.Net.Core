using iTextSharp.text;

namespace Zhoubin.Infrastructure.Common.Document.Pdf
{
    /// <summary>
    /// 字符串对象
    /// </summary>
    public class ContentString : ContentEntity<string>
    {
        /// <summary>
        /// 字体
        /// </summary>
        public Font Font { get; set; }
    }
}