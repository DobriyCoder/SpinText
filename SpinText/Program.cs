using DobriyCoder.Core.Events;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Tls;
using SpiningText.Parser;
using SpinText.Blocks.Services;
using SpinText.Coins.Services;
using SpinText.Exporter.Services;
using SpinText.FaqParser;
using SpinText.Generator.Services;
using SpinText.HT.Services;
using SpinText.Languages.Services;
using SpinText.Models;
using SpinText.Types;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

/*builder.Services.AddDbContext<Db>(options =>
{
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), serverVersion)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors();
});*/

builder.Services.AddTransient<BlocksManager>();
builder.Services.AddTransient<ExporterProvider>();
builder.Services.AddTransient<IExportFile, CsvExportFile>();
builder.Services.AddTransient<ILogFile, TxtLogFile>();
builder.Services.AddTransient<IGenerator, Generator>();
builder.Services.AddSingleton<EventsBus>();
builder.Services.AddSingleton<HTProvider>();
builder.Services.AddTransient<HTManager>();
builder.Services.AddTransient<HTPairsManager>();
builder.Services.AddTransient<HTCommonManager>();
builder.Services.AddTransient<CoinsManager>();
builder.Services.AddTransient<PairsManager>();
builder.Services.AddSingleton<LanguagesManager>();
builder.Services.AddSingleton<TypesManager>();
builder.Services.AddSingleton<DBFactory>();
builder.Services.AddSingleton<ISTParser>(i => new STParser());

builder.Services.AddTransient<IFaqParser, FaqParser>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
