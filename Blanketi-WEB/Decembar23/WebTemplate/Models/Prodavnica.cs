namespace WebTemplate.Models;

public class Prodavnica
{
    [Key]
    public int ID { get; set; }

    [MaxLength(100)]
    public required string Naziv { get; set; }
    [MaxLength(150)]
    public required string Lokacija { get; set; }
    
    [MaxLength(20)]
    public required string BrojTelefona { get; set; }

    public List<Proizvod>? ListaProizvoda { get; set; }
}