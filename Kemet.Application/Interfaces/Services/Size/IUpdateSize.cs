using Entities.Models.DTOs;
using Interfaces.IServices;

namespace IServices.ISizeServices;
public interface IUpdateSize : IUpdateServiceAsync<SizeUpdateDTO, SizeReadDTO>
{
}

