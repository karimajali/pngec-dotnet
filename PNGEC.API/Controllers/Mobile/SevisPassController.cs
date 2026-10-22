using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Helper;
using PNGEC.API.Service.SevisPass;

namespace PNGEC.API.Controllers.Mobile
{
    /// <summary>
    /// Phase 2 SevisPass Identity Verification Controller (Server-to-Server)
    /// Secured via JWT Bearer authentication.
    /// </summary>
    [Route("api/mobile/v2/[controller]")]
    [ApiController]
    public class SevisPassController : Controller
    {
        private readonly ISevisPassService _sevisPassService;

        public SevisPassController(ISevisPassService sevisPassService)
        {
            _sevisPassService = sevisPassService;
        }

        /// <summary>
        /// Verifies a voter's details against DICT SevisPass National Digital ID registry.
        /// In sandbox/offline mode, simulated responses are provided per Phase 2 spec.
        /// </summary>
        [HttpPost("Verify")]
        public async Task<IActionResult> Verify([FromBody] SevisPassVerifyRequest request)
        {
            var result = await _sevisPassService.VerifyElectorAsync(request);
            return Ok(result);
        }
    }
}
