import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Album } from 'src/app/models/album.model';
import { AlbumsService } from 'src/services/albums/albums.service';
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
  pageSize: number = 5;
  totalPages: number = 0;
  tabRoute: string = '';
  type: number = 1;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private userStore: UserStoreService,
    private albumService: AlbumsService) {
    this.route.params.subscribe(params => {
    this.tabRoute = params['subtab'];
      this.setParameters(this.tabRoute);
    });

    this.userStore.getRoleFromStore().subscribe((val) => {
      this.userId = val;
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

    this.albumService.getUserAllAlbums(pageNumber, pageSize, '88F905C0-27C3-4539-5596-08DCA0473827', this.type).subscribe(response => {
      this.albums = response.Items;
      this.totalPages = Math.ceil(response.TotalItems / pageSize);
      this.page = pageNumber;  
    });
  }

  handleAction(albumId: string): void {
    console.log(`${this.actionLabel} clicked for Album ID: ${albumId}`);
  }

  pageChange(newPage: number): void {
    this.loadAlbums(newPage, this.pageSize);   
  }
}
