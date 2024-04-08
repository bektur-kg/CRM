using CRM.Domain.Contracts.Sale;

namespace CRM.Application.Mappings;

public class SaleMapping : Profile
{
	public SaleMapping()
	{
		CreateMap<Sale, SaleResponse>();
	}
}
