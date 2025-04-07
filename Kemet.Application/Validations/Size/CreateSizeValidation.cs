using AutoMapper;
using Entities.Models.DTOs;
using IServices.ISizeServices;
using Kemet.Application.Interfaces.Validations;
using Kemet.Application.Utilities;



namespace Kemet.Application.Validations;


public class CreateSizeValidation(IRetrieveSize _getSize) : ICreateSizeValidation
{

    public async Task Validate(SizeCreateDTO entity)
    {
        Utility.IsNull(entity);

        if (string.IsNullOrEmpty(entity.Name))
            throw new ArgumentException($"SizeDTOs Name cannot by null.");


        entity.Name = entity.Name?.Trim().ToLower();


        var Size = await _getSize.GetByAsync(c => c.Name == entity.Name);

        if (Size != null)
            throw new InvalidOperationException("SizeDTOs is already exist, cant duplicate Size.");

    }
}


internal class UpdateSizeValidation(IRetrieveSize _getSize) : IUpdateSizeValidation
{

    public async Task<SizeReadDTO> Validate(SizeUpdateDTO entity)
    {
        Utility.IsNull(entity);

        if (entity.SizeId <= 0)
            throw new InvalidOperationException("Size's' Id Is out of boundry");


        if (string.IsNullOrEmpty(entity.Name))
            throw new ArgumentException($"Size's name cannot by null.");




        entity.Name = entity.Name?.Trim().ToLower();

        var Size = await _getSize.GetByAsync(c => c.SizeId != entity.SizeId);

        if (Size == null)
            throw new InvalidOperationException($"{entity.SizeId} doesn't exist.");

        return Size;
    }
}




public class DeleteSizeValidation(IRetrieveSize _getSize) : IDeleteSizeValidation
{

    public async Task Validate(SizeDeleteDTO entity)
    {

        Utility.IsNull(entity);
        Utility.IdBoundry(entity.SizeId);

        //var Size = await _getSize.GetByAsync(c => c.SizeId != entity.SizeId);

        //if (Size == null)
        //    throw new InvalidOperationException($"Size With Id {entity.SizeId} doesn't exist.");
    }
}