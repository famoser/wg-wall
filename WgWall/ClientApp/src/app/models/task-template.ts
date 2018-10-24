import { Base } from "./base/base";
import { getLocaleDateTimeFormat } from "@angular/common";

export class TaskTemplate extends Base {
  name: string;
  intervalInDays: number | null;
  lastExecutionAt: Date | null;
  reward: number;

  expectedRelativeCompletion: number;
}
