using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Shopich.Models
{
    public partial class ShopichContext : DbContext
    {
        public ShopichContext()
        {
        }

        public ShopichContext(DbContextOptions<ShopichContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Categories> CategoryCollection { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Order> OrderCollection { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Shopich;Username=postgres;Password=postgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Russian_Russia.1251");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.CategoryId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("category_id");

                entity.Property(e => e.CategoryDescription).HasColumnName("category_description");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasColumnName("category_name");
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.CategoriesId)
                    .HasName("categories_pkey");

                entity.ToTable("categories");

                entity.Property(e => e.CategoriesId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("categories_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategoryCollection)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("categories_category_id_fkey");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CategoryCollection)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("categories_product_id_fkey");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrdersId)
                    .HasName("orders_pkey");

                entity.ToTable("orders");

                entity.Property(e => e.OrdersId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("orders_id");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.HasOne(d => d.OrderNavigation)
                    .WithMany(p => p.OrderCollection)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("orders_order_id_fkey");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("orders_product_id_fkey");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("order_pkey");

                entity.ToTable("order");

                entity.Property(e => e.OrderId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("order_id");

                entity.Property(e => e.IsApproved).HasColumnName("is_approved");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("order_date");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OrderCollection)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("order_user_id_fkey");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.ProductId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("product_id");

                entity.Property(e => e.ProductAvailability).HasColumnName("product_availability");

                entity.Property(e => e.ProductDiscount).HasColumnName("product_discount");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasColumnName("product_name");

                entity.Property(e => e.ProductPrice).HasColumnName("product_price");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("review");

                entity.Property(e => e.ReviewId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("review_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.ReviewDate)
                    .HasColumnType("date")
                    .HasColumnName("review_date");

                entity.Property(e => e.ReviewRating).HasColumnName("review_rating");

                entity.Property(e => e.ReviewText).HasColumnName("review_text");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("review_product_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("review_user_id_fkey");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.RoleId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("role_id");

                entity.Property(e => e.RoleDescription).HasColumnName("role_description");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.UserId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("user_id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.UserBirthday)
                    .HasColumnType("date")
                    .HasColumnName("user_birthday");

                entity.Property(e => e.UserCity).HasColumnName("user_city");

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasColumnName("user_email");

                entity.Property(e => e.UserFlat).HasColumnName("user_flat");

                entity.Property(e => e.UserHouse).HasColumnName("user_house");

                entity.Property(e => e.UserIndex).HasColumnName("user_index");

                entity.Property(e => e.UserName).HasColumnName("user_name");

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasColumnName("user_password");

                entity.Property(e => e.UserPhone).HasColumnName("user_phone");

                entity.Property(e => e.UserStreet).HasColumnName("user_street");

                entity.Property(e => e.UserSurname).HasColumnName("user_surname");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("user_role_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
