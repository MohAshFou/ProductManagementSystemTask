using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystemِAPITask.Models;

namespace ProductManagementSystemِAPITask.Data;

public partial class ProductManagementSystemContext : DbContext
{
    public ProductManagementSystemContext()
    {
    }

    public ProductManagementSystemContext(DbContextOptions<ProductManagementSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)


    { 
    
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC07E2CB965B");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UrlimageProduct).HasColumnName("URLImageProduct");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
