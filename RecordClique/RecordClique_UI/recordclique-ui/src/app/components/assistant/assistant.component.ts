import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AlbumsService } from 'src/services/albums/albums.service';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-assistant',
  templateUrl: './assistant.component.html',
  styleUrls: ['./assistant.component.css']
})
export class AssistantComponent {

 assistantFormGroup!: FormGroup;
 generatedMessage: string | null = null;

  constructor(private albumService: AlbumsService,
    private fb: FormBuilder
  ) {
    this.assistantFormGroup = this.fb.group({
      message: ['']
    });
   }
  
displayText() {
    this.albumService.getGPTResponse(this.assistantFormGroup.value.message).subscribe({
      next: (res) => {
        this.generatedMessage = res.Message;
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

}
