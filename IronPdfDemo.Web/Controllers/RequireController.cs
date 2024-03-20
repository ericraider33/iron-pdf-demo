using IronPdf.Extensions.Mvc.Core;
using IronPdf.Rendering;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace IronPdfDemo.Web.Controllers;

[Route("require")]
public class RequireController : Controller
{
    private readonly ILogger<HomeController> logger;
    private readonly IRazorViewRenderer viewRenderService;
    private readonly IWebHostEnvironment webHostEnvironment;

    public RequireController(
        ILogger<HomeController> logger,
        IRazorViewRenderer viewRenderService,
        IWebHostEnvironment webHostEnvironment
    )
    {
        this.logger = logger;
        this.viewRenderService = viewRenderService;
        this.webHostEnvironment = webHostEnvironment;
    }

    [HttpGet("[action]")]
    public IActionResult index()
    {
        return View();
    }

    [HttpGet("index-print")]
    public async Task<IActionResult> indexPrint()
    {
        ChromePdfRenderer renderer = new ChromePdfRenderer();
        renderer.RenderingOptions.CssMediaType = PdfCssMediaType.Print;
        renderer.RenderingOptions.Timeout = 10; // seconds
        renderer.RenderingOptions.WaitFor.JavaScript(renderer.RenderingOptions.Timeout*1000);       // wait for JS to give green light to print
        renderer.RenderingOptions.JavascriptMessageListener = message => Console.Error.WriteLine($"Chrome Console: {message}");

        // Render from URL
        String url = Request.GetEncodedUrl().Replace("index-print", "index");
        PdfDocument pdf = await renderer.RenderUrlAsPdfAsync(url);
        Response.Headers.Append("Content-Disposition", "inline");

        // Output PDF document
        return File(pdf.BinaryData, "application/pdf", "cciq-ironpdf-demo.pdf");
    }
}