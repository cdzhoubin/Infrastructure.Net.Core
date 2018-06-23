namespace Zhoubin.Infrastructure.Common.Document.Pdf
{
    /// <summary>
    /// 内容基类
    /// </summary>
    /// <typeparam name="T">内容类型</typeparam>
    public class ContentEntity<T> : IContentEntity
    {
        /// <summary>
        /// 内容
        /// </summary>
        public T Content { get; set; }
        /// <summary>
        /// 添加内容后，加入追加空行
        /// </summary>
        public bool AppendNewLine { get; set; }
    }
    
    /// <summary>
    /// 内容接口
    /// </summary>
    public interface IContentEntity
    {
        /// <summary>
        /// 追加空行
        /// </summary>
        bool AppendNewLine { get; set; }
    }
}