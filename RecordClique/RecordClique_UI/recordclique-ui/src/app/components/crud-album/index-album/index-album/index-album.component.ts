import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Album } from 'src/app/models/album.model';
import { SelectOptionResult } from 'src/app/models/select-option-result.model';
import { AlbumsService } from 'src/services/albums/albums.service';
import { ArtistsService } from 'src/services/artists/artists.service';
import { GenresService } from 'src/services/genres/genres.service';

@Component({
  selector: 'app-index-album',
  templateUrl: './index-album.component.html',
  styleUrls: ['./index-album.component.css']
})
export class IndexAlbumComponent {
   filterAlbumsForm!: FormGroup;
   searchAlbumForm!: FormGroup;
   albums: Album[] = [];
   selectedAlbumId! : string | ' ';
   page: number = 1;
   pageSize: number = 5;
   totalPages: number = 0;
   artistOptions: SelectOptionResult[] = [];
   genreOptions: SelectOptionResult[] = [];
   defaultValueFilterArtist = "00000000-0000-0000-0000-000000000000";
   defaultValueFilterGenre = "00000000-0000-0000-0000-000000000000";
 
   constructor(
     private albumService: AlbumsService,
     private fb: FormBuilder,
     private toast: ToastrService,
     private genreService: GenresService,
     private artistService: ArtistsService
   ) {
     this.filterAlbumsForm = this.fb.group({
       artist: [''],
       genre: [''],
       year: ['']
     });

     this.searchAlbumForm = this.fb.group({
      filter: ['']
    });

     this.artistService.getArtistSelectOptions().subscribe({
      next: (res: any) => {
        this.artistOptions = res;
      },
      error: (err) => {
        console.log(err);
      },
    });

    this.genreService.getGenreSelectOptions().subscribe({
      next: (res: any) => {
        this.genreOptions = res;
      },
      error: (err) => {
        console.log(err);
      },
    });
   }
 
   ngOnInit(): void {
     this.loadAlbums(this.page, this.pageSize);  
   }
 
   deleteAlbum(id: string) {
     this.albumService.deleteAlbum(id).subscribe({
       next: (response) => {
         this.toast.success("Album was sucesfully deleted!", "SUCESS");
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

   searchAlbum() {
    if (this.searchAlbumForm.valid) {
      this.page = 1;
      this.loadAlbums(this.page, this.pageSize);
    }
  }
 
   filterAlbums() {
     if (this.filterAlbumsForm.valid) {
       this.page = 1;
       this.loadAlbums(this.page, this.pageSize);
       this.closeFiltersModal();
     }
   }

   clearFilters() {
    this.filterAlbumsForm.get('year')?.setValue('');
    this.filterAlbumsForm.get('artist')?.setValue("00000000-0000-0000-0000-000000000000");
    this.filterAlbumsForm.get('genre')?.setValue("00000000-0000-0000-0000-000000000000");
  } 
 
   closeDeleteModal() {
     const modal = $('#deleteAlbumModal');
     (modal as any).modal('hide');
   }
 
   setSelectedAlbumId(albumId: string) : void{
     this.selectedAlbumId = albumId;
   }
 
   pageChange(newPage: number): void {
     this.loadAlbums(newPage, this.pageSize);   
   }
 
   loadAlbums(pageNumber: number, pageSize: number): void {
      const formValues = this.filterAlbumsForm.value;
       this.albumService.getAlbums(pageNumber, pageSize, this.searchAlbumForm.value.filter, formValues.artist, formValues.genre, formValues.year)
       .subscribe({
         next: (res) => {
           this.albums = res.Items;
           this.totalPages = Math.ceil(res.TotalItems / pageSize);
           this.page = pageNumber;            
         },
         error: (err) => {
           console.error(err);
         }
       });
   }
}
