using BusinessLogicLayer.Abstractions;
using BusinessLogicLayer.Services;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//use Autofac as DI container
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.
builder.Services.AddRazorPages();
//DI registrations
builder.Services.AddScoped<IPostService, postService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// configure Autofac container
{   builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        // Register your own things directly with Autofac here. Don't
        // call builder.Populate(), that happens automatically.
        // containerBuilder.RegisterModule(new YourAutofacModule());

        //Load Persistence Module by name
        var persistenceModule = Assembly.Load("Persistence");
        // register all Autofac modules found in the Persistence
        containerBuilder.RegisterAssemblyModules();
    });
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
