
export class Soba {
    constructor(id,ime,clanovi) {
        this.id = id;
        this.ime = ime;
        this.clanovi = clanovi;
    }
    getID(){
        return this.id;
    }
    draw(conteiner){
      const lblime = document.createElement("label");
      lblime.innerHTML = this.ime;
      lblime.style.fontSize = "large";
      lblime.style.padding = "20px";
      lblime.style.textAlign = "center";
      lblime.style.fontWeight = "bold";
      conteiner.appendChild(lblime);

      const naslov = document.createElement("p");
      naslov.textContent = "Clanovi:";
      naslov.style.fontSize = "medium";
      naslov.style.marginTop = "10px";
      naslov.style.marginBottom = "0px";
      conteiner.appendChild(naslov);

      const lista = document.createElement("ul");
      
      this.clanovi.forEach(c =>{
        let stavka = document.createElement("li");
        stavka.textContent = `${c.nadimak} (${c.korisnickoIme})`;
        stavka.style.color = c.boja;
        lista.appendChild(stavka);
      });

      conteiner.appendChild(lista);
    }
}

