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

    [HttpPost("DodajSalu/{naziv}")]
    public async Task<ActionResult> DodajSalu(string naziv)
    {
        try
        {
            var sala = new Sala
            {
                Naziv = naziv,
                projekcije = new List<Projekcija>()
            };
            await Context.Sale.AddAsync(sala);
            await Context.SaveChangesAsync();
            return Ok($"Uspesno je dodata sala:{sala.Naziv} sa ID-jem:{sala.ID}.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajProjekciju/{naziv}/{vremePrikazivanja}/{sifra}/{SalaID}/{osnovnaCenaKarte}")]

    public async Task<ActionResult> DodajProjekciju(string naziv, DateTime vremePrikazivanja, string sifra, uint SalaID, uint osnovnaCenaKarte)
    {
        try
        {
            var Sala = Context.Sale.FirstOrDefault(s => s.ID == SalaID);
            if (Sala == null)
            {
                return BadRequest("Nema sale u kojoj zelite da emitujete projekciju");
            }

            var NovaProjekcija = new Projekcija
            {
                Naziv = naziv,
                VremePrikazivanja = vremePrikazivanja,
                Sifra = sifra,
                sala = Sala,
                OsnovnaCenaKarte = osnovnaCenaKarte,
                Karte = []
            };

            await Context.Projekcije.AddAsync(NovaProjekcija);
            await Context.SaveChangesAsync();
            return Ok($"Uspesno je dodata projekcija:{NovaProjekcija.Naziv} sa ID-jem:{NovaProjekcija.ID}.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajKartu/{red}/{brojSedista}/{NazivProjekcije}/{DatumProjekcije}")]

    public async Task<ActionResult> DodajKartu(uint red, uint brojSedista, string NazivProjekcije, DateTime DatumProjekcije)
    {
        try
        {
            var projekcija = await Context.Projekcije.SingleOrDefaultAsync(pr => pr.Naziv == NazivProjekcije && pr.VremePrikazivanja == DatumProjekcije);

            if (projekcija == null)
            {
                return BadRequest("Nema projekcije za koju zelite da napravite kartu");
            }

            var novaKarta = new Karta
            {
                BrojSedista = brojSedista,
                Red = red,
                projekcija = projekcija,
            };

            await Context.Karte.AddAsync(novaKarta);
            await Context.SaveChangesAsync();

            return Ok($"Uspeno je dodata nova krata za projekciju:{projekcija.Naziv} u redu:{red}, sa brojem sedista:{brojSedista}.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("RezervisiKartu/{red}/{brojSedista}/{IDProjekcije}")]

    public async Task<ActionResult> RezervisiKratu(uint red, uint brojSedista, uint IDProjekcije)
    {
        try
        {
            var projekcija = await Context.Projekcije
            .Include(p => p.Karte)
            .SingleOrDefaultAsync(pr => pr.ID == IDProjekcije);

            if (projekcija == null)
            {
                return BadRequest("Nema projekcije za koju zelite da rezervisete kartu");
            }

            var karta = projekcija.Karte?.SingleOrDefault(k => k.Red == red && k.BrojSedista == brojSedista);

            if (karta == null)
            {
                return BadRequest("Karta ne postoji");
            }


            karta.Rezervisana = true;

            await Context.SaveChangesAsync();

            return Ok("Karta je uspešno rezervisana");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpGet("VratiPodatkeKarte/{red}/{brojSedista}/{IDProjekcije}")]

    public async Task<ActionResult> VratiPodatkeKarte(uint red, uint brojSedista, uint IDProjekcije)
    {
        var projekcija = await Context.Projekcije.SingleOrDefaultAsync(pr => pr.ID == IDProjekcije);

        if (projekcija == null)
        {
            return BadRequest("Nema projekcije za koju zelite da napravite kartu");
        }

        double izracunataCena = projekcija.OsnovnaCenaKarte;

        for (var i = 2; i <= red; i++)
        {
            izracunataCena *= 0.97;
        }
        var karta = new
        {
            BrojReda = red,
            BrojSedista = brojSedista,
            CenaKarte = izracunataCena,
            SifraProjekcije = projekcija.Sifra
        };

        return Ok(karta);
    }

    [HttpGet("VratiSveProjekcije")]

    public async Task<ActionResult> VratiSveProjekcije()
    {
        try
        {
            var Projekcije = await Context.Projekcije.Select(p => new
            {
                ID = p.ID,
                Naziv = p.Naziv,
                VremePrikazivanja = p.VremePrikazivanja.ToString("d.M.yyyy. HH:mm"),
                Sifra = p.Sifra,
                Sala = p.sala!.Naziv,
                Karte = p.Karte!.Select(k => new
                {
                    Red = k.Red,
                    BrojSedista = k.BrojSedista,
                    Rezervisana = k.Rezervisana

                }).ToList()

            }).ToListAsync();

            return Ok(Projekcije);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
