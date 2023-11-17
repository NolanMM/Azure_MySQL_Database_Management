using System.Net.Mail;
using System.Net;

namespace Cloud_Database_Management_System.Security_Services.OTP_Services
{
    public static class OTP_Module_Services
    {

        private static readonly Random random = new Random();
        private const string allowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string GenerateRandomKey(int length)
        {
            return new string(Enumerable.Repeat(allowedCharacters, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        static public async Task<bool> Send_OTP_CodeAsync(string randomCode, string email_to)
        {
            String from, pass, messageBody;

            // Input the information of Sender 
            from = "group1databaseservicesnolanm@gmail.com";
            pass = "lazslzusaxooyirr";
            messageBody = $"<span style='font-family: Times New Roman; color: red; font-size: larger;'>Hello,</span><br><br>" +
                $"<span style='font-family: Times New Roman; font-size: larger;'>Your OTP Code is: <b>{randomCode}</b></span><br><br>Sincerely,<br>NolanM - Minh Nguyen";

            // Generate new email to send to the receiver
            MailMessage email = new MailMessage();
            // Input the information of the Receiver and the information for the email components
            email.From = new MailAddress(from, "NolanM Cloud Database System");
            email.To.Add(email_to);
            email.Body = messageBody;
            email.Subject = "Password Reset Code";
            email.IsBodyHtml = true; // Enable HTML formatting

            // Generate smtp server to send the verify email 
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.EnableSsl = true;
            SmtpServer.Port = 587;
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            // Checking the app password of the google email and the email of the sender 
            SmtpServer.Credentials = new NetworkCredential(from, pass);

            try
            {
                await SmtpServer.SendMailAsync(email);
                // Console.WriteLine("Email Successfully Sent");
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

