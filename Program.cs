using DreamDay.Business.Interface;
using DreamDay.Business.Service;
using DreamDay.Data;
using DreamDay.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IAppImageService, AppImageService>();
builder.Services.AddScoped<IBudgetService, BudgetService>();
builder.Services.AddScoped<IChecklistItemService, ChecklistItemService>();
builder.Services.AddScoped<IGuestService, GuestService>();
builder.Services.AddScoped<ITimelineService, TimelineService>();
builder.Services.AddScoped<IVendorCategoryService, VendorCategoryService>();
builder.Services.AddScoped<IVendorPackageBookingService, VendorPackageBookingService>();
builder.Services.AddScoped<IVendorPackageService, VendorPackageService>();
builder.Services.AddScoped<IVendorReviewService, VendorReviewService>();
builder.Services.AddScoped<IVendorService, VendorService>();
builder.Services.AddScoped<IWeddingService, WeddingService>();

builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(3000); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

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

// Create Default Roles and Admin User
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    await SeedRolesAsync(userManager, roleManager);
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
{
    string[] roleNames = { "Admin", "Client", "Planner" };

    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    string adminEmail = "admin@admin.com";
    string clientEmail = "client@client.com";
    string plannerEmail = "planner@plannert.com";
    string password = "Pass@123";

    // Create a default Admin user
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            FirstName = "Admin",
        };

        var result = await userManager.CreateAsync(adminUser, password);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }

    // Create a default Client user
    var clientUser = await userManager.FindByEmailAsync(clientEmail);
    if (clientUser == null)
    {
        clientUser = new ApplicationUser
        {
            UserName = clientEmail,
            Email = clientEmail,
            FirstName = "Client",
        };

        var result = await userManager.CreateAsync(clientUser, password);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(clientUser, "Client");
        }
    }

    // Create a default Planner user
    var plannerUser = await userManager.FindByEmailAsync(plannerEmail);
    if (plannerUser == null)
    {
        plannerUser = new ApplicationUser
        {
            UserName = plannerEmail,
            Email = plannerEmail,
            FirstName = "Planner",
        };

        var result = await userManager.CreateAsync(plannerUser, password);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(plannerUser, "Planner");
        }
    }
}