using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using art_gallery_api.Models;

namespace art_gallery_api.Persistence
{
    public partial class GalleryContext : DbContext
    {
        public virtual DbSet<Artifact> Artifacts { get; set; } = null!;
        public virtual DbSet<Exhibition> Exhibitions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        public GalleryContext()
        {
        }

        public GalleryContext(DbContextOptions<GalleryContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=artgallery;Username=postgres;Password=");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artifact>(entity =>
            {
                entity.ToTable("artifacts");

                entity.Property(e => e.ArtifactId)
                    .HasColumnName("artifact_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.AddedDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("added_date");

                entity.Property(e => e.ArtifactTitle)
                    .HasMaxLength(100)
                    .HasColumnName("artifact_title");

                entity.Property(e => e.ArtistId)
                    .HasMaxLength(10)
                    .HasColumnName("artist_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(800)
                    .HasColumnName("description");

                entity.Property(e => e.IsInGallery).HasColumnName("is_in_gallery");

                entity.Property(e => e.StyleId)
                    .HasMaxLength(10)
                    .HasColumnName("style_id");
            });

            modelBuilder.Entity<Exhibition>(entity =>
            {
                entity.ToTable("exhibitions");

                entity.Property(e => e.ExhibitionId)
                    .HasColumnName("exhibition_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Description)
                    .HasMaxLength(800)
                    .HasColumnName("description");

                entity.Property(e => e.ExhibitionDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("exhibition_date");

                entity.Property(e => e.ExhibitionTitle)
                    .HasMaxLength(100)
                    .HasColumnName("exhibition_title");

                entity.Property(e => e.ExpectedCrowd).HasColumnName("expected_crowd");

                entity.Property(e => e.FeaturingArtStyle)
                    .HasMaxLength(6)
                    .HasColumnName("featuring_art_style");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Createddate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("createddate");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .HasColumnName("firstname");

                entity.Property(e => e.HaveMembership).HasColumnName("have_membership");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .HasColumnName("lastname");

                entity.Property(e => e.Modifieddate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("modifieddate");

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .HasColumnName("role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
