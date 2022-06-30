import { connect } from 'mongoose';

const URL = "mongodb://localhost:27017";
const DB_NAME = "spelunky";

export async function connectToDB() {
    let connectStr = `${URL}/${DB_NAME}`;
    await connect(connectStr);
    return connectStr;
}

export function generateID() {
    const NUM_CHARS = 4;
    
    let id = "";
    let possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    for (let i = 0; i < NUM_CHARS; i++) {
        id += possible.charAt(Math.floor(Math.random() * possible.length));
    }

    return id;
}
