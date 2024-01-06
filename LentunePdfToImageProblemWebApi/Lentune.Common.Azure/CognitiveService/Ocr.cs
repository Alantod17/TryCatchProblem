using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using DevExpress.Drawing;
using DevExpress.Pdf;

namespace Lentune.Common.Azure.CognitiveService
{
    public static class Ocr
    {
        

        public static List<byte[]> ConvertPdfToImage(byte[] pdfFileByteArray, DXImageFormat imageFormat = null)
        {
            if(imageFormat == null) imageFormat = DXImageFormat.Png;
            if (pdfFileByteArray == null) return null;
            var resultList = new List<byte[]>();
            int largestEdgeLength = 4000;
            // Create a PDF Document Processor. 
            PdfDocumentProcessor processor = new PdfDocumentProcessor();
            processor.RenderingEngine = PdfRenderingEngine.Skia;
            using (var inputMemoryStream = new MemoryStream(pdfFileByteArray))
            {
                try
				{
					processor.LoadDocument(inputMemoryStream, true);
	                for (int pageNumber = 1; pageNumber <= processor.Document.Pages.Count; pageNumber++)
	                {
	                    var pageText = processor.GetPageText(pageNumber);
	                    var pageTextCount = pageText?.Length;
	                    if (pageTextCount > 10000) continue;
	                    var image = processor.CreateDXBitmap(pageNumber, largestEdgeLength);
	     
	                    using (var outputMemoryStream = new MemoryStream())
	                    {
		                    image.Save(outputMemoryStream, imageFormat);
	                        var pageByteArray = outputMemoryStream.ToArray();
	                        resultList.Add(pageByteArray);
	                    }
	                }
                }
                catch (System.ArgumentException)
				{
                    return new List<byte[]>();
				}
                catch (Exception ex)
                {
	                throw ex;
                }
			}
            return resultList;
        }


    }

}
