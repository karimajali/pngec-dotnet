using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PNGEC.API.Helper
{
    public class Email
    {
        #region Methods
        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="toMailAddress">To mail address.</param>
        /// <param name="mailSubject">The mail subject.</param>
        /// <param name="mailBody">The mail body.</param>
        /// <returns></returns>
        public static bool SendMail(string toMailAddress, string mailSubject, string mailBody)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(SettingsConfig.AppSetting("SmtpServer:Name"));

                mail.From = new MailAddress(SettingsConfig.AppSetting("SmtpServer:FromAddress"));
                mail.To.Add(toMailAddress);
                mail.Subject = mailSubject;
                mail.Body = mailBody;
                mail.IsBodyHtml = true;
                SmtpServer.Port = Convert.ToInt32(SettingsConfig.AppSetting("SmtpServer:Port"));

                if (!String.IsNullOrEmpty(SettingsConfig.AppSetting("SmtpServer:UseDefaultCredentials")))
                {
                    SmtpServer.UseDefaultCredentials = Convert.ToBoolean(SettingsConfig.AppSetting("SmtpServer:UseDefaultCredentials"));
                }
                else
                {
                    SmtpServer.UseDefaultCredentials = true;
                }

                SmtpServer.Credentials = new System.Net.NetworkCredential(SettingsConfig.AppSetting("SmtpServer:FromAddress"), SettingsConfig.AppSetting("SmtpServer:Password"));
                SmtpServer.EnableSsl = Convert.ToBoolean(SettingsConfig.AppSetting("SmtpServer:EnableSsl"));

                SmtpServer.Send(mail);

                return true;
            }
            catch(Exception ex)
            {
                ErrorLog.Write("Email", "SendMail", ex);
                return false;
            }
        }
        #endregion

        #region SendMailBySendGrid
        /// <summary>
        /// Sends the mail by send grid.
        /// </summary>
        /// <param name="toMailAddress">To mail address.</param>
        /// <param name="mailSubject">The mail subject.</param>
        /// <param name="mailBody">The mail body.</param>
        /// <param name="AttachmentPath">The attachment path.</param>
        /// <returns></returns>
        public static async Task<bool> SendMailBySendGrid(string toMailAddress, string mailSubject, string mailBody, string AttachmentPath = "")
        {
            bool result = false;

            try
            {
                SendGrid.Helpers.Mail.EmailAddress aa = new SendGrid.Helpers.Mail.EmailAddress();
                var em = aa.Email;

                var apikey = SettingsConfig.AppSetting("SmtpServer:SendGridApiKey");
                var fromadd = SettingsConfig.AppSetting("SmtpServer:SendGridFromAddress");
                var username = SettingsConfig.AppSetting("SmtpServer:SendGridUsername");

                var client = new SendGrid.SendGridClient(SettingsConfig.AppSetting("SmtpServer:SendGridApiKey"));
                var from = new SendGrid.Helpers.Mail.EmailAddress(SettingsConfig.AppSetting("SmtpServer:SendGridFromAddress"), SettingsConfig.AppSetting("SmtpServer:SendGridUsername"));
                var to = new SendGrid.Helpers.Mail.EmailAddress(toMailAddress, string.Empty);
                var plainTextContent = string.Empty;
                var msg = SendGrid.Helpers.Mail.MailHelper.CreateSingleEmail(from, to, mailSubject, plainTextContent, mailBody);
               
                //if (!string.IsNullOrWhiteSpace(AttachmentPath))
                //{
                //    using (var fileStream = File.OpenRead(AttachmentPath))
                //    {
                //        await msg.AddAttachmentAsync(Path.GetFileName(AttachmentPath), fileStream);
                //    }
                //}

                var response = await client.SendEmailAsync(msg);

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.Accepted)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write("Email", "SendMailBySendGrid", ex);
            }

            return result;
        }
        #endregion
    }
}
