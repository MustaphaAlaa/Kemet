using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices.IColorServices;

namespace Services.ColorServices;

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
        //move all commented code to validate class




        var Color = await _getRepository.GetAsync(Color => Color.ColorId == request.ColorId);

        //if (Color is null)
        //    throw new InvalidOperationException("ColorDTOs isn't exist in db");

        //Color.ColorName = request.Name;
        await _updateRepository.UpdateAsync(Color);

        var result = _mapper.Map<ColorReadDTO>(Color);

        return result;
    }
}