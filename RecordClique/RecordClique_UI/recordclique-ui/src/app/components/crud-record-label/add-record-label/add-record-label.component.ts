import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { RecordLabel } from 'src/app/models/record-label.model';
import { RecordLabelsService } from 'src/services/record-labels/record-labels.service';

@Component({
  selector: 'app-add-record-label',
  templateUrl: './add-record-label.component.html',
  styleUrls: ['./add-record-label.component.css']
})
export class AddRecordLabelComponent {

  addRecordLabelForm!: FormGroup;

  constructor(private toaster: ToastrService, private fb: FormBuilder, private router: Router, private recordLabelsService: RecordLabelsService){
    this.addRecordLabelForm = this.fb.group({
      name: ['', Validators.required],
      picture: ['', Validators.required],
      biography: ['', Validators.required],
    });
  }

  ngOnInit(): void {
  }
  
  addRecordLabel() {
    if (this.addRecordLabelForm.valid) {
      const recordLabel: RecordLabel = {
        Id: '00000000-0000-0000-0000-000000000000',
        Name: this.addRecordLabelForm.value.name,
        Picture: this.addRecordLabelForm.value.picture,
        Biography: this.addRecordLabelForm.value.biography
      };

      this.recordLabelsService.addRecordLabel(recordLabel).subscribe({
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

}
