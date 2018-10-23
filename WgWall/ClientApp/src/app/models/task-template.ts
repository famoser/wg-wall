import { Base } from "./base/base";

export class TaskTemplate extends Base {
    name: string;
    intervalInDays: number | null;
    lastExecutionAt: Date | null;
    reward: number;
}
