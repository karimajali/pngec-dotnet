using PNGEC.API.Domain.Report;
using PNGEC.API.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PNGEC.API.Repository.Report
{
    public interface IReportsRepository
    {
        /// <summary>
        /// Gets the ward status report.
        /// </summary>
        /// <param name="wardStatusReportRequestModel">The ward status report request model.</param>
        /// <returns></returns>
        Task<Result<WardRollBookReortModel>> GetWardStatusReport(WardStatusReportRequestModel wardStatusReportRequestModel);

        /// <summary>
        /// Gets the wards number of form11.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<List<GetWardsNumberOfForm11Response>> GetWardsNumberOfForm11(GetWardsNumberOfForm11Request model);
        /// <summary>
        /// Gets the wards number of form11.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<List<GetWardsNumberOfForm11Response>> GetWardsNumberOfMKAForm11(GetWardsNumberOfForm11Request model);
    }
}
