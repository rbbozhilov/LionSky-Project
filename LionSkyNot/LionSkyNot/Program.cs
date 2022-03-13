using LionSkyNot.Data;
using LionSkyNot.Infrastructure;
using LionSkyNot.Services.Exercises;
using LionSkyNot.Services.Products;
using LionSkyNot.Services.Recipes;
using LionSkyNot.Services.Trainers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LionSkyDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<LionSkyDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IRecipeService, RecipeService>();
builder.Services.AddTransient<IExerciseService, ExerciseService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<ITrainerService, TrainerService>();

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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
