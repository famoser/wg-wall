import { HttpClientModule } from '@angular/common/http';
import { HttpClientInMemoryWebApiModule } from 'angular-in-memory-web-api';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { AppComponent } from './app.component';
import { NewUserComponent } from './new-user/new-user.component';
import { HumanizeDatePipe } from './pipes/humanize-date.pipe';
import { LengthPipe } from './pipes/length.pipe';
import { ProductsComponent } from './products/products.component';
import { TasksComponent } from './tasks/tasks.component';
import { TopBarComponent } from './top-bar/top-bar.component';
import { TransportComponent } from './transport/transport.component';
import { UserComponent } from './user/user.component';
import { UsersComponent } from './users/users.component';
import { InMemoryDataService } from './services/in-memory-data.service';
import { environment } from '../environments/environment';

let enableSampleData = false;

@NgModule({
  declarations: [
    AppComponent,
    UsersComponent,
    UserComponent,
    NewUserComponent,
    TopBarComponent,
    ProductsComponent,
    TransportComponent,
    TasksComponent,

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
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
