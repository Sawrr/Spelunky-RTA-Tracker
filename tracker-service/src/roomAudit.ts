import { Typegoose, prop } from 'typegoose';

export class RoomAudit extends Typegoose {
    @prop()
    _id: string;
    @prop()
    createTime: number;
};

export let RoomAuditModel = new RoomAudit().getModelForClass(RoomAudit);
