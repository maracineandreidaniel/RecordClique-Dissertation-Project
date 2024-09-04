import { ErrorHandler, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './components/navbar/navbar.component';
import { HomeComponent } from './components/home/home.component';
import { DatePipe } from '@angular/common';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgToastModule } from 'ng-angular-popup'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import {MatIconModule} from '@angular/material/icon';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatNativeDateModule} from '@angular/material/core';
import { MyProfileComponent } from './components/my-profile/my-profile.component';
import { GlobalErrorHandler } from 'src/services/global-error-handler/global-error-handler.service';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { NgxPaginationModule } from 'ngx-pagination';
import { MatButtonModule } from '@angular/material/button';
import { UnauthorizedPageComponent } from './components/unauthorized-page/unauthorized-page.component';
import { AddArtistComponent } from './components/crud-artist/add-artist/add-artist.component';
import { UpdateArtistComponent } from './components/crud-artist/update-artist/update-artist.component';
import { IndexArtistComponent } from './components/crud-artist/index-artist/index-artist.component';
import { AddAlbumComponent } from './components/crud-album/add-album/add-album/add-album.component';
import { IndexAlbumComponent } from './components/crud-album/index-album/index-album/index-album.component';
import { UpdateAlbumComponent } from './components/crud-album/update-album/update-album.component';
import { MatSelectModule } from '@angular/material/select';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    LoginComponent,
    SignupComponent,
    ResetPasswordComponent,
    MyProfileComponent,
    UnauthorizedPageComponent,
    AddArtistComponent,
    UpdateArtistComponent,
    IndexArtistComponent,
    AddAlbumComponent,
    IndexAlbumComponent,
    UpdateAlbumComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    CommonModule,
    DatePipe,
    ReactiveFormsModule,
    NgToastModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      timeOut: 2000,
      progressBar : true,
      progressAnimation: 'increasing',
      preventDuplicates: true
    }),
    FormsModule,
    BsDropdownModule.forRoot(),
    ModalModule.forRoot(),
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatIconModule,
    BsDatepickerModule,
    NgxPaginationModule,
    MatButtonModule,
    MatSelectModule
  ],
  providers: [LoginComponent,
  {
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptor,
    multi: true
  },
  {provide: ErrorHandler , useClass: GlobalErrorHandler },
  DatePipe
],
  bootstrap: [AppComponent]
})
export class AppModule { }
