

using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices.IColorServices;

namespace Services.ColorServices;

public class CreateColorService : ICreateColor
{
    public CreateColorService(ICreateAsync<Color> repository, IRetrieveColor getColor,
        IMapper mapper)
    {
        _createColor = repository;
        _getColor = getColor;
        _mapper = mapper;
    }

    ICreateAsync<Color> _createColor;
    IRetrieveColor _getColor;
    IMapper _mapper;

    public async Task<ColorReadDTO> CreateAsync(ColorCreateDTO entity)
    {
        if (entity == null)
            throw new ArgumentNullException($" {typeof(ColorCreateDTO)} is Null");

        if (string.IsNullOrEmpty(entity.NameEn))
            throw new ArgumentException($"ColorDTOs NameEn cannot by null.");

        if (string.IsNullOrEmpty(entity.NameAr))
            throw new ArgumentException($"ColorDTOs NameAr cannot by null.");


        entity.NameAr = entity.NameAr?.Trim().ToLower();
        entity.NameEn = entity.NameEn?.Trim().ToLower();


        var Color = await _getColor.GetByAsync(c => c.Hexacode == entity.Hexacode || (c.NameAr == entity.NameAr) || c.NameEn == entity.NameAr);

        if (Color != null)
            throw new InvalidOperationException("ColorDTOs is already exist, cant duplicate Color.");


        var newColor = await _createColor.CreateAsync(_mapper.Map<Color>(entity));

        var createdColorDTO = _mapper.Map<ColorReadDTO>(newColor);

        return createdColorDTO;
    }
}