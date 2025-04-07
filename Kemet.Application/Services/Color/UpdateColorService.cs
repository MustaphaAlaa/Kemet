using Application.Exceptions;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices.IColorServices;

namespace Application.ColorServices;

public class UpdateColorService : IUpdateColor
{
    public UpdateColorService(IUpdateAsync<Color> updateRepository,
        IRetrieveAsync<Color> getRepository,
        IMapper mapper)
    {
        _updateRepository = updateRepository;
        _getRepository = getRepository;
        _mapper = mapper;
    }

    private readonly IUpdateAsync<Color> _updateRepository;
    private readonly IRetrieveAsync<Color> _getRepository;
    private readonly IMapper _mapper;





    public async Task<ColorReadDTO> UpdateAsync(ColorUpdateDTO request)
    {
        try
        {
            var color = await _getRepository.GetAsync(Color => Color.ColorId == request.ColorId);


            color.NameAr = request.NameAr;
            color.NameEn = request.NameEn;
            color.Hexacode = request.Hexacode;

            await _updateRepository.UpdateAsync(color);

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