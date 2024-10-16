import { Component, OnInit, ViewChild, ElementRef, AfterViewInit, Input, Pipe, Inject } from '@angular/core';
import { SignatureModalComponent } from '../signature-modal/signature-modal.component';
import { CommentModalComponent } from '../comment-modal/comment-modal.component';
import { NgbActiveModal, NgbModal, NgbModalConfig, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { CrudService } from '../../../../core/services/shared/crud.service';
import { DocumentPage } from '../../../../core/models/document-page';
import { DocumentService } from '../../../../core/services/document.service';
import { ActivatedRoute } from '@angular/router';
import { environment } from '../../../../../environments/environment';
import { FieldTypes } from '../../../../core/models/enums/field-types';
import { UserProfileComponent } from '../../../layout/header/topbar/user-profile/user-profile.component';
import { UserService } from '../../../../core/services/security/users.service';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { DocumentField } from '../../../../core/models/document-field';
import { saveAs } from 'file-saver';
import { HttpClient } from '@angular/common/http';
import pdfMake from 'pdfmake/build/pdfmake';
import pdfFonts from 'pdfmake/build/vfs_fonts';
pdfMake.vfs = pdfFonts.pdfMake.vfs;
import { DOCUMENT } from '@angular/common';
import { TranslateService } from '@ngx-translate/core';
import { UserSignature } from '../../../../core/models/user-signature';

@Component({
	selector: 'm-meeting-minutes',
	templateUrl: './meeting-minutes.component.html',
	styleUrls: ['./meeting-minutes.component.scss'],

	providers: [NgbModalConfig, NgbModal]
})


export class MeetingMinutesComponent implements OnInit {
	slideWidth = 50;
	collapsed: boolean = false;
	signHidden: boolean = true;
	documentId: number;
	userToken: string;
	documentPages: Array<DocumentPage> = [];
	isLoaded: boolean = false;
	lastElementIndex: number;
	imageBaseUrl = environment.imagesBaseURL;
	userId: number;
	submitted: boolean = false;
	@ViewChild('signBtns', { static: true }) btns: ElementRef;
	signature: number = FieldTypes.SIGNATURE;
	modifiedDocumentFields: Array<DocumentField>;
	returnURL: string;
	themeName = environment.themeName;
	lang: string;

	constructor(private modalService: NgbModal,
		private http: HttpClient,
		private _documentService: DocumentService,
		private _userService: UserService,
		config: NgbModalConfig,
		private _crudService: CrudService,
		private route: ActivatedRoute,
		@Inject(DOCUMENT) private document: Document,
		private translate: TranslateService) {
		config.backdrop = 'static';
		config.keyboard = false;
	}
	ngOnInit() {
		this.lang = this.route.snapshot.queryParams.lang;
		this.userToken = this.route.snapshot.queryParams.token;
		localStorage.setItem('signature_accessToken', this.userToken);
		this.getCurrentUser();
		this.getDocumentPages();

		if (this.lang === null || this.lang === "" || this.lang === undefined) {
			this.lang = "ar";
		}

	}
	open(documentfield: DocumentField) {
		const signModelRef: NgbModalRef = this.modalService.open(SignatureModalComponent, { centered: true, size: "lg" });
		(<SignatureModalComponent>signModelRef.componentInstance).documentFieldId = documentfield.DocumentFieldID;
		signModelRef.componentInstance.lang = this.lang;
		signModelRef.result.then((res) => {

			if (res == 'Go to SignatureModal') {
				this.open(documentfield);
			} else if (res != 'Close click') {
				documentfield.DocumentFieldComment = res.signObj.DocumentFieldComment;
				documentfield.DocumentFieldValue = res.signObj.DocumentFieldValue;
				documentfield.SignatureTypeID = res.signObj.SignatureTypeID;
				documentfield.DocumentFieldHtml = `<div style="text-align:center;width:37.5%;position: absolute;top:` +
					(documentfield.YPosition) + `%;left:` + (documentfield.XPosition) +
					`% "> <img width="37.5%" onclick="document.getElementById('signatureButton_` + documentfield.DocumentFieldID + `').click()"  src="` + res.signObj.DocumentFieldValue + `"></div>`;
				documentfield.isModified = 1;

				if (res.saveSignature == true) {
					this.saveSignature(documentfield.DocumentFieldValue);
				}
			}
		});
	}

	openComment(documentfield: DocumentField) {
		const rejectModelRef: NgbModalRef = this.modalService.open(CommentModalComponent, { centered: true, scrollable: false, });
		rejectModelRef.componentInstance.lang = this.lang;
		(<CommentModalComponent>rejectModelRef.componentInstance).documentFieldId = documentfield.DocumentFieldID;

		rejectModelRef.result.then((result) => {
			if (result == 'Go to SignatureModal') {
				this.openComment(documentfield);
			} else if (result != 'Close click') {
				documentfield.DocumentFieldValue = 'false';
				documentfield.DocumentFieldComment = result;
				documentfield.DocumentFieldHtml = `<h4 class="m--font-danger"  onclick="document.getElementById('rejectButton_` + documentfield.DocumentFieldID + `').click()"    style="position: absolute;top:` + (documentfield.YPosition + 1) + `%;left:` + (documentfield.XPosition + 15) + `% ">` + this.translate.instant('COMMENTS.REFUSE_COMMENT') + ` </h4>`;
				documentfield.isModified = 1;
			}
		});
	}

	saveSignature(sugnatureValue) {

		this._crudService.add<UserSignature>('UserSignatures', { SignatureValue: sugnatureValue }).subscribe(res => {

		},
			error => {

			});

	}


	zoomInSlide() {
		if (this.slideWidth < 100) {
			this.slideWidth = this.slideWidth + 10;
		}
	}
	zoomOutSlide() {

		if (this.slideWidth > 30) {
			this.slideWidth = this.slideWidth - 10;
		}

	}
	fullScreen() {
		if (this.slideWidth < 100) {
			this.slideWidth = 100;
			this.collapsed = true;
		}
		else if (this.slideWidth = 100) {
			this.slideWidth = 50;
			this.collapsed = false;
		}
	}
	scroll(id: string) {

		const el: HTMLElement = document.getElementById(id);
		el.scrollIntoView({ behavior: "smooth", block: "start", inline: "nearest" });;
	}


	getDocumentPages() {
		const lang = this.route.snapshot.queryParams.lang;
		const timeZone = this.route.snapshot.queryParams.timeZone;
		this._documentService.getDocumentPagesList(lang, timeZone).
			subscribe(res => {
				this.documentPages = res.Results;
				this.lastElementIndex = this.documentPages.length - 1;
				this.isLoaded = true;
				this.returnURL = this.documentPages[0].ReturnURL;
			},
				error => {

				});
	}
	getCurrentUser() {
		this._userService.getCurrentUser().
			subscribe(res => {

				this.userId = res.Results.UserID;

			},
				error => {

				});
	}
	saveAndExit() {
		this.submitted = true;
		this.documentPages.forEach((documentPage, index) => {
			this.modifiedDocumentFields = [];
			this.modifiedDocumentFields = documentPage.DocumentFields.filter(item => item.isModified === 1);
			if (this.modifiedDocumentFields.length == 0 && index == this.documentPages.length - 1) {
				this.document.location.href = this.returnURL;
			}
			this.modifiedDocumentFields.forEach((modifiedDocumentField, fieldIndex) => {

				if (modifiedDocumentField.DocumentFieldValue == 'false') {// rejected field
					if (modifiedDocumentField.DocumentFieldComment == undefined) {
						modifiedDocumentField.DocumentFieldComment = null;
					}
					this._documentService.reject(modifiedDocumentField.DocumentFieldID, { DocumentFieldComment: modifiedDocumentField.DocumentFieldComment }).
						subscribe(res => {

							//this.getDocumentPages();
							this.document.location.href = this.returnURL;
						},
							error => {


							});
				} else {// signature image field
					this._documentService.sign(modifiedDocumentField.DocumentFieldID, { DocumentFieldValue: modifiedDocumentField.DocumentFieldValue, DocumentFieldComment: modifiedDocumentField.DocumentFieldComment ,SignatureTypeID: modifiedDocumentField.SignatureTypeID}).
						subscribe(res => {

							this.document.location.href = this.returnURL;
						},
							error => {


							});

				}
			});

		});
		this.submitted = false;
	}

	discardAndExit() {
		this.document.location.href = this.returnURL;
	}
	printDocument() {
		this._documentService.getDocumentPdf(this.documentPages[0].DocumentID).
			subscribe(res => {
				// Assuming you have the Blob data in pdfBlobData
				const pdfBlob = new Blob([res], { type: 'application/pdf' });
				const pdfBlobUrl = URL.createObjectURL(pdfBlob);
				// Open the Blob URL in a new tab or window
				const pdfWindow = window.open(pdfBlobUrl, '_blank');
				// Optionally, you can wait for the PDF to load and then trigger the print dialog
				pdfWindow.addEventListener('load', () => {
					pdfWindow.print();
				});
				// Optionally, revoke the Blob URL after the user has finished with it
				URL.revokeObjectURL(pdfBlobUrl);
			}, error => { });
	}



	saveDocument() {
		this._documentService.getDocumentPdf(this.documentPages[0].DocumentID).
			subscribe(res => {
				saveAs(res, name);
			}, error => { });
	}
}
