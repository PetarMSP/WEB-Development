namespace WebTemplate.Models;

public class KorisnikuSobi
{
    [Key]
    public int ID { get; set; }

    public required int KorsinikID { get; set; }
    [ForeignKey("KorisnikFK")]
    public Korisnik? korisnikSobe { get; set; }
    
    public required int SobaID { get; set; }
    [ForeignKey("SobaFK")]
    public Soba? sobaKorisnika { get; set; }
    
    [MaxLength(15)]
    public required string Nadimak { get; set; }

    public required string Boja { get; set; }
}