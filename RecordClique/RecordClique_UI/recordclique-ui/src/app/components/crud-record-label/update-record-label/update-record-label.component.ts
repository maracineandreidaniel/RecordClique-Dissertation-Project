import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { tap } from 'rxjs';
import { RecordLabel } from 'src/app/models/record-label.model';
import { RecordLabelsService } from 'src/services/record-labels/record-labels.service';

@Component({
  selector: 'app-update-record-label',
  templateUrl: './update-record-label.component.html',
  styleUrls: ['./update-record-label.component.css']
})
export class UpdateRecordLabelComponent {

  updateRecordLabelForm!: FormGroup;

  constructor(private toaster: ToastrService, private fb: FormBuilder, private router: Router,  private route: ActivatedRoute, private recordLabelService: RecordLabelsService){
    this.updateRecordLabelForm = this.fb.group({
      name: [''],
      picture: [''],
      biography: [''],
    });
  }

  ngOnInit(): void {
    this.setDefaultValues();
  }
  
  updateRecordLabel() {
    if (this.updateRecordLabelForm.valid) {
      const recordLabel: RecordLabel = {
        Id: this.route.snapshot.paramMap.get('id')!,
        Name: this.updateRecordLabelForm.value.name,
        Picture: this.updateRecordLabelForm.value.picture,
        Biography: this.updateRecordLabelForm.value.biography
      };

      this.recordLabelService.updateRecordLabel(recordLabel).subscribe({
        next: (res) => {
          this.router.navigate(['record-labels']);
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
    this.recordLabelService.getRecordLabelById(this.route.snapshot.paramMap.get('id')!).pipe(
      tap((recordLabel: RecordLabel) => {
        this.updateRecordLabelForm.patchValue({
          name: recordLabel.Name,
          picture: recordLabel.Picture,
          biography: recordLabel.Biography
        });
      })
    ).subscribe({
      error: (err) => {
        this.toaster.error(err.message || 'An error occurred while fetching record label data', 'ERROR', {
          timeOut: 5000
        });
      }
    });
  }

}
