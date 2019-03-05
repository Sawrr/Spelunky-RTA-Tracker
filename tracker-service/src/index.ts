import express from 'express';
import bodyparser from 'body-parser';
import morgan from 'morgan';
import path from 'path';
import { router } from './rooms';
import { connectToDB } from './util';

process.env.NODE_ENV = 'production';

const app = express();
const PORT = 8080;

// Log HTTP requests
app.use(morgan('combined'));

// Parse body as JSON
app.use(bodyparser.json());

// API
app.use("/api/rooms", router);

// Front end
const allowed = [
    '.js',
    '.css',
    '.png',
    '.jpg'
];
  
// Catch all other routes and return the angular index file
app.use(express.static('dist'));
app.get('*', (req, res) => {
    if (allowed.filter(ext => req.url.indexOf(ext) > 0).length > 0) {
        res.sendFile(`dist/${req.url}`, { root: __dirname });
    } else {
        res.sendFile('dist/index.html', { root: __dirname });
    }
});

// app.all('/*', function(req, res, next) {
//     // Just send the index.html for other files to support HTML5Mode
//     res.sendFile('dist/index.html', { root: __dirname });
// });

// All other errors
app.use((err: any, req: any, res: any, next: any) => {
    res.sendStatus(err.statusCode || 500);
});

// Establish connection to MongoDB and start listening
connectToDB().then(connectStr => {
    console.log("Connected to DB: " + connectStr);
    
    app.listen(PORT, () => {
        console.log(`Listening on port ${PORT}`);
    });
});
