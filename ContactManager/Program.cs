using ContactManager.Common;
using ContactManager.Repository;
using ContactManager.Repository.Database;
using Microsoft.EntityFrameworkCore;
using ContactManager.Service;
using ContactManager.Common.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ContactDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(Constants.SQLDBKey)));

builder.Services.AddRepositories(builder.Configuration);//Services
builder.Services.AddServices(builder.Configuration);//Services


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
    pattern: "{controller=Contact}/{action=Index}/{id?}");

// Apply migrations at startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ContactDbContext>();
    dbContext.Database.Migrate();
}


app.Run();
