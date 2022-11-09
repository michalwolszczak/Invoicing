using InvoicingWebCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using InvoicingWebCore.Models;
using InvoicingWebCore;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using InvoicingWebCore.Services;
using InvoicingWebCore.Interfaces;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddDbContext<ApplicationDbContext>(options => options
    .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
    .AddDefaultIdentity<ApplicationUser>(options =>
    {
        options.Password.RequiredLength = 3;
        options.Password.RequiredUniqueChars = 3;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireDigit = false;
    }).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/Login");

builder.Services
    .AddControllersWithViews()
    .AddRazorRuntimeCompilation();

builder.Services.AddRazorPages();

builder.Services.AddTransient<IItemService, ItemService>();
builder.Services.AddTransient<IContractorService, ContractorService>();
builder.Services.AddTransient<IInvoiceService, InvoiceService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IDatabaseService, DatabaseService>();
builder.Services.AddLogging();

builder.Services.AddSession(s => s.IdleTimeout = TimeSpan.FromMinutes(60));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var supportedCultures = new[]
{
    new CultureInfo("en-US")
};

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    // Formatting numbers, dates, etc.
    SupportedCultures = supportedCultures,
    // UI strings that we have localized.
    SupportedUICultures = supportedCultures
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
