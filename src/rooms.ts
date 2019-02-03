import { Router } from 'express';
import { generateID } from './util';
import { RoomModel, Room } from './room';

const NUM_JOURNAL_ENTRIES = 3; // TODO update
const NUM_CHARACTER_ENTRIES = 4; // TODO update

let r = Router();

// Get list of opened rooms
r.get("/", async (req, res) => {
    try {
        let data = await RoomModel.find();

        if (data) {
            return res.send(data);
        }

        // No rooms opened
        return res.sendStatus(204);
    } catch (err) {
        // Other error occurred
        return res.sendStatus(500);
    }
});

// Create a new room
r.post("/", async (req, res) => {
    const MAX_ATTEMPTS = 10;

    for (let i = 0; i < MAX_ATTEMPTS; i++) {
        // Generate a random id
        let id = generateID();

        try {
            let newRoom = {
                _id: id,
                createTime: Date.now(),
                joined: false,
                startTime: 0,
                endTime: 0,
                data: {
                    journalTime: 0,
                    charactersTime: 0,
                    speedlunkyTime: 0,
                    bigMoneyTime: 0,
                    noGoldTime: 0,
                    teamworkTime: 0,
                    casanovaTime: 0,
                    publicEnemyTime: 0,
                    addictedTime: 0,

                    journal: new Array<Boolean>(NUM_JOURNAL_ENTRIES).fill(false),
                    characters: new Array<Boolean>(NUM_CHARACTER_ENTRIES).fill(false),
                    deaths: {
                        host: 0,
                        guest: 0
                    }
                }
            }

            // Try to create a room with this id
            await RoomModel.create(newRoom);
        } catch (err) {
            // Failed to create a room, try again
            continue;
        }

        // Success, return the room id
        return res.send(id);
    }

    // Failure, reached max attempts
    res.sendStatus(500);
});

// Get status of a room
r.get("/:id", async (req, res) => {
    try {
        let data = await RoomModel.findById(req.params.id);

        if (data) {
            return res.send(data);
        }

        // Room not found
        return res.sendStatus(404);
    } catch (err) {
        // Other error occurred
        return res.sendStatus(500);
    }
});

// Join a room
r.patch("/:id/join", async (req, res) => {
    try {
        let result = await RoomModel.updateOne({ _id: req.params.id }, { joined: true });

        if (result.n === 0) {
            // Room not found
            return res.sendStatus(404);
        }

        if (result.nModified === 0) {
            // Not modified
            return res.sendStatus(304);
        }

        // Success
        return res.sendStatus(200);
    } catch (err) {
        // Other error occurred
        return res.sendStatus(500);
    }
});

// Start the run
r.patch("/:id/start", async (req, res) => {
    try {
        if (!req.headers.time) {
            // Time header required
            return res.sendStatus(400);
        }

        let room = await RoomModel.findById(req.params.id);

        if (!room) {
            // Room not found
            return res.sendStatus(404);
        }

        if (room.startTime) {
            // Room has already started
            return res.sendStatus(412);
        }

        // Parse time from header
        let time = (+req.headers.time);

        if (time < room.createTime || time > Date.now()) {
            // Start time can't be earlier than room creation time, or later than the current time
            return res.sendStatus(400);
        }

        if (!room.joined) {
            // Room not joined
            return res.sendStatus(412);
        }

        let result = await RoomModel.updateOne({ _id: req.params.id }, { startTime: time });

        if (result.n === 0) {
            // Room not found
            return res.sendStatus(404);
        }

        if (result.nModified === 0) {
            // Not modified
            return res.sendStatus(304);
        }

        // Success
        return res.sendStatus(200);
    } catch (err) {
        // Other error occurred
        return res.sendStatus(500);
    }
});

// Update the run status
r.patch("/:id/update", async (req, res) => {
    try {
        if (!req.headers.time) {
            // Time header required
            return res.sendStatus(400);
        }

        if (req.headers.player !== "host" && req.headers.player !== "guest") {
            // Player header must be 'host' or 'guest'
            return res.sendStatus(400);
        }

        let room = await RoomModel.findById(req.params.id);

        if (!room) {
            // Room not found
            return res.sendStatus(404);
        }

        if (!room.joined) {
            // Room not joined
            return res.sendStatus(412);
        }

        if (!room.startTime) {
            // Room hasn't started
            return res.sendStatus(412);
        }

        // Parse time from header
        let time = (+req.headers.time);

        if (time < room.createTime || time < room.startTime || time > Date.now()) {
            // Timestamp can't be earlier than room creation or start time, or later than the current time
            return res.sendStatus(400);
        }

        // Start with existing data
        let newData = room.data;

        // Check journal
        if (!room.data.journalTime && req.body.journal) {
            if (req.body.journal.length != NUM_JOURNAL_ENTRIES) {
                // Bad journal data
                return res.sendStatus(400);
            }
            for (let i = 0; i < NUM_JOURNAL_ENTRIES; i++) {
                if (!room.data.journal[i] && req.body.journal[i]) {
                    // Entry unlocked
                    newData.journal[i] = true;
                }
            }
            if (newData.journal.every(entry => { return entry })) {
                // All journal entries unlocked, mark as done
                newData.journalTime = time;
            }
        }

        // Check characters
        if (!room.data.charactersTime && req.body.characters) {
            if (req.body.characters.length != NUM_CHARACTER_ENTRIES) {
                // Bad characters data
                return res.sendStatus(400);
            }
            for (let i = 0; i < NUM_CHARACTER_ENTRIES; i++) {
                if (!room.data.characters[i] && req.body.characters[i]) {
                    // Character unlocked
                    newData.characters[i] = true;
                }
            }
            if (newData.characters.every(char => { return char })) {
                // All characters unlocked, mark as done
                newData.charactersTime = time;
            }
        }

        // Check other achievements
        if (!room.data.speedlunkyTime && req.body.speedlunky) {
            newData.speedlunkyTime = time;
        }
        if (!room.data.bigMoneyTime && req.body.bigMoney) {
            newData.bigMoneyTime = time;
        }
        if (!room.data.noGoldTime && req.body.noGold) {
            newData.noGoldTime = time;
        }
        if (!room.data.teamworkTime && req.body.teamwork) {
            newData.teamworkTime = time;
        }
        if (!room.data.casanovaTime && req.body.casanova) {
            newData.casanovaTime = time;
        }
        if (!room.data.publicEnemyTime && req.body.publicEnemy) {
            newData.publicEnemyTime = time;
        }

        // Check deaths
        if (!room.data.addictedTime && req.body.deaths) {
            // Only update the player's death count
            if (req.headers.player === "host") {
                if (req.body.deaths > room.data.deaths.host) {
                    newData.deaths.host = req.body.deaths;
                }
            } else {
                if (req.body.deaths > room.data.deaths.guest) {
                    newData.deaths.guest = req.body.deaths;
                }
            }

            // Check for Addicted
            if (newData.deaths.host + newData.deaths.guest >= 1000) {
                newData.addictedTime = time;
            }
        }

        // Check for all achievements done
        let endTime = 0;

        if (newData.addictedTime 
        && newData.bigMoneyTime 
        && newData.casanovaTime 
        && newData.charactersTime 
        && newData.journalTime 
        && newData.noGoldTime 
        && newData.publicEnemyTime 
        && newData.speedlunkyTime 
        && newData.teamworkTime) {
            // All done!
            endTime = time;
        }

        // Update the data
        let result = await RoomModel.updateOne({ _id: req.params.id }, { data: newData, endTime: endTime });

        if (result.n === 0) {
            // Room not found
            return res.sendStatus(404);
        }

        if (result.nModified === 0) {
            // Not modified
            return res.sendStatus(304);
        }

        // Success
        return res.sendStatus(200);

    } catch (err) {
        // Other error occurred
        return res.sendStatus(500);
    }
});

export = r;
