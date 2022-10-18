using GenericRepository.Extensions;
using LoggerService.Extensions;
using MailService.Extensions;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using SharedModels.Configurations;
using SharedResources.Extensions;
using SharedResources.Helpers.Methods;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// // define the list of cultures your app will support
// // define the list of cultures your app will support
var cultureInfo = new CultureInfo("en-US");
cultureInfo.NumberFormat.CurrencySymbol = "$";

CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// Add services to the container.
#region HttpClient Services
builder.Services.AddHttpContextAccessor();
#endregion


if (builder.Environment.IsDevelopment())
{

    // Db Context Service
    //builder.Services.AddDbContext<AppDbContext>(options =>
    //{
    //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
    //    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    //},
    //ServiceLifetime.Transient);

}
else
{
    // Db Context Service
    //builder.Services.AddDbContext<AppDbContext>(options =>
    //{
    //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
    //    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    //}, ServiceLifetime.Transient);

}
builder.Services.AddRazorPages();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.ConfigureGenericContextRepository();
builder.Services.ConfigureSession(".BlazorAppInversoca.Session");
builder.Services.ConfigureAuthentication(".BlazorAppInversoca.Cookie.Session");
builder.Services.ConfigureControllers(true);
builder.Services.ConfigureMailService(builder.Configuration);
builder.Services.ConfigureLoggerService(builder.Configuration, builder.Logging);
//builder.Services.ConfigureHubBonaServices(builder.Configuration);
//builder.Services.ConfigureBonaSharedServices(builder.Configuration);

builder.Services.ConfigureLoggerService(builder.Configuration, builder.Logging);
//builder.Services.ConfigureBlazorAppInversocaRepository(builder.Configuration);
//builder.Services.ConfigureBlazorAppInversocaServices();

#region AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion
//builder.Services.Configure<MailNotificationAddresses>(builder.Configuration.GetSection("MailNotificationsAddresses"));

//builder.Services.Configure<List<VirtualDirectory>>(builder.Configuration.GetSection("VirtualDirectories"));

builder.Services.Configure<List<FileServer>>(builder.Configuration.GetSection("FileServers"));

//builder.Services.AddVersionedApiExplorer(options =>
//{
//    options.GroupNameFormat = "'v'VVV";
//    options.SubstituteApiVersionInUrl = true;
//    options.AssumeDefaultVersionWhenUnspecified = true;
//    options.DefaultApiVersion = new ApiVersion(1, 0);

//});

//builder.Services.AddApiVersioning(options =>
//{
//    options.ReportApiVersions = true;
//});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("general", new OpenApiInfo { Title = $"{(!builder.Environment.IsProduction() ? $"{builder.Environment.EnvironmentName} " : "")}BlazorAppInversoca API", Version = "v1" });
});


var app = builder.Build();
// Set ServiceValues to 
AppServicesHelper.Services = app.Services;
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseWebAssemblyDebugging();
    app.UseDeveloperExceptionPage();
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger(c =>
{
    c.RouteTemplate = "swagger/{documentName}/swagger.json";

});

app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("general/swagger.json", $"{(!builder.Environment.IsProduction() ? $"{builder.Environment.EnvironmentName} " : "")}BlazorAppInversoca API V1");
});

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.ConfigureRequestLoggingMiddleware();
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(
//        Path.Combine(Directory.GetCurrentDirectory(), "files","invoices")),
//    RequestPath = "/files"
//});

app.UseRouting();

app.ConfigureFileServer(builder.Environment, builder.Configuration.GetSection("FileServers"));

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
