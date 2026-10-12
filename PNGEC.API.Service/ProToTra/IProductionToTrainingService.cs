using PNGEC.API.Domain.Device;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Domain.User;
using PNGEC.API.Helper;
using System.Threading.Tasks;

namespace PNGEC.API.Service.ProductionToTrainingService
{
    public interface IProductionToTrainingService
    {
        Task<Response> BackupPngecStaging(string filePath);
        Task<Response> RestorePngecStaging(string filePath);
        Task<Response> BackupPngecPreliminary(string filePath);
        Task<Response> RestorePngecPreliminary(string filePath);
        Task<Response> BackupPngecFinal(string filePath);
        Task<Response> RestorePngecFinal(string filePath);
        Task<Response> BackupPngecCore(string filePath);
        Task<Response> RestorePngecCore(string filePath);


    }
}
