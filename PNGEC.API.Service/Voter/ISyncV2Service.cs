using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Helper;

namespace PNGEC.API.Service.Voter
{
    public interface ISyncV2Service
    {
        Task<SyncV2BatchResponse> ProcessBatchAsync(SyncV2BatchRequest request, string defaultActionType);
        Task<Response> ProcessSingleVoterAsync(VoterModel voter, string actionType, int userId, string deviceId);
        Task<Response> UploadVoterImageAsync(IFormFile file, string imageType, long electorId, string uploadId);
        Task<Response> GetSyncStatusByUploadIdAsync(string uploadId);
    }
}
