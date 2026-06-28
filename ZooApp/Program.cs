using Microsoft.EntityFrameworkCore;
using ZooApp.Data;
using ZooApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<ZooDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ZooDatabase")));

// Services
builder.Services.AddScoped<IAnimalService, AnimalService>();
builder.Services.AddScoped<ILoggerService, ConsoleLoggerService>();

// MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Seed database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ZooDbContext>();
    SeedData.Initialize(context);
}

app.Run();