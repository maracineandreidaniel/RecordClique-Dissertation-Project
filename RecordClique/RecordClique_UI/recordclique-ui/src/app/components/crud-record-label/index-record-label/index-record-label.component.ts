import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { RecordLabel } from 'src/app/models/record-label.model';
import { RecordLabelsService } from 'src/services/record-labels/record-labels.service';

@Component({
  selector: 'app-index-record-label',
  templateUrl: './index-record-label.component.html',
  styleUrls: ['./index-record-label.component.css']
})
export class IndexRecordLabelComponent {
    // filterScreeningsForm!: FormGroup;
    recordLabels: RecordLabel[] = [];
    selectedRecordLabelId! : string | ' ';
    page: number = 1;
    pageSize: number = 5;
    totalPages: number = 0;
  
    constructor(
      private recordLabelService: RecordLabelsService,
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
      this.loadRecordLabels(this.page, this.pageSize);  
    }
  
    deleteRecordLabel(id: string) {
      this.recordLabelService.deleteRecordLabel(id).subscribe({
        next: (response) => {
          this.toast.success("Record Label was sucesfully deleted!", "SUCESS");
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
      const modal = $('#deleteRecordLabelModal');
      (modal as any).modal('hide');
    }
  
    setSelectedRecordLabelId(screeningId: string) : void{
      this.selectedRecordLabelId = screeningId;
    }
  
    pageChange(newPage: number): void {
      this.loadRecordLabels(newPage, this.pageSize);  
    }
  
    loadRecordLabels(pageNumber: number, pageSize: number): void {
      // const formValues = this.filterScreeningsForm.value;
       this.recordLabelService.getRecordLabels(pageNumber, pageSize)
        .subscribe({
          next: (res) => {
           
            this.recordLabels = res.Items;
            this.totalPages = Math.ceil(res.TotalItems / pageSize);
            this.page = pageNumber;            
          },
          error: (err) => {
            console.error(err);
          }
        });
    }
}
