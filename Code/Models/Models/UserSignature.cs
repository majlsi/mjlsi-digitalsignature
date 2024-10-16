using System.Collections.Generic;

namespace Models
{
    public class UserSignature : BaseModel
    {


        public int UserSignatureID { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public string SignatureValue { get; set; }

        public UserSignature()
        {

        }

    }
}
