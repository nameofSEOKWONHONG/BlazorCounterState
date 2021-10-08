using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Timer = System.Timers.Timer;

namespace CounterState
{
    public class CounterStateBackgroundService : BackgroundService
    {
        private readonly ICounterStateViewModel _viewModel;
        public CounterStateBackgroundService(ICounterStateViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _viewModel.Count += 1;
                await Task.Delay(1000 * 5, stoppingToken);
            }
        }
    }
}