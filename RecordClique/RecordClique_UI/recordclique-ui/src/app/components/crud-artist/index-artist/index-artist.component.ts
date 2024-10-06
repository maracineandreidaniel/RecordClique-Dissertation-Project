import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Artist } from 'src/app/models/artist.model';
import { ArtistsService } from 'src/services/artists/artists.service';

@Component({
  selector: 'app-index-artist',
  templateUrl: './index-artist.component.html',
  styleUrls: ['./index-artist.component.css']
})
export class IndexArtistComponent {
  searchArtistForm!: FormGroup;
  artists: Artist[] = [];
  selectedArtistId! : string | ' ';
  page: number = 1;
  pageSize: number = 5;
  totalPages: number = 0;

  constructor(
    private artistService: ArtistsService,
    private fb: FormBuilder,
    private toast: ToastrService
  ) {
    this.searchArtistForm = this.fb.group({
      filter: ['']
    });
  }

  ngOnInit(): void {
    this.loadScreenings(this.page, this.pageSize);  
  }

  deleteArtist(id: string) {
    this.artistService.deleteArtist(id).subscribe({
      next: (response) => {
        this.toast.success("Artist was sucesfully deleted!", "SUCESS");
        window.location.reload();
      },
      error: (err) => {
        console.error('There was an error:', err);
        this.toast.warning(err, "Error");
      },
    });
  }
  
  closeFiltersModal() {
    const modal = $('#filtersModal');
    (modal as any).modal('hide');
  }

  searchArtist() {
    if (this.searchArtistForm.valid) {
      this.page = 1;
      this.loadScreenings(this.page, this.pageSize);
      this.closeFiltersModal();
    }
  }
  
  closeDeleteModal() {
    const modal = $('#deleteArtistModal');
    (modal as any).modal('hide');
  }

  setSelectedArtistId(screeningId: string) : void{
    this.selectedArtistId = screeningId;
  }

  pageChange(newPage: number): void {
    this.loadScreenings(newPage, this.pageSize);  
  }

  loadScreenings(pageNumber: number, pageSize: number): void {
     const formValues = this.searchArtistForm.value;
     this.artistService.getArtists(pageNumber, pageSize, formValues.filter)
      .subscribe({
        next: (res) => {
         
          this.artists = res.Items;
          this.totalPages = Math.ceil(res.TotalItems / pageSize);
          this.page = pageNumber;            
        },
        error: (err) => {
          console.error(err);
        }
      });
  }
}
