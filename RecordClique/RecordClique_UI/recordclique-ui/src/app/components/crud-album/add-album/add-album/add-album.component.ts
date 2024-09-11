import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Album } from 'src/app/models/album.model';
import { AlbumsService } from 'src/services/albums/albums.service';

interface SelectOption {
  id: string;
  name: string;
}

@Component({
  selector: 'app-add-album',
  templateUrl: './add-album.component.html',
  styleUrls: ['./add-album.component.css']
})
export class AddAlbumComponent implements OnInit {

  addAlbumForm!: FormGroup;
  
  genresOptions: SelectOption[] = [
    { id: '4fa85f64-5717-4562-b3fc-2c963f66afa6', name: 'Rock' },
    { id: '5fa85f64-5717-4562-b3fc-2c963f66afa6', name: 'Jazz' },
  ];
  
  artistsOptions: SelectOption[] = [
    { id: '43ee4b7b-286d-4ff2-dc14-08dcc78198db', name: 'Artist One' },
    { id: '1ea85f64-5717-4562-b3fc-2c963f66afa6', name: 'Artist Two' },
    { id: '1ea85f64-5717-4562-b3fc-2c963f66afa9', name: 'Artist Three' },
  ];

  constructor(
    private toaster: ToastrService, 
    private fb: FormBuilder, 
    private router: Router, 
    private albumService: AlbumsService
  ) {
    this.addAlbumForm = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      cover: ['', Validators.required],
      releaseDate: ['', Validators.required],
      genres: [[], Validators.required],
      artists: [[], Validators.required]
    });
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
        RecordLabel: '7FA85F64-5717-4562-B3FC-2C963F66AFA6',
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
}
