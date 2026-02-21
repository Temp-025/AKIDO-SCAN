using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Utilities
{
    public class BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<BackgroundService> _logger;

        public BackgroundService(IServiceScopeFactory scopeFactory,
            ILogger<BackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        public void FireAndForget<T>(Action<T> action) where T : notnull
        {
            _logger.LogInformation("FireAndForget start...");

            Task.Run(() =>
            {
                using var scope = _scopeFactory.CreateScope();
                var dependency = scope.ServiceProvider.GetRequiredService<T>();
                try
                {
                    action(dependency);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "FireAndForget failed: " + ex.Message);
                }
                finally
                {
                    dependency = default;
                }

                _logger.LogInformation("FireAndForget end...");
            });
        }

        public void FireAndForgetAsync<T>(Func<T, Task> action) where T : notnull
        {
            _logger.LogInformation("FireAndForgetAsync start...");

            Task.Run(async () =>
            {
                using var scope = _scopeFactory.CreateScope();
                var dependency = scope.ServiceProvider.GetRequiredService<T>();
                try
                {
                    await action(dependency);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "FireAndForgetAsync failed: " + ex.Message);
                }
                finally
                {
                    dependency = default;
                }

                _logger.LogInformation("FireAndForgetAsync end...");
            });
        }
    }

}
