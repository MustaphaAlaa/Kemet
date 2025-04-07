using Application.Exceptions;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices.ISizeServices;
using Kemet.Application.Interfaces.Validations;

namespace Application.SizeServices;
public class UpdateSizeService : IUpdateSize
{
    private readonly IUpdateAsync<Size> _updateRepository;
    private readonly IRetrieveAsync<Size> _getRepository;
    private readonly IMapper _mapper;
    private readonly IUpdateSizeValidation _updateSizeValidation;

    public UpdateSizeService(IUpdateAsync<Size> updateRepository,
        IRetrieveAsync<Size> getRepository,
        IMapper mapper,
        IUpdateSizeValidation updateSizeValidation
)
    {
        _updateRepository = updateRepository;
        _getRepository = getRepository;
        _mapper = mapper;
        _updateSizeValidation = updateSizeValidation;
    }

    public async Task<SizeReadDTO> UpdateAsync(SizeUpdateDTO request)
    {
        try
        {
            var dto = await _updateSizeValidation.Validate(request);

            dto.Name = request.Name;


            var size = _mapper.Map<Size>(dto);


            size = await _updateRepository.UpdateAsync(size);


            var result = _mapper.Map<SizeReadDTO>(size);

            return result;
        }
        catch (Exception ex)
        {
            throw new FailedToUpdateException($"{ex.Message}");
            throw;
        }
    }
}