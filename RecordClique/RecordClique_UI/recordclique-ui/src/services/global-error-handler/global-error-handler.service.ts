import { HttpErrorResponse } from '@angular/common/http';
import { ErrorHandler, Inject, Injectable, Injector } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class GlobalErrorHandler implements ErrorHandler {

  constructor(@Inject(Injector) private readonly injector: Injector) { }

  private get toastService() {
    return this.injector.get(ToastrService);
}

  handleError(error: any): void {
      console.log(error);
      if(error instanceof HttpErrorResponse){
        this.toastService.error(error.error);
      }
  }
}
