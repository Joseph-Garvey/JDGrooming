using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace JDGrooming.Classes
{
    public class Email
    {
        #region Variables
        MailAddress address;
        /// <summary>
        /// Address to send email to
        /// </summary>
        public MailAddress Address { get => address; set => address = value; }
        /// <summary>
        /// Address from which to send emails.
        /// </summary>
        public MailAddress BusinessAddress = new MailAddress("jddogcareni@gmail.com");
        public String BusinessAddressPassword = "rwdvM88$fDJh";
        /// <summary>
        /// MailClient to handle all mail.
        /// </summary>
        public SmtpClient MailClient { get; set; } = new SmtpClient
        {
            Port = 587,
            Host = "smtp.gmail.com",
            EnableSsl = true,
            UseDefaultCredentials = false,
            DeliveryMethod = SmtpDeliveryMethod.Network
        };
        /// <summary>
        /// 2FA Authentication Code
        /// </summary>
        private int AuthenticationNo { get; set; }
        #endregion
        // figure out the better way of multithreading this. look at what miles said on discord.
        #region Constructors
        public Email(String _address)
        {
            Address = new MailAddress(_address);
            MailClient.Credentials = new NetworkCredential(BusinessAddress.ToString(), BusinessAddressPassword);
        }
        #endregion
        #region Methods
        /// <summary>
        /// Called to generate and send 2FA email for an account.
        /// </summary>
        /// <returns></returns>
        public int Authenticate()
        {
            Generate2FA();
            BackgroundWorker thread = new BackgroundWorker() { WorkerReportsProgress = true };
            thread.DoWork += Send2FA;
            thread.RunWorkerAsync();
            return AuthenticationNo;
        }
        /// <summary>
        /// Sends email from Business Address to Desired Address.
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="html"></param>
        private void SendEmail(String subject, String html)
        {
            try
            {
                MailMessage message = new MailMessage(BusinessAddress.ToString(), Address.ToString(), "Authentication code from JDDogCare", html);
                message.IsBodyHtml = true;
                MailClient.SendMailAsync(message);
            }
            // if email doesn't send
            catch (SmtpException) { }
            catch (Exception) { }
        }
        /// <summary>
        /// Generates a random int for 2FA
        /// </summary>
        private void Generate2FA()
        {
            Random r = new Random();
            AuthenticationNo = r.Next(999);
        }
        #endregion
        #region Events
        /// <summary>
        /// Generates 2FA code and sends email.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Send2FA(object sender, DoWorkEventArgs e)
        {
            String subject = "Authentication Email from JDDogCare";
            String html = "Your 2FA authentication code is " + AuthenticationNo  + ".";
            SendEmail(subject, html);
        }
        #endregion
    }
}
