using Microsoft.EntityFrameworkCore;

namespace Server.Models;

public sealed class ApplicationContext  : DbContext
{
    private const string SqliteSource = "Data Source=NotNormalizedDB.db";
    
    public DbSet<RentApartment> Apartments => Set<RentApartment>();

    public ApplicationContext()
    {
        Database.EnsureCreated();  
    } 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(SqliteSource);
    }
}