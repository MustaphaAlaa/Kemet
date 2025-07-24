using Entities.Models.DTOs;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using FluentValidation;
using IRepository.Generic;  

namespace Entities.Models.Validations;

public class GovernorateDeliveryCompanyValidation : IGovernorateDeliveryCompanyValidation
{
    private readonly IBaseRepository<GovernorateDeliveryCompany> _governorateDeliveryCompanyRepository;
    private readonly IBaseRepository<DeliveryCompany > _deliveryCompanyRepository;
    private readonly IBaseRepository<Governorate > _governorateRepository;

    private readonly IValidator<GovernorateDeliveryCompanyCreateDTO> _createValidator;
    private readonly IValidator<GovernorateDeliveryCompanyUpdateDTO> _updateValidator;
    private readonly IValidator<GovernorateDeliveryCompanyDeleteDTO> _deleteValidator;

    public GovernorateDeliveryCompanyValidation(
        IBaseRepository<GovernorateDeliveryCompany> governorateDeliveryCompanyRepository,
        IBaseRepository<Governorate> governorateRepository,
        IBaseRepository<DeliveryCompany> deliveryCompanyRepository ,
        IValidator<GovernorateDeliveryCompanyCreateDTO> createValidator,
        IValidator<GovernorateDeliveryCompanyUpdateDTO> updateValidator,
        IValidator<GovernorateDeliveryCompanyDeleteDTO> deleteValidator
    )
    {
        _governorateDeliveryCompanyRepository = governorateDeliveryCompanyRepository;
        _governorateRepository = governorateRepository;
        _deliveryCompanyRepository  = deliveryCompanyRepository ;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _deleteValidator = deleteValidator;
    }

    public async Task ValidateCreate(GovernorateDeliveryCompanyCreateDTO entity)
    {
        await _createValidator.ValidateAndThrowAsync(entity);
        var governorate = await _governorateRepository.RetrieveAsync(g=> g.GovernorateId == entity.GovernorateId);
        var deliveryCompany = await _deliveryCompanyRepository .RetrieveAsync(g=> g.DeliveryCompanyId == entity.DeliveryCompanyId);
        
        Utility.DoesExist(governorate, "Governorate");
        Utility.DoesExist(deliveryCompany, "DeliveryCompany");
    }

    public async Task ValidateDelete(GovernorateDeliveryCompanyDeleteDTO entity)
    {
        await _deleteValidator.ValidateAndThrowAsync(entity);
    }

    public async Task ValidateUpdate(GovernorateDeliveryCompanyUpdateDTO entity)
    {
        await _updateValidator.ValidateAndThrowAsync(entity);

        var governorateDeliveryCompany = await _governorateDeliveryCompanyRepository .RetrieveAsync(g =>
            g.GovernorateDeliveryCompanyId == entity.GovernorateDeliveryCompanyId
        );

        Utility.DoesExist(governorateDeliveryCompany, "Governorate Delivery Compnay");
    }
} 