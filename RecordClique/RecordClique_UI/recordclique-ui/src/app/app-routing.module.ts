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

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
