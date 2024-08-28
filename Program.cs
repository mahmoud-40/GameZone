
using GameZone.Services;

var builder = WebApplication.CreateBuilder(args);


//Connect to SQLServer
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                       ?? throw new InvalidOperationException("No Connection String was found !!");

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(connectionString));
//.................................

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ICategoriesServices, CategoriesServices>();
builder.Services.AddScoped<IDevicesServices, DevicesServices>();
builder.Services.AddScoped<IGamesServices, GamesServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
