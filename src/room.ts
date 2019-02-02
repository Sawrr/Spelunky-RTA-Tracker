import { Schema, model } from 'mongoose';

let achievementSchema = {
    done: Boolean,
    time: Number
};

let achievementArraySchema = {
    unlockables: [Boolean],
    time: Number
};

let dataSchema = {
    journal: achievementArraySchema,
    characters: achievementArraySchema,
    speedlunky: achievementSchema,
    bigMoney: achievementSchema,
    noGold: achievementSchema,
    teamwork: achievementSchema,
    casanova: achievementSchema,
    publicEnemy: achievementSchema,
    addicted: achievementSchema,
    deaths: {
        host: Boolean,
        guest: Boolean
    }
};

let roomSchema = new Schema({
    _id: String,
    createTime: Number,
    joined: Boolean,
    startTime: Number,
    endTime: Number,
    hostIP: String,
    guestIP: String,
    data: dataSchema
});

export let Room = model('rooms', roomSchema);
