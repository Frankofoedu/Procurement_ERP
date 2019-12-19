using BsslProcurement.Interfaces;
using DcProcurement;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly EmailSettings _emailSettings;
        private readonly IWebHostEnvironment _env;

        public EmailSenderService(
            IOptions<EmailSettings> emailSettings,
            IWebHostEnvironment env)
        {
            _emailSettings = emailSettings.Value;
            _env = env;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var mimeMessage = new MimeMessage();

                mimeMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.Sender));

                mimeMessage.To.Add(new MailboxAddress(email));

                mimeMessage.Subject = subject;

                mimeMessage.Body = new TextPart("html")
                {
                    Text = message
                };

                var client = new SmtpClient();
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                if (_env.IsDevelopment())
                {
                    // The third parameter is useSSL (true if the client should make an SSL-wrapped
                    // connection to the server; otherwise, false).
                    await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, false);
                }
                else
                {
                    await client.ConnectAsync(_emailSettings.MailServer);
                }

                // Note: only needed if the SMTP server requires authentication
                await client.AuthenticateAsync(_emailSettings.Sender, _emailSettings.Password);

                await client.SendAsync(mimeMessage);

                await client.DisconnectAsync(true);

            }
            catch (Exception ex)
            {
                // TODO: handle exception
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task SendInvitationEmailToListAsync(List<string> emails, string subject, string companyName, string eRFxNo, string projectTitle, DateTime erFxEndDate, string assignedStaffName, string assignedStaffDesignation)
        {
            try
            {
                var message = $"{companyName.ToUpper()}" + Environment.NewLine +
                    "Dear Esteemed Vendor," + Environment.NewLine +
                    "Invitation to Participate" + Environment.NewLine +
                    $"{companyName} has invited you to Bid for “Project ID ({eRFxNo})” for “{projectTitle}”." + Environment.NewLine +
                    "Please provide necessary information, download all required documents (if any) from the library, and review each document in its entirety; the Purchase Requisition, Specifications & Standards, and all relevant Drawings (if any)." + Environment.NewLine +
                    "You are expected to fill out the Technical Information and Financial information (where necessary) and submit to the system on or before submission deadline." + Environment.NewLine +
                    $"The deadline for Submission is {erFxEndDate.ToString("dddd, dd MMMM yyyy")}" + Environment.NewLine +
                    "Please confirm receipt." + Environment.NewLine +
                    "There is a Help button at the extreme of Tabs, which will provide you with further assistance." + Environment.NewLine +
                    "Click Vendor Access Point: https://www.bsslsoftware.com/" + Environment.NewLine +
                    "Best Regards," + Environment.NewLine +
                    $"{assignedStaffName}" + Environment.NewLine +
                    $"{assignedStaffDesignation}";



                var mimeMessage = new MimeMessage();

                mimeMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.Sender));

                InternetAddressList list = new InternetAddressList();

                foreach (string email in emails)
                {
                    list.Add(new MailboxAddress(email));
                }



                mimeMessage.To.AddRange(list);

                mimeMessage.Subject = subject;

                mimeMessage.Body = new TextPart("html")
                {
                    Text = message
                };


                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;


                    if (_env.IsDevelopment())
                    {
                        // The third parameter is useSSL (true if the client should make an SSL-wrapped
                        // connection to the server; otherwise, false).
                        await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, false);
                    }
                    else
                    {
                        await client.ConnectAsync(_emailSettings.MailServer);
                    }

                    // Note: only needed if the SMTP server requires authentication
                    await client.AuthenticateAsync(_emailSettings.Sender, _emailSettings.Password);

                    await client.SendAsync(mimeMessage);

                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                // TODO: handle exception
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
