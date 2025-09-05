namespace WebTemplate.Models;

public class Soba
{
    [Key]
    public int ID { get; set; }
    
    [MaxLength(30)]
    public required string Ime { get; set; }

    public uint MaxBrojClanova { get; set; }

    public List<KorisnikuSobi>? korisniciSobe { get; set; }
}