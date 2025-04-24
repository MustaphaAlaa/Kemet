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

public class SizeService : SaveService, ISizeService
{
    private readonly IBaseRepository<Size> _repository;
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
        : base(unitOfWork)
    {
        _sizeValidation = sizeValidation;
        _mapper = mapper;
        _logger = logger;
        _repositoryHelper = repoHelper;
        _repository = _unitOfWork.GetRepository<Size>();
    }

    public async Task<SizeReadDTO> CreateAsync(SizeCreateDTO entity)
    {
        try
        {
            await _sizeValidation.ValidateCreate(entity);

            var size = _mapper.Map<Size>(entity);

            var newSize = await _repository.CreateAsync(size);

            var createdSizeDTO = _mapper.Map<SizeReadDTO>(newSize);

            return createdSizeDTO;
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

    public async Task DeleteAsync(SizeDeleteDTO entity)
    {
        try
        {
            await _sizeValidation.ValidateDelete(entity);

            await _repository.DeleteAsync(Size => Size.SizeId == entity.SizeId);
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

    public async Task<SizeReadDTO> GetById(int key)
    {
        return await this.RetrieveByAsync(entity => entity.SizeId == key);
    }

    public async Task<SizeReadDTO> Update(SizeUpdateDTO updateRequest)
    {
        try
        {
            await _sizeValidation.ValidateUpdate(updateRequest);

            var size = _mapper.Map<Size>(updateRequest);

            size = _repository.Update(size);

            var result = _mapper.Map<SizeReadDTO>(size);

            return result;
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
