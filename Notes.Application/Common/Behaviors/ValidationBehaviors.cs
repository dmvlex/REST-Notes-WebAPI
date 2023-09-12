using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Common.Behaviors
{
    //Задаем поведение валидации. Встраиваем в пайплайн медиатора
    public class ValidationBehaviors<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidationBehaviors(IEnumerable<IValidator<TRequest>> _validators)
        {
            validators = _validators;
        }

        public Task<TResponse> Handle(TRequest request, 
            RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(failure => failure != null)
                .ToList(); //получаем список ошибок

            if (failures.Count != 0)
            {
                throw new FluentValidation.ValidationException(failures);
            }

            return next();
        }
    }
}
