namespace WebTemplate.Models;

public class Sala
{
    [Key]

    public int ID { get; set; }

    [MaxLength(30)]
    public required string Naziv { get; set; }

    public List<Projekcija>? projekcije { get; set; }

}