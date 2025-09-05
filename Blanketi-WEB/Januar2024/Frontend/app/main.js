import { Application } from "./application.js";

const korisnci = await fetch("https://localhost:7080/Ispit/VratiKorisnike").then(response => response.json());
const sobe = await fetch("https://localhost:7080/Ispit/VratiSveSobe").then(response => response.json());

const app = new Application(korisnci,sobe);
app.draw(document.body);