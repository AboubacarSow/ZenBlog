namespace zenblog.application.Common.Behaviors;

/// <summary>
/// ValidationBehavior is a pipeline behavior that validates requests using FluentValidation.
/// It intercepts requests before they reach the handler and performs validation.
/// If validation fails, it throws a ValidationException.
/// </summary>
public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
       if (!validators.Any())
        {
            return await next(cancellationToken);
        }

        var context = new ValidationContext<TRequest>(request);

        // Exécuter tous les validateurs
        var validationResults = await Task.WhenAll(
            validators.Select(v => v.ValidateAsync(context, cancellationToken))
        );

        // Aplatir la liste des erreurs (IEnumerable<ValidationFailure>)
        var failures = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();

        // S'il y a des erreurs, on lance UNE SEULE exception contenant TOUTES les erreurs
        if (failures.Count != 0)
        {
            throw new ValidationException(failures);
        }

        return await next(cancellationToken);
        // Call the next handler in the pipeline
    }
        
}