using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;


namespace Helpers
{
    public class SecurityHelper
    {
        /// <summary>
        /// Claims Instance
        /// </summary>
        private readonly ClaimsPrincipal _principal;

        public SecurityHelper(IHttpContextAccessor httpContextAccessor)
        {
            _principal = httpContextAccessor?.HttpContext?.User;

        }
        /// <summary>
        /// Encrypt Using MD5
        /// </summary>
        /// <param name="src">String to be Encrypted</param>
        /// <returns></returns>
        public  String Md5Encryption(String src)
        {
            System.Text.UnicodeEncoding encode = new System.Text.UnicodeEncoding();
            System.Text.Decoder decode = encode.GetDecoder();
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] password = new byte[encode.GetByteCount(src)];
            char[] char_password = new char[encode.GetCharCount(password)];
            password = encode.GetBytes(src);
            password = md5.ComputeHash(password);
            int length = encode.GetByteCount(src);
            if (length > 16)
                length = 16;
            decode.GetChars(password, 0, length, char_password, 0);
            return new String(char_password);

        }

        public int getUserIDFromToken()
        {
            string APPID = "";
            if (_principal != null)
            {
                // Get the claims values
                APPID = _principal.Claims.Where(c => c.Type == "UserId")
                    .Select(c => c.Value).SingleOrDefault();
            }
            int id = int.Parse(APPID);
            return id;
        }
        public int getDocumentIDFromToken()
        {
            string DocumentID = "";
            // Get the claims values
            if (_principal != null)
            {
                DocumentID = _principal.Claims.Where(c => c.Type == "DocumentId")
                    .Select(c => c.Value).SingleOrDefault();
            }
            int id = int.Parse(DocumentID);
            return id;
        }
        public int getRoleIDFromToken()
        {
            string APPID = "";
            // Get the claims values
            if (_principal != null)
            {
                APPID = _principal.Claims.Where(c => c.Type == "RoleId")
                    .Select(c => c.Value).SingleOrDefault();
            }
            int id = int.Parse(APPID);
            return id;
        }
        public int getAppIDFromToken()
        {
            string APPID = "";
            // Get the claims values
            if (_principal != null)
            {
                APPID = _principal.Claims.Where(c => c.Type == "AppId")
                    .Select(c => c.Value).SingleOrDefault();
            }
            int id = int.Parse(APPID);
            return id;
        }
        public int? getCompanyIDFromToken()
        {
            return null;
          
        }
        public string getReturnURLFromToken()
        {
            string APPID = "";
            // Get the claims values
            if (_principal != null)
            {
                APPID = _principal.Claims.Where(c => c.Type == "ReturnURL")
                    .Select(c => c.Value).SingleOrDefault();
            }
            return APPID;
        }
        public string getCallbackURLFromToken()
        {
            string APPID = "";
            // Get the claims values
            if (_principal != null)
            {
                APPID = _principal.Claims.Where(c => c.Type == "CallbackURL")
                    .Select(c => c.Value).SingleOrDefault();
            }
            return APPID;
        }

        public string getUserEmailFromToken()
        {
            string APPID = "";
            // Get the claims values
            if (_principal != null)
            {
                APPID = _principal.Claims.Where(c => c.Type == "UserEmail")
                    .Select(c => c.Value).SingleOrDefault();
            }
            return APPID;
        }
    }
}
