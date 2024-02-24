using SmartGallery.Server;
using SmartGallery.Server.Extensions;
using SmartGallery.Server.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureAllRequiredServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(Constants.AppPolicy);
app.UseStaticFiles();
// this custom middleware
app.UseMiddleware<GlobalErrorHandlerMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("Index.html");

app.Run();
