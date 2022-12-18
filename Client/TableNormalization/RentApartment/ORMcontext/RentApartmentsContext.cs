using Microsoft.EntityFrameworkCore;

namespace Client.TableNormalization.RentApartment.ORMcontext;

public sealed class RentApartmentsContext : DbContext
{
    private readonly string _pgsqlConnectionString;

    public DbSet<RentApartment> Apartments => Set<RentApartment>();
    public DbSet<Flats> Flats => Set<Flats>();
    public DbSet<FlatCategories> FlatCategories => Set<FlatCategories>();
    public DbSet<Clients> Clients => Set<Clients>();
    public DbSet<Contracts> Contracts => Set<Contracts>();
    
    
    public RentApartmentsContext()
    {
        _pgsqlConnectionString =
            $"User ID = postgres; " +
            $"Password = postgres; " +
            $"Host = localhost; " +
            $"Port = 5434; " +
            $"CommandTimeout=180;" +
            $"Pooling = true; " +
            $"Database = Normalized3NFDatabase";
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_pgsqlConnectionString);
    }
}