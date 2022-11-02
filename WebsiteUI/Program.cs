using Business.Abstract;
using Business.Concrete;
using Data.Abstract;
using Data.Concrete.EfCore;
using Data.UserData;
using Entities.UserEntities.SiteUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebsiteUI.FunctionsLibrary.EmailServices;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

//Controller routes

//Product Controller
app.MapControllerRoute(
    name: "shoplist",
    pattern: "product/list/{sortingIndex?}",
    defaults: new { controller = "Product", action = "List" }
    );

app.MapControllerRoute(
    name: "productdetail",
    pattern: "product/details/{productUrl}",
    defaults: new { controller = "Product", action = "Details" }
    );

app.MapControllerRoute(
    name: "shoplistwithcategory",
    pattern: "product/list/{categoryUrl?}",
    defaults: new { controller = "Product", action = "List" }
    );

app.MapControllerRoute(
    name: "shoplistwithcategory",
    pattern: "product/search/{searchString?}/{sortingIndex?}",
    defaults: new { controller = "Product", action = "Search" }
    );

//Admin Controller

app.MapControllerRoute(
    name: "adminproductlist",
    pattern: "admin/productlist",
    defaults: new { controller = "Admin", action = "ProductList" }
    );

app.MapControllerRoute(
    name: "admincategorylist",
    pattern: "admin/categorylist",
    defaults: new { controller = "Admin", action = "CategoryList" }
    );

app.MapControllerRoute(
    name: "admincreateproduct",
    pattern: "admin/createproduct",
    defaults: new { controller = "Admin", action = "CreateProduct" }
    );

app.MapControllerRoute(
    name: "admineditproduct",
    pattern: "admin/editproduct/{productId?}",
    defaults: new { controller = "Admin", action = "EditProduct" }
    );

app.MapControllerRoute(
    name: "adminremoveimagefromproduct",
    pattern: "admin/removeimagefromproduct/{productId}/{imageUrl}",
    defaults: new { controller = "Admin", action = "RemoveImageFromProduct" }
    );

app.MapControllerRoute(
    name: "admineditcategory",
    pattern: "admin/editcategory/{categoryId}/{page?}",
    defaults: new { controller = "Admin", action = "EditCategory" }
    );
//admin controller Identity related stuff
app.MapControllerRoute(
    name: "adminroleedit",
    pattern: "admin/roleedit/{roleId?}",
    defaults: new { controller = "Admin", action = "RoleEdit" }
    );

app.MapControllerRoute(
    name: "adminrolelist",
    pattern: "admin/rolelist",
    defaults: new { controller = "Admin", action = "RoleList" }
    );

app.MapControllerRoute(
    name: "adminrolecreate",
    pattern: "admin/rolecreate",
    defaults: new { controller = "Admin", action = "RoleCreate" }
    );

app.MapControllerRoute(
    name: "adminuserlist",
    pattern: "admin/userlist",
    defaults: new { controller = "Admin", action = "UserList" }
    );

app.MapControllerRoute(
    name: "adminuseredit",
    pattern: "admin/useredit/{userId}",
    defaults: new { controller = "Admin", action = "UserEdit" }
    );

app.MapControllerRoute(
    name: "adminuserdelete",
    pattern: "admin/userdelete",
    defaults: new { controller = "Admin", action = "UserDelete" }
    );



//Home Controller
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

//configures necessary elements
static void ConfigureServices(WebApplicationBuilder builder)
{
    //database configurations
    builder.Services.AddDbContext<ShopContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection")));
    builder.Services.AddDbContext<SiteUserContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection")));
    builder.Services.AddIdentity<SiteUser, IdentityRole>().AddEntityFrameworkStores<SiteUserContext>().AddDefaultTokenProviders();

    builder.Services.Configure<IdentityOptions>(options =>
    {
        //password
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 9;
        options.Password.RequireNonAlphanumeric = true;

        //lockout
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
        options.Lockout.AllowedForNewUsers = true;

        //usernae and sign in
        options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = true;
        options.SignIn.RequireConfirmedEmail = true;
        options.SignIn.RequireConfirmedPhoneNumber = false;
    });

    builder.Services.ConfigureApplicationCookie(options =>
    {//cookie: data left in users browser. Thus the browser recognizes user on subsequent visits.
        options.LoginPath = "/account/login";
        options.LogoutPath = "/account/logout";
        options.AccessDeniedPath = "/account/accessdenied";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(6);
        options.Cookie = new CookieBuilder
        {
            HttpOnly = true,
            Name = ".Cinarmak.Security.Cookie",
            SameSite = SameSiteMode.Strict
        };
    });

    builder.Services.AddMvc();

    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

    builder.Services.AddScoped<IProductService, ProductManager>();
    builder.Services.AddScoped<ICategoryService, CategoryManager>();
    builder.Services.AddScoped<IImageService, ImageManager>();

    builder.Services.AddScoped<IEmailSender, SmtpEmailSender>();
}
