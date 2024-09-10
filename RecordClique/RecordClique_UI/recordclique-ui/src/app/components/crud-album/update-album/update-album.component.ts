import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { tap } from 'rxjs';
import { Album } from 'src/app/models/album.model';
import { AlbumsService } from 'src/services/albums/albums.service';

interface SelectOption {
  id: string;
  name: string;
}

@Component({
  selector: 'app-update-album',
  templateUrl: './update-album.component.html',
  styleUrls: ['./update-album.component.css']
})
export class UpdateAlbumComponent {

  updateAlbumForm!: FormGroup;


  genresOptions: SelectOption[] = [
    { id: '4FA85F64-5717-4562-B3FC-2C963F66AFA6', name: 'Rock' },
    { id: '5FA85F64-5717-4562-B3FC-2C963F66AFA6', name: 'Jazz' },
  ];
  
  artistsOptions: SelectOption[] = [
    { id: '43EE4B7B-286D-4FF2-DC14-08DCC78198DB', name: 'Artist One' },
    { id: '1EA85F64-5717-4562-B3FC-2C963F66AFA6', name: 'Artist Two' },
    { id: '1EA85F64-5717-4562-B3FC-2C963F66AFA9', name: 'Artist Three' },
  ];

  selectedArtists: string[] = ['43EE4B7B-286D-4FF2-DC14-08DCC78198DB','1EA85F64-5717-4562-B3FC-2C963F66AFA6'];


  toppings = new FormControl();
  toppingList: string[] = ['Extra cheese', 'Mushroom', 'Onion', 'Pepperoni', 'Sausage', 'Tomato'];
  selected: string[] = this.toppingList.filter((item, i) => i % 2 === 0);

  constructor(private toaster: ToastrService, private fb: FormBuilder, private router: Router,  private route: ActivatedRoute, private albumService: AlbumsService){
    this.updateAlbumForm = this.fb.group({
      title: [''],
      cover: [''],
      description: [''],
      releaseDate: [''],
      genres: [[]],
      artists: [[]]
    });
    
  }
  

  ngOnInit(): void {
    this.setDefaultValues();
    
  }
  
  updateAlbum() {
    if (this.updateAlbumForm.valid) {
      const album: Album = {
        Id: this.route.snapshot.paramMap.get('id')!,
        Title: this.updateAlbumForm.value.title,
        Cover: this.updateAlbumForm.value.cover,
        Description: this.updateAlbumForm.value.description,
        ReleaseDate: this.updateAlbumForm.value.releaseDate,
        Genres: this.updateAlbumForm.value.genres,
        Artists: this.updateAlbumForm.value.artist,
        RecordLabel: '7FA85F64-5717-4562-B3FC-2C963F66AFA6',
      };

      this.albumService.updateAlbum(album).subscribe({
        next: (res) => {
          this.router.navigate(['albums']);
        },
        error: (err) => {
          this.toaster.error(err.message || 'An error occurred', 'ERROR', {
            timeOut: 5000
          });
        }
      });
    } else {
      this.toaster.error('Form is invalid', 'ERROR', {
        timeOut: 5000
      });
    }
  }

  setDefaultValues() {
    this.albumService.getAlbumById(this.route.snapshot.paramMap.get('id')!).pipe(
      tap((album: Album) => {
        
        this.updateAlbumForm.patchValue({
          title: album.Title,
          cover: album.Cover,
          description: album.Description,
          releaseDate: album.ReleaseDate
          //genres: album.Genres, 
          //artists: album.Artists 
        });
        console.log(album.Genres);
        console.log(album.Artists);
      })
    ).subscribe({
      error: (err) => {
        this.toaster.error(err.message || 'An error occurred while fetching artist data', 'ERROR', {
          timeOut: 5000
        });
      }
    });
  }
  

}
