using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Blazored.Toast;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using CounterStateServer.Data;
using CounterState;
using CounterStateServer;
using Fluxor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<ICounterStateViewModel, CounterStateViewModel>();
builder.Services.AddTransient<QueryExecutor>();
builder.Services.AddBlazoredToast();
builder.Services.AddHostedService<CounterStateBackgroundService>();
builder.Services.AddSingleton<IEditStateViewModel, EditStateViewModel>();

var assembly = Assembly.GetExecutingAssembly();
builder.Services.AddFluxor(options => options.ScanAssemblies(assembly).UseReduxDevTools());
//builder.Services.AddHostedService<KafkaConsumerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();


app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

/*add hub*/
app.MapHub<BlazorChatHub>(BlazorChatHub.HubUrl);

app.Run();