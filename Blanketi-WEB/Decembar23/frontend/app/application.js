import { Prodavnica } from "./Prodavnica.js";

export class Application {
    constructor(ProdavniceFetched) {
        this.polje = [
         {naziv:"Naziv:",klasa:"brNaziv"},
         {naziv:"Kategorija:",klasa:"birajkateg"},
         {naziv:"Cena:",klasa:"brCena"},
         {naziv:"Kolicina:",klasa:"brKolicina"},
        ];

        this.kategorije = ["knjiga","igracka","pribor","ostalo"];

        this.prodavnice = ProdavniceFetched.map(pr =>{
           console.log(pr);
           return new Prodavnica(pr.id,pr.naziv,pr.listaProizovda);
        });
    }
    draw(conteiner){
       const forma = document.createElement("div");
       forma.classList.add("Forma");
       this.drawProdavnica(forma);
       conteiner.appendChild(forma);
    }
    drawForma(conteiner,ProdavnicaID){
        const lblNaslov = document.createElement("label");
        lblNaslov.innerHTML = "Upisi Proizvod";
        lblNaslov.classList.add("Labela-Upisi");
        conteiner.appendChild(lblNaslov);
        
        const divDodaj = document.createElement("div");
        divDodaj.classList.add("divDodaj");

        this.polje.forEach(p =>{
            let lbl = document.createElement("label");
            lbl.innerHTML = p.naziv;
            lbl.classList.add("margin");
            divDodaj.appendChild(lbl);

            if(p.naziv == "Kategorija:")
            {
               let dropdown = document.createElement("select");
               dropdown.classList.add(`dropdown-${p.klasa}`,"margin");
               this.kategorije.forEach(k =>{
                  let option = document.createElement("option");
                  option.value = k;
                  option.textContent = k;
                  dropdown.appendChild(option);
               });
               divDodaj.appendChild(dropdown);
            }
            else if(p.naziv == "Naziv:")
            {
               let input = document.createElement("input");
               input.classList.add(`input-${p.klasa}`,"margin");
               divDodaj.appendChild(input);
            }
            else
            {
               let inputNum = document.createElement("input");
               inputNum.type = "number";
               inputNum.value = 0;
               inputNum.max = "100";
               inputNum.min = "0";
               inputNum.classList.add(`input-${p.klasa}`,"margin");
               divDodaj.appendChild(inputNum);
            }
        });
       conteiner.appendChild(divDodaj);

       const dugme = document.createElement("input");
       dugme.type = "button";
       dugme.value = "Dodaj";
       dugme.onclick = () => this.DodajProizvod(ProdavnicaID);
       dugme.classList.add("dugme-Dodaj");
       conteiner.appendChild(dugme);
    }
    drawProdavnica(conteiner){
        this.prodavnice.forEach(p =>{
           let prodDiv = document.createElement("div");
           p.draw(prodDiv);
           let ProdavnicaID = p.getID();
           conteiner.appendChild(prodDiv);

           let formaDod = document.createElement("div");
           formaDod.classList.add(`FormaD-${ProdavnicaID}`,"Forma-Dodaj");
           this.drawForma(formaDod,ProdavnicaID);
           conteiner.appendChild(formaDod);          
        });
    }
    DodajProizvod = async(ProdavnicaID) =>{
      const vrednosti = [];

      this.polje.forEach(p =>{
         if(p.naziv == "Kategorija:"){
            let kategorija = documnet.querySelector(`dropdown-${p.klasa}`);
            vrednosti[p.klasa] = kategorija.value;
         }
         else{
            let ostalo = document.querySelector(`input-${p.klasa}`);
            vrednosti[p.klasa] = ostalo.value;
         }
      });
      console.log(polje);
    };
}