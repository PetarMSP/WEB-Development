namespace WebTemplate.Models;

public class IspitContext : DbContext
{
    // DbSet kolekcije!
    public DbSet<Dete> Deca { get; set; }

    public DbSet<Grupa> Grupe { get; set; }
    
    public IspitContext(DbContextOptions options) : base(options)
    {
        
    }
}
