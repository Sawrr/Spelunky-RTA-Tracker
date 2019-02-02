import { Router } from 'express';
import { generateID } from './util';
import { RoomModel } from './room';

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
            // Try to create a room with this id
            await RoomModel.create({ _id: id, createTime: Date.now() });
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

        if (result.n == 0) {
            // Room not found
            return res.sendStatus(404);
        }

        if (result.nModified == 0) {
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

        let result = await RoomModel.updateOne({ _id: req.params.id }, { startTime: req.headers.time });

        if (result.n == 0) {
            // Room not found
            return res.sendStatus(404);
        }

        if (result.nModified == 0) {
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
r.patch("/:id/update/:data", (req, res) => {  // TODO make sure proper headers exist
    // TODO
});

export = r;
