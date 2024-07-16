using AspNetCoreHero.ToastNotification;
using Library.Data;
using Library.Implementation.Repositories;
using Library.Implementation.Services;
using Library.Implementation.Repository;
using Library.Implementation.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer
                                                     (builder.Configuration.GetConnectionString("Default Connection")));

builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 15;
    config.IsDismissable = true;
    config.Position = NotyfPosition.TopRight;
}
);



builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IBookService,BookService>();
builder.Services.AddScoped<ICategoryService,CategoryService>();


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
