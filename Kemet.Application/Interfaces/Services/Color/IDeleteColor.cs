using Entities.Models.DTOs;
using Interfaces.IServices;
namespace IServices.IColorServices;

public interface IDeleteColor : IDeleteServiceAsync<ColorDeleteDTO>
{

}