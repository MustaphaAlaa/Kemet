using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Application.Exceptions;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices;
using Kemet.Application.DTOs;
using Kemet.Application.Interfaces.Validations;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ColorService : IColorService
{
    private IColorValidation _colorValidation;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IBaseRepository<Color> _repository;
    private readonly ILogger<ColorService> _logger;

    public ColorService(
        IColorValidation colorValidation,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<ColorService> logger
    )
    {
        _colorValidation = colorValidation;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _repository = _unitOfWork.GetRepository<Color>();
        _logger = logger;
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

    public async Task<bool> DeleteAsync(ColorDeleteDTO entity)
    {
        try
        {
            await _colorValidation.ValidateDelete(entity);
            await _repository.DeleteAsync(Color => Color.ColorId == entity.ColorId);
            return (await _unitOfWork.CompleteAsync()) > 0;
        }
        catch (Exception ex)
        {
            string msg = $"An error throwed while deleting the size. {ex.Message}";
            _logger.LogError(msg);
            throw new FailedToDeleteException(msg);
            throw;
        }
    }

    public async Task<List<ColorReadDTO>> RetrieveAllAsync()
    {
        List<Color> colors = await _repository.RetrieveAllAsync();

        return colors.Select(Color => _mapper.Map<ColorReadDTO>(Color)).ToList();
    }

    public async Task<IEnumerable<ColorReadDTO>> RetrieveAllAsync(
        Expression<Func<Color, bool>> predicate
    )
    {
        var colors = await _repository.RetrieveAllAsync(predicate);
        return colors.Select(Color => _mapper.Map<ColorReadDTO>(Color));
    }

    public async Task<ColorReadDTO> RetrieveByAsync(Expression<Func<Color, bool>> predicate)
    {
        var color = await _repository.RetrieveAsync(predicate);
        var colorReadDTO = _mapper.Map<ColorReadDTO>(color);
        return colorReadDTO;
    }

    public async Task<ColorReadDTO> UpdateAsync(ColorUpdateDTO updateRequest)
    {
        try
        {
            await _colorValidation.ValidateUpdate(updateRequest);

            var color = _mapper.Map<Color>(updateRequest);

            color = _repository.Update(color);

            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<ColorReadDTO>(color);

            return result;
        }
        catch (Exception ex)
        {
            throw new FailedToUpdateException($"{ex.Message}");
            throw;
        }
    }
}
