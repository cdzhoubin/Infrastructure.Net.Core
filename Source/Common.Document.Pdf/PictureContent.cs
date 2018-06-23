using System.IO;

namespace Zhoubin.Infrastructure.Common.Document.Pdf
{
    /// <summary>
    /// 图片内容
    /// </summary>
    public class PictureContent : ContentEntity<Stream>
    {
        /// <summary>
        /// 图片宽度
        /// </summary>
        public float? Width { get; set; }
        /// <summary>
        /// 图片高度
        /// </summary>
        public float? Height { get; set; }
        /// <summary>
        /// 对齐方式
        /// </summary>
        public int? Alignment { get; set; }
    }
}