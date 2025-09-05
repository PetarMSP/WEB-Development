import {Projekcija} from "./Projekcija.js"

export class Application{
    
    constructor(ProjekcijeFetched) {
        this.polje = [
            {naziv:"Red:",klasa:"brojReda"},
            {naziv:"Broj Sedista:",klasa:"brojSedista"},
            {naziv:"Cena Karte:",klasa:"cenaKarte"},
            {naziv:"Sifra:",klasa:"sifraProjekcije"},
        ];
        this.projekcije = ProjekcijeFetched.map(p =>{
            console.log(p);
            return new Projekcija(p.id,p.naziv,p.vremePrikazivanja,p.sifra,p.sala,p.karte);
        });

    }
    draw(conteiner){
        const GlavnaForma = document.createElement("div");
        GlavnaForma.classList.add("div-Bisokop");
            this.projekcije.forEach(p =>{
                let divProjekcija = document.createElement("div");
                divProjekcija.classList.add("div-Projekcija");     
                    let lblTekst = document.createElement("label");
                    lblTekst.classList.add("lbl-Tekst");
                    lblTekst.innerHTML = `${p.naziv}:${p.vremePrikazivanja} - ${p.sala}`;
                    divProjekcija.appendChild(lblTekst);

                    let divInfo = document.createElement("div");
                    divInfo.classList.add("div-Info");
                      let divKupovinaKarte = document.createElement("div");
                      divKupovinaKarte.classList.add("div-KupovinaKarte");
                          let lblKupiKartu = document.createElement("label");
                          lblKupiKartu.classList.add("lbl-TekstKupiKartu");
                          lblKupiKartu.innerHTML = "Kupi Kartu";
                          divKupovinaKarte.appendChild(lblKupiKartu);

                          let divPolja = document.createElement("div");
                          divPolja.classList.add("div-Polja");
                          this.drawPolja(divPolja);
                          divKupovinaKarte.appendChild(divPolja);

                          let dugme = document.createElement("input");
                          dugme.type = "button";
                          dugme.value = "Kupi Kartu";
                          dugme.onclick = () => {
                              let red = document.querySelector(`.input-brojReda`).value;
                              let sediste = document.querySelector(`.input-brojSedista`).value;
                              this.RezervisiKartu(red, sediste, p.id);
                              let divZaKartu = divListaKarta.querySelector(
                                  `[data-red='${red}'][data-sediste='${sediste}']`
                              );

                              if (divZaKartu) {
                                  divZaKartu.classList.remove("div-KartaSlobodna");
                                  divZaKartu.classList.add("div-KartaZauzeta");
                              };
                          };
                          dugme.classList.add("dugme-Kupi");
                          divKupovinaKarte.appendChild(dugme);
                      divInfo.appendChild(divKupovinaKarte);
                      
                      let divListaKarta = document.createElement("div");
                      divListaKarta.classList.add("div-ListaKarata");
                      p.karte.forEach(k =>{
                        let divKarta = document.createElement("div");

                        divKarta.dataset.red = k.red;
                        divKarta.dataset.sediste = k.brojSedista;

                        if(k.rezervisana == false){
                          divKarta.classList.add("div-KartaSlobodna");
                        }
                        else{
                          divKarta.classList.add("div-KartaZauzeta");
                        }
                        divKarta.onclick = () =>{
                          if(k.rezervisana == false){
                            let red = divKarta.getAttribute("data-red");
                            let sediste = divKarta.getAttribute("data-sediste");
                            this.PodaciKarte(red, sediste, p.id);
                          }else{
                            alert("Karta je vec rezervisana!");
                          }
                        };
                            let lblRed = document.createElement("label");
                            lblRed.innerHTML = `Red:${k.red};`
                            divKarta.appendChild(lblRed);
                            
                            let lblSediste = document.createElement("label");
                            lblSediste.innerHTML =`Broj:${k.brojSedista}`;
                            divKarta.appendChild(lblSediste); 

                        divListaKarta.appendChild(divKarta);
                      });
                      divInfo.appendChild(divListaKarta);
                    divProjekcija.appendChild(divInfo);

              GlavnaForma.appendChild(divProjekcija);
            })
        conteiner.appendChild(GlavnaForma);
    }

    drawPolja(conteiner){
      this.polje.forEach(e =>{
        let lblnaziv = document.createElement("label");
        lblnaziv.innerHTML = e.naziv;
        lblnaziv.classList.add("margin");
        conteiner.appendChild(lblnaziv);

        let input = document.createElement("input");
        input.classList.add(`input-${e.klasa}`,"margin");
        conteiner.appendChild(input);
      });
    }
    PodaciKarte = async (Red,BrojSedista,IDProjekcije) =>{
       const vrednosti = await fetch(`https://localhost:7080/Ispit/VratiPodatkeKarte/${Red}/${BrojSedista}/${IDProjekcije}`).then(response => response.json());
       console.log(vrednosti);
       this.polje.forEach(e =>{
          let element = document.querySelector(`.input-${e.klasa}`);
          element.value = vrednosti[e.klasa];
       });
    }
    RezervisiKartu = async (Red,BrojSedista,IDProjekcije) =>{
       const rezultat = await fetch(`https://localhost:7080/Ispit/RezervisiKartu/${Red}/${BrojSedista}/${IDProjekcije}`,
        {
           method: "POST",
           headers:{
             "Content-Type":"application/json",
           }
        }).then(response => response.json());
       console.log(rezultat);
    }
    
}