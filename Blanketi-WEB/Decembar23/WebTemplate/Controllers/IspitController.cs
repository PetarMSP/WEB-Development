namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class IspitController : ControllerBase
{
    public IspitContext Context { get; set; }

    public IspitController(IspitContext context)
    {
        Context = context;
    }
    
    [HttpPost("DodajProdavnicu")]

    public async Task<ActionResult> DodajProdavnicu([FromBody]Prodavnica prodavnica)
    {
        try
        {
            await Context.Prodavnice.AddAsync(prodavnica);
            await Context.SaveChangesAsync();
    
            return Ok($"Uspesno je dodata prodavnica:{prodavnica.Naziv} sa ID-em:{prodavnica.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajProizvod/{prodavnicaID}/{naziv}/{kategorija}/{cena}/{kolicina}")]

    public async Task<ActionResult> DodajProizvod(int prodavnicaID,string naziv,string kategorija,double cena,int kolicina)
    {
        try
        {
            var Prodavnica = await Context.Prodavnice.FindAsync(prodavnicaID);
            if(Prodavnica == null)
            {
                return BadRequest("Prodavnica ne postoji");
            }
            var novi = new Proizvod {
                Naziv = naziv,
                Kategorija = kategorija,
                Cena = cena,
                Kolicina = kolicina,
                prodavnica = Prodavnica,
            };
            await Context.Proizvodi.AddAsync(novi);
            await Context.SaveChangesAsync();
            
            var proizvod = await Context.Proizvodi.Where(p => p.ID == novi.ID).Select(pr => new{
                ID = pr.ID,
                Naziv = pr.Naziv,
                Kolicina = pr.Kolicina 
            }).ToListAsync();

            return Ok(proizvod);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("ProdajProizvod/{proizvodID}/{kolicina}")]

    public async Task<ActionResult> ProdajProzivod(int proizvodID,int kolicina)
    {
        try
        {
            var proizvod = await Context.Proizvodi.FindAsync(proizvodID);

            if(proizvod == null)
            {
                return BadRequest("Proizvod ne postoji!");
            }
            if(proizvod.Kolicina < kolicina){
                return BadRequest("Nema dovoljno proizvoda!");
            }

            proizvod.Kolicina -= kolicina;
            await Context.SaveChangesAsync();

            return Ok($"Prodato je {kolicina} proizvoda.");
            
        }
        catch (Exception e)
        {
           return BadRequest(e.Message);
        }
    }
    
    [HttpGet("VratiSveProdavnice")]

    public async Task<ActionResult> VratiSveProdavnice()
    {
        try
        {
            var prodavnice = await Context.Prodavnice.Select(pr => new{
                ID = pr.ID,
                Naziv = pr.Naziv,
                ListaProizovda = pr.ListaProizvoda!.Select(lp => new{
                   ID = lp.ID,
                   Naziv = lp.Naziv,
                   Kolicina = lp.Kolicina                
                }).ToList()

            }).ToListAsync();

            if(prodavnice == null){
                return BadRequest("Nema prodavnica!");
            }

            return Ok(prodavnice);
        }
        catch (Exception e)
        {
           return BadRequest(e.Message);
        }
    }
}
