using Microsoft.EntityFrameworkCore;

namespace NewsFetcherAPI.Data
{
    public class NfDbContext: DbContext
    {
        public NfDbContext(DbContextOptions<NfDbContext> options): base(options)
        { }
        
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("Category");

            var category = modelBuilder.Entity<Category>();

            category.HasKey(x => x.Id);
            category.Property(x => x.Name).IsRequired();
            category.Property(x => x.Value).IsRequired();
        }
    }
}