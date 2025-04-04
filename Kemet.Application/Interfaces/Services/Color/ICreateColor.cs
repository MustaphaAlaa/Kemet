
using Entities.Models.DTOs;
using Interfaces.IServices;

namespace IServices.IColorServices;
public interface ICreateColor : ICreateServiceAsync<ColorCreateDTO, ColorReadDTO>
{

}