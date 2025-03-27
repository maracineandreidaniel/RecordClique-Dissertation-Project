import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Review } from 'src/app/models/review.model';
import { Track } from 'src/app/models/track.model';
import { AuthenticationService } from 'src/services/authentication/authentication.service';
import { ReviewsService } from 'src/services/reviews/reviews.service';
import { TracksService } from 'src/services/tracks/tracks.service';
import { UserStoreService } from 'src/services/user-store/user-store.service';

@Component({
  selector: 'app-album-details',
  templateUrl: './album-details.component.html',
  styleUrls: ['./album-details.component.css']
})
export class AlbumDetailsComponent {

  tracks: Track[] = [];
  reviews: Review[] = [];
  page: number = 1;
  pageSize: number = 10;
  totalPages: number = 0;
  selectedReviewId! : string | ' ';
  addReviewForm!: FormGroup;
  albumId!: string;
  userId: string = '';
  audio = new Audio();
  isPlaying = false;
  currentTrack: Track | null = null;

  constructor( private fb: FormBuilder, 
      private router: Router,  
      private route: ActivatedRoute,
      private tracksService: TracksService,
      private reviewsService: ReviewsService,
      private userStore: UserStoreService,
      private toast: ToastrService,
      private auth: AuthenticationService) {
        this.addReviewForm = this.fb.group({
          rating: [0, Validators.required],
          comment: ['', [Validators.required, Validators.minLength(5)]] 
        });

        this.albumId = this.route.snapshot.paramMap.get('id')!;

        this.userStore.getIdFromStore().subscribe((val) => {
          let idFromToken = this.auth.getIdFromToken();
          this.userId = val || idFromToken;
        });
      }

  ngOnInit(): void {
    this.tracksService.getTracks(this.albumId).subscribe((data) => {
      this.tracks = data;
    });

    this.loadScreenings(this.page, this.pageSize);  
  }

  pageChange(newPage: number): void {
    this.loadScreenings(newPage, this.pageSize);  
  }

  loadScreenings(pageNumber: number, pageSize: number): void {
     this.reviewsService.getReviews(pageNumber, pageSize, this.albumId)
      .subscribe({
        next: (res) => {
          this.reviews = res.Items;
          this.totalPages = Math.ceil(res.TotalItems / pageSize);
          this.page = pageNumber;            
        },
        error: (err) => {
          console.error(err);
        }
      });
  }

  closeDeleteModal() {
    const modal = $('#deleteReviewModal');
    (modal as any).modal('hide');
  }

  setSelectedReviewId(reviewId: string) : void{
    this.selectedReviewId = reviewId;
  }

  deleteReview(id: string) {
    this.reviewsService.deleteReview(id).subscribe({
      next: (response) => {
        this.toast.success("Review was sucesfully deleted!", "SUCESS");
        window.location.reload();
      },
      error: (err) => {
        console.error('There was an error:', err);
        this.toast.warning(err, "Error");
      },
    });
  }

  closeAddModal() {
    const modal = $('#addReviewModal');
    (modal as any).modal('hide');
  }


  onStarSelected(stars: number) {
    this.addReviewForm.patchValue({ rating: stars });
  }

  addReview() {
      if (this.addReviewForm.valid) {
        const review: Review = {
          Id: '00000000-0000-0000-0000-000000000000',
          Comment: this.addReviewForm.value.comment,
          Rating: this.addReviewForm.value.rating,
          FK_AlbumId: this.albumId,
          FK_UserId: this.userId
        };
  
        this.reviewsService.addReview(review).subscribe({
          next: (res) => {
            window.location.reload();
          },
          error: (err) => {
            this.toast.error(err.message || 'An error occurred', 'ERROR', {
              timeOut: 5000
            });
          }
        });
      } else {
        this.toast.error('Form is invalid', 'ERROR', {
          timeOut: 5000
        });
      }
    }

    togglePlayPause(track: Track) {
      if (this.currentTrack && this.currentTrack.Id === track.Id) {
        if (this.isPlaying) {
          this.audio.pause();
        } else {
          this.audio.play();
        }
        this.isPlaying = !this.isPlaying;
      } else {
        if (!this.audio.paused) {
          this.audio.pause();
        }
        this.audio.src = track.Path;
        this.audio.load();
        this.audio.play();
        this.currentTrack = track;
        this.isPlaying = true;
  
        this.audio.onended = () => {
          this.isPlaying = false;
          this.currentTrack = null;
        };
      }
    }
}
