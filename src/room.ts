import { Typegoose, prop } from 'typegoose';

export class Room extends Typegoose {
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

export let RoomModel = new Room().getModelForClass(Room);
