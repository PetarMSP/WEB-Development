
export class Grupa{
    constructor(id,imeGrupe,boja,vaspitac,brojDece,deca){
         this.id = id;
         this.vaspitac = vaspitac;
         this.imeGrupe = imeGrupe;
         this.boja = boja;
         this.brojDece = brojDece;
         this.deca = deca;
    }
    GetBoja(){
        return this.boja;
    }
    draw(conteiner){
        const lblime = document.createElement("label");
        lblime.textContent = `Vaspitac:`;
        const lblimespIme = document.createElement("span");
        lblimespIme.textContent = this.vaspitac;
        lblime.appendChild(lblimespIme);
        lblime.classList.add("LabelaGrupe");
        conteiner.appendChild(lblime);

        const lblvas = document.createElement("label");
        lblvas.textContent = `Ime grupe:`;
        const lblimespVas = document.createElement("span");
        lblimespVas.textContent = this.imeGrupe;
        lblvas.appendChild(lblimespVas);
        lblvas.classList.add("LabelaGrupe");
        conteiner.appendChild(lblvas);

        const lblbroj = document.createElement("label");
        lblbroj.textContent = `Broj dece:`;
        const lblimespBroj = document.createElement("span");
        lblimespBroj.textContent = this.brojDece;
        lblbroj.appendChild(lblimespBroj);
        lblbroj.classList.add("LabelaGrupe");
        conteiner.appendChild(lblbroj);

        const divDeca = document.createElement("div");
        divDeca.classList.add("div-Deca");
        this.deca.forEach(d =>{
          let detelbl = document.createElement("label");
          detelbl.style.padding = "10px";
          detelbl.style.border = "2px dotted black";
          detelbl.innerHTML = `${d.imePrezime},${d.imeRoditelja},${d.jmbg}`;
          divDeca.appendChild(detelbl);
        });
        conteiner.appendChild(divDeca);
      }
}