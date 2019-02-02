import { connect } from 'mongoose';

const URL = "mongodb://localhost:27017";
const DB_NAME = "spelunky";

export async function init() {
    return await connect(URL + '/' + DB_NAME);
}
