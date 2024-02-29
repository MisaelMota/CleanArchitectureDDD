using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Behaviours
{
    //validaciones para los request
    public class UnhandledExceptionBehaviour<TResquest, TResponse> : IPipelineBehavior<TResquest, TResponse>
    {
        private readonly ILogger<TResquest> _logger;

        public UnhandledExceptionBehaviour(ILogger<TResquest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TResquest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TResquest).Name;
                _logger.LogError(ex, "Application Request: Sucedio una excepcion para el request {Name} {@Request}",requestName,request);
                throw;
            }
        }
    }
}
