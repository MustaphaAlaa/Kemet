using Entities.Models.DTOs;
using Domain.IServices;
namespace IServices.IColorServices;

public interface IDeleteColor : IDeleteServiceAsync<ColorDeleteDTO>
{

}