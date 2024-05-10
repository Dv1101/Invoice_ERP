using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Invoice_ERP.Data;
using Invoice_ERP.Areas.Identity.Data;
using Microsoft.Extensions.Options;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Invoice_ERPContextConnection") ?? throw new InvalidOperationException("Connection string 'Invoice_ERPContextConnection' not found.");

builder.Services.AddDbContext<Invoice_ERPContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<Invoice_ERPUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddDefaultTokenProviders()
    .AddRoles<IdentityRole>()
    .AddDefaultUI()
    .AddEntityFrameworkStores<Invoice_ERPContext>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("TwoFactorEnabled", x => x.RequireClaim("amr", "mfa"));
    options.AddPolicy("HRPolicy", policy => policy.RequireRole("HR"));
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("CashierPolicy", policy => policy.RequireRole("Cashier"));
    options.AddPolicy("GeneralAccessPolicy", policy =>
    {
        policy.RequireRole("HR", "Cashier", "Admin");
    });
});


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

app.MapRazorPages(); //Identity is using razor pages.

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); //Identity
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
