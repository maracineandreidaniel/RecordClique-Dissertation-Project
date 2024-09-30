import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { tap } from 'rxjs';
import { Album } from 'src/app/models/album.model';
import { SelectOptionResult } from 'src/app/models/select-option-result.model';
import { AlbumsService } from 'src/services/albums/albums.service';
import { ArtistsService } from 'src/services/artists/artists.service';
import { GenresService } from 'src/services/genres/genres.service';

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
  genresOptions!: SelectOptionResult[];
  artistsOptions!: SelectOptionResult[];

  constructor(private toaster: ToastrService, 
    private fb: FormBuilder, 
    private router: Router,  
    private route: ActivatedRoute, 
    private albumService: AlbumsService, 
    private artistService: ArtistsService, 
    private genreService: GenresService){
    this.updateAlbumForm = this.fb.group({
      title: [''],
      cover: [''],
      description: [''],
      releaseDate: [''],
      genres: [[]],
      artists: [[]]
    });
        this.setDefaultValues();
  }
  
  ngOnInit(): void {}
  
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
          releaseDate: album.ReleaseDate,
          genres: album.Genres, 
          artists:  album.Artists
        });
      })
    ).subscribe({
      error: (err) => {
        this.toaster.error(err.message || 'An error occurred while fetching artist data', 'ERROR', {
          timeOut: 5000
        });
      }
    });

    this.artistService.getArtistSelectOptions().subscribe({
      next: (res) => {
        this.artistsOptions = res;
      },
      error: (err) => {
        this.toaster.error(err.message || 'An error occurred', 'ERROR', { timeOut: 5000 });
      }
    });

    this.genreService.getGenreSelectOptions().subscribe({
      next: (res) => {
        this.genresOptions = res;
      },
      error: (err) => {
        this.toaster.error(err.message || 'An error occurred', 'ERROR', { timeOut: 5000 });
      }
    });

  }
  

}
