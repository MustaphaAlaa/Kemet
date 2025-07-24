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
using Kemet.Application.Interfaces;
using Kemet.Application.Services;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ColorService : GenericService<Color, ColorReadDTO, ColorService>, IColorService
{
    private IColorValidation _colorValidation;
    private readonly IBaseRepository<Color> _repository;

    public ColorService(
        IServiceFacade_DependenceInjection<Color, ColorService> facadeDI,
        IColorValidation colorValidation
    )
        : base(facadeDI, "Color")
    {
        _colorValidation = colorValidation;
        _repository = _unitOfWork.GetRepository<Color>();

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
            string msg = $"Validating Exception is thrown while creating the {TName}. {ex.Message}";
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
                $"An error thrown while validating the creation of the {TName}. {ex.Message}";
            _logger.LogInformation(msg);
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
            string msg = $"Validating Exception is thrown while updating the {TName}. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (DoesNotExistException ex)
        {
            string msg = $"{TName} doesn't exist. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg =
                $"An error thrown while validating the updating of the {TName}. {ex.Message}";
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
            string msg = $"An error thrown while deleting the {TName}. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg = $"An error thrown while deleting the {TName}. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
    }
}
