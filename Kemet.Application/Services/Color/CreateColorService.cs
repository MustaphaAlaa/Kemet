using System.Linq.Expressions;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices;
using Kemet.Application.Interfaces.Validations;

namespace Application.ColorServices;

public class CreateColorService : IColorService
{
    private IColorValidation _ColorValidation;
    private readonly IUnitOfWork _unitOfWork;
    IMapper _mapper;

    public async Task<ColorReadDTO> CreateAsync(ColorCreateDTO entity)
    {
        try
        {
            await _ColorValidation.ValidateCreate(entity);

            var color = _mapper.Map<Color>(entity);
            ,
            var newColor = await _unitOfWork.GetRepository<Color>().CreateAsync(color);

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

    public Task<ColorReadDTO> CreateAsync(Color entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(ColorDeleteDTO id)
    {
        throw new NotImplementedException();
    }

    public Task<List<ColorReadDTO>> RetrieveAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ColorReadDTO>> RetrieveAllAsync(Expression<Func<Color, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<ColorReadDTO> RetrieveByAsync(Expression<Func<Color, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<ColorReadDTO> UpdateAsync(ColorUpdateDTO updateRequest)
    {
        throw new NotImplementedException();
    }
}
