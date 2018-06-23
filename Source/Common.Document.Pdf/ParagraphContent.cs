namespace Zhoubin.Infrastructure.Common.Document.Pdf
{
    /// <summary>
    /// 段落对象
    /// </summary>
    public sealed class ParagraphContent : ContentString
    {
        /// <summary>
        /// 段落构造函数
        /// 
        /// </summary>
        public ParagraphContent()
        {
            Leading = 2*15.5f;
        }
        /// <summary>
        /// 对象方式
        /// </summary>
        public int? Align { get; set; }
        /// <summary>
        /// 左缩进
        /// </summary>
        public int? IndentLeft { get; set; }
        /// <summary>
        /// 首先缩进
        /// </summary>
        public int? IndentFirst { get; set; }

        /// <summary>
        /// 默认行间距为31
        /// </summary>
        public float Leading { get; set; }
    }
}