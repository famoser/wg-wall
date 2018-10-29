import { FrontendUser } from "./frontend-user";
import { Base } from "./base/base";

export class Plate extends Base {
  frontendUserId: number;
  frontendUser: FrontendUser;
  dinnerState: number;
}
