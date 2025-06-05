using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Domain.Aggregates.QrMasterAggregate.Repositories;
using Domain.Aggregates.UserAggregate.Repositories;
using Domain.Aggregates.DecisionMapAggregate.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories;
using MediatR;
using Infrastructure.Behaviors;

namespace Application
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer("Server=localhost;Database=DecisionMapDB;Trusted_Connection=True;TrustServerCertificate=True;"));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));

            services.AddTransient<IUserAccountRepository, UserAccountRepository>();
            services.AddTransient<IQrMasterRepository, QrMasterRepository>();
            services.AddTransient<IDecisionMapRepository, DecisionMapRepository>();
            return services;
        }
    }
}
