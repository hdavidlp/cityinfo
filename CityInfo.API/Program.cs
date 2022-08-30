using CityInfo.API;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.StaticFiles;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/cityinfo.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();


var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Host.UseSerilog();



// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
})
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Inject a FileExtensionContentTypeProvider
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

# if DEBUG 
builder.Services.AddTransient<IMailService, LocalMailService>();
# else
builder.Services.AddTransient<IMailService, CloudMailService>();
#endif

builder.Services.AddSingleton<CitiesDataStore>();


var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
//app.UseAuthorization();
//app.MapControllers();

// The next code is used when no extra url pattern is abailable
//app.Run(async(context) =>
//{
//    await context.Response.WriteAsync("Hello Worlds");
//});

app.UseHttpsRedirection();
app.UseRouting();           // enabled endpoint Routing
app.UseAuthorization();

// Use EndPoints
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
