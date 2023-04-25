using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TechTreeWebApp.Data;

// Builder Contains IConfiguration
var builder = WebApplication.CreateBuilder(args); 

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// .AddDbContext(What type of code is our db using?);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
// M:AddDefaultIdentity<ApplicationUser>
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false) //switched to false
    .AddRoles<IdentityRole>() //D: Roles of User as part of the authentication of the MVC
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

/* Add our custom class with the .Net Core Dependency Injection so that we can reuse code within the Registered type throughout multiple control classes*/
builder.Services.AddScoped<IDataFunctions, DataFunctions>();
// Registering DataFunctions.cs for Dependency Injec
// AddTransient() -- But Factor: Object's Instanciation and Lifetime a new DataFunctions is instanciated everutime an IDataFunc() is injected -- a new object is created before a new object is injected  -- new instance to every controller and every service
// AddSingleTon()-- created once even through multiple request
// AddScope()  --  IDataFunctions is instanciated once when a new request in made; DataFunctions is available throughout the course of that single request until ejected -- different to every request


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
// MVC Area : "Admin" Areas allows you to separate your modules and organize Model, View, Controller, Web.config 
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "Admin",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
