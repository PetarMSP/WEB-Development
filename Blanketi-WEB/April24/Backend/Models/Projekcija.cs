namespace WebTemplate.Models;

public class Projekcija
{
    [Key]
    public int ID { get; set; }

    [MaxLength(70)]
    public required string Naziv { get; set; }

    public required DateTime VremePrikazivanja { get; set; }

    public Sala? sala { get; set; }

    public required string Sifra { get; set; }

    public required uint OsnovnaCenaKarte { get; set; }

    public List<Karta>? Karte { get; set; }


}