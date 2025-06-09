using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Domain.Aggregates.QrMasterAggregate.Repositories;
using Domain.Aggregates.UserAggregate.Repositories;
using Domain.Aggregates.DecisionMapAggregate.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories;
using MediatR;
using Infrastructure.Behaviors;
using Microsoft.Extensions.Logging;

namespace Application
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer("Server=localhost;Database=DecisionMapDB;Trusted_Connection=True;TrustServerCertificate=True;");
                options.LogTo(Console.WriteLine,
                  new[] { Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.CommandExecuted },
                  LogLevel.Information);
            });


            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));

            services.AddTransient<IUserAccountRepository, UserAccountRepository>();
            services.AddTransient<IQrMasterRepository, QrMasterRepository>();
            services.AddTransient<IDecisionMapRepository, DecisionMapRepository>();
            return services;
        }
    }
}
