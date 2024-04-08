using CRM.Domain.Contracts.Sale;

namespace CRM.Api.Controllers;

[ApiController]
[Route("sales")]
public class SaleController : ControllerBase
{
    private readonly ISaleService _saleService;

    public SaleController(ISaleService saleService)
    {
        _saleService = saleService;
    }

    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult<List<SaleResponse>>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ForbidResult))]
    public async Task<ActionResult<BaseResult<List<SaleResponse>>>> GetAllSales()
    {
        var result = await _saleService.GetAllSalesAsync();

        return Ok(result);
    }

    [Authorize(Roles = nameof(UserRole.Seller))]
    [HttpGet("seller-sales")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult<List<SaleResponse>>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ForbidResult))]
    public async Task<ActionResult<BaseResult<List<SaleResponse>>>> GetCurrentSellerSales()
    {
        var result = await _saleService.GetCurrentSellerSalesAsync();

        return Ok(result);
    }

    [Authorize(Roles = nameof(UserRole.Seller))]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult<SaleResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ForbidResult))]
    public async Task<ActionResult<BaseResult<SaleResponse>>> CreateSale(long leadId)
    {
        var result = await _saleService.CreateSale(leadId);

        if(!result.IsSuccess) return BadRequest(new BaseResult { ErrorMessage = result.ErrorMessage });

        return Ok(result);
    }
}
