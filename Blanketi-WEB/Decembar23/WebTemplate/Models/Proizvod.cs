namespace WebTemplate.Models;

public class Proizvod
{
    [Key]
    public int ID { get; set; }

    [MaxLength(100)]
    public required string Naziv { get; set; }
    [MaxLength(15)]
    public required string Kategorija { get; set; }

    [Range(0,double.MaxValue)]
    public double Cena { get; set; }
    
    [Range(0,100)]
    public int Kolicina { get; set; }

    public Prodavnica? prodavnica { get; set; }
}