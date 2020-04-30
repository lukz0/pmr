using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace backend.Services
{
    public class RobotService : BackgroundWorker, IHostedService
    {
        public int Number { get; set; }
        public ILogger Logger { get; set; }

        public RobotService(ILogger<RobotService> logger)
        {
            Number = 1;
            Logger = logger;
        }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Number++;
                Logger.LogInformation("StartAsync ...." + Number);
                await Task.Delay(3000, cancellationToken);
            }
            throw new System.NotImplementedException();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Number = 0;
                Logger.LogInformation("StopetAsync ... " + Number);
                await Task.Delay(3000, cancellationToken);
            }
            throw new System.NotImplementedException();
        }
    }
}