using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Services;
using SmartGallery.Client.Data;
using SmartGallery.Client.Helpers;
using SmartGallery.Client.Services;
using SmartGallery.Client.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthenticationCore();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddHttpClient<ILoginService, LoginService>(client => client.BaseAddress = new Uri("https://localhost:7247"));
builder.Services.AddHttpClient<IServicesService, ServicesService>(client => client.BaseAddress = new Uri("https://localhost:7247"));
builder.Services.AddHttpClient<IReservationsService, ReservationsService>(client => client.BaseAddress = new Uri("https://localhost:7247"));
builder.Services.AddHttpClient<IServiceItemsService, ServiceItemsService>(client => client.BaseAddress = new Uri("https://localhost:7247"));
builder.Services.AddHttpClient<IReviewsService, ReviewsService>(client => client.BaseAddress = new Uri("https://localhost:7247"));


builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

