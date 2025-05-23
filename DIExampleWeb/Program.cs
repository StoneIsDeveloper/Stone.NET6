using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Stone.ServiceContract;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(
    new AutofacServiceProviderFactory());
// Add services to the container.
builder.Services.AddControllersWithViews();

// Use Autofac for IOC/DI
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuild =>
{
    containerBuild.RegisterType<CitiesService>().As<ICitiesService>().InstancePerLifetimeScope();
    //containerBuild.RegisterType<CitiesService>().As<ICitiesService>().SingleInstance();
    
});

//builder.Services.AddScoped<ICitiesService, CitiesService>();

//builder.Services.Add(
//    new ServiceDescriptor(
//        typeof(ICitiesService),
//        typeof(CitiesService),
//        ServiceLifetime.Scoped
//    )
//);

//builder.Services.AddTransient<ICitiesService, CitiesService>(); 


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();

var mykey = app.Configuration["MyKey"];


//app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// cmd to run app use setting Environment
// $Env:ASPNETCORE_ENVIRONMENT="Staging"
// dotnet run --no-launch-profile
