using System.Collections.Generic;
using System.Data;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Zhoubin.Infrastructure.Common.Document.Pdf
{
    /// <summary>
    /// Document对象扩展方法
    /// </summary>
    public static class DocumentExtent
    {
        /// <summary>
        /// 字符串对象写入
        /// </summary>
        /// <param name="document">itext Document对象</param>
        /// <param name="contentEntity">内容对象</param>
        /// <returns>itext Document对象</returns>
        public static iTextSharp.text.Document Write(this iTextSharp.text.Document document, ContentString contentEntity)
        {
            var p = new Phrase(contentEntity.Content, contentEntity.Font);
            document.Add(p);
            if (contentEntity.AppendNewLine)
            {
                document.Add(Chunk.NEWLINE);
            }

            return document;
        }

        /// <summary>
        /// 段落写入
        /// </summary>
        /// <param name="document">itext Document对象</param>
        /// <param name="content">段落对象</param>
        /// <returns>itext Document对象</returns>
        public static iTextSharp.text.Document Write(this iTextSharp.text.Document document, ParagraphContent content)
        {
            var p = new Paragraph(content.Content, content.Font)
            {
                Leading = content.Leading,
                Alignment = content.Align != null ? content.Align.Value : Element.ALIGN_JUSTIFIED
            };
            if (content.IndentLeft != null)
            {
                p.IndentationLeft = content.IndentLeft.Value * content.Font.Size;
            }

            if (content.IndentFirst != null)
            {
                p.FirstLineIndent = content.IndentFirst.Value * content.Font.Size;
            }

            document.Add(p);
            return document;
        }

        /// <summary>
        /// 图片写入
        /// </summary>
        /// <param name="document">itext Document对象</param>
        /// <param name="picture">图片对象</param>
        /// <returns>itext Document对象</returns>
        public static iTextSharp.text.Document Write(this iTextSharp.text.Document document, PictureContent picture)
        {
            var image = Image.GetInstance(picture.Content);
            if (picture.Alignment != null)
            {
                image.Alignment = picture.Alignment.Value;
            }

            if (picture.Width != null && picture.Height != null)
            {
                image.ScaleToFit(picture.Width.Value, picture.Height.Value);
            }
            else
            {
                if (picture.Width != null)
                {
                    image.ScaleAbsoluteWidth(picture.Width.Value);
                }

                if (picture.Height != null)
                {
                    image.ScaleAbsoluteHeight(picture.Height.Value);
                }
            }
            //image.ScalePercent();
            document.Add(image);
            if (picture.AppendNewLine)
            {
                document.Add(Chunk.NEWLINE);
            }

            return document;
        }

        /// <summary>
        /// 新行写入
        /// </summary>
        /// <param name="document">itext Document对象</param>
        /// <param name="content">换行对象</param>
        /// <returns>itext Document对象</returns>
        public static iTextSharp.text.Document Write(this iTextSharp.text.Document document, NewLineContent content)
        {
            for (var i = 0; i < content.Count; i++)
                document.Add(content.Content);
            return document;
        }

        /// <summary>
        /// 表格写入
        /// </summary>
        /// <param name="document">itext Document对象</param>
        /// <param name="content">表格</param>
        /// <returns>itext Document对象</returns>
        public static iTextSharp.text.Document Write(this iTextSharp.text.Document document, TableContent content)
        {
            if (content == null || content.Content == null)
            {
                return document;
            }

            var table = new PdfPTable(content.RelativeWidths.ToArray());
            PdfPRow row;
            var cells = new List<PdfPCell>();
            if (content.ShowHeader)
            {
                var headcells = new List<PdfPHeaderCell>();
                foreach (DataColumn column in content.Content.Columns)
                {
                    var cell = new PdfPHeaderCell();
                    cell.AddElement(new Phrase(column.Caption, content.HeaderFont));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Padding = content.HeaderFont.Size * 0.4f;
                    headcells.Add(cell);
                }

                row = new PdfPRow(headcells.ConvertAll(p => (PdfPCell)p).ToArray());
                table.Rows.Add(row);
            }

            foreach (DataRow dr in content.Content.Rows)
            {
                cells.Clear();
                cells.AddRange(from DataColumn column in content.Content.Columns
                               select new PdfPCell(new Phrase(dr[column.ColumnName] == null ? "" : dr[column.ColumnName].ToString(), content.ContentFont))
                               {
                                   HorizontalAlignment = Element.ALIGN_CENTER,
                                   VerticalAlignment = Element.ALIGN_MIDDLE,
                                   Padding = content.ContentFont.Size*0.3f,
                               });

                row = new PdfPRow(cells.ToArray());
                table.Rows.Add(row);
            }

            document.Add(table);
            return document;
        }
    }
}
