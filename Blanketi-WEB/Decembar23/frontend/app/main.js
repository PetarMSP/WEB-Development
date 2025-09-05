import { Application } from "./application.js";

const prodavnice = await fetch("https://localhost:7080/Ispit/VratiSveProdavnice").then(response => response.json());

const app = new Application(prodavnice);
app.draw(document.body);