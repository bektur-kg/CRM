namespace CRM.Domain.Interfaces.Validations;

public interface ISaleValidation
{
    BaseResult ValidateOnCreateSale(Lead lead);

}
