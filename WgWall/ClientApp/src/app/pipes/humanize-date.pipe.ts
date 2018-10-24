import { PipeTransform, Pipe } from "@angular/core";
import * as moment from 'moment';
import { Moment } from 'moment';

type nullableDate = Date | null;

@Pipe({ name: 'humanizeDate', pure: false })
export class HumanizeDatePipe implements PipeTransform {

  transform(value: nullableDate): string {
    if (value == null) {
      return "not set"
    }
    return moment(value).fromNow();
  }
}
