using Entities.Models;
using Entities.Models.DTOs;
using Domain.IServices;

namespace IServices.ISizeServices;
public interface IRetrieveAllSizes : IRetrieveAllServiceAsync<Size, SizeReadDTO>
{
}

