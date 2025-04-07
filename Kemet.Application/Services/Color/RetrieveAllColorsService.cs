

using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices.IColorServices;
using System.Linq.Expressions;

namespace Application.ColorServices;

public class RetrieveAllColorsService : IRetrieveAllColors

{
    private readonly IRetrieveAllAsync<Color> _getColors;
    private IMapper _mapper;


    public RetrieveAllColorsService(IRetrieveAllAsync<Color> getColors, IMapper mapper)
    {
        _getColors = getColors;
        _mapper = mapper;
    }


    public async Task<List<ColorReadDTO>> GetAllAsync()
    {
        List<Color> colors = await _getColors.GetAllAsync();

        return colors
            .Select(Color => _mapper.Map<ColorReadDTO>(Color))
            .ToList();
    }


    public async Task<IQueryable<ColorReadDTO>> GetAllAsync(Expression<Func<Color, bool>> predicate)
    {
        var colors = await _getColors.GetAllAsync(predicate);
        var queryColors = colors.Select(Color => _mapper.Map<ColorReadDTO>(Color));
        return queryColors;
    }
}