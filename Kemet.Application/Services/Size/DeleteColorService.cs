

using Entities.Models.DTOs;
using IRepository.Generic;
using IServices.IColorServices;

namespace Services.ColorServices;

public class DeleteColorService : IDeleteColor
{
    public DeleteColorService(IDeleteAsync<ColorDeleteDTO> deleteRepository)
    {
        _deleteRepository = deleteRepository;

    }

    private readonly IDeleteAsync<ColorDeleteDTO> _deleteRepository;


    public async Task<bool> DeleteAsync(ColorDeleteDTO dto)
    {
        if (dto.ColorId <= 0)
            throw new InvalidOperationException($"Invalid InternationalDrivingLicenseId.");

        //I didn't write get Color because DeleteAsync Repository will check if Color exist or not

        return await _deleteRepository.DeleteAsync(Color => Color.ColorId == dto.ColorId) > 0;
    }
}