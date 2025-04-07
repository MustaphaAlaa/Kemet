

using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices.ISizeServices;
using Kemet.Application.Interfaces.Validations;

namespace Services.SizeServices;

public class CreateSizeService(ICreateSizeValidation _createSizeValidation,
    ICreateAsync<Size> _createSize,
    IMapper _mapper) : ICreateSize
{

    public async Task<SizeReadDTO> CreateAsync(SizeCreateDTO entity)
    {
        try
        {

            await _createSizeValidation.Validate(entity);

            var newSize = await _createSize.CreateAsync(_mapper.Map<Size>(entity));

            var createdSizeDTO = _mapper.Map<SizeReadDTO>(newSize);

            return createdSizeDTO;
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