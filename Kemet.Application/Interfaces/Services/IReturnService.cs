using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface IReturnService
    : IServiceAsync<Return, ReturnDeleteDTO, ReturnUpdateDTO, ReturnReadDTO> { }
