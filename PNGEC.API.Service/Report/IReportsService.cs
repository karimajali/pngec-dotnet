using PNGEC.API.Domain.Report;
using PNGEC.API.Helper;
using System.Threading.Tasks;

namespace PNGEC.API.Service.Report
{
    public interface IReportsService
    {
        /// <summary>
        /// Gets the ward status report.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response<WardRollBookReortModel>> GetWardStatusReport(WardStatusReportRequestModel model);

        /// <summary>
        /// Gets the wards number of form11.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> GetWardsNumberOfForm11(GetWardsNumberOfForm11Request model);
        /// <summary>
        /// Gets the wards number of form11.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> GetWardsNumberOfMKAForm11(GetWardsNumberOfForm11Request model);
    }
}
