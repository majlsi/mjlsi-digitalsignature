
using Models.DTO;
using System;
using System.Text.RegularExpressions;

namespace Helpers
{
    public class ControlsHelper
    {
        private readonly ManageResources _manageRes;
        public ControlsHelper()
        {
            _manageRes = new ManageResources();
        }
        public string getFieldHtml(int userId, DocumentFieldDTO documentField, int timeZone, string LangCode = "ar-SA")
        {

            if (documentField.FieldTypeID == 1 && documentField.DocumentFieldValue == null)//signature box
            {
                if (documentField.UserID == userId)
                {
                    string html = @"<div   class=""signature-btns"" #signBtns  style=""position: absolute;top:" + documentField.YPosition.ToString() + @"%;left:" + documentField.XPosition.ToString() + @"%;"" >
                 <button class=""btn btn-primary"" onclick=""document.getElementById('signatureButton_" + documentField.DocumentFieldID.ToString() + @"').click()"" >" + _manageRes.GetNamebyLanguage("Sign", LangCode) + @"<i class=""fa fa-pen-nib""></i></button><button class=""btn btn-outline-secondary dark"" onclick=""document.getElementById('rejectButton_" + documentField.DocumentFieldID.ToString() + @"').click()"">" + _manageRes.GetNamebyLanguage("Cancel", LangCode) + @"<i class=""la la-times""></i></button></div>";
                    html = Regex.Replace(html, @"\t+", " ");
                    html = Regex.Replace(html, @"\n+", " ");
                    return html;
                }
                else
                {
                    string html = @"<div   class=""signature-btns"" style=""position: absolute;top:" + documentField.YPosition.ToString() + @"%;left:" + documentField.XPosition.ToString() + @"%;"" >
                    <h4 class=""text-secondary"">" + _manageRes.GetNamebyLanguage("NotSignedYet", LangCode) + "</h4></div>";
                    html = Regex.Replace(html, @"\t +", " ");
                    html = Regex.Replace(html, @"\n+", " ");
                    return html;

                }

            }
            else if (documentField.FieldTypeID == 1 && documentField.DocumentFieldValue == "false")// rejected 
            {
                string html = @"<div   class=""signature-btns""  style=""position: absolute;top:" + documentField.YPosition.ToString() + @"%;left:" + documentField.XPosition.ToString() + @"%;"" >
                   <h4 class=""m--font-danger"">" + _manageRes.GetNamebyLanguage("SignRefused", LangCode) + "</h4></div>";
                html = Regex.Replace(html, @"\t+", " ");
                html = Regex.Replace(html, @"\n+", " ");
                return html;
            }
            else if (documentField.FieldTypeID == 1)//signature image
            {
                var signDate = documentField.SignDate?.AddHours(timeZone) ?? DateTime.UtcNow.AddHours(timeZone);
                string html = @"<div style=""text-align:center;width:50%;position: absolute;top:" + documentField.YPosition.ToString() + @"%;left:" + documentField.XPosition.ToString() + @"%;""> 
               <img  width=""50%"" src=""" + documentField.DocumentFieldValue + @"""><h5 style=""margin-top: 3%;"">" + signDate + "</h5></div>";
                html = Regex.Replace(html, @"\t+", " ");
                html = Regex.Replace(html, @"\n+", " ");
                return html;

            }
            else
            {
                return "";
            }
        }


    }
}
