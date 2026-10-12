using System.Collections.Generic;
using PNGEC.API.Domain.MKAEnrolmentForm;
using PNGEC.API.Domain.Mobile.Voter;

namespace PNGEC.API.Domain.EnrolmentForm
{
    /// <summary>
    /// ward quality check sp response model
    /// </summary>
    public class WardQualityCheckSPResponseModel
    {
        /// <summary>
        /// Gets or sets the voter models.
        /// </summary>
        /// <value>
        /// The voter models.
        /// </value>
        public IEnumerable<VoterModel> voterModels { get; set; }

        /// <summary>
        /// Gets or sets the enrolment form models.
        /// </summary>
        /// <value>
        /// The enrolment form models.
        /// </value>
        public IEnumerable<EnrolmentFormModel> enrolmentFormModels { get; set; }
        /// <summary>
        /// Gets or sets the enrolment form models.
        /// </summary>
        /// <value>
        /// The enrolment form models.
        /// </value>
        public IEnumerable<MKAEnrolmentFormModel> mkaenrolmentFormModels { get; set; }
    }
}
