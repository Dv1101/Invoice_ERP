using Invoice_ERP.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Invoice_ERP.Data;

public class Invoice_ERPContext : IdentityDbContext<Invoice_ERPUser>
{
    public Invoice_ERPContext(DbContextOptions<Invoice_ERPContext> options)
        : base(options)
    {

    }

    public DbSet<Invoice_ERPUser> extendedIdentity { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
