import { Application } from "./application.js";

const grupe = await fetch("https://localhost:7080/Ispit/VratiSveGrupe").then(response => response.json());


const app = new Application(grupe);
app.draw(document.body);