using AutoMapper;
using Entities.Models.DTOs;
using IServices.IColorServices;
using Kemet.Application.Interfaces.Validations;
namespace Kemet.Application.Validations;


public class CreateColorValidation(IRetrieveColor _getColor) : ICreateColorValidation
{
    public async Task Validate(ColorCreateDTO entity)
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

    }
}


public class UpdateColorValidation(IRetrieveColor _getColor) : IUpdateColorValidation
{

    public async Task<ColorReadDTO> Validate(ColorUpdateDTO entity)
    {
        if (entity == null)
            throw new ArgumentNullException($" {typeof(ColorUpdateDTO)} is Null");

        if (entity.ColorId <= 0)
            throw new InvalidOperationException("Color's' Id Is out of boundry");


        if (string.IsNullOrEmpty(entity.NameEn))
            throw new ArgumentException($"Color's English name cannot by null.");

        if (string.IsNullOrEmpty(entity.NameAr))
            throw new ArgumentException($"Color's Arabic name cannot by null.");

        if (string.IsNullOrEmpty(entity.Hexacode))
            throw new ArgumentException($"Hexacode cannot by null.");



        entity.NameAr = entity.NameAr?.Trim().ToLower();
        entity.NameEn = entity.NameEn?.Trim().ToLower();
        entity.Hexacode = entity.Hexacode?.Trim().ToLower();

        var Color = await _getColor.GetByAsync(c => c.ColorId != entity.ColorId && (c.Hexacode == entity.Hexacode || (c.NameAr == entity.NameAr) || c.NameEn == entity.NameAr));

        if (Color == null)
            throw new InvalidOperationException($"{entity.ColorId} doesn't exist.");

        return Color;
    }
}



public class DeleteColorValidation(IRetrieveColor _getColor) : IDeleteColorValidation
{

    public async Task Validate(ColorDeleteDTO entity)
    {
        if (entity == null)
            throw new ArgumentNullException($" {typeof(ColorUpdateDTO)} is Null");

        if (entity.ColorId <= 0)
            throw new InvalidOperationException("Color's' Id Is out of boundry");


        var Color = await _getColor.GetByAsync(c => c.ColorId != entity.ColorId);

        if (Color == null)
            throw new InvalidOperationException($"{entity.ColorId} doesn't exist.");
    }
}