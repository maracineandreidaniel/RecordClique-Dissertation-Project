import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Album } from 'src/app/models/album.model';
import { AlbumsService } from 'src/services/albums/albums.service';

@Component({
  selector: 'app-index-album',
  templateUrl: './index-album.component.html',
  styleUrls: ['./index-album.component.css']
})
export class IndexAlbumComponent {
   // filterScreeningsForm!: FormGroup;
   albums: Album[] = [];
   selectedAlbumId! : string | ' ';
   page: number = 1;
   pageSize: number = 5;
   totalPages: number = 0;
 
   constructor(
     private albumService: AlbumsService,
     private fb: FormBuilder,
     private toast: ToastrService
   ) {
     // this.filterScreeningsForm = this.fb.group({
     //   date: [''],
     //   room: [''],
     //   genre: [''],
     // });
   }
 
   ngOnInit(): void {
     this.loadScreenings(this.page, this.pageSize);  
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
 
   // filterScreenings() {
   //   if (this.filterScreeningsForm.valid) {
   //     this.page = 1;
   //     this.loadScreenings(this.page, this.pageSize);
   //     this.closeFiltersModal();
   //   }
   // }
   
 
   closeDeleteModal() {
     const modal = $('#deleteAlbumModal');
     (modal as any).modal('hide');
   }
 
   setSelectedAlbumId(albumId: string) : void{
     this.selectedAlbumId = albumId;
   }
 
   pageChange(newPage: number): void {
     this.loadScreenings(newPage, this.pageSize);  
   }
 
   loadScreenings(pageNumber: number, pageSize: number): void {
     // const formValues = this.filterScreeningsForm.value;
      this.albumService.getAlbums(pageNumber, pageSize)
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
