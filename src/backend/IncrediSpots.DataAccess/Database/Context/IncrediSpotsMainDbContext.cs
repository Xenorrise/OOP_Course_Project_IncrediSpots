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
	}
	
}
