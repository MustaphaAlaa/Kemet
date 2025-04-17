using System.Linq.Expressions;
using Application.Exceptions;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Helpers;
using Entities.Models.Interfaces.Validations;
using FluentValidation;
using IRepository.Generic;
using IServices;
using Microsoft.Extensions.Logging;

namespace Application.Services;

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

    private async Task<SizeReadDTO> CreateSizeCore(SizeCreateDTO entity)
    {
        await _sizeValidation.ValidateCreate(entity);

        var size = _mapper.Map<Size>(entity);

        var newSize = await _repository.CreateAsync(size);

        var createdSizeDTO = _mapper.Map<SizeReadDTO>(newSize);

        return createdSizeDTO;
    }

    public async Task<SizeReadDTO> CreateInternalAsync(SizeCreateDTO entity)
    {
        try
        {
            var newSize = await this.CreateSizeCore(entity);

            await _unitOfWork.SaveChangesAsync();

            return newSize;
        }
        catch (ValidationException ex)
        {
            string msg = $"Validating Exception is thrown while creating the size. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (AlreadyExistException ex)
        {
            string msg = $"Color is already exist. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg = $"An error thrown while validating the creation of the size. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
    }

    public async Task<SizeReadDTO> CreateAsync(SizeCreateDTO entity)
    {
        try
        {
            var size = await CreateSizeCore(entity);
            return size;
        }
        catch (ValidationException ex)
        {
            string msg = $"Validating Exception is thrown while creating the size. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (AlreadyExistException ex)
        {
            string msg = $"Size is already exist. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg = $"An error thrown while validating the creation of the size. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
    }

    public async Task DeleteSizeCore(SizeDeleteDTO entity)
    {
        await _sizeValidation.ValidateDelete(entity);

        await _repository.DeleteAsync(Size => Size.SizeId == entity.SizeId);
    }

    public async Task DeleteAsync(SizeDeleteDTO entity)
    {
        try
        {
            await DeleteSizeCore(entity);
        }
        catch (ValidationException ex)
        {
            string msg = $"An error thrown while deleting the size. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg = $"An error thrown while deleting the size. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
    }

    public async Task<bool> DeleteInternalAsync(SizeDeleteDTO entity)
    {
        try
        {
            await this.DeleteSizeCore(entity);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
        catch (ValidationException ex)
        {
            string msg = $"An error thrown while deleting the size. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg = $"An error thrown while deleting the size. {ex.Message}";
            _logger.LogInformation(msg);
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

    private async Task<ColorReadDTO> UpdateSizeCore(SizeUpdateDTO updateRequest)
    {
        await _sizeValidation.ValidateUpdate(updateRequest);

        var size = _mapper.Map<Size>(updateRequest);

        size = _repository.Update(size);

        var result = _mapper.Map<ColorReadDTO>(size);

        return result;
    }

    public async Task<SizeReadDTO> UpdateInternalAsync(SizeUpdateDTO updateRequest)
    {
        try
        {
            var size = await this.Update(updateRequest);
            await _unitOfWork.SaveChangesAsync();
            return size;
        }
        catch (ValidationException ex)
        {
            string msg = $"Validating Exception is thrown while updating the size. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (DoesNotExistException ex)
        {
            string msg = $"Size doesn't exist. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg = $"An error thrown while validating the updating of the size. {ex.Message}";
            _logger.LogInformation(msg);
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
        catch (ValidationException ex)
        {
            string msg = $"Validating Exception is thrown while updating the size. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (DoesNotExistException ex)
        {
            string msg = $"Size doesn't exist. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg = $"An error thrown while validating the updating of the size. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
    }
}
