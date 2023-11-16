using System.Net.Mail;
using System.Net;

namespace Cloud_Database_Management_System.Services.Security_Services.OTP_Services
{
    public class OTP_Module
    {
        public static bool Send_OTP_Code(string randomCode, string email_to)
        {
            // Create new variable to hold the sender email, password of the sender, and the message is the code
            String from, pass, messageBody;

            // Input the information of Sender
            from = "group4sendblackmailtou@gmail.com";
            pass = "bbsmmmsnfhealzte";
            messageBody = " Your Registercod Code Is: " + randomCode;

            // Generate new email to send to the receiver
            MailMessage email = new MailMessage();
            // Input the information of the Receiver and the information for the email components
            email.From = new MailAddress(from);
            email.To.Add(email_to);
            email.Body = messageBody;
            email.Subject = "Password Reseting Code";

            // Generate smtp server to send the verify email
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.EnableSsl = true;
            SmtpServer.Port = 587;
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            // Checking the app password of the google email and the email of the sender
            SmtpServer.Credentials = new NetworkCredential(from, pass);

            try
            {
                SmtpServer.Send(email);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
