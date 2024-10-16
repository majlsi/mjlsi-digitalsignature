using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.DTO
{
	public class VerificationCodeGenerationDTO
	{
		[Required]
		public bool SendEmail { get; set; }
		[Required]
		public bool SendSMS { get; set; }
	}
}
