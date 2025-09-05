using backend.Models;

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
    
    [HttpPost("DodajStan")]
    public async Task<ActionResult> DodajStan([FromBody]Stan stan)
    {
        try
        {
            await Context.Stanovi.AddAsync(stan);
            await Context.SaveChangesAsync();
            return Ok($"Uspesno dodat stan sa IDem:{stan.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
   [HttpPost("DodajRacun/{StanID}/{mesec}/{voda}/{placen}")]
   public async Task<ActionResult> DodajRacun(int StanID,uint mesec,string placen,int voda)
   {
        try
        {
            var stan = await Context.Stanovi.FindAsync(StanID);
            if(stan != null){
                var racun =  new Racun {
                    MesecIzdavanja = mesec,
                    Placen = placen,
                    Stan = stan,
                    Voda = voda,
                    Struja =  150*(int)(stan.BrojClanova),
                    KomunalneUsluge = 100 * (int)(stan.BrojClanova)

                };
                await Context.Racuni.AddAsync(racun);
                await Context.SaveChangesAsync();
                return Ok($"Racun je uspesno dodat stanu {stan.ID}.");
            }
            else
            {
                return BadRequest("Stan nije pronadjen!");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
   }

    [HttpGet("InformacijeOStanu/{StanID}")]
    public async Task<ActionResult> VratiInfoStana(int StanID)
    {
        try
        {
            var stan = await Context.Stanovi.FindAsync(StanID);
            return Ok(stan);
        }
        catch (Exception e)
        {
        return BadRequest(e.Message);
        }
    }
    
    [HttpGet("VratiSveID")]

    public async Task<ActionResult> VratiSveID()
    {
        try
        {
            var stanovi = await Context.Stanovi.Select(p => new
            {
                ID = p.ID

            }).ToListAsync();
            return Ok(stanovi);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("RacunizaStan/{StanID}")]
    public async Task<ActionResult> RacunizaStan(int StanID)
    {
        try
        {
            var racuni = await Context.Racuni.Include(p => p.Stan)
                         .Where(p => p.Stan!.ID == StanID)
                         .Select(p => new {
                             ID = p.ID,
                             Mesec = p.MesecIzdavanja,
                             Voda = p.Voda,
                             Struja = p.Struja,
                             KomunalneUsluge = p.KomunalneUsluge,
                             Placen = p.Placen
                         }).ToListAsync();
            return Ok(racuni);
        }
        catch (Exception e)
        {           
            return BadRequest(e.Message);
        }
    }
  
    [HttpGet("Troskovi/{StanID}")]
    public async Task<ActionResult> Troskovi(int StanID)
    {
        try
        {
            var troskovi = await Context.Racuni.Include(p => p.Stan)
                               .Where(p => p.Stan!.ID == StanID && p.Placen == "Ne")
                               .Select(p => new {
                                  Voda = p.Voda,
                                  Struja = p.Struja,
                                  Komunalije = p.KomunalneUsluge
                               }).ToListAsync();
            var ukupno = 0;
            foreach(var racun in troskovi)
            {
               ukupno += racun.Struja + racun.Voda + racun.Komunalije;
            }
            return Ok(ukupno);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}

