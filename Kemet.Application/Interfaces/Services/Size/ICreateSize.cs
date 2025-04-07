
using Entities.Models.DTOs;
using Domain.IServices;

namespace IServices.ISizeServices;
public interface ICreateSize : ICreateServiceAsync<SizeCreateDTO, SizeReadDTO>
{
}

