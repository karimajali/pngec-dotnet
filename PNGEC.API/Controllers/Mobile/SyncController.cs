using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Helper;
using PNGEC.API.Service.Voter;

namespace PNGEC.API.Controllers.Mobile
{
    /// <summary>
    /// Phase 1 Modular Idempotent Sync Endpoints
    /// Provides per-status sync, background batch sync, safe retries with UploadId, and separate image uploads.
    /// Secured via JWT Bearer authentication with backward-compatible anonymous fallback.
    /// </summary>
    [Route("api/mobile/v2/[controller]")]
    [ApiController]
    public class SyncController : Controller
    {
        private readonly ISyncV2Service _syncV2Service;

        public SyncController(ISyncV2Service syncV2Service)
        {
            _syncV2Service = syncV2Service;
        }

        /// <summary>
        /// Sync New Voter Enrolments
        /// </summary>
        [HttpPost("New")]
        public async Task<IActionResult> SyncNew([FromBody] SyncV2BatchRequest request)
        {
            var result = await _syncV2Service.ProcessBatchAsync(request, "NEW");
            return Ok(result);
        }

        /// <summary>
        /// Sync Voter Profile Updates
        /// </summary>
        [HttpPost("Update")]
        public async Task<IActionResult> SyncUpdate([FromBody] SyncV2BatchRequest request)
        {
            var result = await _syncV2Service.ProcessBatchAsync(request, "UPDATE");
            return Ok(result);
        }

        /// <summary>
        /// Sync Voter Transfer-Ins
        /// </summary>
        [HttpPost("TransferIn")]
        public async Task<IActionResult> SyncTransferIn([FromBody] SyncV2BatchRequest request)
        {
            var result = await _syncV2Service.ProcessBatchAsync(request, "TRANSFER_IN");
            return Ok(result);
        }

        /// <summary>
        /// Sync Voter Status Changes (GHOST, DECEASED, DUPLICATE, UNDERAGE, TRANSFERRED OUT)
        /// </summary>
        [HttpPost("StatusChange")]
        public async Task<IActionResult> SyncStatusChange([FromBody] SyncV2BatchRequest request)
        {
            var result = await _syncV2Service.ProcessBatchAsync(request, "STATUS_CHANGE");
            return Ok(result);
        }

        /// <summary>
        /// Universal Batch Sync Endpoint
        /// </summary>
        [HttpPost("Batch")]
        public async Task<IActionResult> SyncBatch([FromBody] SyncV2BatchRequest request)
        {
            var result = await _syncV2Service.ProcessBatchAsync(request, request.ActionType ?? "BATCH");
            return Ok(result);
        }

        /// <summary>
        /// Independent Voter Image Upload Endpoint (Separated from text voter data)
        /// </summary>
        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file, [FromForm] string imageType, [FromForm] long electorId, [FromForm] string uploadId)
        {
            var result = await _syncV2Service.UploadVoterImageAsync(file, imageType, electorId, uploadId);
            return Ok(result);
        }

        /// <summary>
        /// Check Sync Status by UploadId (Idempotent query)
        /// </summary>
        [HttpGet("Status/{uploadId}")]
        public async Task<IActionResult> GetStatus(string uploadId)
        {
            var result = await _syncV2Service.GetSyncStatusByUploadIdAsync(uploadId);
            return Ok(result);
        }
    }
}
