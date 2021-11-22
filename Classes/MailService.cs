using System;
using System.Net;
using System.Net.Mail;
using projectsleemwebapp.Classes;

namespace projectsleemwebapp.Classes
{
    public class MailService
    {
        private readonly string From = "mzadlny1@gmail.com";
        private readonly string Pass = "mz1mz2mz3";
       
        public string SendMail(string To, string Subject, string Body)
        {
            MailMessage Message = new MailMessage
            {
                Subject = Subject,
                Body = Body,
                From = new MailAddress(From),
                IsBodyHtml = true,
                Priority = MailPriority.High
            };

            SmtpClient smtp = new SmtpClient
            {
                Port = 587,
                EnableSsl = true,
                Host = "smtp.gmail.com",
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(From, Pass)
            };
            Message.To.Add(new MailAddress(To));

            try
            {
                smtp.Send(Message);

                return "تم إرسال البريد بِنجاح";
            }
            catch (Exception ex)
            {
             
                throw;
            }
        }
    }
}