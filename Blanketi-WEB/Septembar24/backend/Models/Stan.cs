namespace backend.Models;


public class Stan
{
    [Key]

    public int ID { get; set; }
    [MaxLength]
    public required string ImeVlasnika { get; set; }

    public required uint Povrsina { get; set; }

    public required uint BrojClanova { get; set; }

    public List<Racun>? RacuniZaStan { get; set; }


}