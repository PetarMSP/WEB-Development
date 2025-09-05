
import { Korisnik }  from "./Korisnik.js";
import { Soba } from "./Soba.js"

export class Application {
    constructor(KorisniciFetched,SobeFetched) {
        this.polje=[
          {naziv:"Soba:",klasa:"sobaime"},
          {naziv:"Korisnik:", klasa:"birajKorisnika"},
          {naziv:"Nadimak:",klasa:"upNadimak"},
          {naziv:"Boja:",klasa:"birajBoju"}
        ];
        
        this.korisnici = KorisniciFetched.map(k =>{
            console.log(k);
            return new Korisnik(k.id,k.imePrezime,k.korisnickoIme,k.email,k.sifra);
        });
        this.sobe = SobeFetched.map(s =>{
            console.log(s);
            return new Soba(s.id,s.ime,s.clanovi);
        });

    }

    draw(conteiner){
       const formaDodaj = document.createElement("div");
       formaDodaj.classList.add("Forma");
       this.drawForma(formaDodaj);
       conteiner.appendChild(formaDodaj);

       const rezultati = document.createElement("div");
       rezultati.classList.add("ListaSobi");
       this.drawSobe(rezultati);
       conteiner.appendChild(rezultati);

    }
    drawForma(conteiner){
      const div = document.createElement("div");
      div.classList.add("divZaFormu");
       this.polje.forEach(k =>{
          let lblnaziv = document.createElement("label");
          lblnaziv.innerHTML = k.naziv;
          lblnaziv.classList.add("margin");
          div.appendChild(lblnaziv);
          if(k.naziv == "Korisnik:")
          {
            let dropdown = document.createElement("select");
            dropdown.classList.add(`dropdown-${k.klasa}`,"margin");
            this.korisnici.forEach(ks =>{
               let option = document.createElement("option");
               option.value = ks.korisnickoIme;
               option.textContent = ks.korisnickoIme;
               dropdown.appendChild(option);
            });
            div.appendChild(dropdown);
          }
          else if(k.naziv == "Boja:")
          {
            let boja = document.createElement("input");
            boja.type = "color";
            boja.classList.add(`Forma-${k.klasa}`);
            div.appendChild(boja);
          }
          else
          {
            let input = document.createElement("input");
            input.classList.add(`input-${k.klasa}`,"margin");
            div.appendChild(input);
          }
       });
       conteiner.appendChild(div);

        const dugme = document.createElement("input");
        dugme.type = "button";
        dugme.value = "Dodaj";
        dugme.onclick = this.DodajKorisnika.bind(this);
        dugme.classList.add("dugme-Dodaj");
        conteiner.appendChild(dugme);
    }
    drawSobe(conteiner){
        this.sobe.forEach(s =>{
          let Sobadiv = document.createElement("div");
          Sobadiv.classList.add("div-Soba");
          s.draw(Sobadiv);
          const IDsobe = s.getID();

          let dugme = document.createElement("input");
          dugme.type = "button";
          dugme.value = "Prebroji jedinstvene";
          dugme.onclick =  () => this.PrebrojiJedinstvene(IDsobe);
          dugme.classList.add(`SobaID-${IDsobe}`,"dugme-Prebroji");
          
          Sobadiv.appendChild(dugme);
          conteiner.appendChild(Sobadiv);
        });
    }
    DodajKorisnika = async () => {
        try {
            let vrednosti = {};
            this.polje.forEach(k => {
                if (k.naziv == "Korisnik:") {
                    let ks = document.querySelector(`.dropdown-${k.klasa}`);
                    vrednosti[k.klasa] = ks.value;
                } else if (k.naziv == "Boja:") {
                    let boja = document.querySelector(`.Forma-${k.klasa}`);
                    vrednosti[k.klasa] = boja.value;
                } else {
                    let input = document.querySelector(`.input-${k.klasa}`);
                    vrednosti[k.klasa] = input.value;
                }
            });
    
            console.log("Vrednosti za slanje:", vrednosti);
    
            const Boja = encodeURIComponent(vrednosti["birajBoju"]);
            const result = await fetch(`https://localhost:7080/Ispit/DodajKorisnikaUSobu/${vrednosti["birajKorisnika"]}/${vrednosti["sobaime"]}/${vrednosti["upNadimak"]}/${Boja}`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                }
            }).then(response => response.json());
            console.log(result);
            const postojecaSoba = this.sobe.find(s => s.id == result.id);
            const listasoba = document.querySelector(".ListaSobi");
            if( postojecaSoba == null)
            {
                const novaSoba = new Soba(result.id,result.ime,result.clanovi);
                this.sobe.push(novaSoba);

                listasoba.replaceChildren();

                this.drawSobe(listasoba);
            }else{
               result.clanovi.forEach(clan =>{
                  if(postojecaSoba.clanovi.find(ks => ks.korisnickoIme == clan.korisnickoIme) == null)
                  {
                      postojecaSoba.clanovi.push(clan);
                  }
               });

               listasoba.replaceChildren();

               this.drawSobe(listasoba);
            }
    
        } catch (error) {
            console.error("Došlo je do greške:", error);
        }
    };
    
    PrebrojiJedinstvene = async(IDsobe) =>{
        
        const vrednost = await fetch(`https://localhost:7080/Ispit/PreborjiJedinstvene/${IDsobe}`).then(response => response.json());
        console.log(vrednost);
        const btn = document.querySelector(`.SobaID-${IDsobe}`);
        btn.value = vrednost;
    };
}