using EnterpriseGatewayPortal.Core.Domain.Repositories;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace EnterpriseGatewayPortal.Core.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly ILogger<EmailSenderService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public EmailSenderService(ILogger<EmailSenderService> logger,
            IUnitOfWork unitOfWork,
            IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _configuration = configuration;
        }

        private Task<MimeMessage> CreateEmailMessage(Message message)
        {
            _logger.LogDebug("-->CreateEmailMessage");
            var fromEmailAddr = _configuration["SMTP_Settings:FromEmailAddr"];

            // Validate Input Parameters
            if (null == message)
            {
                _logger.LogError("Invalid Input Parameter");
                return null;
            }
            var emailMessage = new MimeMessage();

            try
            {
                emailMessage.From.Add(MailboxAddress.Parse(fromEmailAddr));
                emailMessage.To.AddRange(message.To);
                emailMessage.Subject = message.Subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
                {
                    Text = message.Content
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("CreateEmailMessage Failed : {0}", ex.Message);
                return null;
            }

            _logger.LogDebug("<--CreateEmailMessage");
            return Task.FromResult(emailMessage);
        }

        private async Task<int> Send(MimeMessage mailMessage)
        {
            _logger.LogDebug("-->Send");
            int result = -1;
            var smtpServer = _configuration["SMTP_Settings:SmtpServer"];
            var port = _configuration.GetValue<int>("SMTP_Settings:Port");
            var userName = _configuration["SMTP_Settings:UserName"];
            var password = _configuration["SMTP_Settings:Password"];
            // Validate Input Parameters
            if (null == mailMessage)
            {
                _logger.LogError("Invalid Input Parameter");
                return result;
            }

            using (var client = new SmtpClient())
            {
                try
                {
                    var mailConfig = new EmailConfiguration
                    {
                        SmtpServer = smtpServer,
                        Port = port,
                        UserName = userName,
                        Password = password
                    };

                    SecureSocketOptions socketOptions = SecureSocketOptions.None;

                    switch (mailConfig.Port)
                    {
                        case 465:
                            socketOptions = SecureSocketOptions.SslOnConnect;
                            break;

                        case 587:
                            socketOptions = SecureSocketOptions.StartTls;
                            break;

                        case 25:
                            socketOptions = SecureSocketOptions.None;
                            break;

                        default:
                            _logger.LogError("UnKnown port number:" + mailConfig.Port);
                            socketOptions = SecureSocketOptions.None;
                            break;
                    }

                    await client.ConnectAsync(mailConfig.SmtpServer, mailConfig.Port, socketOptions);

                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    if (mailConfig.Port != 25 && !string.IsNullOrWhiteSpace(mailConfig.UserName))
                    {
                        await client.AuthenticateAsync(mailConfig.UserName, mailConfig.Password);
                    }
                    //client.Connect(smtpServer,
                    //    port, false);
                    //client.AuthenticationMechanisms.Remove("XOAUTH2");
                    //client.Authenticate(userName,
                    //    password);
                    client.Send(mailMessage);
                    result = 0;
                }
                catch (Exception error)
                {
                    _logger.LogError("Failed to send Mail: {0}",
                        error.Message);
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }

            _logger.LogDebug("<--Send");
            return result;
        }

        public async Task<int> SendEmail(Message message)
        {
            _logger.LogDebug("-->SendEmail");
            int result = -1;

            // Validate Input Parameters
            if (null == message)
            {
                _logger.LogError("Invalid Input Parameter");
                return result;
            }

            var emailMessage = await CreateEmailMessage(message);
            if (null == emailMessage)
            {
                _logger.LogError("CreateEmailMessage Failed");
                return result;
            }

            // Send Email
            result = await Send(emailMessage);
            if (0 != result)
            {
                _logger.LogError("Send Email Failed");
                return result;
            }

            //Return Success
            result = 0;

            _logger.LogDebug("<--SendEmail");
            return result;
        }
    }
}
