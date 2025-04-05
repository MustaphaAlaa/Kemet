using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices.IColorServices;
using System.Linq.Expressions;

namespace Services.ColorServices;

public class RetrieveColorService : IRetrieveColor

{
    private readonly IRetrieveAsync<Color> _getColor;
    private IMapper _mapper;


    public RetrieveColorService(IRetrieveAsync<Color> getColor, IMapper mapper)
    {
        _getColor = getColor;
        _mapper = mapper;
    }


    public async Task<ColorReadDTO?> GetByAsync(Expression<Func<Color?, bool>> predicate)
    {
        var color = await _getColor.GetAsync(predicate);
        var colorReadDTO = _mapper.Map<ColorReadDTO>(color);
        return colorReadDTO;
    }
}