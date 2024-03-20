using IronPdf.Extensions.Mvc.Core;

IronPdf.License.LicenseKey = "IRONSUITE.EESCHENBACH.CHRONICCAREIQ.COM.10292-B05B296EA1-BT4EALWBCCZBW2U2-AZL3DY2T3UXD-PIT5O5S4B37N-RC6QJUUELGKU-WKJ6FAEF7CDI-JNB4QKDBUKNS-4HPLGH-TS2E4Y5W3O2MEA-DEPLOYMENT.TRIAL-R7YFNG.TRIAL.EXPIRES.14.APR.2024";
IronPdf.Installation.LinuxAndDockerDependenciesAutoConfig = true;
IronPdf.Installation.AutomaticallyDownloadNativeBinaries = true;
Console.WriteLine($"IronPDF License Valid? {IronPdf.License.IsLicensed}\n");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation(); 

// needed for IronPdf
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IRazorViewRenderer, RazorViewRenderer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();