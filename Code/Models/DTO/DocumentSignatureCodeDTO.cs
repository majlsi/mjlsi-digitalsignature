using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.DTO
{
	public class DocumentSignatureCodeDTO
	{
		[Required]
		public string VerificationCode { get; set; }
	}
}
