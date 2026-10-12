using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Domain.User;
using PNGEC.API.Helper;
using PNGEC.API.Service.DataMigration;
using PNGEC.API.Service.ProductionToTrainingService;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace PNGEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionToTrainingController : ControllerBase
    {
        #region variable declaration

        /// <summary>
        /// The i ProductionToTraining service
        /// </summary>
        private IProductionToTrainingService iProductionToTrainingService;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityLogController"/> class.
        /// </summary>
        /// <param name="ActivityLogService">The ProductionToTraining service.</param>
        public ProductionToTrainingController(IProductionToTrainingService activityLogService)
        {
            iProductionToTrainingService = activityLogService;
        }
        #endregion

        #region api

        #region Backup Staging
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("BackupPngecStaging")]
        public async Task<IActionResult> BackupPngecStaging()
        {
            response = new Response();

            // string fileNameWithPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "//pngec_staging.bak";
            string fileNameWithPath = "C://DB_Backup//pngec_staging.bak";
            if (fileNameWithPath != null)
            {
                response = await iProductionToTrainingService.BackupPngecStaging(fileNameWithPath);
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }

            return Ok(response);
        }
        #endregion

        #region Backup Preliminary
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("BackupPngecPreliminary")]
        public async Task<IActionResult> BackupPngecPreliminary()
        {
            response = new Response();

            //string fileNameWithPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "//pngec_Preliminary.bak";
            string fileNameWithPath = "C://DB_Backup//pngec_Preliminary.bak";

            if (fileNameWithPath != null)
            {
                response = await iProductionToTrainingService.BackupPngecPreliminary(fileNameWithPath);
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }

            return Ok(response);
        }
        #endregion

        #region Backup Final
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("BackupPngecFinal")]
        public async Task<IActionResult> BackupPngecFinal()
        {
            response = new Response();

            // string fileNameWithPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "//pngec_Final.bak";
            string fileNameWithPath = "C://DB_Backup//pngec_Final.bak";

            if (fileNameWithPath != null)
            {
                response = await iProductionToTrainingService.BackupPngecFinal(fileNameWithPath);
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }

            return Ok(response);
        }
        #endregion

        #region Backup Core
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("BackupPngecCore")]
        public async Task<IActionResult> BackupPngecCore()
        {
            response = new Response();

            //string fileNameWithPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "//pngec_Core.bak";

            string fileNameWithPath = "C://DB_Backup//pngec_Core.bak";

            if (fileNameWithPath != null)
            {
                response = await iProductionToTrainingService.BackupPngecCore(fileNameWithPath);
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }

            return Ok(response);
        }
        #endregion

        #region Restore Staging
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("RestorePngecStaging")]
        public async Task<IActionResult> RestorePngecStaging()
        {
            response = new Response();

            //string fileNameWithPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "//pngec_staging.bak";
            string fileNameWithPath = "C://DB_Backup//pngec_staging.bak";

            if (fileNameWithPath != null)
            {
                response = await iProductionToTrainingService.RestorePngecStaging(fileNameWithPath);
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }

            return Ok(response);
        }
        #endregion

        #region Restore Preliminary
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("RestorePngecPreliminary")]
        public async Task<IActionResult> RestorePngecPreliminary()
        {
            response = new Response();

            // string fileNameWithPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "//pngec_Preliminary.bak";
            string fileNameWithPath = "C://DB_Backup//pngec_Preliminary.bak";

            if (fileNameWithPath != null)
            {
                response = await iProductionToTrainingService.RestorePngecPreliminary(fileNameWithPath);
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }

            return Ok(response);
        }
        #endregion

        #region Restore Final
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("RestorePngecFinal")]
        public async Task<IActionResult> RestorePngecFinal()
        {
            response = new Response();

            //string fileNameWithPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "//pngec_Final.bak";
            string fileNameWithPath = "C://DB_Backup//pngec_Final.bak";

            if (fileNameWithPath != null)
            {
                response = await iProductionToTrainingService.RestorePngecFinal(fileNameWithPath);
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }

            return Ok(response);
        }
        #endregion

        #region Restore Core
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("RestorePngecCore")]
        public async Task<IActionResult> RestorePngecCore()
        {
            response = new Response();

            //string fileNameWithPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "//pngec_Core.bak";
            string fileNameWithPath = "C://DB_Backup////pngec_Core.bak";

            if (fileNameWithPath != null)
            {
                response = await iProductionToTrainingService.RestorePngecCore(fileNameWithPath);
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }

            return Ok(response);
        }
        #endregion

        #endregion
    }
}
