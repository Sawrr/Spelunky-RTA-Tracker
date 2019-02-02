import express from 'express';
import rooms from './rooms';
import { init } from './dao';

const app = express();
const PORT = 8080;

app.get("/", (req, res) => {
    res.send("Success");
});

app.use("/rooms", rooms);

init().then(result => {
    
    app.listen(PORT, () => {
        console.log(`Listening on port ${PORT}`);
    });
});
