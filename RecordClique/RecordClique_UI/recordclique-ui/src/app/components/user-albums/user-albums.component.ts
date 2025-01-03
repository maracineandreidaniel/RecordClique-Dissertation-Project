import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Album } from 'src/app/models/album.model';
import { AlbumsService } from 'src/services/albums/albums.service';
import { AuthenticationService } from 'src/services/authentication/authentication.service';
import { UserStoreService } from 'src/services/user-store/user-store.service';

@Component({
  selector: 'app-user-albums',
  templateUrl: './user-albums.component.html',
  styleUrls: ['./user-albums.component.css']
})
export class UserAlbumsComponent {
  actionLabel = '';
  albums: Album[] = [];
  userId: string = '';
  page: number = 1;
  pageSize: number = 3;
  totalPages: number = 0;
  tabRoute: string = '';
  type: number = 1;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private userStore: UserStoreService,
    private albumService: AlbumsService,
    private auth: AuthenticationService) {

    this.userStore.getIdFromStore().subscribe((val) => {
      let idFromToken = this.auth.getIdFromToken();
      this.userId = val || idFromToken;
    });

    this.route.params.subscribe(params => {
      this.tabRoute = params['subtab'];
        this.setParameters(this.tabRoute);
        
      });
  }

  ngOnInit(): void {

    this.loadAlbums(this.page, this.pageSize);
  }

  setParameters(route: string | undefined): void {
    switch (route) {
      case 'favourites':
        this.actionLabel = 'Favourite';
        this.type = 1;
        break;
      case 'listening':
        this.actionLabel = 'Listening';
        this.type = 2;
        break;
      case 'wishlist':
        this.actionLabel = 'Wishlist';
        this.type = 3;
        break;
      default:
        this.router.navigate(['/users-page/favourites']);
        return;
    }
    this.loadAlbums(this.page, this.pageSize);
  }

  loadAlbums(pageNumber: number, pageSize: number): void {

    this.albumService.getUserAllAlbums(pageNumber, pageSize, this.userId, this.type).subscribe(response => {
      this.albums = response.Items;
      this.totalPages = Math.ceil(response.TotalItems / pageSize);
      this.page = pageNumber;  
    });
  }

  pageChange(newPage: number): void {
    this.loadAlbums(newPage, this.pageSize);   
  }

  updateAlbumLink(albumId: string): void {
    this.albumService.updateUserAlbumLink(albumId, this.userId, false, this.type).subscribe((result) => {
      window.location.reload();
    });
  }
}
