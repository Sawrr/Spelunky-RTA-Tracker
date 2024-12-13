import { getModelForClass, prop } from "@typegoose/typegoose";


export class RoomAudit {
    @prop()
    _id: string;
    @prop()
    createTime: number;
};

export let RoomAuditModel = getModelForClass(RoomAudit);
