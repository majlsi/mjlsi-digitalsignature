using Microsoft.AspNetCore.Hosting;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using System.IO;

namespace WebAPI
{
    public static class RazorBootStrapper
    {
        static IRazorEngineService razorService;
        public static void Init(IWebHostEnvironment WebHostEnvironment)
        {
            var config = new TemplateServiceConfiguration();
            config.TemplateManager = new DelegateTemplateManager();
            config.CachingProvider = new DefaultCachingProvider();
            razorService = RazorEngineService.Create(config);
            Engine.Razor = razorService;
            string currentDirec = WebHostEnvironment.ContentRootPath;

            string UserRegisterPath = Path.Combine(currentDirec,"EmailTemplates", "UserRegister.cshtml");
            string UserRegisterTemplate = File.ReadAllText(UserRegisterPath);
            Engine.Razor.AddTemplate("UserRegister", UserRegisterTemplate);
            Engine.Razor.Compile("UserRegister", typeof(Models.User));



            string EmailTemplatePath = Path.Combine(currentDirec, "EmailTemplates", "SendVerificationCodeEmail-en.cshtml");
            string EmailTemplate = File.ReadAllText(EmailTemplatePath);
            Engine.Razor.AddTemplate("SendVerificationCodeEmail-en", EmailTemplate);
            Engine.Razor.Compile("SendVerificationCodeEmail-en", typeof(Models.DocumentSignatureCode));

            string EmailTemplateArPath = Path.Combine(currentDirec, "EmailTemplates", "SendVerificationCodeEmail-ar.cshtml");
            string EmailTemplateAr = File.ReadAllText(EmailTemplateArPath);
            Engine.Razor.AddTemplate("SendVerificationCodeEmail-ar", EmailTemplateAr);
            Engine.Razor.Compile("SendVerificationCodeEmail-ar", typeof(Models.DocumentSignatureCode));

            string SMSTemplatePath = Path.Combine(currentDirec, "EmailTemplates", "SendVerificationCodeSMS-en.cshtml");
            string SMSTemplate = File.ReadAllText(SMSTemplatePath);
            Engine.Razor.AddTemplate("SendVerificationCodeSMS-en", SMSTemplate);
            Engine.Razor.Compile("SendVerificationCodeSMS-en", typeof(Models.DocumentSignatureCode));

            string SMSTemplateArPath = Path.Combine(currentDirec, "EmailTemplates", "SendVerificationCodeSMS-ar.cshtml");
            string SMSTemplateAr = File.ReadAllText(SMSTemplateArPath);
            Engine.Razor.AddTemplate("SendVerificationCodeSMS-ar", SMSTemplateAr);
            Engine.Razor.Compile("SendVerificationCodeSMS-ar", typeof(Models.DocumentSignatureCode));
        }
    }
}