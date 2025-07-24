using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface ISizeService
    : IServiceAsync<Size, int, SizeCreateDTO, SizeDeleteDTO, SizeUpdateDTO, SizeReadDTO>
{ }
