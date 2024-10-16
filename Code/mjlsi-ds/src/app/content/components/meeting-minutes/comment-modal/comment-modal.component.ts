import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal, NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import { DocumentService } from '../../../../core/services/document.service';
import { VerificationMethodModalComponent } from '../verification-method-modal/verification-method-modal.component';
@Component({
  selector: 'm-comment-modal',
  templateUrl: './comment-modal.component.html',
  styleUrls: ['./comment-modal.component.scss']
})
export class CommentModalComponent implements OnInit {
	submitted: boolean = false;
	rejectComment: string;
	rejectObject: Object;
	@Input() documentFieldId: number;
	@Input() lang: string;
	constructor(config: NgbModalConfig, private modalService: NgbModal, public activeModal: NgbActiveModal, private _documentService: DocumentService) {
        config.backdrop = 'static';
        config.keyboard = false;
      }

	ngOnInit() {
	}
	save() {
		
		const ModelRef = this.modalService.open(VerificationMethodModalComponent, {
			centered: true,
			size: "md",
			scrollable: false
		});
		ModelRef.componentInstance.lang = this.lang;
		ModelRef.result.then((result) => {
			if (result === true) {
				this.activeModal.close(this.rejectComment);
				this.submitted = true;
				if (this.rejectComment != undefined) {
					this.rejectObject = { DocumentFieldComment: this.rejectComment };
				} else {
					this.rejectObject = { DocumentFieldComment: null };
				}
			}
		});
		
	}
}
