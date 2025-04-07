using Application.Exceptions;
using AutoMapper;
using Entities.Models.DTOs;
using IServices.IColorServices;
using Kemet.Application.Interfaces.Validations;
using Kemet.Application.Utilities;
namespace Kemet.Application.Validations;


public class CreateColorValidation : ICreateColorValidation
{
    private readonly IRetrieveColor _getColor;

    public CreateColorValidation(IRetrieveColor getColor)
    {
        _getColor = getColor;
    }

    public async Task Validate(ColorCreateDTO entity)
    {
        Utility.IsNull(entity);


        Utility.IsNullOrEmpty(entity.NameEn, "Color's English name");
        Utility.IsNullOrEmpty(entity.NameAr, "Color's Arabic name");
        Utility.IsNullOrEmpty(entity.Hexacode, "Hexacode");


        entity.NameAr = entity.NameAr?.Trim().ToLower();
        entity.NameEn = entity.NameEn?.Trim().ToLower();
        entity.Hexacode = entity.Hexacode?.Trim().ToLower();


        var Color = await _getColor.GetByAsync(c => c.Hexacode == entity.Hexacode || (c.NameAr == entity.NameAr) || c.NameEn == entity.NameAr);

        if (Color != null)
            throw new InvalidOperationException("ColorDTOs is already exist, cant duplicate Color.");

    }
}


public class UpdateColorValidation : IUpdateColorValidation
{
    private readonly IRetrieveColor _getColor;

    public UpdateColorValidation(IRetrieveColor getColor)
    {
        _getColor = getColor;
    }

    public async Task<ColorReadDTO> Validate(ColorUpdateDTO entity)
    {
        Utility.IsNull(entity);


        Utility.IdBoundry(entity.ColorId);

        Utility.IsNullOrEmpty(entity.NameEn, "Color's English name");
        Utility.IsNullOrEmpty(entity.NameAr, "Color's Arabic name");
        Utility.IsNullOrEmpty(entity.Hexacode, "Hexacode");

        entity.NameAr = entity.NameAr?.Trim().ToLower();
        entity.NameEn = entity.NameEn?.Trim().ToLower();
        entity.Hexacode = entity.Hexacode?.Trim().ToLower();

        var Color = await _getColor.GetByAsync(c => c.ColorId == entity.ColorId);

        Utility.DoesExist(Color, "Color");


        return Color;
    }


}



public class DeleteColorValidation : IDeleteColorValidation
{
    private readonly IRetrieveColor _getColor;

    public DeleteColorValidation(IRetrieveColor getColor)
    {
        getColor = _getColor;
    }

    public async Task Validate(ColorDeleteDTO entity)
    {
        Utility.IsNull(entity);
        Utility.IdBoundry(entity.ColorId);

        //Repository Will check if it exist or not

        //var Color = await _getColor.GetByAsync(c => c.ColorId == entity.ColorId);

        //Utility.DoesExist(Color, "Color");

    }
}