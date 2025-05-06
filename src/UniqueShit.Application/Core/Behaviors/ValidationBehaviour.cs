using FluentValidation;
using MediatR;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Domain.Core.Primitives;

namespace UniqueShit.Application.Core.Behaviors
{
    internal sealed class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
          where TRequest : class, ICommand<TResponse>
          where TResponse : class
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var validationFailures = await Task.WhenAll(
                _validators.Select(validator => validator.ValidateAsync(context)));

            var errors = validationFailures
             .SelectMany(x => x.Errors)
             .ToList()
             .ConvertAll(error => Error.Validation
             (
                 errorCode: error.PropertyName,
                 message: error.ErrorMessage
             ));

            if (errors.Count != 0)
            {
                return (dynamic)errors;
            }

            return await next();
        }
    }
}
