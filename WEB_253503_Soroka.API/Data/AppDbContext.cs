using Microsoft.EntityFrameworkCore;
using WEB_253503_Soroka.Domain.Entities;

namespace WEB_253503_Soroka.API.Data;

public class AppDbContext : DbContext
{
    public DbSet<Show> Shows { get; set; }
    public DbSet<Genre> Genres { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
    {
        // Database.EnsureDeleted();
        // Database.EnsureCreated();

        
    }
}
