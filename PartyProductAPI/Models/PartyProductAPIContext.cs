using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PartyProductAPI.Models
{
    public partial class PartyProductAPIContext : DbContext
    {
        public PartyProductAPIContext()
        {
        }

        public PartyProductAPIContext(DbContextOptions<PartyProductAPIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Assigns> Assigns { get; set; }
        public virtual DbSet<Invoices> Invoices { get; set; }
        public virtual DbSet<Parties> Parties { get; set; }
        public virtual DbSet<ProductRate> ProductRate { get; set; }
        public virtual DbSet<Products> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-1TALNNC\\MSSQLSERVER02;Database=PartyProductAPI;Trusted_Connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assigns>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.HasOne(d => d.Party)
                    .WithMany(p => p.Assigns)
                    .HasForeignKey(d => d.PartyId)
                    .HasConstraintName("FK_dbo.Assigns_dbo.Parties_PartyId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Assigns)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_dbo.Assigns_dbo.Products_ProductId");
            });

            modelBuilder.Entity<Invoices>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PartyId).HasColumnName("Party_id");

                entity.Property(e => e.ProductId).HasColumnName("Product_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Rate).HasColumnName("rate");

                entity.HasOne(d => d.Party)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.PartyId)
                    .HasConstraintName("FK_dbo.Invoices_dbo.Parties_Party_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_dbo.Invoices_dbo.Products_Product_id");
            });

            modelBuilder.Entity<Parties>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PName)
                    .IsRequired()
                    .HasColumnName("p_name");
            });

            modelBuilder.Entity<ProductRate>(entity =>
            {
                entity.ToTable("Product_Rate");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.PrId).HasColumnName("Pr_id");

                entity.Property(e => e.Rate).HasColumnName("rate");

                entity.HasOne(d => d.Pr)
                    .WithMany(p => p.ProductRate)
                    .HasForeignKey(d => d.PrId)
                    .HasConstraintName("FK_dbo.Product_Rate_dbo.Products_Pr_id");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PrName)
                    .IsRequired()
                    .HasColumnName("pr_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
