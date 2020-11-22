import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { ClientComponent } from './client/client.component';
import { AccountCreationComponent } from './account/account-creation.component';
import { ManageAccountComponent } from './account/manage-account/manage-account.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    ClientComponent,
    AccountCreationComponent,
    ManageAccountComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: ClientComponent, pathMatch: 'full' },
      { path: 'client', component: ClientComponent },
      { path: 'account-creation/:id', component: AccountCreationComponent },
      { path: 'manage-account/:id', component: ManageAccountComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
