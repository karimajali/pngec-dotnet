using PNGEC.API.Domain.User;
using PNGEC.API.Helper;
using PNGEC.API.Repository.ProductionToTrainingService;
using System;
using System.Threading.Tasks;

namespace PNGEC.API.Service.ProductionToTrainingService
{
    public class ProductionToTrainingService : IProductionToTrainingService
    {
        #region variable declaration
        /// <summary>
        /// The i ActivityLog repository
        /// </summary>
        private readonly IProductionToTraining iProductionToTraining;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

      
        /// <summary>
        /// The result
        /// </summary>
        private Result<ActivityLogModel> result = null;

        /// <summary>
        /// The module name
        /// </summary>
        private const string ModuleName = "ProductionToTraining";
        #endregion

        #region constructor 
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductionToTrainingService"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public ProductionToTrainingService(string connection)
        {
            this.iProductionToTraining = new ProductionToTrainingRepository(connection);
        }

        public async Task<Response> BackupPngecStaging(string filePath)
        {
            response = new Response();
            try
            {
                bool result = await iProductionToTraining.BackupPngecStaging(filePath);
                if (result)
                {
                    response.Status = true;
                    response.Message = string.Format(ResponseMessage.RecordUpdateSuccess, "Backup to Staging Done");
                }
                else
                {
                    response.Status = false;
                    response.Message = ResponseMessage.SomethingWentWrong;
                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("ProductionToTrainingService", "BackupPngecStaging", ex);
            }
            return response;
        }

        public async Task<Response> RestorePngecStaging(string filePath)
        {
            response = new Response();
            try
            {
                bool result = await iProductionToTraining.RestorePngecStaging(filePath);
                if (result)
                {
                    response.Status = true;
                    response.Message = string.Format(ResponseMessage.RecordUpdateSuccess, "Restore to Staging Done");
                }
                else
                {
                    response.Status = false;
                    response.Message = ResponseMessage.SomethingWentWrong;
                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("ProductionToTrainingService", "RestorePngecStaging", ex);
            }
            return response;
        }

        public async Task<Response> BackupPngecPreliminary(string filePath)
        {
            response = new Response();
            try
            {
                bool result = await iProductionToTraining.BackupPngecPreliminary(filePath);
                if (result)
                {
                    response.Status = true;
                    response.Message = string.Format(ResponseMessage.RecordUpdateSuccess, "Backup to Preliminary Done");
                }
                else
                {
                    response.Status = false;
                    response.Message = ResponseMessage.SomethingWentWrong;
                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("ProductionToTrainingService", "BackupPngecPreliminary", ex);
            }
            return response;
        }

        public async Task<Response> RestorePngecPreliminary(string filePath)
        {
            response = new Response();
            try
            {
                bool result = await iProductionToTraining.RestorePngecPreliminary(filePath);
                if (result)
                {
                    response.Status = true;
                    response.Message = string.Format(ResponseMessage.RecordUpdateSuccess, "Restore to Preliminary Done");
                }
                else
                {
                    response.Status = false;
                    response.Message = ResponseMessage.SomethingWentWrong;
                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("ProductionToTrainingService", "RestorePngecPreliminary", ex);
            }
            return response;
        }

        public async Task<Response> BackupPngecFinal(string filePath)
        {
            response = new Response();
            try
            {
                bool result = await iProductionToTraining.BackupPngecFinal(filePath);
                if (result)
                {
                    response.Status = true;
                    response.Message = string.Format(ResponseMessage.RecordUpdateSuccess, "Backup to Final Done");
                }
                else
                {
                    response.Status = false;
                    response.Message = ResponseMessage.SomethingWentWrong;
                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("ProductionToTrainingService", "BackupPngecFinal", ex);
            }
            return response;
        }

        public async Task<Response> RestorePngecFinal(string filePath)
        {
            response = new Response();
            try
            {
                bool result = await iProductionToTraining.RestorePngecFinal(filePath);
                if (result)
                {
                    response.Status = true;
                    response.Message = string.Format(ResponseMessage.RecordUpdateSuccess, "Restore to Final Done");
                }
                else
                {
                    response.Status = false;
                    response.Message = ResponseMessage.SomethingWentWrong;
                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("ProductionToTrainingService", "RestorePngecFinal", ex);
            }
            return response;
        }

        public async Task<Response> BackupPngecCore(string filePath)
        {
            response = new Response();
            try
            {
                bool result = await iProductionToTraining.BackupPngecCore(filePath);
                if (result)
                {
                    response.Status = true;
                    response.Message = string.Format(ResponseMessage.RecordUpdateSuccess, "Backup to Core Done");
                }
                else
                {
                    response.Status = false;
                    response.Message = ResponseMessage.SomethingWentWrong;
                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("ProductionToTrainingService", "BackupPngecCore", ex);
            }
            return response;
        }

        public async Task<Response> RestorePngecCore(string filePath)
        {
            response = new Response();
            try
            {
                bool result = await iProductionToTraining.RestorePngecCore(filePath);
                if (result)
                {
                    response.Status = true;
                    response.Message = string.Format(ResponseMessage.RecordUpdateSuccess, "Restore to Core Done");
                }
                else
                {
                    response.Status = false;
                    response.Message = ResponseMessage.SomethingWentWrong;
                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("ProductionToTrainingService", "RestorePngecCore", ex);
            }
            return response;
        }
        #endregion

        #region service





        #endregion
    }
}
