namespace WebTemplate.Models;

public class Korisnik
{
    [Key]

    public int ID { get; set; }
    
    [MaxLength(30)]
    public required string KorisnickoIme { get; set; }
    
    [MaxLength(70)]
    public required string ImePrezime { get; set; }
    
    public required string Email { get; set; }

    public required string Sifra { get; set; }
     
    public List<KorisnikuSobi>? SobekojeKoristi { get; set; }

}