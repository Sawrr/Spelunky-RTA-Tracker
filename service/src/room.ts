import { getModelForClass, prop } from "@typegoose/typegoose";


export class Room {
    @prop()
    _id: string;
    @prop()
    createTime: number;
    @prop()
    joined: boolean;
    @prop()
    startTime: number;
    @prop()
    endTime: number;
    @prop()
    data: {
        journalTime: number;
        charactersTime: number;
        speedlunkyTime: number;
        bigMoneyTime: number;
        noGoldTime: number;
        teamworkTime: number;
        casanovaTime: number;
        publicEnemyTime: number;
        addictedTime: number;
        
        journal: {
            places: boolean[];
            monsters: boolean[];
            items: boolean[];
            traps: boolean[];
        };    
        characters: boolean[];
        plays: {
            host: number,
            guest: number
        }
    }
};

export let RoomModel = getModelForClass(Room);
