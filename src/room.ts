import { Typegoose, prop, Ref } from 'typegoose';

export class Achievement extends Typegoose {
    @prop()
    done: Boolean;
    @prop()
    time: Number;
}

export class AchievementArray extends Typegoose {
    @prop()
    unlockables: [Boolean];
    @prop()
    time: Number;
};

export class Data extends Typegoose {
    @prop()
    journal: Ref<AchievementArray>;
    @prop()
    characters: Ref<AchievementArray>;
    @prop()
    speedlunky: Ref<Achievement>;
    @prop()
    bigMoney: Ref<Achievement>;
    @prop()
    noGold: Ref<Achievement>;
    @prop()
    teamwork: Ref<Achievement>;
    @prop()
    casanova: Ref<Achievement>;
    @prop()
    publicEnemy: Ref<Achievement>;
    @prop()
    addicted: Ref<Achievement>;
    @prop()
    deaths: {
        host: Boolean,
        guest: Boolean
    }
};

export class Room extends Typegoose {
    @prop()
    _id: String;
    @prop()
    createTime: Number;
    @prop()
    joined: Boolean;
    @prop()
    startTime: Number;
    @prop()
    endTime: Number;
    @prop()
    data: Ref<Data>;
};

export let RoomModel = new Room().getModelForClass(Room);
