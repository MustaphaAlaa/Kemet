using System.Linq.Expressions;
using Application.Exceptions;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices;
using Entities.Models.Interfaces.Helpers;
using Entities.Models.Interfaces.Validations;
using Microsoft.Extensions.Logging;

namespace Application.SizeServices;

public class SizeService : ISizeService
{
    private readonly IBaseRepository<Size> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISizeValidation _sizeValidation;
    private readonly IMapper _mapper;
    private readonly ILogger<SizeService> _logger;
    private readonly IRepositoryRetrieverHelper<Size> _repositoryHelper;

    public SizeService(
        IUnitOfWork unitOfWork,
        ISizeValidation sizeValidation,
        IMapper mapper,
        ILogger<SizeService> logger,
        IRepositoryRetrieverHelper<Size> repoHelper
    )
    {
        _unitOfWork = unitOfWork;
        _sizeValidation = sizeValidation;
        _mapper = mapper;
        _logger = logger;
        _repositoryHelper = repoHelper;
        _repository = _unitOfWork.GetRepository<Size>();
    }

    public async Task<SizeReadDTO> CreateInternalAsync(SizeCreateDTO entity)
    {
        try
        {
            var newSize = await this.CreateAsync(entity);

            await _unitOfWork.SaveChangesAsync();

            return newSize;
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while creating the Size. /n{ex.Message}");
            throw;
        }
    }

    public async Task<SizeReadDTO> CreateAsync(SizeCreateDTO entity)
    {
        try
        {
            await _sizeValidation.ValidateCreate(entity);

            var newSize = await _repository.CreateAsync(_mapper.Map<Size>(entity));

            return _mapper.Map<SizeReadDTO>(newSize);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while creating the Size. /n{ex.Message}");
            throw;
        }
    }

    public async Task<bool> DeleteAsync(SizeDeleteDTO entity)
    {
        try
        {
            await _sizeValidation.ValidateDelete(entity);

            await _repository.DeleteAsync(Size => Size.SizeId == entity.SizeId);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            string msg = $"An error throwed while deleting the size. {ex.Message}";
            _logger.LogError(msg);
            throw new FailedToDeleteException(msg);
            throw;
        }
    }

    public async Task<List<SizeReadDTO>> RetrieveAllAsync()
    {
        return await _repositoryHelper.RetrieveAllAsync<SizeReadDTO>();
    }

    public async Task<IEnumerable<SizeReadDTO>> RetrieveAllAsync(
        Expression<Func<Size, bool>> predicate
    )
    {
        return await _repositoryHelper.RetrieveAllAsync<SizeReadDTO>(predicate);
    }

    public async Task<SizeReadDTO> RetrieveByAsync(Expression<Func<Size, bool>> predicate)
    {
        return await _repositoryHelper.RetrieveByAsync<SizeReadDTO>(predicate);
    }

    public async Task<SizeReadDTO> UpdateInternalAsync(SizeUpdateDTO updateRequest)
    {
        try
        {
            var size = await this.Update(updateRequest);
            await _unitOfWork.SaveChangesAsync();
            return size;
        }
        catch (Exception ex)
        {
            throw new FailedToUpdateException($"{ex.Message}");
            throw;
        }
    }

    public async Task<SizeReadDTO> Update(SizeUpdateDTO updateRequest)
    {
        try
        {
            await _sizeValidation.ValidateUpdate(updateRequest);

            var size = _mapper.Map<Size>(updateRequest);

            size = _repository.Update(size);
            return _mapper.Map<SizeReadDTO>(size);
        }
        catch (Exception ex)
        {
            throw new FailedToUpdateException($"{ex.Message}");
            throw;
        }
    }
}
