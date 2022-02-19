using System;
using System.Collections.Generic;
using CTA.BlazorWasm.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CTA.BlazorWasm.Shared.Context
{
    public partial class CtaContext : DbContext
    {
        public CtaContext()
        {
        }

        public CtaContext(DbContextOptions<CtaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CorrespondenceSubType> CorrespondenceSubTypes { get; set; } = null!;
        public virtual DbSet<CorrespondenceType> CorrespondenceTypes { get; set; } = null!;
        public virtual DbSet<Poc> Pocs { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<ToFrom> ToFroms { get; set; } = null!;
        public virtual DbSet<Tracking> Trackings { get; set; } = null!;
        public virtual DbSet<TrackingThread> TrackingThreads { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=beast;database=contractsdatabase;trusted_connection=true;");
                //optionsBuilder.UseSqlServer("Server=F5010-9Y7HZH3-L;database=contractsdatabase;trusted_connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CorrespondenceSubType>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CorrespondenceType>(entity =>
            {
                entity.HasIndex(e => e.CorrespondenceSubTypeId, "IX_CorrespondenceTypes_CorrespondenceSubTypeId");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.CorrespondenceSubType)
                    .WithMany(p => p.CorrespondenceTypes)
                    .HasForeignKey(d => d.CorrespondenceSubTypeId)
                    .HasConstraintName("FK_cor_type_cor_sub");
            });

            modelBuilder.Entity<Poc>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ToFrom>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tracking>(entity =>
            {
                entity.Property(e => e.Comments).IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DocumentPath)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.SentOrReceived).HasColumnType("datetime");

                entity.Property(e => e.Subject)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.CorrespondenceType)
                    .WithMany(p => p.Trackings)
                    .HasForeignKey(d => d.CorrespondenceTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trackings_CorrespondenceTypes");

                entity.HasOne(d => d.Poc)
                    .WithMany(p => p.Trackings)
                    .HasForeignKey(d => d.PocId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trackings_Pocs");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Trackings)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trackings_Statuses");

                entity.HasOne(d => d.Thread)
                    .WithMany(p => p.Trackings)
                    .HasForeignKey(d => d.ThreadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trackings_TrackingThreads");

                entity.HasOne(d => d.ToFrom)
                    .WithMany(p => p.Trackings)
                    .HasForeignKey(d => d.ToFromId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trackings_ToFroms");
            });

            modelBuilder.Entity<TrackingThread>(entity =>
            {
                entity.HasIndex(e => e.ProjectId, "IX_TrackingThreads_ProjectId");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.TrackingThreads)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_tracking_thread_project");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
