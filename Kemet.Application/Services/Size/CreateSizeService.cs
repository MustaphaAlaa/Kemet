

using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices.ISizeServices;
using Kemet.Application.Interfaces.Validations;
using Microsoft.Extensions.Logging;

namespace Application.SizeServices;

public class CreateSizeService : ICreateSize
{
    private readonly ICreateSizeValidation _createSizeValidation;
    private readonly ICreateAsync<Size> _createSize;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSizeService> _logger;

    public CreateSizeService(
        ICreateSizeValidation createSizeValidation,
        ICreateAsync<Size> createSize,
        IMapper mapper,
        ILogger<CreateSizeService> logger
)
    {
        _createSizeValidation = createSizeValidation;
        _createSize = createSize;
        _mapper = mapper;
        _logger = logger;
    }

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
            _logger.LogError($"An error occurred while creating the Size. /n{ex.Message}");
            throw;
        }
    }
}
