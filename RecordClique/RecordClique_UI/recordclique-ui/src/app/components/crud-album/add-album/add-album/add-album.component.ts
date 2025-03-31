import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Album } from 'src/app/models/album.model';
import { SelectOptionResult } from 'src/app/models/select-option-result.model';
import { AlbumsService } from 'src/services/albums/albums.service';
import { ArtistsService } from 'src/services/artists/artists.service';
import { GenresService } from 'src/services/genres/genres.service';
import { RecordLabelsService } from 'src/services/record-labels/record-labels.service';

@Component({
  selector: 'app-add-album',
  templateUrl: './add-album.component.html',
  styleUrls: ['./add-album.component.css']
})
export class AddAlbumComponent implements OnInit {

  addAlbumForm!: FormGroup;
  genresOptions!: SelectOptionResult[];
  artistsOptions!: SelectOptionResult[];
  recordLabelOptions!: SelectOptionResult[];

  constructor(
    private toaster: ToastrService, 
    private fb: FormBuilder, 
    private router: Router, 
    private albumService: AlbumsService,
    private artistService: ArtistsService,
    private genreService: GenresService,
    private recordLabelService: RecordLabelsService
  ) {
    this.addAlbumForm = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      cover: ['', Validators.required],
      releaseDate: ['', Validators.required],
      genres: [[], Validators.required],
      artists: [[], Validators.required],
      recordLabel: [[], Validators.required]
    });

    this.setDefaultValues();
  }

  ngOnInit(): void {}

  addAlbum() {
    if (this.addAlbumForm.valid) {
      const album: Album = {
        Id: '00000000-0000-0000-0000-000000000000',
        Title: this.addAlbumForm.value.title,
        Description: this.addAlbumForm.value.description,
        Cover: this.addAlbumForm.value.cover,
        ReleaseDate: this.addAlbumForm.value.releaseDate,
        RecordLabel: this.addAlbumForm.value.recordLabel,
        Genres: this.addAlbumForm.value.genres,
        Artists: this.addAlbumForm.value.artists
      };

      this.albumService.addAlbum(album).subscribe({
        next: (res) => {
          this.router.navigate(['albums']);
        },
        error: (err) => {
          this.toaster.error(err.message || 'An error occurred', 'ERROR', { timeOut: 5000 });
        }
      });
    } else {
      this.toaster.error('Form is invalid', 'ERROR', { timeOut: 5000 });
    }
  }

  setDefaultValues(){
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
