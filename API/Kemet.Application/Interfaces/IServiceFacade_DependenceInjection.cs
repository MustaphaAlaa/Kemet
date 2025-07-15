using AutoMapper;
using Entities.Models.Interfaces.Helpers;
using IRepository.Generic;
using Microsoft.Extensions.Logging;

namespace Kemet.Application.Interfaces;

public interface IServiceFacade_DependenceInjection<T, TService> where T : class
{
    ILogger<TService> logger { get; }
    IMapper mapper { get; }
    IRepositoryRetrieverHelper<T> repositoryHelper { get; }
    IUnitOfWork unitOfWork { get; }
}

