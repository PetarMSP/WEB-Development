import { Application } from "./application.js";

const IDs = await fetch("https://localhost:7080/Ispit/VratiSveID").then(response => response.json());

const app = new Application(IDs);
app.draw(document.body);
