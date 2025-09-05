namespace WebTemplate.Models;

public class Karta
{
    [Key]

    public int ID { get; set; }

    [Range(0, double.MaxValue)]

    public bool Rezervisana { get; set; } = false;
    public required uint BrojSedista { get; set; }

    public required uint Red { get; set; }

    public Projekcija? projekcija { get; set; }
}