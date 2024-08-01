import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { tap } from 'rxjs';
import { Artist } from 'src/app/models/artist.model';
import { ArtistsService } from 'src/services/artists/artists.service';

@Component({
  selector: 'app-update-artist',
  templateUrl: './update-artist.component.html',
  styleUrls: ['./update-artist.component.css']
})
export class UpdateArtistComponent {

  updateArtistForm!: FormGroup;

  constructor(private toaster: ToastrService, private fb: FormBuilder, private router: Router,  private route: ActivatedRoute, private artistService: ArtistsService){
    this.updateArtistForm = this.fb.group({
      name: [''],
      picture: [''],
      biography: [''],
    });
  }

  ngOnInit(): void {
    this.setDefaultValues();
  }
  
  updateArtist() {
    if (this.updateArtistForm.valid) {
      const artist: Artist = {
        Id: this.route.snapshot.paramMap.get('id')!,
        Name: this.updateArtistForm.value.name,
        Picture: this.updateArtistForm.value.picture,
        Biography: this.updateArtistForm.value.biography
      };

      this.artistService.updateArtist(artist).subscribe({
        next: (res) => {
          this.router.navigate(['artists']);
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
    this.artistService.getArtistById(this.route.snapshot.paramMap.get('id')!).pipe(
      tap((artist: Artist) => {
        this.updateArtistForm.patchValue({
          name: artist.Name,
          picture: artist.Picture,
          biography: artist.Biography
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