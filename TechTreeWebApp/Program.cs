using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TechTreeWebApp.Data;
using TechTreeWebApp.Filters;
using TechTreeWebApp.ServiceContracts;
using TechTreeWebApp.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("AzureSQLTechTreeDatabase"); 

if(connectionString == null)
    connectionString = Environment.GetEnvironmentVariable("AzureSQLTechTreeDatabase");

// .AddDbContext(What type of code is our db using?);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
// M:AddDefaultIdentity<ApplicationUser>
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false) //switched to false
    .AddRoles<IdentityRole>() 
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IDataFunctions, DataFunctions>();
builder.Services.AddScoped<ICategoryItemDetailsGetterService, CategoryItemDetailsGetterService>();
builder.Services.AddScoped<IEmailService,EmailService>();

builder.Services.AddScoped<ICategoryItemAdderService, CategoryItemAdderService>();
builder.Services.AddScoped<ICategoryItemGetterService, CategoryItemGetterService>();
builder.Services.AddScoped<ICategoryItemDeleterService, CategoryItemDeleterService>();
builder.Services.AddScoped<ICategoryItemUpdaterService, CategoryItemUpdaterService>();

builder.Services.AddScoped<ICategoriesAdderService, CategoriesAdderService>();
builder.Services.AddScoped<ICategoriesGetterService, CategoriesGetterService>();
builder.Services.AddScoped<ICategoriesDeleterService, CategoriesDeleterService>();
builder.Services.AddScoped<ICategoriesUpdaterService, CategoriesUpdaterService>();
builder.Services.AddScoped<ICategoriesToUserGetterServices, CategoriesToUserGetterService>();

builder.Services.AddScoped<IContentAdderService, ContentAdderService>();
builder.Services.AddScoped<IContentGetterService, ContentGetterService>();
builder.Services.AddScoped<IContentUpdaterService, ContentUpdaterService>();

builder.Services.AddScoped<IMediaTypeAdderService, MediaTypeAdderService>();
builder.Services.AddScoped<IMediaTypeDeleterService, MediaTypeDeleterService>();
builder.Services.AddScoped<IMediaTypeGetterService, MediaTypeGetterService>();
builder.Services.AddScoped<IMediaTypeUpdaterService, MediaTypeUpdaterService>();

// builder.Services.AddScoped<AdminHomeAuthorizationFilter>();
builder.Services.AddApplicationInsightsTelemetry();
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
