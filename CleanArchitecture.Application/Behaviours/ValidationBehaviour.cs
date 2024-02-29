using CleanArchitecture.Application.Exceptions;
using FluentValidation;
using MediatR;
using ValidationException = CleanArchitecture.Application.Exceptions.ValidationException;

namespace CleanArchitecture.Application.Behaviours
{
    /*
     Tambien llamado "tubo" o "pipeline", es una clase que valida los mensajes de error y en caso de encontrar uno
     lo intercepta antes de que caiga como un error o exeption en la repuesta de la api 
     */
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
           if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                //Esta linea busca y ejecuta las vslaidaciones en el pipeline y no al final en el metodo ejecutado
                var validationResults = await Task.WhenAll(_validators.Select(v=> v.ValidateAsync(context, cancellationToken)));

               var failtures= validationResults.SelectMany(r=> r.Errors).Where(f=>f!=null).ToList();

                if (failtures.Count()!=0) 
                { 
                    throw new ValidationException(failtures);
                }

            }
           return await next();
        }
    }
}
