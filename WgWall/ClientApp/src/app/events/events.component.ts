import * as moment from 'moment';

import { Component, OnInit } from '@angular/core';
import {
  faPencilAlt, faPlus, faSave, faUndo, faTrash
} from '@fortawesome/free-solid-svg-icons';

import { EventEntity } from '../models/event';
import { EventService } from '../services/event.service';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html'
})
export class EventsComponent implements OnInit {
  //icons
  public faTrash = faTrash;
  public faPencilAlt = faPencilAlt;
  public faSave = faSave;
  public faUndo = faUndo;
  public faPlus = faPlus;

  //task lists
  public futureEvents: EventEntity[] = [];
  public pastEvents: EventEntity[] = [];

  //to disable buttons when appropiate
  public actionsActive: number;

  //edit entries
  public editSource: EventEntity;
  public editContainer: EventEntity;

  private customDateTimeFormat: string = 'DD.MM.YYYY HH:mm'

  constructor(private eventService: EventService) { }

  ngOnInit() {
    this.eventService.get().subscribe(events => {
      events.sort((a, b) => a.startDate.localeCompare(b.startDate));

      let now = moment().endOf('day').toISOString();
      this.pastEvents = events.filter(e => now.localeCompare(e.startDate) >= 0);
      this.futureEvents = events.filter(e => now.localeCompare(e.startDate) < 0);

      if (this.futureEvents.length == 0) {
        this.startAdd();
      }
    });
  }

  public startAdd() {
    this.editContainer = new EventEntity();
  }

  public startEdit(source: EventEntity) {
    this.editSource = source;
    let entity = new EventEntity();
    entity.name = source.name;
    entity.startDate = moment(source.startDate).format(this.customDateTimeFormat);
    this.editContainer = entity;
  }

  public add(source: EventEntity) {
    //lock
    this.actionsActive++;

    //save to api
    this.actionsActive++;
    source.startDate = moment(source.startDate).toISOString();
    this.eventService.create(source).subscribe(newEvent => {
      this.futureEvents.push(newEvent);
      this.futureEvents.sort((a, b) => a.startDate.localeCompare(b.startDate));
      this.actionsActive--;
    });

    //allow to add new directly
    this.editContainer = new EventEntity();
    this.actionsActive--;
  }

  public abort() {
    this.editSource = null;
    this.editContainer = null;
  }

  public save(source: EventEntity, target: EventEntity) {
    //lock
    this.actionsActive++;

    //write props
    target.name = source.name;
    target.startDate = moment(source.startDate, this.customDateTimeFormat).toISOString();

    //lock & persist changes
    this.actionsActive++;
    this.eventService.update(target).subscribe(() => {
      this.actionsActive--;
      this.futureEvents.sort((a, b) => a.startDate.localeCompare(b.startDate));
    });

    //stop edit
    this.abort();
    this.actionsActive--;
  }

  public remove(subject: EventEntity) {
    //lock
    this.actionsActive++;

    //lock & remove entity
    this.actionsActive++;
    this.eventService.remove(subject).subscribe(() => {
      this.futureEvents.splice(this.futureEvents.indexOf(subject), 1);
      this.actionsActive--;
    });

    //stop edit
    this.abort();
    this.actionsActive--;
  }

  trackByFn(index) {
    return index;
  }
}
