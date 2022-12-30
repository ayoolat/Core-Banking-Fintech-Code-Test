using Innovectives.Groups.Business.Layer.Dtos.CoreBankingDto;
using Innovectives.Groups.Business.Layer.Services.Intreface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Innovectives.Group.Application.Layer.Controllers
{
    [ApiController]
    [Route("api/v1/core-banking")]
    [Authorize]
    public class CoreBankingController : ControllerBase
    {
        protected readonly ICoreBankingService _coreBankingService;
        public CoreBankingController(ICoreBankingService coreBankingService)
        {
            _coreBankingService = coreBankingService;
        }

        [HttpGet("banks/{provider}")]
        public async Task<ActionResult<List<BankListDto>>> ListBanks(string provider)
        {
            return Ok(await _coreBankingService.ListBanks(provider));
        }

        [HttpPost("validateBankAccount")]
        public async Task<ActionResult<AccountNumberVerifiedDto>> ValidateBankAccount(VerifyAccountNumberDto verifyAccountNumber)
        {
            return Ok(await _coreBankingService.ValidateAccountNumber(verifyAccountNumber));
        }

        [HttpPost("bankTransfer")]
        public async Task<ActionResult<BankTransferResp>> bankTransfer(BankTransfersReqDto bankTransfersReq)
        {
            return Ok(await _coreBankingService.Transfer(bankTransfersReq));
        }
    }
}
