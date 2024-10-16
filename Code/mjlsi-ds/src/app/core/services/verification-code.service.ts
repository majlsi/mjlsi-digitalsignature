import { Injectable } from '@angular/core';
import { RequestService } from './shared/request.service';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { VerificationCodeType } from '../models/verificationCodeType';
import { VerificationCode } from '../models/verificationCode';
import { ActivatedRoute } from '@angular/router';


@Injectable()
export class VerificationCodeService {
    constructor(private _requestService: RequestService) { }

    generateCode(verificationCodeType: VerificationCodeType, lang: string) {
        this._requestService.SendRequest('POST',
            environment.apiBaseURL + "DocumentSignatureCodes/" + lang,
            verificationCodeType, null).subscribe();
    }
    verifyCode(verificationCode: VerificationCode): Observable<any> {
        return this._requestService.SendRequest('POST',
            environment.apiBaseURL + "DocumentSignatureCodes/VerifyCode",
            verificationCode, null);
    }
}
