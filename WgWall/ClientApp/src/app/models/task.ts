import { TaskTemplate } from "./task-template";

export class Task {
    id: number;
    activatedAt: Date;
    taskTemplateId: number;
    taskTemplate: TaskTemplate | null;
}