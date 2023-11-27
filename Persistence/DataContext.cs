namespace Persistence;

public class DataContext : IdentityDbContext<AppUser>
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<BookDetail> Books { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public DbSet<BookCopies> BookCopies { get; set; }
    public DbSet<BookCopiesHistory> BookCopiesHistory { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Series> Series { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<BookCopies>(b =>
        {
            b.HasKey(k => new { k.BookDetailId, k.LocationId });

            b.HasOne(o => o.BookDetail)
            .WithMany(p => p.BookCopies)
            .HasForeignKey(o => o.BookDetailId)
            .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(o => o.Location)
            .WithMany(p => p.BookCopies)
            .HasForeignKey(o => o.LocationId)
            .OnDelete(DeleteBehavior.Cascade);

        });

        builder.Entity<BookCopiesHistory>().Property(e => e.ActionType).HasConversion<string>();
    }

}
