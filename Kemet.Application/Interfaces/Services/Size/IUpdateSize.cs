using Entities.Models.DTOs;
using Domain.IServices;

namespace IServices.ISizeServices;
public interface IUpdateSize : IUpdateServiceAsync<SizeUpdateDTO, SizeReadDTO>
{
}

