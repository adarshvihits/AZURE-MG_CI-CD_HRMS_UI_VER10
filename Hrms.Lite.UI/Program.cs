using Hrms.Lite.Shared.General;
using Hrms.Lite.UI;
using Hrms.Lite.UI.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using Telerik.Reporting.Cache.File;
using Telerik.Reporting.Services;
using Telerik.Reporting.Services.AspNetCore;

// ======================================================
// BUILDER
// ======================================================

var builder = WebApplication.CreateBuilder(args);

// ======================================================
// DATA PROTECTION
// ======================================================

builder.Services.AddDataProtection();

// ======================================================
// DISTRIBUTED CACHE
// ======================================================

builder.Services.AddDistributedMemoryCache();

// ======================================================
// SESSION
// ======================================================

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(1440);

    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = "HRMS_Cookie";
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
});

// ======================================================
// ANTIFORGERY
// ======================================================

builder.Services.AddAntiforgery(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
});

// ======================================================
// APP SETTINGS
// ======================================================

builder.Services.Configure<AppSettings>(
    builder.Configuration);

// ======================================================
// DEPENDENCY INJECTION
// ======================================================

builder.Services.AddDependencyInjectionServices();

// ======================================================
// CONFIGURATION
// ======================================================

builder.Services.AddSingleton<IConfiguration>(
    builder.Configuration);

// ======================================================
// TELERIK REPORTING
// ======================================================

builder.Services.TryAddSingleton<IReportServiceConfiguration>(sp =>
{
    var environment =
        sp.GetRequiredService<IWebHostEnvironment>();

    return new ReportServiceConfiguration
    {
        ReportingEngineConfiguration =
            ConfigurationHelper.ResolveConfiguration(
                environment),

        HostAppId =
            builder.Configuration.GetValue<string>(
                "ReportHostAppID"),

        Storage = new FileStorage(),

        ReportResolver = new ReportFileResolver(
            Path.Combine(
                environment.ContentRootPath,
                "Reports"))
    };
});

// ======================================================
// RAZOR PAGES / MVC / CONTROLLERS / VIEWS
// ======================================================

builder.Services
    .AddControllersWithViews(options =>
    {
        options.MaxModelBindingCollectionSize =
            int.MaxValue;
    })
    .AddNewtonsoftJson()
    .AddCookieTempDataProvider(options =>
    {
        options.Cookie.Name =
            ".AspNetCore.Mvc.CookieTempDataProvider";

        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
        options.Cookie.SecurePolicy =
            CookieSecurePolicy.Always;
        options.Cookie.SameSite =
            SameSiteMode.Strict;
    })
    .AddRazorRuntimeCompilation();

builder.Services
    .AddRazorPages()
    .AddNewtonsoftJson();

// ======================================================
// KENDO
// ======================================================

builder.Services.AddKendo();

// ======================================================
// FORM OPTIONS
// ======================================================

builder.Services.Configure<FormOptions>(options =>
{
    options.ValueCountLimit = int.MaxValue;

    // 100 MB
    options.MultipartBodyLengthLimit = 104857600;
});

// ======================================================
// BUILD APPLICATION
// ======================================================

var app = builder.Build();

// ======================================================
// ERROR HANDLING
// ======================================================

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Account/Error");
}

// ======================================================
// STATIC FILES
// ======================================================

app.UseStaticFiles();

// ======================================================
// ROUTING
// ======================================================

app.UseRouting();

// ======================================================
// AUTHORIZATION
// ======================================================

app.UseAuthorization();

// ======================================================
// SESSION
// ======================================================

app.UseSession();

app.UseCookiePolicy();

// ======================================================
// MAX REQUEST BODY SIZE
// ======================================================

app.Use(async (context, next) =>
{
    var maxRequestBodySizeFeature =
        context.Features
            .Get<IHttpMaxRequestBodySizeFeature>();

    if (maxRequestBodySizeFeature != null)
    {
        maxRequestBodySizeFeature.MaxRequestBodySize =
            104857600; // 100 MB
    }

    await next();
});

// ======================================================
// ENCODING
// ======================================================

System.Text.Encoding.RegisterProvider(
    System.Text.CodePagesEncodingProvider.Instance);

// ======================================================
// ROUTES
// ======================================================

app.MapControllerRoute(
    name: "areaRoute",
    pattern:
        "{area:exists}/{controller=Department}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "subAreaRoute",
    pattern:
        "{area:exists}/{subarea:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern:
        "{controller=Home}/{action=Index}/{id?}");

// ======================================================
// RAZOR PAGES
// ======================================================

app.MapRazorPages();

// ======================================================
// RUN
// ======================================================

app.Run();