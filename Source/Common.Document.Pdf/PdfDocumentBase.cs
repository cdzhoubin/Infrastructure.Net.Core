using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.io;
using iTextSharp.text.pdf;

namespace Zhoubin.Infrastructure.Common.Document.Pdf
{
    /// <summary>
    /// Pdf文档生成对象基类
    /// </summary>
    /// <typeparam name="T">文档类型</typeparam>
    public abstract class PdfDocumentBase<T> : DocumentBase<T> where T : PdfDocumentBase<T>
    {
        /// <summary>
        /// 启用中文支持
        /// </summary>
        protected bool EnableAsian { get { return true; } }

        private iTextSharp.text.Document _document;

        private Rectangle _pageSize = PageSize.A4;
        private PdfWriter _writer;
        float _marginLeft = 90;
        float _marginRigth = 70f;
        float _marginTop = 50f;
        float _marginBottom = 50f;

        private string _tempFileName;
// ReSharper disable StaticFieldInGenericType
        static volatile object _loadSync = new object();
        static readonly List<string> RegisterFontFiles = new List<string>();
// ReSharper restore StaticFieldInGenericType

        /// <summary>
        /// 构造函数
        /// </summary>
        protected PdfDocumentBase():base(false)
        {
            FontFiles = new List<string>();
            PageEvent = new TextWatermarker();
        }

        static PdfDocumentBase()
        {
            StreamUtil.AddToResourceSearch("iTextAsian.dll");
            StreamUtil.AddToResourceSearch("iTextAsianCmaps.dll");
        }

        /// <summary>
        /// 创建字体
        /// </summary>
        /// <param name="fontName">字体名</param>
        /// <param name="size">字号</param>
        /// <param name="style">样式</param>
        /// <param name="color">颜色</param>
        /// <returns>返回创建成功的字体对象</returns>
        protected Font CreateFont(string fontName, float size, int style, BaseColor color)
        {
            return FontFactory.GetFont(fontName, BaseFont.IDENTITY_H, size, style, color);
        }

        /// <summary>
        /// 创建字体
        /// </summary>
        /// <param name="fontName">字体名</param>
        /// <param name="size">字号</param>
        /// <param name="style">样式</param>
        /// <returns>返回创建成功的字体对象</returns>
        protected Font CreateFont(string fontName, float size, int style)
        {
            return FontFactory.GetFont(fontName, BaseFont.IDENTITY_H, size, style);
        }

        /// <summary>
        /// 创建字体
        /// </summary>
        /// <param name="fontName">字体名</param>
        /// <param name="size">字号</param>
        /// <returns>返回创建成功的字体对象</returns>
        protected Font CreateFont(string fontName, float size)
        {
            return FontFactory.GetFont(fontName, BaseFont.IDENTITY_H, size);
        }

        /// <summary>
        /// 创建字体
        /// </summary>
        /// <param name="fontName">字体名</param>
        /// <returns>返回创建成功的字体对象</returns>
        protected Font CreateFont(string fontName)
        {
            return FontFactory.GetFont(fontName, BaseFont.IDENTITY_H);
        }


        /// <summary>
        /// 文档事件，用于写入水印和其它一些特殊需求使用
        /// </summary>
        protected virtual PdfPageEventHelper PageEvent { get; private set; }
        /// <summary>
        /// 初始化文档
        /// </summary>
        /// <param name="pageSize">页面，默认为A4</param>
        /// <param name="marginLeft">左间距</param>
        /// <param name="marginRigth">右间距</param>
        /// <param name="marginTop">顶部间距</param>
        /// <param name="marginBottom">底部间距</param>
        public virtual void Initialize(Rectangle pageSize, int marginLeft, int marginRigth, int marginTop, int marginBottom)
        {
            _pageSize = pageSize;
            _marginLeft = marginLeft;
            _marginRigth = marginRigth;
            _marginTop = marginTop;
            _marginBottom = marginBottom;
            Initialize();
        }

        /// <summary>
        /// 文档初始
        /// </summary>
        /// <exception cref="Exception"></exception>
        protected override void DocumentInitialize()
        {
            if (_document != null)
            {
                throw new Exception("已经初始化，不允许重复初始化。");
            }

            lock (_loadSync)
            {
                FontFiles.ForEach(p =>
                            {
                                if (File.Exists(p) && !RegisterFontFiles.Contains(p))
                                {
                                    FontFactory.Register(p);
                                    RegisterFontFiles.Add(p);
                                }
                            });
            }

            _document = new iTextSharp.text.Document(_pageSize, _marginLeft, _marginRigth, _marginTop, _marginBottom);
        }

        /// <summary>
        /// 注册字体文件
        /// </summary>
        protected List<string> FontFiles { get; private set; }
        /// <summary>
        /// 文档打开
        /// </summary>
        /// <exception cref="Exception">当文档对象未初始化时，抛出此异常</exception>
        protected override void DocumentOpen()
        {
            if (_document == null)
            {
                throw new Exception("文档还没有初始化，请调用方法（Initialize）进行。");
            }

            _tempFileName = Path.GetTempFileName();
            _writer = PdfWriter.GetInstance(_document, new FileStream(_tempFileName, FileMode.OpenOrCreate));
            _writer.PageEvent = PageEvent;
            _document.Open();
        }

        /// <summary>
        /// 文档生成
        /// </summary>
        /// <param name="stream">输出流</param>
        protected override void DocumentSave(Stream stream)
        {
            WriteDocument(_document, _writer);
            _document.Close();


            using (var tmp = new FileStream(_tempFileName, FileMode.Open, FileAccess.Read))
            {
                var buffer = new byte[2048];
                int count;
                while ((count = tmp.Read(buffer, 0, buffer.Length)) > 0)
                {
                    stream.Write(buffer, 0, count);
                }
            }

            stream.Position = 0;
        }


        /// <summary>
        /// 文档内容写入
        /// </summary>
        /// <param name="document">文档对象</param>
        /// <param name="writer">PdfWriter对象，用于高级设置如水印等等</param>
        protected virtual void WriteDocument(iTextSharp.text.Document document, PdfWriter writer)
        {
            WriteDocument(document);
        }

        /// <summary>
        /// 文档内容写入
        /// </summary>
        /// <param name="document">文档对象</param>
        protected abstract void WriteDocument(iTextSharp.text.Document document);

        /// <summary>
        /// 自动回收
        /// </summary>
        public override void Dispose()
        {
            if (_document != null)
            {
                _document.Dispose();
                _document = null;
            }

            if (_writer != null)
            {
                _writer.Dispose();
            }

            if (File.Exists(_tempFileName))
            {
                File.Delete(_tempFileName);
            }
            base.Dispose();
        }
    }
}
