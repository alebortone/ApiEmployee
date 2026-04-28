using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MinhaApi.Email.App;
using MinhaAPI.Aplication.Interfaces;
using MinhaAPI.Aplication.UseCases.Employees.GetPdfEmployee;

namespace MinhaApi.Scheduler
{
    public class SchedulerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public SchedulerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var pdfHandler = scope.ServiceProvider.GetRequiredService<GetPdfEmployeeHandler>();
                    var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                    var repo = scope.ServiceProvider.GetRequiredService<IEmployeeRepository>();

                    var employees = await repo.GetAll(); 

                    foreach (var employee in employees)
                    {
                        if (string.IsNullOrEmpty(employee.Photo))
                        {
                            continue;
                        }
                        var pdf = await pdfHandler.Handler(new GetPdfEmployeeQuery(employee.Id));

                        var emailModel = new EmployeeEmailModel(employee.Name, employee.Email);

                        emailService.SendEmail(emailModel, pdf);
                    }
                }

                await Task.Delay(TimeSpan.FromMinutes(0.5), stoppingToken);
            }
        }
    }
}
