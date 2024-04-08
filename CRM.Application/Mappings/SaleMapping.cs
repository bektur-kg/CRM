using AutoMapper;
using CRM.Domain.Contracts.Sale;
using CRM.Domain.Entities;

namespace CRM.Application.Mappings;

public class SaleMapping : Profile
{
	public SaleMapping()
	{
		CreateMap<Sale, SaleResponse>();
	}
}
