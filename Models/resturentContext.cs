using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApplication3.Dto;

namespace WebApplication3.Models
{
    public partial class resturentContext : DbContext
    {

        public bool IgnorFilter { get; set; }

        public resturentContext()
        {
        }

        public resturentContext(DbContextOptions<resturentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<RestaurantMenu> RestaurantMenus { get; set; }
        
        public virtual DbSet<RestViewTable> RestViewTables { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=root;database=resturent", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.34-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Archived).HasColumnType("tinyint(4)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(45)
                    .HasColumnName("firstName");

                entity.Property(e => e.LastName).HasMaxLength(45);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Order>(entity =>
            {

                entity.ToTable("Order");
                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();
                entity.HasIndex(e => e.CustomerId, "id_idx");

                entity.HasIndex(e => e.RestaurantMenuId, "restaurant_menu_id_idx");

                entity.Property(e => e.CustomerId)
                    .HasColumnType("int(11)")
                    .HasColumnName("customer_id");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.RestaurantMenuId)
                    .HasColumnType("int(11)")
                    .HasColumnName("restaurant_menu_id");

                entity.HasOne(d => d.Customer)
                    .WithMany()
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("customer_id");

                entity.HasOne(d => d.RestaurantMenu)
                    .WithMany()
                    .HasForeignKey(d => d.RestaurantMenuId)
                    .HasConstraintName("restaurant_menu_id");
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.ToTable("Restaurant");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Archived).HasColumnType("tinyint(4)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(45);

                entity.Property(e => e.PhoneNumber).HasMaxLength(45);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<RestaurantMenu>(entity =>
            {
                entity.ToTable("RestaurantMenu");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Archived).HasColumnType("tinyint(4)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.MealName).HasMaxLength(45);

                entity.Property(e => e.Quantity).HasColumnType("int(11)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
                
                entity.Property(e => e.RestaurantId)
                    .HasColumnType("int(11)")
                    .HasColumnName("restaurant_id");
                
                entity.HasOne(e => e.Restaurant)
                    .WithMany()
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("restaurant_id");
            });
            
            modelBuilder.Entity<RestViewTable>(entity =>
            {
                entity.ToView("CSVResturant");
                entity.HasNoKey();
                
            });

            modelBuilder.Entity<Restaurant>().Ignore(e => (e.Archived == 1) || IgnorFilter);
            modelBuilder.Entity<RestaurantMenu>().Ignore(e => (e.Archived == 1) || IgnorFilter);
            modelBuilder.Entity<Customer>().Ignore(e => (e.Archived == 1) || IgnorFilter);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
