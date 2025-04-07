using Entities.Models.DTOs;
using Domain.IServices;

namespace IServices.IColorServices;
public interface IUpdateColor : IUpdateServiceAsync<ColorUpdateDTO, ColorReadDTO>
{
}