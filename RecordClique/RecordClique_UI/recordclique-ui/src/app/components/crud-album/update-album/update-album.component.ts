import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
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

  genres: SelectOption[] = [
    { id: '4FA85F64-5717-4562-B3FC-2C963F66AFA6', name: 'Rock' },
    { id: '2f4e8900-1a11-4fc0-a5f8-e3bbf9ebc9e2', name: 'Pop' },
    { id: '5FA85F64-5717-4562-B3FC-2C963F66AFA6', name: 'Jazz' },
    { id: '4f4e8900-1a11-4fc0-a5f8-e3bbf9ebc9e4', name: 'Classical' }
  ];
  
  artists: SelectOption[] = [
    { id: '43EE4B7B-286D-4FF2-DC14-08DCC78198DB', name: 'Artist One' },
    { id: '1EA85F64-5717-4562-B3FC-2C963F66AFA6', name: 'Artist Two' },
    { id: '3a6e6700-2a22-5dc0-b6f9-f4cc0fae9e13', name: 'Artist Three' },
    { id: '4a6e6700-2a22-5dc0-b6f9-f4cc0fae9e14', name: 'Artist Four' }
  ];

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
        // Ensure the form is patched with the correct values for the multi-selects
        this.updateAlbumForm.patchValue({
          title: album.Title,
          cover: album.Cover,
          description: album.Description,
          releaseDate: album.ReleaseDate,
          genres: album.Genres, // Ensure this is an array of genre IDs
          artists: album.Artists // Ensure this is an array of artist IDs
        });
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
