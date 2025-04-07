using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices.ISizeServices;
using System.Linq.Expressions;

namespace Application.SizeServices;
public class RetrieveSizeService : IRetrieveSize

{
    private readonly IRetrieveAsync<Size> _getSize;
    private readonly IMapper _mapper;


    public RetrieveSizeService(IRetrieveAsync<Size> getSize, IMapper mapper)
    {
        _getSize = getSize;
        _mapper = mapper;
    }


    public async Task<SizeReadDTO?> GetByAsync(Expression<Func<Size?, bool>> predicate)
    {
        var color = await _getSize.GetAsync(predicate);
        var colorReadDTO = _mapper.Map<SizeReadDTO>(color);
        return colorReadDTO;
    }
}