namespace WebTemplate.Models;

public class Grupa
{
    [Key]
    public int ID { get; set; }
    public required string Naziv { get; set; }
    public required string Boja { get; set; }
    public required string Vaspitac { get; set; }
    public int BrojUpisaneDece { get; set; }

    public List<Dete>? Deca { get; set; }
}