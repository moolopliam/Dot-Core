using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TEST.Models
{
    public partial class TestpContext : DbContext
    {
        public TestpContext()
        {
        }

        public TestpContext(DbContextOptions<TestpContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TCatategory> TCatategory { get; set; }
        public virtual DbSet<TCustomer> TCustomer { get; set; }
        public virtual DbSet<TProduct> TProduct { get; set; }
        public virtual DbSet<TSatus> TSatus { get; set; }
        public virtual DbSet<TTitel> TTitel { get; set; }
        public virtual DbSet<TType> TType { get; set; }
        public virtual DbSet<TUnit> TUnit { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-RPVUC2D;Initial Catalog=TestP;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TCatategory>(entity =>
            {
                entity.HasKey(e => e.CatId);

                entity.ToTable("T_Catategory");

                entity.Property(e => e.CatId)
                    .HasColumnName("CatID")
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.NameCat)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TCustomer>(entity =>
            {
                entity.HasKey(e => e.CustId);

                entity.ToTable("T_Customer");

                entity.Property(e => e.CustId)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CustType)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.InitialCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CustTypeNavigation)
                    .WithMany(p => p.TCustomer)
                    .HasForeignKey(d => d.CustType)
                    .HasConstraintName("FK_T_Customer_T_Type");

                entity.HasOne(d => d.InitialCodeNavigation)
                    .WithMany(p => p.TCustomer)
                    .HasForeignKey(d => d.InitialCode)
                    .HasConstraintName("FK_T_Customer_T_Titel");
            });

            modelBuilder.Entity<TProduct>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.ToTable("T_Product");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CatId)
                    .HasColumnName("CatID")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Qty)
                    .HasColumnName("qty")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StatusId)
                    .HasColumnName("statusID")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.UnitCode)
                    .HasColumnName("unitCode")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.UnitPerPrice).HasColumnName("unitPerPrice");

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.TProduct)
                    .HasForeignKey(d => d.CatId)
                    .HasConstraintName("FK_T_Product_T_Catategory");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TProduct)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_T_Product_T_Satus");

                entity.HasOne(d => d.UnitCodeNavigation)
                    .WithMany(p => p.TProduct)
                    .HasForeignKey(d => d.UnitCode)
                    .HasConstraintName("FK_T_Product_T_Unit");
            });

            modelBuilder.Entity<TSatus>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.ToTable("T_Satus");

                entity.Property(e => e.StatusId)
                    .HasColumnName("StatusID")
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.StatusName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TTitel>(entity =>
            {
                entity.HasKey(e => e.InitialCode);

                entity.ToTable("T_Titel");

                entity.Property(e => e.InitialCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.InitialName)
                    .HasColumnName("initialName")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TType>(entity =>
            {
                entity.HasKey(e => e.CustType);

                entity.ToTable("T_Type");

                entity.Property(e => e.CustType)
                    .HasColumnName("custType")
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.NameType).HasMaxLength(10);
            });

            modelBuilder.Entity<TUnit>(entity =>
            {
                entity.HasKey(e => e.UnitCode);

                entity.ToTable("T_Unit");

                entity.Property(e => e.UnitCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.NameUnit)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
