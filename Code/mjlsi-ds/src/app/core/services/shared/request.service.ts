import {Injectable} from '@angular/core';
import {Router} from '@angular/router';
import {Observable, throwError as observableThrowError} from 'rxjs';
import { AuthenticationService } from '../../auth/authentication.service';
import { HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import { catchError } from 'rxjs/operators';


@Injectable()
export class RequestService {
	constructor(
		private http: HttpClient, private _router: Router, private authService: AuthenticationService) {
	}

	SendRequest(method: string, url: string, data: any, responseType: any): Observable<any> {

		if (!responseType) {
			responseType = 'json';
		}
		return this.http.request(method, url,
			{
				headers: this.jwt(),
				responseType: responseType,
				body: data
			}).pipe(catchError((err: HttpErrorResponse) => this.handleError(err)));
	}

	private jwt() {
		// create authorization header with jwt token
		const token = localStorage.getItem('signature_accessToken');
		if (token) {
			const headers = new HttpHeaders({
				'Content-Type': 'application/json',
				'Authorization': 'Bearer ' + token,
				// 'cashe': 'false',
				// 'foobar': '' + new Date().getTime() + '',
			});
			return headers;
		}
	}

	private handleError(res: HttpErrorResponse) {
		console.error(res.error);
		if (res.status === 500) {
			return Observable.throw(res.error);
		} else if (res.status === 400) {
			return Observable.throw(res.error);
		} else if (res.status === 401) {
			this.authService.logout(true);
			return Observable.throw(res.error);
		} else if (res.status === 409) {
			return Observable.throw(res.error);
		}
	}

}
