using EnterpriseGatewayPortal.Core.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;

namespace EnterpriseGatewayPortal.Core.Utilities
{
    public class LogClient : ILogClient
    {
        private readonly ILogger<LogClient> _logger;
        private readonly IConfiguration _configuration;

        protected static IModel AdminChannel { get; private set; }

        private readonly IUnitOfWork _unitOfWork;
        public static bool initLibrary = false;

        public LogClient(ILogger<LogClient> logger, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _logger.LogDebug("-->LogClient");

            if (false == initLibrary)
            {
                _logger.LogInformation("****INSIDE INITLIBRARY****");

                // Create connection to Rabbitmq server
                //int result = ConnectToRabbitMq();
                //if (0 != result)
                //{
                //    _logger.LogCritical("Cannot connect to RabbitMQ server");
                //    throw new Exception("Cannot connect to RabbitMQ server");
                //}
                initLibrary = true;
                _logger.LogInformation("****INITLIBRARY FINISHED****");
            }

            _logger.LogDebug("<--LogClient");
        }

        public int SendAdminLogMessage(string adminLogMessage)
        {
            _logger.LogDebug("-->SendAdminLogMessage");

            string queueName = _configuration["QueueName"];

            // Initialize variable
            int result = 0;

            // Validate input parameters
            if (null == adminLogMessage)
            {
                _logger.LogError("Invalid Input Parameter");
                return result;
            }

            try
            {
                // Convert central log message to bytes.
                var body = Encoding.UTF8.GetBytes(adminLogMessage);

                if (null != AdminChannel)
                {
                    // Publish log message to queue
                    AdminChannel.BasicPublish(exchange: "",
                                         routingKey: queueName,
                                         basicProperties: null,
                                         body: body);
                    // Set success value
                    result = 0;
                }
                else
                {
                    _logger.LogError("RabbitMQ Channel was invalid");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while sending Admin log message" +
                    " to server:{0}", ex.Message);
            }

            _logger.LogDebug("<--SendAdminLogMessage");
            return result;
        }

    }
}
