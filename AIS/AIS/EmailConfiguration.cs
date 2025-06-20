using AIS;
using AIS.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailConfiguration
    {
    private readonly EmailCredentails emailCredentails = new EmailCredentails();

    public bool ConfigEmail(string to = "", string cc = "", string subj = "", string body = "")
        {
        try
            {
            EmailCredentailsModel em = emailCredentails.GetEmailCredentails();

            if (string.IsNullOrEmpty(to))
                throw new ArgumentException("Recipient email address is required.");

            MailMessage mail = new MailMessage
                {
                From = new MailAddress(em.EMAIL),
                Subject = subj,
                Body = body
                };

            // Add recipients
            mail.To.Add(to);
            if (!string.IsNullOrEmpty(cc)) // Add CC if provided
                {
                mail.CC.Add(cc);
                }

            SmtpClient SmtpServer = new SmtpClient(em.Host)
                {
                Port = em.Port,
                Credentials = new NetworkCredential(em.EMAIL, em.PASSWORD),
                EnableSsl = true // If the server requires SSL
                };

            // Send the email
            SmtpServer.Send(mail);
            return true;
            }
        catch (SmtpException ex)
            {
            // Log SmtpException details for debugging
            Console.WriteLine($"SMTP error: {ex.Message}");
            return false;
            }
        catch (Exception ex)
            {
            // Log general exceptions
            Console.WriteLine($"General error: {ex.Message}");
            return false;
            }
        }


    public async Task<bool> ConfigEmailAsync(string to = "", string cc = "", string subj = "", string body = "")
        {
        try
            {
            EmailCredentailsModel em = emailCredentails.GetEmailCredentails();

            if (string.IsNullOrEmpty(to))
                throw new ArgumentException("Recipient email address is required.");

            using (var mail = new MailMessage
                {
                From = new MailAddress(em.EMAIL),
                Subject = subj,
                Body = body
                })
                {
                mail.To.Add(to);
                if (!string.IsNullOrEmpty(cc))
                    mail.CC.Add(cc);

                using (var smtp = new SmtpClient(em.Host)
                    {
                    Port = em.Port,
                    Credentials = new NetworkCredential(em.EMAIL, em.PASSWORD),
                    EnableSsl = true
                    })
                    {
                    await smtp.SendMailAsync(mail);
                    }
                }
            return true;
            }
        catch (SmtpException ex)
            {
            Console.WriteLine($"SMTP error: {ex.Message}");
            return false;
            }
        catch (Exception ex)
            {
            Console.WriteLine($"General error: {ex.Message}");
            return false;
            }
        }
    }