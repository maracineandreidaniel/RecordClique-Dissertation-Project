import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { AuthGuard } from './guards/auth.guard';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';
import { MyProfileComponent } from './components/my-profile/my-profile.component';
import { UnauthorizedPageComponent } from './components/unauthorized-page/unauthorized-page.component';
import { IndexArtistComponent } from './components/crud-artist/index-artist/index-artist.component';
import { AddArtistComponent } from './components/crud-artist/add-artist/add-artist.component';
import { UpdateArtistComponent } from './components/crud-artist/update-artist/update-artist.component';
import { IndexAlbumComponent } from './components/crud-album/index-album/index-album/index-album.component';
import { AddAlbumComponent } from './components/crud-album/add-album/add-album/add-album.component';
import { UpdateAlbumComponent } from './components/crud-album/update-album/update-album.component';
import { IndexRecordLabelComponent } from './components/crud-record-label/index-record-label/index-record-label.component';
import { AddRecordLabelComponent } from './components/crud-record-label/add-record-label/add-record-label.component';
import { UpdateRecordLabelComponent } from './components/crud-record-label/update-record-label/update-record-label.component';
import { UserAlbumsComponent } from './components/user-albums/user-albums.component';
import { AssistantComponent } from './components/assistant/assistant.component';
import { AlbumDetailsComponent } from './components/album-details/album-details/album-details.component';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard]},
  { path: 'sign-up', component: SignupComponent},
  { path: 'reset', component: ResetPasswordComponent},
  { path: 'my-profile/:username', component: MyProfileComponent},
  { path: 'unauthorized', component: UnauthorizedPageComponent},
  { path: 'artists', component: IndexArtistComponent},
  { path: 'artists/add', component: AddArtistComponent},
  { path: 'artists/update/:id', component: UpdateArtistComponent},
  { path: 'albums', component: IndexAlbumComponent},
  { path: 'albums/add', component: AddAlbumComponent},
  { path: 'albums/update/:id', component: UpdateAlbumComponent},
  { path: 'record-labels', component: IndexRecordLabelComponent},
  { path: 'record-labels/add', component: AddRecordLabelComponent},
  { path: 'record-labels/update/:id', component: UpdateRecordLabelComponent},
  { path: 'users-page/:subtab', component: UserAlbumsComponent },
  { path: 'assistant', component: AssistantComponent},
  { path: 'album-details/:id', component: AlbumDetailsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
