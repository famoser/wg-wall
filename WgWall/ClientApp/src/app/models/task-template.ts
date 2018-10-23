export class TaskTemplate {
    id: number;
    name: string;
    intervalInDays: number | null;
    lastExecutionAt: Date | null;
    hidden: boolean;
}
