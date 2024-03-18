using System.Diagnostics;
using IronPdf.Extensions.Mvc.Core;
using IronPdf.Rendering;
using Microsoft.AspNetCore.Mvc;
using IronPdfDemo.Web.Models;

namespace IronPdfDemo.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> logger;
    private readonly IRazorViewRenderer viewRenderService;
    private readonly IWebHostEnvironment webHostEnvironment;

    public HomeController(
        ILogger<HomeController> logger,
        IRazorViewRenderer viewRenderService,
        IWebHostEnvironment webHostEnvironment
    )
    {
        this.logger = logger;
        this.viewRenderService = viewRenderService;
        this.webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("/home.pdf")]
    public async Task<IActionResult> Pdf()
    {
        ChromePdfRenderer renderer = new ChromePdfRenderer(); 
        renderer.RenderingOptions.CssMediaType = PdfCssMediaType.Print;
        renderer.RenderingOptions.Timeout = 30;                     // seconds
        renderer.RenderingOptions.WaitFor.JavaScript();

        // Render from URL
        // var uri = new UriBuilder(Request.Scheme, Request.Host.Host, Request.Host.Port.GetValueOrDefault(), "/").Uri;
        // PdfDocument pdf = await renderer.RenderUrlAsPdfAsync(uri);
        
        // Render from Razor View
        var baseUri = new UriBuilder(Request.Scheme, Request.Host.Host, Request.Host.Port ?? -1).Uri;
        var html = await viewRenderService.RenderRazorViewAsync(new object(), "/Views/Home/Index.cshtml");
        PdfDocument pdf = renderer.RenderHtmlAsPdf(html, baseUri);
        Response.Headers.Append("Content-Disposition", "inline");

        // Output PDF document
        return File(pdf.BinaryData, "application/pdf", "cciq-ironpdf-demo.pdf");
    }

    public async Task<IActionResult> Html()
    {
        ChromePdfRenderer renderer = new ChromePdfRenderer();
        renderer.RenderRazorViewToPdf(viewRenderService, "/Views/Home/Index.cshtml", new object());
        var html = await viewRenderService.RenderRazorViewAsync(new object(), "/Views/Home/Index.cshtml");
        return Content(html, "text/html");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}