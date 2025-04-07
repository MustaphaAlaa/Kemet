using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Entities.Models.DTOs;
using IServices.IColorServices;
using Kemet.Application.Interfaces.Facades;

namespace Kemet.Application.Facades;

public class ColorFacade : IColorFacade
{
    private ICreateColor _createColor;
    private IUpdateColor _updateColor;
    private IDeleteColor _deleteColor;
    private IRetrieveColor _retrieveColor;
    private IRetrieveAllColors _retrieveAllColors;

    public ColorFacade(
        ICreateColor createColor,
        IUpdateColor updateColor,
        IDeleteColor deleteColor,
        IRetrieveColor retrieveColor,
        IRetrieveAllColors retrieveAllColors
    )
    {
        _createColor = createColor;
        _updateColor = updateColor;
        _deleteColor = deleteColor;
        _retrieveColor = retrieveColor;
        _retrieveAllColors = retrieveAllColors;
    }

    public async Task<ColorReadDTO> Create(ColorCreateDTO dto)
    {
        var color = await _createColor.CreateAsync(dto);
        return color;
    }

    public async Task<ColorReadDTO> Update(ColorUpdateDTO dto)
    {
        var color = await _updateColor.UpdateAsync(dto);
        return color;
    }

    public async Task<ColorReadDTO> Retrieve(Expression<Func<Color, bool>> predicate)
    {
        var color = await _retrieveColor.GetByAsync(predicate);
        return color;
    }

    public async Task<List<ColorReadDTO>> RetrieveAll()
    {
        var colors = await _retrieveAllColors.GetAllAsync();
        return colors;
    }

    public async Task<IQueryable<ColorReadDTO>> RetrieveAll(
        Expression<Func<Color, bool>> predicate
    )
    {
        var colors = await _retrieveAllColors.GetAllAsync(predicate);
        return colors;
    }

    public Task<bool> Delete(ColorDeleteDTO dto)
    {
        var result = _deleteColor.DeleteAsync(dto);
        return result;
    }
}
