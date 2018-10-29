import { HttpClientModule } from '@angular/common/http';
import { HttpClientInMemoryWebApiModule } from 'angular-in-memory-web-api';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { AppComponent } from './app.component';
import { HumanizeDatePipe } from './pipes/humanize-date.pipe';
import { LengthPipe } from './pipes/length.pipe';
import { ProductsComponent } from './products/products.component';
import { TasksComponent } from './tasks/tasks.component';
import { TopBarComponent } from './top-bar/top-bar.component';
import { TransportComponent } from './transport/transport.component';
import { UsersComponent } from './users/users.component';
import { InMemoryDataService } from './services/in-memory-data.service';
import { environment } from '../environments/environment';
import { EventsComponent } from './events/events.component';
import { WeatherComponent } from './weather/weather.component';
import { Store } from './store.service';


let enableSampleData = false;

@NgModule({
  declarations: [
    AppComponent,
    UsersComponent,
    TopBarComponent,
    ProductsComponent,
    TransportComponent,
    TasksComponent,
    EventsComponent,
    WeatherComponent,

    LengthPipe,
    HumanizeDatePipe
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    environment.production || !enableSampleData ? [] : HttpClientInMemoryWebApiModule.forRoot(
      InMemoryDataService, { dataEncapsulation: false, passThruUnknownUrl: true }
    ),
    FormsModule,
    FontAwesomeModule
  ],
  providers: [Store],
  bootstrap: [AppComponent]
})
export class AppModule { }
