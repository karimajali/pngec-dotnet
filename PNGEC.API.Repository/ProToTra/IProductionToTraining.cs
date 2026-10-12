using PNGEC.API.Domain.Device;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.User;
using PNGEC.API.Helper;
using System.Threading.Tasks;

namespace PNGEC.API.Repository.ProductionToTrainingService
{
    public interface IProductionToTraining
    {
        /// <summary>
        /// backup png staging DB
        /// </summary>
        /// <returns></returns>
        Task<bool> BackupPngecStaging(string filePath);

        /// <summary>
        /// restore Png Staging DB
        /// </summary>
        /// <returns></returns>
        Task<bool> RestorePngecStaging(string filePath);

        /// <summary>
        /// backup png Preliminary DB
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> BackupPngecPreliminary(string filePath);

        /// <summary>
        /// restore PNG Preliminary DB
        /// </summary>
        /// <returns></returns>
        Task<bool> RestorePngecPreliminary(string filePath);

        /// <summary>
        /// backup png final DB
        /// </summary>
        /// <returns></returns>
        Task<bool> BackupPngecFinal(string filePath);

        /// <summary>
        /// restore PNG Final DB to training 
        /// </summary>
        /// <returns></returns>
        Task<bool> RestorePngecFinal(string filePath);

        /// <summary>
        /// backup Png Core user and geo
        /// </summary>
        /// <returns></returns>
        Task<bool> BackupPngecCore(string filePath);

        /// <summary>
        /// restore PNG Core user and geo DB to training 
        /// </summary>
        /// <returns></returns>
        Task<bool> RestorePngecCore(string filePath);

    }

}
