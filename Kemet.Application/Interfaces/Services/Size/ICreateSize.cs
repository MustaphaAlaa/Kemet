
using Entities.Models.DTOs;
using Interfaces.IServices;

namespace IServices.ISizeServices;
public interface ICreateSize : ICreateServiceAsync<SizeCreateDTO, SizeReadDTO>
{
}

