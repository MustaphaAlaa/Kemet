
using Entities.Models;
using Entities.Models.DTOs;
using Interfaces.IServices;

namespace IServices.IColorServices;

public interface IRetrieveColor : IRetrieveServiceAsync<Color?, ColorReadDTO?>
{

}



