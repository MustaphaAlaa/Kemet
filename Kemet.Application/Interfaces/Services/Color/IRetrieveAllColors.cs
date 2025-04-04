﻿using Entities.Models;
using Entities.Models.DTOs;
using Interfaces.IServices;

namespace IServices.IColorServices;

public interface IRetrieveAllColors : IRetrieveAllServiceAsync<Color, ColorReadDTO>
{
}