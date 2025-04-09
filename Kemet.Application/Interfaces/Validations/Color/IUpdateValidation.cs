namespace Kemet.Application.Interfaces.Validations;

public interface IUpdateValidation<in T, TResult>
{
    Task<TResult> Validate(T entity);
}
