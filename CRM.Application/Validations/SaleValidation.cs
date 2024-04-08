namespace CRM.Application.Validations;

public class SaleValidation : ISaleValidation
{
    public BaseResult ValidateOnCreateSale(Lead lead)
    {
        if (lead == null) return new BaseResult { ErrorMessage = ResultMessages.LeadNotFound };

        return new BaseResult();
    }
}
