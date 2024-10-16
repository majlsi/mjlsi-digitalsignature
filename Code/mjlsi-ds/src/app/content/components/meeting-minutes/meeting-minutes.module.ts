import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MeetingMinutesComponent } from './meeting-minutes/meeting-minutes.component';
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { SignatureModalComponent } from './signature-modal/signature-modal.component';
import { CommentModalComponent } from './comment-modal/comment-modal.component';
import { SanitizeHtmlPipe } from '../../../core/pipes/sanitize-html.pipe';
import { NgxDynamicTemplateModule } from 'ngx-dynamic-template';
import { SignaturePadModule } from 'angular2-signaturepad';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { VerificationMethodModalComponent } from './verification-method-modal/verification-method-modal.component';
import { MatRadioModule } from '@angular/material/radio';
import { VerificationModalComponent } from './verification-modal/verification-modal.component';

@NgModule({
  imports: [
    CommonModule,
    NgbModule,
    NgxDynamicTemplateModule.forRoot(),
    SignaturePadModule,
    FormsModule,
    TranslateModule,
	MatRadioModule

  ],
  declarations: [MeetingMinutesComponent, SignatureModalComponent, CommentModalComponent, SanitizeHtmlPipe, VerificationMethodModalComponent, VerificationModalComponent],
  exports: [MeetingMinutesComponent, SignatureModalComponent, CommentModalComponent, SanitizeHtmlPipe
  ], entryComponents: [SignatureModalComponent, CommentModalComponent, VerificationMethodModalComponent, VerificationModalComponent]
})
export class MeetingMinutesModule { }
