using LionSkyNot.Data;
using LionSkyNot.Data.Models.User;

using LionSkyNot.Infrastructure;

using LionSkyNot.Services.Classes;
using LionSkyNot.Services.Exercises;
using LionSkyNot.Services.Gym;
using LionSkyNot.Services.Products;
using LionSkyNot.Services.Recipes;
using LionSkyNot.Services.Statistics;
using LionSkyNot.Services.Trainers;
using LionSkyNot.Services.Users;

using LionSkyNot.Services.WishLists;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LionSkyDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LionSkyDbContext>();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddAuthentication()
    .AddFacebook(options =>
    {
        options.AppId = builder.Configuration.GetValue<string>("Facebook:AppId");
        options.AppSecret = builder.Configuration.GetValue<string>("Facebook:AppSecret");
    })
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration.GetValue<string>("Google:ClientId");
        options.ClientSecret = builder.Configuration.GetValue<string>("Google:ClientSecret");
    });





builder.Services.AddMemoryCache();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
});

builder.Services.AddTransient<IRecipeService, RecipeService>();
builder.Services.AddTransient<IExerciseService, ExerciseService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<ITrainerService, TrainerService>();
builder.Services.AddTransient<IStatisticsService, StatisticsService>();
builder.Services.AddTransient<IClassService, ClassService>();
builder.Services.AddTransient<IWishListService, WishListService>();
builder.Services.AddTransient<IClientService, ClientService>();
builder.Services.AddTransient<IUserService, UserService>();

var app = builder.Build();

app.PrepareDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();

app.UseStatusCodePagesWithRedirects("/Home/StatusCode?code={0}");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
