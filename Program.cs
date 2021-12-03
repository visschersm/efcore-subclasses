using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");


public class ProductContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Products;Trusted_Connection=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(properties => properties.Id);
            entity.OwnsOne(properties => properties.Limits, b =>
            {
                b.Property(e => e.MinWeight);
                b.Property(e => e.MaxWeight);
            });
        });
    }

    DbSet<Product> Products => Set<Product>();

}

public class PhysicalLimits
{
    public int MinWeight { get; private set; } = 0;
    public int MaxWeight { get; private set; } = 2000;

    public PhysicalLimits(int minWeight, int maxWeight) => (MinWeight, MaxWeight) = (minWeight, maxWeight);
}

public class Product
{
    public int Id { get; private set; }
    public PhysicalLimits Limits { get; private set; }

    public Product()
    {

    }

    public Product(int id, PhysicalLimits limits)
    {
        Id = id;
        Limits = limits;
    }
}
