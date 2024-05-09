using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Invoice_ERP.Data;
using Invoice_ERP.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Invoice_ERPContextConnection") ?? throw new InvalidOperationException("Connection string 'Invoice_ERPContextConnection' not found.");

builder.Services.AddDbContext<Invoice_ERPContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddIdentity<Invoice_ERPUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<Invoice_ERPContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

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

app.UseRouting();
app.UseAuthentication(); //Identity
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); //Identity is using razor pages.

app.Run();
