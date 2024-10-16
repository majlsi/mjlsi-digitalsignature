import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { VerificationCode } from '../../../../core/models/verificationCode';
import { VerificationCodeService } from '../../../../core/services/verification-code.service';
import { SignatureModalComponent } from '../signature-modal/signature-modal.component';

@Component({
  selector: 'm-verification-modal',
  templateUrl: './verification-modal.component.html',
  styleUrls: ['./verification-modal.component.scss']
})
export class VerificationModalComponent implements OnInit {
	@Input() public verificationCodeType;
	@Input() lang: string;
	@Input() useEmail: boolean;
	@Input() useSms: boolean;
	verificationCode: VerificationCode = new VerificationCode();
	hasEmptyCode: boolean = false;
	codeResent: boolean = false;
	wrongVerificationCode: boolean = false;
	constructor(public activeModal: NgbActiveModal,
		private modalService: NgbModal,
		private verificationCodeService:VerificationCodeService) { }

		ngOnInit() {}
		close() {
			this.modalService.dismissAll();
			// this.modalService.open(SignatureModalComponent, { centered: true, size: "lg" });
		}

	OnInput() {
		this.hasEmptyCode = false;
		this.wrongVerificationCode = false;
		this.codeResent = false;
	}

	resend() {
		this.verificationCodeService.generateCode(this.verificationCodeType,this.lang);
		this.codeResent = true;
	}

	verifyCode() {
		this.codeResent = false;

		this.verificationCodeService.verifyCode(this.verificationCode)
			.subscribe(res => {
				if (res.Results) {
					this.activeModal.close(true);
				} else {
					this.wrongVerificationCode = true;
				}
			},
			error => {
				this.hasEmptyCode = true;
			});
	}

}
