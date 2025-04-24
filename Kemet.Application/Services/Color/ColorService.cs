using System.Linq.Expressions;
using Application.Exceptions;
using AutoMapper;
using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Helpers;
using Entities.Models.Interfaces.Validations;
using FluentValidation;
using IRepository.Generic;
using IServices;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ColorService : SaveService, IColorService
{
    private IColorValidation _colorValidation;

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
        : base(unitOfWork)
    {
        _colorValidation = colorValidation;
        _mapper = mapper;
        _repository = _unitOfWork.GetRepository<Color>();
        _logger = logger;

        _repositoryHelper = repositoryRetrieverHelper;
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
        catch (ValidationException ex)
        {
            string msg = $"Validating Exception is thrown while creating the color. {ex.Message}";
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
            string msg =
                $"An error thrown while validating the creation of the color. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
    }

    public async Task<List<ColorReadDTO>> RetrieveAllAsync()
    {
        try
        {
            return await _repositoryHelper.RetrieveAllAsync<ColorReadDTO>();
        }
        catch (Exception ex)
        {
            string msg =
                $"Unexpected exception throws while retrieving color records. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task<IEnumerable<ColorReadDTO>> RetrieveAllAsync(
        Expression<Func<Color, bool>> predicate
    )
    {
        try
        {
            return await _repositoryHelper.RetrieveAllAsync<ColorReadDTO>(predicate);
        }
        catch (Exception ex)
        {
            string msg =
                $"Unexpected exception throws while retrieving color records. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task<ColorReadDTO> RetrieveByAsync(Expression<Func<Color, bool>> predicate)
    {
        try
        {
            return await _repositoryHelper.RetrieveByAsync<ColorReadDTO>(predicate);
        }
        catch (Exception ex)
        {
            string msg =
                $"Unexpected exception throws while retrieving the color record. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task<ColorReadDTO> GetById(int key)
    {
        return await this.RetrieveByAsync(entity => entity.ColorId == key);
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
        catch (ValidationException ex)
        {
            string msg = $"Validating Exception is thrown while updating the color. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (DoesNotExistException ex)
        {
            string msg = $"Color doesn't exist. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg =
                $"An error thrown while validating the updating of the color. {ex.Message}";
            _logger.LogInformation(msg);
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
        catch (ValidationException ex)
        {
            string msg = $"An error thrown while deleting the color. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg = $"An error thrown while deleting the color. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
    }
}
