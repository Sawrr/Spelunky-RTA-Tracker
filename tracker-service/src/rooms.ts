import { Router } from 'express';
import { generateID } from './util';
import { RoomModel } from './room';
import { RoomAuditModel } from './roomAudit';

const NUM_JOURNAL_PLACES = 10;
const NUM_JOURNAL_MONSTERS = 56;
const NUM_JOURNAL_ITEMS = 34;
const NUM_JOURNAL_TRAPS = 14;
const NUM_CHARACTER_ENTRIES = 16;

let r = Router();

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

                    journal: {
                        places: new Array<Boolean>(NUM_JOURNAL_PLACES).fill(false),
                        monsters: new Array<Boolean>(NUM_JOURNAL_MONSTERS).fill(false),
                        items: new Array<Boolean>(NUM_JOURNAL_ITEMS).fill(false),
                        traps: new Array<Boolean>(NUM_JOURNAL_TRAPS).fill(false)
                    },
                    characters: new Array<Boolean>(NUM_CHARACTER_ENTRIES).fill(false),
                    plays: {
                        host: 0,
                        guest: 0
                    }
                }
            }

            let newRoomAudit = {
                _id: id,
                createTime: Date.now()
            }

            // Try to create a room with this id
            await RoomModel.create(newRoom);
            // Audit trail
            await RoomAuditModel.create(newRoomAudit);
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
r.put("/:id/join", async (req, res) => {
    try {
        let result = await RoomModel.updateOne({ _id: req.params.id }, { joined: true });

        if (result.n === 0) {
            // Room not found
            return res.sendStatus(404);
        }
        
        // Success
        return res.sendStatus(200);
    } catch (err) {
        // Other error occurred
        return res.sendStatus(500);
    }
});

// Start the run
r.put("/:id/start", async (req, res) => {
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

        if (time < room.createTime) {
            // Start time can't be earlier than room creation time
            return res.sendStatus(400);
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
r.put("/:id/update", async (req, res) => {
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

        if (!room.startTime) {
            // Room hasn't started
            return res.sendStatus(412);
        }

        // Parse time from header
        let time = (+req.headers.time);

        if (time < room.createTime || time < room.startTime) {
            // Timestamp can't be earlier than room creation or start time
            return res.sendStatus(400);
        }

        // Start with existing data
        let newData = room.data;

        // Check journal
        if (!room.data.journalTime && req.body.journal) {
            if (req.body.journal.places.length != NUM_JOURNAL_PLACES
            || req.body.journal.monsters.length != NUM_JOURNAL_MONSTERS
            || req.body.journal.items.length != NUM_JOURNAL_ITEMS
            || req.body.journal.traps.length != NUM_JOURNAL_TRAPS) {
                // Bad journal data
                return res.sendStatus(400);
            }
            // Places
            for (let i = 0; i < NUM_JOURNAL_PLACES; i++) {
                if (!room.data.journal.places[i] && req.body.journal.places[i]) {
                    // Entry unlocked
                    newData.journal.places[i] = true;
                }
            }
            // Monsters
            for (let i = 0; i < NUM_JOURNAL_MONSTERS; i++) {
                if (!room.data.journal.monsters[i] && req.body.journal.monsters[i]) {
                    // Entry unlocked
                    newData.journal.monsters[i] = true;
                }
            }
            // Items
            for (let i = 0; i < NUM_JOURNAL_ITEMS; i++) {
                if (!room.data.journal.items[i] && req.body.journal.items[i]) {
                    // Entry unlocked
                    newData.journal.items[i] = true;
                }
            }
            // Traps
            for (let i = 0; i < NUM_JOURNAL_TRAPS; i++) {
                if (!room.data.journal.traps[i] && req.body.journal.traps[i]) {
                    // Entry unlocked
                    newData.journal.traps[i] = true;
                }
            }
            
            if (newData.journal.places.every(entry => { return entry })
            && newData.journal.monsters.every(entry => { return entry })
            && newData.journal.items.every(entry => { return entry })
            && newData.journal.traps.every(entry => { return entry })) {
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

        // Check plays
        if (!room.data.addictedTime && req.body.plays) {
            // Only update the player's death count
            if (req.headers.player === "host") {
                if (req.body.plays > room.data.plays.host) {
                    newData.plays.host = req.body.plays;
                }
            } else {
                if (req.body.plays > room.data.plays.guest) {
                    newData.plays.guest = req.body.plays;
                }
            }

            // Check for Addicted
            if (newData.plays.host + newData.plays.guest >= 1000) {
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

export let router = r;
