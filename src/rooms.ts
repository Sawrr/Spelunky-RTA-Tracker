import { Router } from 'express';
import { generateID } from './util';
import { Room } from './room';

let r = Router();

// Get list of opened rooms
r.get("/", (req, res) => {
    console.log(req);
    res.send("This is a test response");
});

// Create a new room
r.post("/", async (req, res) => {
    const MAX_ATTEMPTS = 10;

    for (let i = 0; i < MAX_ATTEMPTS; i++) {
        // Generate a random id
        let id = generateID();

        try {
            // Try to create a room with this id
            await Room.create({ _id: id, createTime: Date.now() });
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
        let data = await Room.findById(req.params.id);

        if (!data) {
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
r.put("/:id/join", (req, res) => {
    // TODO
});

// Start the run
r.put("/:id/start", (req, res) => {
    // TODO
});

// Update the run status
r.put("/:id/update/:data", (req, res) => {  // TODO make sure proper headers exist
    // TODO
});

export = r;
