

using AutoMapper;
using Entities.Models.Interfaces.Helpers;
using IRepository.Generic;
using Kemet.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Kemet.Application.Services
{



    public class ServiceFacade_DependenceInjection<T> : IServiceFacade_DependenceInjection<T> where T : class

    {
        public IUnitOfWork unitOfWork { get; private set; }

        public ILogger logger { get; private set; }

        public IRepositoryRetrieverHelper<T> repositoryHelper { get; private set; }
        public IMapper mapper { get; private set; }

        public ServiceFacade_DependenceInjection(IUnitOfWork unitOfWork,
            ILogger logger,
            IRepositoryRetrieverHelper<T> repositoryHelper,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.repositoryHelper = repositoryHelper;
            this.mapper = mapper;
        }
    }
}
