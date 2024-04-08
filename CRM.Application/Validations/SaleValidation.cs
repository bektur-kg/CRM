using CRM.Application.Resources;
using CRM.Domain.Entities;
using CRM.Domain.Interfaces.Services;
using CRM.Domain.Interfaces.Validations;
using CRM.Domain.Results;

namespace CRM.Application.Validations;

public class SaleValidation : ISaleValidation
{
    public BaseResult ValidateOnCreateSale(Lead lead)
    {
        if (lead == null) return new BaseResult { ErrorMessage = ResultMessages.LeadNotFound };

        return new BaseResult();
    }
}
