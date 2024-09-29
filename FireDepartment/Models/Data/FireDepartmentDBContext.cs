using System;
using System.Collections.Generic;
using FireDepartment.Models;
using Microsoft.EntityFrameworkCore;

namespace FireDepartment.Models.Data;

public partial class FireDepartmentDBContext : DbContext
{
    public FireDepartmentDBContext(DbContextOptions<FireDepartmentDBContext> options) : base(options)
    {
    }

    public virtual DbSet<Call> Call { get; set; }

    public virtual DbSet<CallOborudovaniye> CallOborudovaniye { get; set; }

    public virtual DbSet<Inventory> Inventorie { get; set; }

    public virtual DbSet<Oborudovaniye> Oborudovaniye { get; set; }

    public virtual DbSet<PreventionEvent> PreventionEvent { get; set; }

    public virtual DbSet<Sotrudniki> Sotrudniki { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-ID583BI\\SQLEXPRESS;Database=ProtivopozharnayaSluzhba;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False",
            x => x.UseDateOnlyTimeOnly());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Call>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_CallID");

            entity.ToTable("Call");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Category).HasMaxLength(20);
            entity.Property(e => e.DateTimeCall).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(60);
            entity.Property(e => e.Location).HasMaxLength(30);
            entity.Property(e => e.SotrudnikId).HasColumnName("SotrudnikID");

            entity.HasOne(d => d.Sotrudnik).WithMany(p => p.Calls)
                .HasForeignKey(d => d.SotrudnikId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Call_Sotrudniki");
        });

        modelBuilder.Entity<CallOborudovaniye>(entity =>
        {
            entity.HasIndex(e => e.OborudovaniyeId, "IX_CallOborudovaniye_OborudovaniyeID");

            entity.HasKey(e => new { e.CallId, e.OborudovaniyeId });

            entity.ToTable("CallOborudovaniye");

            entity.Property(e => e.CallId).HasColumnName("CallID");
            entity.Property(e => e.OborudovaniyeId).HasColumnName("OborudovaniyeID");

            entity.HasOne(d => d.Call).WithMany(p => p.CallOborudovaniye)
                .HasForeignKey(d => d.CallId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CallOborudovaniye_Call");

            entity.HasOne(d => d.Oborudovaniye).WithMany(p => p.CallOborudovaniye)
                .HasForeignKey(d => d.OborudovaniyeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CallOborudovaniye_Oborudovaniye");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_InventoryID");

            entity.ToTable("Inventory");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Location).HasMaxLength(20);
            entity.Property(e => e.OborudovaniyeId).HasColumnName("OborudovaniyeID");

            entity.HasOne(d => d.Oborudovaniye).WithMany(p => p.Inventory)
                .HasForeignKey(d => d.OborudovaniyeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inventory_Oborudovaniye");
        });

        modelBuilder.Entity<Oborudovaniye>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_OborudovaniyeID");

            entity.ToTable("Oborudovaniye");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateTimeOfService).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Type).HasMaxLength(20);
        });

        modelBuilder.Entity<PreventionEvent>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_PreventionEventID");

            entity.ToTable("PreventionEvent");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.Goal).HasMaxLength(40);
            entity.Property(e => e.Location).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(55);
            entity.Property(e => e.SotrudnikId).HasColumnName("SotrudnikID");

            entity.HasOne(d => d.Sotrudnik).WithMany(p => p.PreventionEvents)
                .HasForeignKey(d => d.SotrudnikId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PreventionEvent_Sotrudniki");
        });

        modelBuilder.Entity<Sotrudniki>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_SotrudnikiID");

            entity.ToTable("Sotrudniki");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateOfReceipt).HasColumnType("datetime");
            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.LastName).HasMaxLength(30);
            entity.Property(e => e.Mail).HasMaxLength(30);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("Phone_number");
            entity.Property(e => e.Rank).HasMaxLength(20);
            entity.Property(e => e.Specialization).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}