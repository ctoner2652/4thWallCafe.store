using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace _4ThWallCafe.MVC.Core.Entities;

public partial class FourthWallCafeContext : DbContext
{

    public FourthWallCafeContext(DbContextOptions<FourthWallCafeContext> options)
        : base(options)
    {
    }
    public virtual DbSet<CafeOrder> CafeOrder { get; set; }

    public virtual DbSet<CartItem> CartItem { get; set; }

    public virtual DbSet<Category> Category { get; set; }

    public virtual DbSet<Item> Item { get; set; }

    public virtual DbSet<ItemPrice> ItemPrice { get; set; }

    public virtual DbSet<OrderItem> OrderItem { get; set; }

    public virtual DbSet<PaymentType> PaymentType { get; set; }

    public virtual DbSet<Server> Server { get; set; }

    public virtual DbSet<TimeOfDay> TimeOfDay { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CafeOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__CafeOrde__C3905BAFE0A6FEBD");

            entity.ToTable("CafeOrder");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.AmountDue).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PaymentTypeId).HasColumnName("PaymentTypeID");
            entity.Property(e => e.ServerId).HasColumnName("ServerID");
            entity.Property(e => e.SubTotal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Tax).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Tip).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.PaymentType).WithMany(p => p.CafeOrders)
                .HasForeignKey(d => d.PaymentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CafeOrder__Payme__33D4B598");

            entity.HasOne(d => d.Server).WithMany(p => p.CafeOrders)
                .HasForeignKey(d => d.ServerId)
                .HasConstraintName("FK__CafeOrder__Serve__32E0915F");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.CartItemId).HasName("PK__CartItem__488B0B2AA8C743D8");

            entity.ToTable("CartItem");

            entity.Property(e => e.CartItemId).HasColumnName("CartItemID");
            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.ItemPriceId)
                .HasDefaultValue(1)
                .HasColumnName("ItemPriceID");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UserSessionId).HasColumnName("UserSessionID");

            entity.HasOne(d => d.Item).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK_CartItem_Item");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A2B58977E74");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Item__727E83EB2ECBE843");

            entity.ToTable("Item");

            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.ItemDescription).HasMaxLength(255);
            entity.Property(e => e.ItemName).HasMaxLength(50);

            entity.HasOne(d => d.Category).WithMany(p => p.Items)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Item__CategoryID__267ABA7A");
        });

        modelBuilder.Entity<ItemPrice>(entity =>
        {
            entity.HasKey(e => e.ItemPriceId).HasName("PK__ItemPric__7E70A20251CE98EB");

            entity.ToTable("ItemPrice");

            entity.Property(e => e.ItemPriceId).HasColumnName("ItemPriceID");
            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.Price).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.TimeOfDayId).HasColumnName("TimeOfDayID");

            entity.HasOne(d => d.Item).WithMany(p => p.ItemPrices)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ItemPrice__ItemI__2B3F6F97");

            entity.HasOne(d => d.TimeOfDay).WithMany(p => p.ItemPrices)
                .HasForeignKey(d => d.TimeOfDayId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ItemPrice__TimeO__2C3393D0");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__OrderIte__57ED06A1750A3466");

            entity.ToTable("OrderItem");

            entity.Property(e => e.OrderItemId).HasColumnName("OrderItemID");
            entity.Property(e => e.ExtendedPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ItemPriceId).HasColumnName("ItemPriceID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");

            entity.HasOne(d => d.ItemPrice).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ItemPriceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__ItemP__37A5467C");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__Order__36B12243");
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.HasKey(e => e.PaymentTypeId).HasName("PK__PaymentT__BA430B15F4C199F8");

            entity.ToTable("PaymentType");

            entity.Property(e => e.PaymentTypeId).HasColumnName("PaymentTypeID");
            entity.Property(e => e.PaymentTypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Server>(entity =>
        {
            entity.HasKey(e => e.ServerId).HasName("PK__Server__C56AC886D65D2B11");

            entity.ToTable("Server");

            entity.Property(e => e.ServerId).HasColumnName("ServerID");
            entity.Property(e => e.FirstName).HasMaxLength(25);
            entity.Property(e => e.LastName).HasMaxLength(25);
        });

        modelBuilder.Entity<TimeOfDay>(entity =>
        {
            entity.HasKey(e => e.TimeOfDayId).HasName("PK__TimeOfDa__866813FF8533DBEB");

            entity.ToTable("TimeOfDay");

            entity.Property(e => e.TimeOfDayId).HasColumnName("TimeOfDayID");
            entity.Property(e => e.TimeOfDayName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
