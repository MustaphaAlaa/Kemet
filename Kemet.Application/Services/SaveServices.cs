using IRepository.Generic;

namespace Application.Services;

public abstract class SaveService
{
    protected readonly IUnitOfWork _unitOfWork;

    protected SaveService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> SaveAsync()
    {
        return await _unitOfWork.SaveChangesAsync();
    }
}
