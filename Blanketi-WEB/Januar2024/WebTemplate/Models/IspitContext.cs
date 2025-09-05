namespace WebTemplate.Models;

public class IspitContext : DbContext
{
    // DbSet kolekcije!
    public DbSet<Korisnik> Korisnici { get; set; }

    public DbSet<Soba> Sobe { get; set; }

    public DbSet<KorisnikuSobi> KorisniciUSobama { get; set; }

    public IspitContext(DbContextOptions options) : base(options)
    {
        
    }
}
