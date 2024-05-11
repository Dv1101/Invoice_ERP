using Invoice_ERP.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Invoice_ERP.Models;
using System.Reflection.Emit;

namespace Invoice_ERP.Data;

public class Invoice_ERPContext : IdentityDbContext<Invoice_ERPUser>
{
    public Invoice_ERPContext(DbContextOptions<Invoice_ERPContext> options)
        : base(options)
    {

    }

    public DbSet<Invoice_ERPUser> extendedIdentity { get; set; } //Extra Fields Tables for identity

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.Entity<ItemsStock>()
            .Property(p => p.UnitPrice)
            .HasColumnType("decimal(18, 2)"); // Adjust precision and scale as needed
        builder.Entity<ItemsStock>()
            .Property(p => p.SellPrice)
            .HasColumnType("decimal(18, 2)"); // Adjust precision and scale as needed
        builder.Entity<ItemsStock>()
            .Property(p => p.TotalEarned)
            .HasColumnType("decimal(18, 2)"); // Adjust precision and scale as needed
    }

public DbSet<Invoice_ERP.Models.Category> CategoryModel { get; set; } = default!;

public DbSet<Invoice_ERP.Models.UnitOfMeasure> UnitOfMeasure { get; set; } = default!;

public DbSet<Invoice_ERP.Models.Supplier> Supplier { get; set; } = default!;

public DbSet<Invoice_ERP.Models.ItemsStock> ItemsStock { get; set; } = default!;
}
