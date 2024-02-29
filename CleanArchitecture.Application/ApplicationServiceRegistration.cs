using CleanArchitecture.Application.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitecture.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services)
        {
            //Lee e inyecta todas las clases que esten implementando las interfaces del automaper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

           /* Busca todas las clases del proyecto application que este referenciando a los paquetes de fluentValidation 
            y va a inyectar las dependencias necesarias para que funcionen dentro del proyecto */
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            //Lee e inyecta todas las clases que esten implementando las interfaces del Mediatr
            services.AddMediatR(Assembly.GetExecutingAssembly());

            //Lee e inyecta todas las clases que esten implementando las interfaces del PipeLineBehavior
            services.AddTransient(typeof(IPipelineBehavior<,>),typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>),typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
