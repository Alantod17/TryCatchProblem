using Lentune.Common.Azure.CognitiveService;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PdfController : ControllerBase
	{

		private readonly ILogger<PdfController> _logger;

		public PdfController(ILogger<PdfController> logger)
		{
			_logger = logger;
		}

		[HttpGet(Name = "GetPdf")]
		public string Get()
		{
			var file3033 = System.IO.File.ReadAllBytes("./TestFiles/File3033.pdf");
			var bmpByte3033 = Ocr.ConvertPdfToImage(file3033);

			var file3666 = System.IO.File.ReadAllBytes("./TestFiles/File3666.pdf");
			var bmpByte3666 = Ocr.ConvertPdfToImage(file3666);

			return "done";
		}
	}
}