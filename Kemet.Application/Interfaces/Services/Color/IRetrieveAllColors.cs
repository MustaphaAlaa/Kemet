using Entities.Models;
using Entities.Models.DTOs;
using Domain.IServices;

namespace IServices.IColorServices;

public interface IRetrieveAllColors : IRetrieveAllServiceAsync<Color, ColorReadDTO>
{
}