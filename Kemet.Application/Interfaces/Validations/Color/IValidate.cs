namespace Kemet.Application.Interfaces.Validations;

public interface IValidate<T>
{
    Task Validate(T entity);
}
