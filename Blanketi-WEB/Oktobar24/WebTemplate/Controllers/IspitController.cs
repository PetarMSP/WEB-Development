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
    
    [HttpPost("DodajGrupu")]

    public async Task<ActionResult> DodajGrupu([FromBody]Grupa grupa)
    {
        try
        {
            await Context.Grupe.AddAsync(grupa);
            await Context.SaveChangesAsync();

            return Ok($"Uspesno dodata grupa sa ID-em{grupa.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("UpisiDete/{imePrezime}/{jmbg}/{imeroditelja}")]

    public async Task<ActionResult> UpisiDete(string imePrezime,string imeroditelja,string jmbg)
    {
        try
        { 

            var grupa1 = await Context.Grupe.SingleOrDefaultAsync(s => s.Naziv == "Leptiric");
            var grupa2 = await Context.Grupe.SingleOrDefaultAsync(s => s.Naziv == "Pcelica");
            var grupa3 = await Context.Grupe.SingleOrDefaultAsync(s => s.Naziv == "Pecurka");
            
            
            if (grupa1 == null || grupa2 == null || grupa3 == null)
            {
                return BadRequest("Jedna ili više grupa ne postoji!");
            }
            
            
            var grupe = new List<Grupa> { grupa1, grupa2, grupa3 };
            grupe = grupe.OrderBy(g => g.BrojUpisaneDece).ThenBy(g => g.Naziv).ToList();

            
            var dodeljenaGrupa = grupe.First();
        
            dodeljenaGrupa.BrojUpisaneDece += 1;
        
            var novoDete = new Dete
            {
                ImePrezime = imePrezime,
                ImeRoditelja = imeroditelja,
                JMBG = jmbg,
                grupa = dodeljenaGrupa,
           };

          await Context.Deca.AddAsync(novoDete);
          await Context.SaveChangesAsync();
          
          var deteID = await Context.Deca.FindAsync(novoDete.ID);
          if(deteID == null){
                return BadRequest("Dete ne postoji");
          }

          var dete = new {
             grupaID = dodeljenaGrupa.ID,
             id = deteID.ID,
             ImePrezime = imePrezime,
             ImeRoditelja = imeroditelja,
             JMBG = jmbg,
          };
        
          return Ok(dete);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("VratiSveGrupe")]

    public async Task<ActionResult> VratiSveGrupe()
    {
        try
        {
            var grupe = await Context.Grupe.Select(g => new{
               ID = g.ID,
               imeGrupe = g.Naziv,
               boja = g.Boja,
               vaspitac = g.Vaspitac,
               brojDece = g.BrojUpisaneDece,
               Deca = g.Deca!.Select(d => new{
                   ID = d.ID,
                   imePrezime = d.ImePrezime,
                   imeRoditelja = d.ImeRoditelja,
                   jmbg = d.JMBG,
                }).ToList()
            }).ToListAsync();

            return Ok(grupe);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
