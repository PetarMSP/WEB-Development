import { Application } from "./aplication.js"

const projekcije = await fetch("https://localhost:7080/Ispit/VratiSveProjekcije").then(response => response.json());
const app = new Application(projekcije);
app.draw(document.body);