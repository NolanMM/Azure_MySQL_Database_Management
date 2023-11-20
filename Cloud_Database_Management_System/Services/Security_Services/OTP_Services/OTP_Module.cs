using System.Net.Mail;
using System.Net;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Xml.Linq;
using System;

namespace Cloud_Database_Management_System.Security_Services.OTP_Services
{
    public static class OTP_Module_Services
    {

        private static readonly Random random = new Random();
        private const string allowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static string _nolanMLogoUrl = "https://drive.google.com/uc?id=1w3mfBR7NBooRlJWaQRmIcoNRbFROo32A";
        public static string GenerateRandomKey(int length)
        {
            return new string(Enumerable.Repeat(allowedCharacters, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        static public async Task<bool> Send_OTP_CodeAsync(string OTP_CODE, string OTP_CODE_ID, string email_to, string username)
        {
            String from, pass, messageBody;

            // Input the information of Sender 
            from = "group1databaseservicesnolanm@gmail.com";
            pass = "lazslzusaxooyirr";
            string endpointLink = $"https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController/RegisterVerifyOTP/{OTP_CODE_ID}/{OTP_CODE}";

            messageBody = CreateEmailBody(OTP_CODE, endpointLink, username);

            // Generate new email to send to the receiver
            MailMessage email = new MailMessage();
            // Input the information of the Receiver and the information for the email components
            email.From = new MailAddress(from, "NolanM Cloud Database System");
            email.To.Add(email_to);
            email.Body = messageBody;
            email.Subject = "OTP Verify Code";
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
        private static string CreateEmailBody(string OTP_CODE, string endpointLink, string username)
        {
            return $@"
                    <!DOCTYPE html>
                    <html>
                    <head>
                      <base target='_top'>
                    </head>
                    <body>
                      <div style='font-family: Helvetica, Arial, sans-serif; min-width: 1000px; overflow: auto; line-height: 2'>
                        <div style='margin: 50px auto; width: 80%; padding: 20px 0'>
                            <div style='border-bottom: 5px solid #eee'>
                                <a href='' style='font-size: 30px; color: #CC0000; text-decoration: none; font-weight: 600'>
                                    Database Management System - Group 1
                                </a>
                            </div>
                            <br>
                        <p style='font-size: 22px'>Hello {username},</p>
                        <p>Thank you for choosing our Database. Use this OTP to complete your Sign Up procedures and verify your account.</p>
                        <p>Remember, Never share this OTP with anyone.</p><br><br>
                          <h2 style='margin: 0 auto; width: max-content; padding: 0 10px; color: #CC0000; border-radius: 4px;'>{OTP_CODE}</h2><br>
                          <div style='text-align: center;'>
                          <a href='{endpointLink}' style='background: #CC0000; color: #fff; text-decoration: none; padding: 2px 16px; border-radius: 10px; display: inline-block;'>Verify OTP</a><br><br></div><br>
                          <p style='font-size: 15px;'>Regards,<br /><br>NolanM - Group 1</p>
                          <hr style='border: none; border-top: 5px solid #eee' />
                          <div style='float: left; padding: 8px 0; color: #aaa; font-size: 0.8em; line-height: 1; font-weight: 300'>
                           <img src='{_nolanMLogoUrl}' alt='NolanM Logo' style='width: 50%; max-height: 108px;'>
                        </div>
                        </div>
                      </div>
                    </body>
                    </html>
                    ";
        }
        public static string ResponseRegister(string otpId)
        {
            return $@"
                    <!DOCTYPE html>
                    <html lang='en'>
                    <head>
                        <meta charset='UTF-8'>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        <title>Account Verification</title>
                    </head>
                    <body style='font-family: ""Segoe UI"", Tahoma, Geneva, Verdana, sans-serif; text-align: center; margin: 50px; background-color: #f8f8f8;'>
                        <div style='background-color: #ffffff; padding: 30px; border-radius: 10px; box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); max-width: 600px; margin: 0 auto; '>
                            <h2 style='color: #CC0000; text-align: center'>Account Verification</h2>
                            <p style='font-size: 20px;'>Thank you for starting the registration process.</p>
                            <p>Your OTP ID: <b><span style='color: #CC0000;'><b>{otpId}</span></b></p>
                            <p>Please check your email for the OTP code to complete the registration.</p>
                        <div style='background-color: #f0f0f0; padding: 15px; border-radius: 8px; margin-top: 20px;'>
                            <p>Please make the request to this endpoint to verify your account:</p>
                            <p style='font-size: 12px;'>https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController</p><p style='font-size: 12px;'>/RegisterVerifyOTP/{otpId}/(OTP_CODE)</p>
                        </div>
                        </div></b>
                    </body>
                    </html>
        ";
        }
    }
}

