using Blazored.Toast;
using FamilyTree_UI.Configuration.Extension;
using FamilyTree_UI.Manager.EndPoints.EndPointsManager;
using FamilyTree_UI.Shared.Services;
using FamilyTreeUI.Manager.Implementation;
using FamilyTreeUI.Manager.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Globalization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddCustomServices();
builder.Services.AddHttpClient();
builder.Services.AddBlazoredToast();
// Program.cs
builder.Services.AddScoped<NavStateService>();
// In Program.cs or Startup.cs
builder.Services.AddSingleton<LoaderService>();
builder.Services.AddScoped<ConfirmDialogService>();
builder.Services.AddLocalization();
var baseUrl = builder.Configuration["ApiSettings:BaseUrl"];
EndPointsManager.InitializeAllEndPoints(baseUrl);


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
// Register the Toast Service

app.Run();
