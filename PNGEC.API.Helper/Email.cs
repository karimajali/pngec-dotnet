using System;
using System.Net.Mail;

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
    }
}
