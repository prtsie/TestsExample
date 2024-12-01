using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TestsExample.Database.Context;
using TestsExample.Database.Repositories.PostRepository;
using TestsExample.Database.Repositories.UserRepository;
using TestsExample.Services.IdentityProvider;
using TestsExample.Services.PostsService;
using TestsExample.Services.UsersService;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<TestsSampleDbContext>(opts => opts.UseSqlServer(connectionString));
builder.Services.AddScoped<IGenericReader>(provider => provider.GetRequiredService<TestsSampleDbContext>());
builder.Services.AddScoped<IGenericWriter>(provider => provider.GetRequiredService<TestsSampleDbContext>());
builder.Services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<TestsSampleDbContext>());

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IPostsRepository, PostsRepository>();

builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IPostsService, PostsService>();

builder.Services.AddScoped<IIdentityProvider, IdentityProvider>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/User/Login";
        options.AccessDeniedPath = options.LoginPath;
        options.LogoutPath = "/User/Logout";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=TheWall}/{id?}");

app.Run();