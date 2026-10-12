using PNGEC.API.Domain.EnrolmentForm;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.MKAEnrolmentForm;
using PNGEC.API.Helper;
using System.Threading.Tasks;

namespace PNGEC.API.Repository.EnrolmentForm
{
    public interface IEnrolmentFormRepository
    {
        /// <summary>
        /// Add the EnrolmentForm
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<EnrolmentFormModel> AddEnrolmentForm(EnrolmentFormModel model);
        /// <summary>
        /// Add the EnrolmentForm
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<MKAEnrolmentFormModel> AddMKAEnrolmentForm(MKAEnrolmentFormModel model);

        /// <summary>
        /// Gets the EnrolmentForm list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Result<EnrolmentFormModel>> GetEnrolmentFormList(PageList model);
        /// <summary>
        /// Gets the MKAEnrolmentForm list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Result<MKAEnrolmentFormModel>> GetMKAEnrolmentFormList(PageList model);

        /// <summary>
        /// Gets the ward quality check data.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<WardQualityCheckSPResponseModel> GetWardQualityCheckData(EnrolmentFormModel model);

        /// <summary>
        /// Gets the ward quality check data.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<WardQualityCheckSPResponseModel> GetMKAWardQualityCheckData(MKAEnrolmentFormModel model);

        /// <summary>
        /// Deletes the EnrolmentForm.
        /// </summary>
        /// <param name="Id">The Enrolment form Id.</param>
        /// <returns></returns>
        Task<bool> DeleteEnrolmentForm(int enrolmentFormId);
        /// <summary>
        /// Deletes the EnrolmentForm.
        /// </summary>
        /// <param name="Id">The Enrolment form Id.</param>
        /// <returns></returns>
        Task<bool> DeleteMKAEnrolmentForm(int enrolmentFormId);
    }
}
