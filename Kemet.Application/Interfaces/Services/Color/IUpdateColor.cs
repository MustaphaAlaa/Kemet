using Entities.Models.DTOs;
using Interfaces.IServices;

namespace IServices.IColorServices;
public interface IUpdateColor : IUpdateServiceAsync<ColorUpdateDTO, ColorReadDTO>
{
}