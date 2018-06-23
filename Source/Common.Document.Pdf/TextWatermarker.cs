using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Zhoubin.Infrastructure.Common.Document.Pdf
{
    /// <summary>
    /// 文本水印
    /// </summary>
    public sealed class TextWatermarker : PdfPageEventHelper
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TextWatermarker()
            : this("四川日报招标比选网")
        {

        }
        private readonly Phrase _phrase;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="waterMaker">水印文本</param>
        public TextWatermarker(string waterMaker)
        {
            _phrase = new Phrase(waterMaker, FontFactory.GetFont("华文宋体", BaseFont.IDENTITY_H, 55, Font.NORMAL, new GrayColor(0.85f)));
        }
        /// <summary>
        /// Called when a page is finished, just before being written to the document.
        /// </summary>
        /// <param name="writer">水印文本 <CODE>PdfWriter</CODE></param>
        /// <param name="document"><code>iTextSharp.text.Document</code></param>
        public override void OnEndPage(PdfWriter writer, iTextSharp.text.Document document)
        {
            if (_phrase == null)
            {
                return;
            }

            ColumnText.ShowTextAligned(writer.DirectContentUnder, Element.ALIGN_CENTER, _phrase, 300, 421, writer.PageNumber % 2 == 1 ? 45 : -45);
            base.OnEndPage(writer, document);
        }
    }
}