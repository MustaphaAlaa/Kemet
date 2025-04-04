using Entities.Models;
using Entities.Models.DTOs;
using Interfaces.IServices;

namespace IServices.ISizeServices;
public interface IRetrieveAllSizes : IRetrieveAllServiceAsync<Size, SizeReadDTO>
{
}

