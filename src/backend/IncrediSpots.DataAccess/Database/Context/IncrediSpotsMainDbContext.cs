using Microsoft.EntityFrameworkCore;
using IncrediSpots.DataAccess.Entities;
using IncrediSpots.Domain.Models;

namespace IncrediSpots.DataAccess.Context;

public class IncrediSpotsMainDbContext : DbContext
{
	public IncrediSpotsMainDbContext() {}
	public IncrediSpotsMainDbContext(DbContextOptions<IncrediSpotsMainDbContext> options)
        : base(options)
    {
        //Database.EnsureCreated();
    }

	public virtual DbSet<SpotModel> Spots { get; set; }

	public virtual DbSet<SpotCategoryModel> SpotCategories { get; set; }

	public virtual DbSet<CommentModel> Comments { get; set; }

	public virtual DbSet<UserModel> Users { get; set; }


	//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseNpgsql("server=localhost;database=incredispots_db_dev;Username=incredispots_root;password=12345");

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<SpotModel>(entity => 
		{
			entity.HasKey(e => e.Id);

			entity.ToTable("spots");

			entity.HasIndex(e => e.CategoryId, "category_id");
			entity.HasIndex(e => e.UserId, "user_id");

			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.CategoryId).HasColumnName("category_id");
			entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
			entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
			entity.Property(e => e.Latitude).HasColumnName("latitude");
			entity.Property(e => e.Longitude).HasColumnName("longitude");
			entity.Property(e => e.CreatedAt).HasColumnName("created_at");
			entity.Property(e => e.Rating).HasColumnName("rating");
			entity.Property(e => e.UserId).HasColumnName("user_id");
		});

		modelBuilder.Entity<SpotCategoryModel>(entity => 
		{
			entity.HasKey(e => e.Id);

			entity.ToTable("spot_categories");

			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.Name)
				.HasMaxLength(255)
                .HasColumnName("Name");
			entity.Property(e => e.Emoji)
                .HasMaxLength(255)
                .HasColumnName("Emoji");
		});
		modelBuilder.Entity<UserModel>(entity =>
		{
			entity.ToTable("users");

			entity.HasKey(u => u.Id);

			entity.HasIndex(u => u.Email).IsUnique();

			entity.Property(u => u.Email)
				.IsRequired()
				.HasMaxLength(255);

			entity.Property(u => u.PasswordHash)
				.IsRequired();

		});
		modelBuilder.Entity<CommentModel>(entity =>
		{
			entity.HasKey(c => c.Id);

			entity.Property(c => c.Text)
				.IsRequired()
				.HasMaxLength(1000);

			entity.Property<DateTime>("CreatedAt")
				.HasDefaultValueSql("CURRENT_TIMESTAMP");

			entity.Property<int>("SpotId");
			entity.Property<int>("AuthorId");

			entity.HasOne(c => c.Spot)
              .WithMany(s => s.Comments)
              .HasForeignKey(c => c.SpotId)
              .OnDelete(DeleteBehavior.Cascade);

			entity.HasOne(c => c.Author)
				.WithMany(u => u.Comments)
				.HasForeignKey(c => c.AuthorId)
				.OnDelete(DeleteBehavior.Cascade);

		});

	}
	
}
