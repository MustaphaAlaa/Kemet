namespace Entities.Models.Interfaces.Validations;

public interface IValidator<TCreate, TUpdate, TDelete>
{
    Task ValidateCreate(TCreate entity);
    Task ValidateUpdate(TUpdate entity);
    Task ValidateDelete(TDelete entity);
}
