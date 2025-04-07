using System.Linq.Expressions;
using Entities.Models;
using Entities.Models.DTOs;
using IServices.ISizeServices;
using Kemet.Application.Interfaces.Facades;

namespace Kemet.Application.Facades
{
    public class SizeFacade(
            ICreateSize _createSize,
            IUpdateSize _updateSize,
            IDeleteSize _deleteSize,
            IRetrieveSize _retrieveSize,
            IRetrieveAllSizes _retrieveAllSizes ) : ISizeFacade
    {


        public async Task<SizeReadDTO> Create(SizeCreateDTO dto)
        {
            var color = await _createSize.CreateAsync(dto);
            return color;
        }

        public async Task<SizeReadDTO> Update(SizeUpdateDTO dto)
        {
            var color = await _updateSize.UpdateAsync(dto);
            return color;
        }

        public async Task<SizeReadDTO> Retrieve(Expression<Func<Size, bool>> predicate)
        {
            var color = await _retrieveSize.GetByAsync(predicate);
            return color;
        }

        public async Task<List<SizeReadDTO>> RetrieveAll()
        {
            var colors = await _retrieveAllSizes.GetAllAsync();
            return colors;
        }

        public async Task<IQueryable<SizeReadDTO>> RetrieveAll(
            Expression<Func<Size, bool>> predicate
        )
        {
            var colors = await _retrieveAllSizes.GetAllAsync(predicate);
            return colors;
        }

        public Task<bool> Delete(SizeDeleteDTO dto)
        {
            var result = _deleteSize.DeleteAsync(dto);
            return result;
        }
    }
}
