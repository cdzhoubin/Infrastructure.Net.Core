using iTextSharp.text;

namespace Zhoubin.Infrastructure.Common.Document.Pdf
{
    /// <summary>
    /// 新行内容
    /// </summary>
    public class NewLineContent : ContentEntity<Chunk>
    {
        /// <summary>
        /// 换行内容
        /// </summary>
        public new Chunk Content
        {
            get
            {
                return Chunk.NEWLINE;
            }
        }

        /// <summary>
        /// 换行次数
        /// </summary>
        public int Count { get; set; }
    }
}