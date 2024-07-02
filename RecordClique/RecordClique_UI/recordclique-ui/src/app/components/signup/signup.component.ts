import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import ValidateForm from 'src/app/helpers/validate-form';
import { AuthenticationService } from 'src/services/authentication/authentication.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
})
export class SignupComponent {
  signUpForm!: FormGroup;
  submitted = false;
  passwordFields = {
    password: { isText: false, eyeIcon: 'fa-eye-slash', type: 'password' },
    confirmedPassword: { isText: false, eyeIcon: 'fa-eye-slash', type: 'password' }
  };

  constructor(
    private fb: FormBuilder,
    private authenticationService: AuthenticationService,
    private toaster: ToastrService
  ) {}

  ngOnInit(): void {
    this.signUpForm = this.fb.group(
      {
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        userName: ['', Validators.required],
        password: ['', Validators.required],
        confirmedPassword: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
      },
      {
        validators: this.MustMatch('password', 'confirmedPassword'),
      }
    );
  }

  get f() {
    return this.signUpForm.controls;
  }

  MustMatch(controlName: string, matchingControlName: string) {
    return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];
      if (matchingControl.errors && !matchingControl.errors.MustMatch) {
        return;
      }
      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ MustMatch: true });
      } else {
        matchingControl.setErrors(null);
      }
    };
  }

  hideShowPass(field: 'password' | 'confirmedPassword') {
    const fieldState = this.passwordFields[field];
    fieldState.isText = !fieldState.isText;
    fieldState.eyeIcon = fieldState.isText ? 'fa-eye' : 'fa-eye-slash';
    fieldState.type = fieldState.isText ? 'text' : 'password';
  }

  onSignUp() {
    this.submitted = true;
    if (this.signUpForm.valid) {
      this.authenticationService.signUp(this.signUpForm.value).subscribe({
        next: (res) => {
          this.toaster.success(res.message, "SUCCES", {
            timeOut: 5000
          });
        },
        error: (err) => {
          this.toaster.error(err, "ERROR", {
            timeOut: 5000
          });
        },
      });
    } else {
      ValidateForm.validateAllFormFields(this.signUpForm);
      alert('Your form is invalid!');
    }
  }
}
