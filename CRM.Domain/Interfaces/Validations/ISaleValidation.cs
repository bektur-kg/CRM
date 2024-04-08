using CRM.Domain.Entities;
using CRM.Domain.Results;

namespace CRM.Domain.Interfaces.Validations;

public interface ISaleValidation
{
    BaseResult ValidateOnCreateSale(Lead lead);

}
