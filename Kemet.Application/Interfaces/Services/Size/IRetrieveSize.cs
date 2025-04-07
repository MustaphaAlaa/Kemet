using Entities.Models;
using Entities.Models.DTOs;
using Domain.IServices;

namespace IServices.ISizeServices;
public interface IRetrieveSize : IRetrieveServiceAsync<Size?, SizeReadDTO?>
{
}

