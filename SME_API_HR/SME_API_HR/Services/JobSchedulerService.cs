using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SME_API_HR.Entities;
using SME_API_HR.Models;
using SME_API_HR.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class JobSchedulerService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public JobSchedulerService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<HRAPIDBContext>();
                var now = DateTime.Now;
                var jobs = await db.MScheduledJobs
                    .Where(j => j.IsActive == true && j.RunHour == now.Hour && j.RunMinute == now.Minute)
                    .ToListAsync(stoppingToken);

                foreach (var job in jobs)
                {
                    _ = RunJobAsync(job.JobName, scope.ServiceProvider);
                }
            }

            // Check every minute
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }

    private async Task RunJobAsync(string jobName, IServiceProvider serviceProvider)
    {
        switch (jobName)
        {
            case "business-units":
                await serviceProvider.GetRequiredService<BusinessUnitService>().BatchEndOfDay();
                break;
            case "job-titles":
                await serviceProvider.GetRequiredService<JobService>().BatchEndOfDay();
                break;
            case "employee":
                var Models = new searchEmployeeModels
                {
                    page = 1,
                    perPage = 100
                };
                await serviceProvider.GetRequiredService<EmployeeService>().GetEmployeeByBatchEndOfDay(Models);
                break;
        
            case "job-level":
                await serviceProvider.GetRequiredService<IMJobLevelService>().BatchEndOfDay();
                break;
           
            case "organization-tree":
                await serviceProvider.GetRequiredService<TOrganizationTreeService>().BatchEndOfDay();
                break;
            case "employee-contracts":
                var msearch = new searchEmployeeContractModels
                {
              
                    employmentDate = DateTime.Now.Date,
                    page = 1,
                    perPage = 100
                };
                await serviceProvider.GetRequiredService<ITEmployeeContractService>().BatchEndOfDay(msearch);
                break;
           
            
          
            case "position":
                await serviceProvider.GetRequiredService<IMPositionService>().BatchEndOfDay();
                break;
            default:
                // Optionally log unknown job
                break;
        }
    }
}