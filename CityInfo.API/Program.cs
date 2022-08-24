using Microsoft.AspNetCore.StaticFiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Inject a FileExtensionContentTypeProvider
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

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
