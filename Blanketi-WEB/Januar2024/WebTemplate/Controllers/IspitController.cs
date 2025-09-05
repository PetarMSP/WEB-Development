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
    
    [HttpPost("DodajKorinsika")]

    public async Task<ActionResult> DodajKorisinika([FromBody]Korisnik korisnik){
       try
       {
           await Context.Korisnici.AddAsync(korisnik);
           await Context.SaveChangesAsync();
           return Ok($"Uspesno je dodat korisnik:{korisnik.ImePrezime} sa ID-em:{korisnik.ID}");
       }
       catch (Exception e)
       {
         return BadRequest(e.Message);
       }
    }

    [HttpPost("DodajSobu")]

    public async Task<ActionResult> DodajSobu([FromBody]Soba soba){
       try
       {
          await Context.Sobe.AddAsync(soba);
          await Context.SaveChangesAsync();
          return Ok($"Uspesno je dodata soba sa ID-em:{soba.ID}");
        
       }
       catch (Exception e)
       {
        return BadRequest(e.Message);
       }
    }

    [HttpPost("DodajKorisnikaUSobu/{korisnickoIme}/{ImeSobe}/{nadimak}/{boja}")]
    
    public async Task<ActionResult> DodajKorisnikaUSobu(string korisnickoIme,string ImeSobe,string nadimak,string boja){
      try
      {
         var soba = await Context.Sobe.Include(s => s.korisniciSobe)
                                      .SingleOrDefaultAsync(s => s.Ime == ImeSobe);
         if(soba == null)
         {
            soba = new Soba{
               Ime = ImeSobe,
               MaxBrojClanova = 5,
               korisniciSobe = new List<KorisnikuSobi>()
            };
            await Context.Sobe.AddAsync(soba);
            await Context.SaveChangesAsync();
         }
         if(soba.korisniciSobe!.Count >= soba.MaxBrojClanova)
         {
            return BadRequest("Kapacitet sobe je dostignut");
         }
         var korisnik = await Context.Korisnici.SingleOrDefaultAsync(k => k.KorisnickoIme == korisnickoIme);
         if(korisnik == null)
         {
            return BadRequest("Korisnik ne postoji!!");
         }
         var korisnikUSobi = await Context.KorisniciUSobama.SingleOrDefaultAsync(ks => ks.KorsinikID == korisnik.ID && ks.SobaID == soba.ID);
         if(korisnikUSobi != null){
            return BadRequest("Korisnik je vec u sobi");
         }
         if (nadimak.Length > 15)
        {
            return BadRequest("Nadimak ne sme biti duzi od 15 karaktera.");
        }
        KorisnikuSobi novi = new KorisnikuSobi{
             KorsinikID = korisnik.ID,
             SobaID = soba.ID,
             sobaKorisnika = soba,
             korisnikSobe = korisnik,
             Nadimak = nadimak,
             Boja = boja
         };               
         await Context.KorisniciUSobama.AddAsync(novi);
         await Context.SaveChangesAsync();

         soba = await Context.Sobe.Include(s => s.korisniciSobe!)
                                  .ThenInclude(ks => ks.korisnikSobe)
                                  .SingleOrDefaultAsync(s => s.ID == soba.ID);
         var novaSoba = new {
            ID = soba!.ID,
            Ime = soba.Ime,
            Clanovi = soba.korisniciSobe!.Select(ks => new{
               Nadimak = ks.Nadimak,
               korisnickoIme = ks.korisnikSobe!.KorisnickoIme,
               Boja = ks.Boja
            }).ToList()
         };
        return Ok(novaSoba);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("VratiSveSobe")]

    public async Task<ActionResult> VratiSveSobe(){
      try
      {
         var SobeSaClanovima = await Context.Sobe.Select(s => new{
            ID = s.ID,
            Ime = s.Ime,
            Clanovi = s.korisniciSobe!.Select(ks => new{
              Nadimak = ks.Nadimak,
              korisnickoIme = ks.korisnikSobe!.KorisnickoIme,
              Boja = ks.Boja,
            }).ToList()

         }).ToListAsync();
         return Ok(SobeSaClanovima);
         
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("VratiKorisnike")]

    public async Task<ActionResult> VratiKorisnike()
    {
        try
        {
            var korisnici = await Context.Korisnici.Select(k => new {
                ID = k.ID,
                ImePrezime = k.ImePrezime,
                KorisnickoIme = k.KorisnickoIme,
                Email = k.Email,
                Sifra = k.Sifra               
            }).ToListAsync();

            return Ok(korisnici);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("PreborjiJedinstvene/{sobaID}")]

    public async Task<ActionResult> Prebroji(int sobaID)
    {
        try
        {
            var soba = await Context.Sobe
                             .Include(s => s.korisniciSobe!)
                             .ThenInclude(ks => ks.korisnikSobe)
                             .SingleOrDefaultAsync(s => s.ID == sobaID);
            if(soba == null){
                return BadRequest("Soba ne postoji!");
            }
            int jedinstveni = soba.korisniciSobe!
                                       .Where(ks => !Context.KorisniciUSobama
                                       .Any(ksu => ksu.KorsinikID == ks.KorsinikID && ksu.SobaID != soba.ID)).Count();

            return Ok(jedinstveni);
        }
        catch (Exception e)
        {
           return BadRequest(e.Message);
        }
    }
}
