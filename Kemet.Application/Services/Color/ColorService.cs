﻿using System.Linq.Expressions;
using Application.Exceptions;
using AutoMapper;
using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Helpers;
using Entities.Models.Interfaces.Validations;
using IRepository.Generic;
using IServices;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ColorService : IColorService
{
    private IColorValidation _colorValidation;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IBaseRepository<Color> _repository;
    private readonly ILogger<ColorService> _logger;
    private readonly IRepositoryRetrieverHelper<Color> _repositoryHelper;

    public ColorService(
        IColorValidation colorValidation,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<ColorService> logger,
        IRepositoryRetrieverHelper<Color> repositoryRetrieverHelper
    )
    {
        _colorValidation = colorValidation;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _repository = _unitOfWork.GetRepository<Color>();
        _logger = logger;

        _repositoryHelper = repositoryRetrieverHelper;
    }

    public async Task<ColorReadDTO> CreateInternalAsync(ColorCreateDTO entity)
    {
        try
        {
            var newColor = await this.CreateAsync(entity);

            await _unitOfWork.SaveChangesAsync();

            return newColor;
        }
        catch (Exception ex)
        {
            // Log the exception (ex) here if needed
            // You can use a logging framework like Serilog, NLog, etc.
            // For now, just rethrow the exception to be handled by the caller
            throw new Exception("An error occurred while creating the color.", ex);
        }
    }

    public async Task<ColorReadDTO> CreateAsync(ColorCreateDTO entity)
    {
        try
        {
            await _colorValidation.ValidateCreate(entity);

            var color = _mapper.Map<Color>(entity);

            var newColor = await _repository.CreateAsync(color);

            var createdColorDTO = _mapper.Map<ColorReadDTO>(newColor);

            return createdColorDTO;
        }
        catch (Exception ex)
        {
            // Log the exception (ex) here if needed
            // You can use a logging framework like Serilog, NLog, etc.
            // For now, just rethrow the exception to be handled by the caller
            throw new Exception("An error occurred while creating the color.", ex);
        }
    }

    public async Task<List<ColorReadDTO>> RetrieveAllAsync()
    {
        return await _repositoryHelper.RetrieveAllAsync<ColorReadDTO>();
    }

    public async Task<IEnumerable<ColorReadDTO>> RetrieveAllAsync(
        Expression<Func<Color, bool>> predicate
    )
    {
        return await _repositoryHelper.RetrieveAllAsync<ColorReadDTO>(predicate);
    }

    public async Task<ColorReadDTO> RetrieveByAsync(Expression<Func<Color, bool>> predicate)
    {
        return await _repositoryHelper.RetrieveByAsync<ColorReadDTO>(predicate);
    }

    public async Task<ColorReadDTO> UpdateInternalAsync(ColorUpdateDTO updateRequest)
    {
        try
        {
            var updateDto = await this.Update(updateRequest);

            await _unitOfWork.SaveChangesAsync();

            return updateDto;
        }
        catch (Exception ex)
        {
            throw new FailedToUpdateException($"{ex.Message}");
            throw;
        }
    }

    public async Task<ColorReadDTO> Update(ColorUpdateDTO updateRequest)
    {
        try
        {
            await _colorValidation.ValidateUpdate(updateRequest);

            var color = _mapper.Map<Color>(updateRequest);

            color = _repository.Update(color);

            var result = _mapper.Map<ColorReadDTO>(color);

            return result;
        }
        catch (Exception ex)
        {
            throw new FailedToUpdateException($"{ex.Message}");
            throw;
        }
    }

    public async Task DeleteAsync(ColorDeleteDTO entity)
    {
        try
        {
            await _colorValidation.ValidateDelete(entity);
            await _repository.DeleteAsync(Color => Color.ColorId == entity.ColorId);
        }
        catch (Exception ex)
        {
            string msg = $"An error throwed while deleting the size. {ex.Message}";
            _logger.LogError(msg);
            throw new FailedToDeleteException(msg);
            throw;
        }
    }

    public async Task<bool> DeleteInternalAsync(ColorDeleteDTO entity)
    {
        try
        {
            await DeleteAsync(entity);
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
}
