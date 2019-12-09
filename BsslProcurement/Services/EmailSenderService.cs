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

                using var client = new SmtpClient
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    ServerCertificateValidationCallback = (s, c, h, e) => true
                };

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

        public async Task SendEmailToListAsync(List<string> emails, string subject, string message)
        {
            try
            {

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

                using var client = new SmtpClient
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    ServerCertificateValidationCallback = (s, c, h, e) => true
                };

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
    }
}
