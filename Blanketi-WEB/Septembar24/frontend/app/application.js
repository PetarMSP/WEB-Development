import { Stan } from "./Stan.js"

export class Application{
     constructor(IDsFetched) {
        this.polje = [
           {naziv:"Broj stana:",klasa: "birajStan"}
        ];
        this.IDs = IDsFetched.map(p =>{
            console.log(p);
            return new Stan(p.id);
        });
     }

     draw(conteiner)
     {
        const forma = document.createElement("div");
        forma.classList.add("Forma");

        const pocetna = document.createElement("div");
        pocetna.classList.add("BirajForma");
        this.drawForma(pocetna);
        forma.appendChild(pocetna);

        const infoStan = document.createElement("div");
        infoStan.classList.add("FormaStanInfo");
        forma.appendChild(infoStan);

        conteiner.appendChild(forma);

        const pretraga = document.createElement("div");
        pretraga.classList.add("pretragaTroskova");
        conteiner.appendChild(pretraga);

     }
     drawForma(conteiner)
     {
       const div = document.createElement("div");
       div.classList.add("DivIzbor");

       this.polje.forEach(p => {
         const lbl = document.createElement("label");
         lbl.innerHTML = p.naziv;
         lbl.classList.add("margin-10");
         div.appendChild(lbl);
         
         const dropdown = document.createElement("select");
         dropdown.classList.add(`dropdown-${p.klasa}`,"margin-10");
         this.IDs.forEach(stan => {
            const option  = document.createElement("option");
            option.value = stan.id;
            option.textContent = stan.id;
            dropdown.appendChild(option);
         });
         div.appendChild(dropdown);
       });
       conteiner.appendChild(div);

       const dugme = document.createElement("input");
       dugme.type = "button";
       dugme.value = "Prikazi informacije";
       dugme.onclick = this.PrikupiInformacije;
       dugme.classList.add("dugme-prikaz");
       conteiner.appendChild(dugme);
     }
     PrikupiInformacije = async() =>{
         const stanID = document.querySelector(".dropdown-birajStan").value;
         console.log(stanID);
         try{
             const infoStan = await fetch(`https://localhost:7080/Ispit/InformacijeOStanu/${stanID}`).then(response => response.json());
             const infoRacuni = await fetch(`https://localhost:7080/Ispit/RacunizaStan/${stanID}`).then(response => response.json());
             console.log(infoRacuni);
             console.log(infoStan);
             this.drawStan(infoStan);
             this.drawRacuni(infoRacuni);
         }
         catch(error)
         {
            console.log("Greska prilikom preuzimanja ID-a:",error);
            return []
         }
     };
     drawStan(infoStan){
        const rezultati = document.querySelector('.FormaStanInfo');
        rezultati.innerHTML = " "; 
        rezultati.style.border = "2px dotted black";
        const divStan = document.createElement("div");
        divStan.classList.add("StanDiv");
          
          const lbl1 = document.createElement("label");
          lbl1.innerHTML = "Broj stana:";
          lbl1.classList.add("labelBrojStana");

          const lbl1Value = document.createElement("span");
          lbl1Value.innerHTML = infoStan.id;
          lbl1.classList.add("labelBrojStanaValue");
          lbl1.appendChild(lbl1Value);
          divStan.appendChild(lbl1);

          const lbl2 = document.createElement("label");
          lbl2.innerHTML = "Ime vlasnika:";
          lbl2.classList.add("labelImeVlasnika");

          const lbl2Value = document.createElement("span");
          lbl2Value.innerHTML = infoStan.imeVlasnika;
          lbl2.appendChild(lbl2Value)
          divStan.appendChild(lbl2);

          const lbl3 = document.createElement("label");
          lbl3.innerHTML = "Povrsina(m2):";
          lbl3.classList.add("labelPovrsina");

          const lbl3Value = document.createElement("span");
          lbl3Value.innerHTML = infoStan.povrsina;
          lbl3.appendChild(lbl3Value);
          divStan.appendChild(lbl3);

          const lbl4 = document.createElement("label");
          lbl4.innerHTML = "Broj Clanova:";
          lbl4.classList.add("labelBrojClanova");

          const lbl4Value = document.createElement("span");
          lbl4Value.innerHTML = infoStan.brojClanova;
          lbl4.appendChild(lbl4Value);
          divStan.appendChild(lbl4);

        rezultati.appendChild(divStan);

        const dugme = document.createElement("input");
        dugme.type = "button";
        dugme.disabled = false;
        dugme.value = "Izracunaj ukupno zaduzenje";
        dugme.onclick = this.Izracunaj;
        dugme.classList.add("dugme-Izracunaj");
        rezultati.appendChild(dugme);

    }
    drawRacuni(infoRacuni){
       const rezultati = document.querySelector('.pretragaTroskova');
       rezultati.innerHTML = " ";
       
       infoRacuni.forEach(data =>{

          const divInfo = document.createElement("div");
          divInfo.classList.add("DivInfo")

           const Mesec = document.createElement("label");
           Mesec.innerHTML = `Mesec:&nbsp;&nbsp;&nbsp;${data.mesec}`;
           Mesec.classList.add("InfoLabele");
           divInfo.appendChild(Mesec);

           const Voda = document.createElement("label");
           Voda.innerHTML = `Voda:&nbsp;&nbsp;&nbsp;${data.voda}`;
           Voda.classList.add("InfoLabele");
           divInfo.appendChild(Voda);

           const Struja = document.createElement("label");
           Struja.innerHTML = `Struja:&nbsp;&nbsp;&nbsp;${data.struja}`;
           Struja.classList.add("InfoLabele");
           divInfo.appendChild(Struja);

           const Komunalije = document.createElement("label");
           Komunalije.innerHTML = `Komunalne  usluge:&nbsp;&nbsp;&nbsp;${data.komunalneUsluge}`;
           Komunalije.classList.add("InfoLabele");
           divInfo.appendChild(Komunalije);

           const Placen = document.createElement("label");
           Placen.innerHTML = `Placen:&nbsp;&nbsp;&nbsp;${data.placen}`;
           Placen.classList.add("InfoLabele");
           if(data.placen == "Da"){
             divInfo.style.backgroundColor = "lightgreen";
           }else{
              divInfo.style.backgroundColor = "lightsalmon";
           }
           divInfo.appendChild(Placen);

           rezultati.appendChild(divInfo);
       });
       
    }

    Izracunaj = async() =>{
       const stanID = document.querySelector('.dropdown-birajStan').value;
       try
       {
          const ukupno = await fetch(`https://localhost:7080/Ispit/Troskovi/${stanID}`).then(response => response.json());
          console.log(ukupno);
          const btn = document.querySelector('.dugme-Izracunaj');
          btn.value = ukupno;
          btn.disabled = true;
       }
       catch(error){
          console.log("Greska prilikom preuzimanja ID-a",error);
          return []
       }
    }

}