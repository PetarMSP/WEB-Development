export class Projekcija{

    constructor(id,naziv,vremePrikazivanja,sifra,sala,karte = []) {
        this.id = id;
        this.naziv = naziv;
        this.vremePrikazivanja = vremePrikazivanja;
        this.sifra = sifra;
        this.sala = sala;
        this.karte = karte;
    }
}