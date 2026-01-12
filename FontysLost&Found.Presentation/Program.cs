//using BusinessLogicLayer.Abstractions;
using BusinessLogicLayer.Services;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using System.Reflection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// use Autofac as DI container
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options=>
    {
        options.LoginPath = "/Login";
        options.LogoutPath= "/Logout";
        options.AccessDeniedPath = "/AccessDenied";
    });

// Configure Autofac container (must run before Build)
builder.Host.ConfigureContainer<ContainerBuilder>((ctx, container) =>
{
    var persistenceAsm = Assembly.Load("Persistence");
    var businessAsm = Assembly.Load("BusinessLogicLayer");

    container.RegisterAssemblyModules(businessAsm, persistenceAsm);
});

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
