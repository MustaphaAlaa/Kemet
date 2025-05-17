

using AutoMapper;
using Entities.Models.Interfaces.Helpers;
using IRepository.Generic;
using Microsoft.Extensions.Logging;

namespace Kemet.Application.Services
{



    public class ServiceFacade_DependenceInjection<T> where T : class

    {
        public readonly IUnitOfWork unitOfWork;

        public readonly ILogger logger;

        public readonly IRepositoryRetrieverHelper<T> repositoryHelper;
        public readonly IMapper mapper;

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
