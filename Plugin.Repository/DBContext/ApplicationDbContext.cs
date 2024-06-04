using Microsoft.EntityFrameworkCore;

namespace Plugin.Repository.DbContexts
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                #warning Move to appsettings.json
                optionsBuilder.UseSqlServer("Server=.;Database=ImageManager;Trusted_Connection=True;");
            }
        }

        public virtual DbSet<Effect> Effects { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<ImageEffect> ImageEffects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Effect>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Path).IsRequired();
            });

            modelBuilder.Entity<ImageEffect>(entity =>
            {
                entity.HasOne(d => d.Effect)
                    .WithMany(p => p.ImageEffects)
                    .HasForeignKey(d => d.EffectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImageEffects_Effects");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.ImageEffects)
                    .HasForeignKey(d => d.ImageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImageEffects_Images");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
