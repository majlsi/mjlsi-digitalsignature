import { NgModule } from '@angular/core';
// import {
// 	AUTH_SERVICE,
// 	AuthModule,
// 	PROTECTED_FALLBACK_PAGE_URI,
// 	PUBLIC_FALLBACK_PAGE_URI
// } from 'ngx-auth';

import { TokenStorage } from './token-storage.service';
import { AuthenticationService } from './authentication.service';

export function factory(authenticationService: AuthenticationService) {
	return authenticationService;
}

@NgModule({
	imports: [],
	providers: [
		TokenStorage,
		AuthenticationService
	]
})
export class AuthenticationModule { }


