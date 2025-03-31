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
import { RecordLabelsService } from 'src/services/record-labels/record-labels.service';

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
  recordLabelOptions!: SelectOptionResult[];

  constructor(private toaster: ToastrService, 
    private fb: FormBuilder, 
    private router: Router,  
    private route: ActivatedRoute, 
    private albumService: AlbumsService, 
    private artistService: ArtistsService, 
    private genreService: GenresService,
    private recordLabelService: RecordLabelsService){
    this.updateAlbumForm = this.fb.group({
      title: [''],
      cover: [''],
      description: [''],
      releaseDate: [''],
      genres: [[]],
      artists: [[]],
      recordLabel: [[]]
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
        Artists: this.updateAlbumForm.value.artists,
        RecordLabel: this.updateAlbumForm.value.recordLabel
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
          artists:  album.Artists,
          recordLabel: album.RecordLabel
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

    this.recordLabelService.getRecordLabelSelectOptions().subscribe({
      next: (res) => {
        this.recordLabelOptions = res;
      },
      error: (err) => {
        this.toaster.error(err.message || 'An error occurred', 'ERROR', { timeOut: 5000 });
      }
    });

  }
  

}
