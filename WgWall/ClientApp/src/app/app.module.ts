import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { AppComponent } from './app.component';
import { NewUserComponent } from './new-user/new-user.component';
import { ProductsComponent } from './products/products.component';
import { TasksComponent } from './tasks/tasks.component';
import { TopBarComponent } from './top-bar/top-bar.component';
import { TransportComponent } from './transport/transport.component';
import { UserComponent } from './user/user.component';
import { UsersComponent } from './users/users.component';
import { LengthPipe } from './pipes/length.pipe';

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

    LengthPipe
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    FontAwesomeModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
