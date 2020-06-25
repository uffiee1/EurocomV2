using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace EurocomV2_Logic
{
    public class SendMail
    {
        public string GetMailServerConnection()
        {
            string receiverMail = "tony.zhou125@gmail.com";
            string subject = "Test";
            string message = "Het gaat niet goed met de gezondheid van Bob";
            string userNameSender = "testmaileurocom@gmail.com";
            //This is a secret. Sometimes it is better not to know certain things.
            string senderPassword = "TestEurocom2020!";

            try
            {
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new System.Net.NetworkCredential(userNameSender, senderPassword),
                    Timeout = 2500
                };

                MailMessage mail = new MailMessage(userNameSender, receiverMail, subject, message);
                mail.IsBodyHtml = true;
                smtp.Send(mail);
            }

            catch (Exception e)
            {
                return string.Format("Error {0} {1} Request Timeout: The mail sending failed", "408", e.ToString());
            }
            return "The mail was send succesfully";
        }
    }
}