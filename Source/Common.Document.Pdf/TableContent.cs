using System.Collections.Generic;
using System.Data;
using iTextSharp.text;

namespace Zhoubin.Infrastructure.Common.Document.Pdf
{
    /// <summary>
    /// 表格内容
    /// </summary>
    public class TableContent : ContentEntity<DataTable>
    {
        /// <summary>
        /// 标题字体
        /// </summary>
        public Font HeaderFont { get; set; }

        /// <summary>
        /// 内容字体
        /// </summary>
        public Font ContentFont { get; set; }

        /// <summary>
        /// 列宽度
        /// </summary>
        public List<float> RelativeWidths { get; set; }
        /// <summary>
        /// 是是否显示标题
        /// </summary>
        public bool ShowHeader { get; set; }
    }
}