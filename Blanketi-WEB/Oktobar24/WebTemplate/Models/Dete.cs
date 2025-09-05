namespace WebTemplate.Models;

public class Dete
{
    [Key]
    public int ID { get; set; }
    
    [MaxLength(80)]
    public required string ImePrezime { get; set; }
     
    [MaxLength(50)]
    public required string ImeRoditelja { get; set; }
    
    [MaxLength(13)]
    public required  string JMBG { get; set; }

    public Grupa? grupa { get; set; }
}