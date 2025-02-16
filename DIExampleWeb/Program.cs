using Microsoft.Extensions.DependencyInjection;
using Stone.ServiceContract;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddScoped<ICitiesService, CitiesService>();

builder.Services.Add(
    new ServiceDescriptor(
        typeof(ICitiesService),
        typeof(CitiesService),
        ServiceLifetime.Transient
    )
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();

//app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
