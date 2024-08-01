import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Artist } from 'src/app/models/artist.model';
import { ArtistsService } from 'src/services/artists/artists.service';

@Component({
  selector: 'app-add-artist',
  templateUrl: './add-artist.component.html',
  styleUrls: ['./add-artist.component.css']
})
export class AddArtistComponent {

  addArtistForm!: FormGroup;

  constructor(private toaster: ToastrService, private fb: FormBuilder, private router: Router, private artistService: ArtistsService){
    this.addArtistForm = this.fb.group({
      name: ['', Validators.required],
      picture: ['', Validators.required],
      biography: ['', Validators.required],
    });
  }

  ngOnInit(): void {
  }
  
  addArtist() {
    if (this.addArtistForm.valid) {
      const artist: Artist = {
        Id: '00000000-0000-0000-0000-000000000000',
        Name: this.addArtistForm.value.name,
        Picture: this.addArtistForm.value.picture,
        Biography: this.addArtistForm.value.biography
      };

      this.artistService.addArtist(artist).subscribe({
        next: (res) => {
          this.router.navigate(['index-artist']);
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

}

