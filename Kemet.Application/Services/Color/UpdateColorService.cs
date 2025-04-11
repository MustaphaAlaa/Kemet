using System.Linq.Expressions;
using Application.Exceptions;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices;
using Kemet.Application.Interfaces.Validations;

namespace Application.ColorServices;

public class UpdateColorService : IColorService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IColorValidation _colorValidation;

    public UpdateColorService(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IColorValidation colorValidation
    )
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _colorValidation = colorValidation;
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

    public async Task<ColorReadDTO> UpdateAsync(ColorUpdateDTO request)
    {
        try
        {
            await _colorValidation.ValidateUpdate(request);

            var color = _mapper.Map<Color>(request);

            color = _unitOfWork.GetRepository<Color>().Update(color);

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
