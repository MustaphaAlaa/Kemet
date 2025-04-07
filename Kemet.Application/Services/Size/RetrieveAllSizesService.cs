

using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices.ISizeServices;
using System.Linq.Expressions;

namespace Application.SizeServices;
public class RetrieveAllSizesService : IRetrieveAllSizes

{
    private readonly IRetrieveAllAsync<Size> _getSizes;
    private IMapper _mapper;


    public RetrieveAllSizesService(IRetrieveAllAsync<Size> getSizes, IMapper mapper)
    {
        _getSizes = getSizes;
        _mapper = mapper;
    }


    public async Task<List<SizeReadDTO>> GetAllAsync()
    {
        List<Size> sizes = await _getSizes.GetAllAsync();

        return sizes
            .Select(Size => _mapper.Map<SizeReadDTO>(Size))
            .ToList();
    }


    public async Task<IQueryable<SizeReadDTO>> GetAllAsync(Expression<Func<Size, bool>> predicate)
    {
        var sizes = await _getSizes.GetAllAsync(predicate);
        var querySizes = sizes.Select(Size => _mapper.Map<SizeReadDTO>(Size));
        return querySizes;
    }
}