using Models;
using Models.DTO;
using System.Threading.Tasks;

namespace Services
{
    public interface INotificationService
    {
       void SendRegisterationNotification(int userID,string lang);
       void SendVerificationCode(int userID, VerificationCodeGenerationDTO verificationCodeGenerationDTO,
           DocumentSignatureCode documentSignatureCode,string lang);
    }
}
