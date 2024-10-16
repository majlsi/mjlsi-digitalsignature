
import {Observable} from 'rxjs';
import { Injectable } from '@angular/core';
import { RequestService } from './shared/request.service';
import { BaseModel } from '../models/baseModel';
import { environment } from '../../../environments/environment';


@Injectable()
export class DocumentService {
    constructor( private _requestService: RequestService) { }

	getDocumentPagesList(langCode: string, timeZone) {
		return this._requestService.SendRequest('GET', environment.apiBaseURL + 'documents/documentPages/' + langCode + '?timeZone=' + timeZone, null, null);
    }
	sign( documentFieldId: number, signObject: Object){
        return this._requestService.SendRequest('PUT', environment.apiBaseURL + 'documentfields/sign/' + documentFieldId, signObject, null);
	}
	reject( documentFieldId: number, rejectObject: Object){
        return this._requestService.SendRequest('PUT', environment.apiBaseURL + 'documentfields/reject/' + documentFieldId, rejectObject, null);
	}
	
	deleteSignature( documentFieldId: number){
        return this._requestService.SendRequest('PUT', environment.apiBaseURL + 'documentfields/sign-delete/' + documentFieldId, null, null);
	}
	getDocumentPdf( documentFieldId: number) {
		return this._requestService.SendRequest('GET', environment.apiBaseURL + 'Documents/documentBinaries/' + documentFieldId, null, 'blob' as 'json');
	}
}

