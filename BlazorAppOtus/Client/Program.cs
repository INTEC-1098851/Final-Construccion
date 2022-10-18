using BlazorAppOtus.Client;
using BlazorAppOtus.Client.Services;
using MatBlazor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SharedModels.Configurations;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMatBlazor();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri($"{builder.HostEnvironment.BaseAddress}api/") });
// // define the list of cultures your app will support
var cultureInfo = new CultureInfo("en-US");
cultureInfo.NumberFormat.CurrencySymbol = "$";

CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
//builder.Services.Configure<RequestLocalizationOptions>(options =>

//{
//    // // define the list of cultures your app will support
//    var supportedCultures = new List<CultureInfo>()
//{
//new CultureInfo("en-US"),
//};

//    // set the default culture
//    options.DefaultRequestCulture = new RequestCulture("en-US");
//    options.DefaultRequestCulture.Culture.NumberFormat.CurrencySymbol = supportedCultures[1].NumberFormat.CurrencySymbol;
//    options.SupportedCultures = supportedCultures;
//    options.SupportedUICultures = supportedCultures;
//    options.RequestCultureProviders = new List<IRequestCultureProvider>() {
//        new QueryStringRequestCultureProvider()
//    };
//});
#region Services
builder.Services.AddTransient<IInvoiceService, InvoiceService>();
#endregion

await builder.Build().RunAsync();
