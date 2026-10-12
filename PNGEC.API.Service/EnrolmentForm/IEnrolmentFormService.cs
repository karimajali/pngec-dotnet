using PNGEC.API.Domain.EnrolmentForm;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.MKAEnrolmentForm;
using PNGEC.API.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PNGEC.API.Service.EnrolmentForm
{
    public interface IEnrolmentFormService
    {
        /// <summary>
        /// Adds/update the EnrolmentForm
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> AddEnrolmentForm(EnrolmentFormModel model);

        /// <summary>
        /// Adds/update the MKAEnrolmentForm
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> AddMKAEnrolmentForm(MKAEnrolmentFormModel model);

        /// <summary>
        /// Gets the EnrolmentForm list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response<EnrolmentFormModel>> GetEnrolmentFormList(PageList model);
        /// <summary>
        /// Gets the MKAEnrolmentForm list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response<MKAEnrolmentFormModel>> GetMKAEnrolmentFormList(PageList model);

        /// <summary>
        /// Gets the ward quality check data.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<CommonResponse<List<WardQualityCheckResponseModel>>> GetWardQualityCheckData(EnrolmentFormModel model);

        /// <summary>
        /// Gets the ward quality check data.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<CommonResponse<List<WardQualityCheckResponseModel>>> GetMKAWardQualityCheckData(MKAEnrolmentFormModel model);
        /// <summary>
        /// Deletes the EnrolmentForm.
        /// </summary>
        /// <param name="Id">The Enrolment form Id.</param>
        /// <returns></returns>
        Task<Response> DeleteEnrolmentForm(int enrolmentFormId);
        /// <summary>
        /// Deletes the EnrolmentForm.
        /// </summary>
        /// <param name="Id">The Enrolment form Id.</param>
        /// <returns></returns>
        Task<Response> DeleteMKAEnrolmentForm(int enrolmentFormId);
    }
}
