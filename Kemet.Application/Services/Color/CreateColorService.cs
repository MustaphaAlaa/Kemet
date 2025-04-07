

using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices.IColorServices;
using Kemet.Application.Interfaces.Validations;

namespace Application.ColorServices;

public class CreateColorService : ICreateColor
{

    private ICreateColorValidation _createColorValidation;
    ICreateAsync<Color> _createColor;
    IMapper _mapper;

    public CreateColorService(ICreateColorValidation createColorValidation, ICreateAsync<Color> createColor, IMapper mapper)
    {
        _createColorValidation = createColorValidation;
        _createColor = createColor;
        _mapper = mapper;
    }

    public async Task<ColorReadDTO> CreateAsync(ColorCreateDTO entity)
    {

        try
        {

            await _createColorValidation.Validate(entity);

            var newColor = await _createColor.CreateAsync(_mapper.Map<Color>(entity));

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
}