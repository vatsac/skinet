using System;
using Core.Model;
using Core.Model.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure.Data
{
    public partial class skinetContext : DbContext
    {
        public skinetContext()
        {
        }

        public skinetContext(DbContextOptions<skinetContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DeliveryMethod> DeliveryMethod { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductBrand> ProductBrand { get; set; }
        public virtual DbSet<ProductType> ProductType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
              optionsBuilder.UseSqlServer("Server=localhost;Database=skinet;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeliveryMethod>(entity =>
            {
                entity.Property(e => e.DeliveryTime).IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ShortName).IsUnicode(false);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(e => e.ItemOrderedPictureUrl)
                    .HasColumnName("ItemOrdered_PictureUrl")
                    .IsUnicode(false);

                entity.Property(e => e.ItemOrderedProductItemId).HasColumnName("ItemOrdered_ProductItemId");

                entity.Property(e => e.ItemOrderedProductName)
                    .HasColumnName("ItemOrdered_ProductName")
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdersId");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(e => e.BuyerEmail).IsUnicode(false);

                entity.Property(e => e.OrderDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PaymentIntenId).IsUnicode(false);

                entity.Property(e => e.ShipToAddressCity)
                    .HasColumnName("ShipToAddress_City")
                    .IsUnicode(false);

                entity.Property(e => e.ShipToAddressFirstName)
                    .HasColumnName("ShipToAddress_FirstName")
                    .IsUnicode(false);

                entity.Property(e => e.ShipToAddressLastName)
                    .HasColumnName("ShipToAddress_LastName")
                    .IsUnicode(false);

                entity.Property(e => e.ShipToAddressState)
                    .HasColumnName("ShipToAddress_State")
                    .IsUnicode(false);

                entity.Property(e => e.ShipToAddressStreet)
                    .HasColumnName("ShipToAddress_Street")
                    .IsUnicode(false);

                entity.Property(e => e.ShipToAddressZipcode)
                    .HasColumnName("ShipToAddress_Zipcode")
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Pending')");

                entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.DeliveryMethodNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.DeliveryMethod)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DeliveryMethod");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.PictureUrl).IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.ProductBrand)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProductBrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_ProductBrandId");

                entity.HasOne(d => d.ProductType)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProductTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductTypeId");
            });

            modelBuilder.Entity<ProductBrand>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
