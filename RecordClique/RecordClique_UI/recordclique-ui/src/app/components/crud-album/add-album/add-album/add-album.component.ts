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
