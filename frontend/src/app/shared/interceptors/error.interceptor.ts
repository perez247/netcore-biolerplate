import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HTTP_INTERCEPTORS, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { AppRoute } from '../routes/app.routes';
import { NgRedux } from '@angular-redux/store';
import { IAppState } from '../state/store';
import { AuthUserActionConstant } from '../state/auth-user-state/auth-user-action-constant';
import { IServerError } from '../domain/AppErrors';
import { NotifyService } from '../services/common/notify/notify.service';

@Injectable()

/**
 * Class to catch all kind of http error send from the server
 */
 export class ErrorInterceptor implements HttpInterceptor {

    routes = AppRoute.generateRoutes();

    constructor(private router: Router, private redux: NgRedux<IAppState>, private notify: NotifyService) {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(


            catchError((error) => {

                console.log(error);

                if (error instanceof HttpErrorResponse) {

                    if (error.status === 424) {

                      this.router.navigate([this.routes.public.sendEmailVerification().$absolutePath]);

                      // Throw error back
                      return throwError(error);
                    }

                    // If user has unauthorized access then they have to go back to the sign in page to
                    // give credentials to have access
                    if (error.status === 401) {

                        this.redux.dispatch({ type: AuthUserActionConstant.LOG_OUT });

                        this.router.navigate(['public/signin'], {
                            queryParams : {
                            returnUrl : this.router.url
                            }
                        });

                        return throwError('Login required');
                    }

                    // If its an internal error from server then show the message send back from the server
                    if (error.status === 500) {
                      const serverError = error.error as IServerError;

                      // If its a single error
                      if (serverError.error) {

                        // If length of error is <= 100 then present it
                        if (serverError.error.length <= 100) {
                          this.notify.error(serverError.error);

                          // else show default error and console log stack trace
                          // stacktrace will show in a development of staging environment
                        } else {
                          this.notify.error('It seems something went wrong, kindly try again later');

                          if (serverError.stackTrace) {
                            this.notify.error('Check console for error : Stack trace');
                            console.log(serverError.stackTrace);
                          }
                        }
                      }

                      // Throw error back
                      return throwError(error);
                    }

                }

                return throwError(error);
            })

        );
    }

}

export const ErrorInterceptorProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorInterceptor,
    multi: true
};
