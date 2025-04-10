
using Entities.Models.DTOs;
using Domain.IServices;

namespace IServices.ISizeServices;
public interface ICreateSize : IServiceAsync<SizeCreateDTO, SizeReadDTO>
{
}

