using AutoMapper;
using Entities.Models.Interfaces.Helpers;
using IRepository.Generic;
using Kemet.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Kemet.Application.Services
{
    public class ServiceFacade_DependenceInjection<T, TService>
        : IServiceFacade_DependenceInjection<T, TService>
        where T : class
    {
        public IUnitOfWork unitOfWork { get; private set; }

        public ILogger<TService> logger { get; private set; }

        public IRepositoryRetrieverHelper<T> repositoryHelper { get; private set; }
        public IMapper mapper { get; private set; }

        public ServiceFacade_DependenceInjection(
            IUnitOfWork unitOfWork,
            ILogger<TService> logger,
            IRepositoryRetrieverHelper<T> repositoryHelper,
            IMapper mapper
        )
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.repositoryHelper = repositoryHelper;
            this.mapper = mapper;
        }
    }
}
