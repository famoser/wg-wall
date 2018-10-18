export class TaskTemplate {
    id: number;
    name: string;
    intervalInDays: number | null;
    lastActivationAt: Date | null;
    hide: boolean;
}
