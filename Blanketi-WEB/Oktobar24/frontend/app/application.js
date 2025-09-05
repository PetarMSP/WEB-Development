import { Grupa } from "./Grupa.js"

export class Application {
    constructor(GrupeFetched) {
        this.polje=[
          {naziv:"Ime i Prezime:",klasa:"deteImeP"},
          {naziv:"JMBG:", klasa:"deteJmbg"},
          {naziv:"Ime roditelja:",klasa:"rodIme"},
        ];
        
        this.grupe = GrupeFetched.map(g =>{
            console.log(g);
            return new Grupa(g.id,g.imeGrupe,g.boja,g.vaspitac,g.brojDece,g.deca);
        });
    }

    draw(conteiner){
       const forma = document.createElement("div");
       this.drawForma(forma);
       forma.classList.add("Forma");
       conteiner.appendChild(forma);

       const grupe = document.createElement("div");
       this.drawGrupe(grupe);
       grupe.classList.add("GrupeForma");
       conteiner.appendChild(grupe);
    }
    drawForma(conteiner){
      const div = document.createElement("div");
      div.classList.add("divZaFormu");
       this.polje.forEach(k =>{
          let lblnaziv = document.createElement("label");
          lblnaziv.innerHTML = k.naziv;
          lblnaziv.classList.add("margin");
          div.appendChild(lblnaziv);
          
            let input = document.createElement("input");
            input.classList.add(`input-${k.klasa}`,"margin");
            div.appendChild(input);
          
       });
       conteiner.appendChild(div);

        const dugme = document.createElement("input");
        dugme.type = "button";
        dugme.value = "Upisi dete";
        dugme.onclick = this.UpisiDete.bind(this);
        dugme.classList.add("dugme-Upisi");
        conteiner.appendChild(dugme);
    }
    drawGrupe(conteiner){
        this.grupe.forEach(g =>{
          let Grupadiv = document.createElement("div");
          g.draw(Grupadiv);
          let boja = g.GetBoja();
          Grupadiv.style.backgroundColor = `${boja}`;
          Grupadiv.classList.add("div-Grupa");
          conteiner.appendChild(Grupadiv);
        });
    }
    UpisiDete = async () => {
        let vrednosti = {};
        this.polje.forEach(k => {
            let input = document.querySelector(`.input-${k.klasa}`);
            vrednosti[k.klasa] = input.value;
        });
    
        console.log(vrednosti);
    
        const result = await fetch(`https://localhost:7080/Ispit/UpisiDete/${vrednosti["deteImeP"]}/${vrednosti["deteJmbg"]}/${vrednosti["rodIme"]}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            }
        }).then(response => response.json());
        console.log(result);
        const { grupaID, ...dete } = result;
        console.log(dete);
        const postojecaGrupa = this.grupe.find(g => g.id == result.grupaID);
        const listaGrupa = document.querySelector(".GrupeForma");
        if (postojecaGrupa != null) {
            if (postojecaGrupa.deca.find(d => d.jmbg == result.jmbg) == null) {
                postojecaGrupa.deca.push(dete);
            }
        }
        listaGrupa.innerHTML = " ";
    
        this.drawGrupe(listaGrupa);
    };    
    
}