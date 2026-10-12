using PNGEC.API.Domain.Report;
using PNGEC.API.Helper;
using PNGEC.API.Repository.Report;
using System;
using System.Threading.Tasks;

namespace PNGEC.API.Service.Report
{
    public class ReportsService : IReportsService
    {
        #region variable declaration
        /// <summary>
        /// The i reports repository
        /// </summary>
        private readonly IReportsRepository iReportsRepository;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The voterResponse
        /// </summary>
        private Response<WardRollBookReortModel> voterResponse = null;

        /// <summary>
        /// The voterResult
        /// </summary>
        private Result<WardRollBookReortModel> voterResult = null;

        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportsService"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public ReportsService(string connection)
        {
            iReportsRepository = new ReportsRepository(connection);
        }
        #endregion

        #region service

        #region get ward Status Report
        /// <summary>
        /// Gets the ward status report.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response<WardRollBookReortModel>> GetWardStatusReport(WardStatusReportRequestModel model)
        {
            voterResponse = new Response<WardRollBookReortModel>();
            try
            {
                voterResult = await iReportsRepository.GetWardStatusReport(model);
                if (voterResult != null)
                {
                    voterResponse.Data = voterResult;
                    voterResponse.Status = true;
                    voterResponse.Message = "Success";
                }
                else
                {
                    voterResponse.Message = ResponseMessage.RecordNotExist;
                }
            }
            catch (Exception ex)
            {
                voterResponse.Message = ResponseMessage.SomethingWentWrong;
                voterResponse.Data = null;
                voterResponse.Status = false;
                ErrorLog.Write("ReportsService", "GetWardStatusReport", ex);
            }
            return voterResponse;
        }
        #endregion

        #region Get wards number of form11

        /// <summary>
        /// Gets the wards number of form11.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> GetWardsNumberOfForm11(GetWardsNumberOfForm11Request model)
        {
            var res = new Response();
            try
            {
                var response = await iReportsRepository.GetWardsNumberOfForm11(model);
                res.Data = response;
                res.Status = true;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.Message = ResponseMessage.SomethingWentWrong;
                res.Data = null;
                res.Status = false;
                ErrorLog.Write("ReportsService", "GetWardsNumberOfForm11", ex);
            }
            return res;
        }
        #endregion
        #region Get wards number of form11

        /// <summary>
        /// Gets the wards number of form11.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> GetWardsNumberOfMKAForm11(GetWardsNumberOfForm11Request model)
        {
            var res = new Response();
            try
            {
                var response = await iReportsRepository.GetWardsNumberOfMKAForm11(model);
                res.Data = response;
                res.Status = true;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.Message = ResponseMessage.SomethingWentWrong;
                res.Data = null;
                res.Status = false;
                ErrorLog.Write("ReportsService", "GetWardsNumberOfForm11", ex);
            }
            return res;
        }
        #endregion
        #endregion
    }
}
