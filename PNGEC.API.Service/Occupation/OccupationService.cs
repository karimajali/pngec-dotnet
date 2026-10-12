using System;
using System.Threading.Tasks;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Mobile.General;
using PNGEC.API.Helper;
using PNGEC.API.Repository.Occupation;

namespace PNGEC.API.Service.Occupation
{
    /// <summary>
    /// occupation service
    /// </summary>
    /// <seealso cref="PNGEC.API.Service.Occupation.IOccupationService" />
    public class OccupationService : IOccupationService
    {
        #region variable declaration

        /// <summary>
        /// The i occupation repository
        /// </summary>
        private readonly IOccupationRepository iOccupationRepository;

        #endregion

        #region constructor 
        /// <summary>
        /// Initializes a new instance of the <see cref="OccupationService"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public OccupationService(string connection)
        {
            iOccupationRepository = new OccupationRepository(connection);
        }
        #endregion

        #region Service
        /// <summary>
        /// Adds the update occupation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// return occupation
        /// </returns>
        public async Task<Response> AddUpdateOccupation(OccupationModel model)
        {
            Response response = new Response();

            try
            {
                OccupationModel occupation = await iOccupationRepository.AddUpdateOccupation(model);

                response.Status = occupation.ResponseCode > 0;

                if (occupation.ResponseCode == -1)
                {
                    response.Message = ResponseMessage.OccupationAlreadyExist;
                }
                else
                {
                    if (model.OccupationPK <= 0)
                    {
                        response.Message = string.Format(ResponseMessage.RecordAddSuccess, "Occupation");
                        response.Data = occupation;
                    }
                    else
                    {
                        response.Message = string.Format(ResponseMessage.RecordUpdateSuccess, "Occupation");
                        response.Data = occupation;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("OccupationService", "AddUpdateOccupation", ex);
            }

            return response;
        }

        /// <summary>
        /// Gets the occupation list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// return occupation list
        /// </returns>
        public async Task<Response> GetOccupationList(PageList model)
        {
            Response response = new Response();

            try
            {
                var occupationlist = await iOccupationRepository.GetOccupationList(model);

                if (occupationlist != null)
                {
                    response.Data = occupationlist;
                    response.Status = true;
                }
                else
                {
                    response.Message = ResponseMessage.RecordNotExist;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("OccupationService", "GetOccupationList", ex);
            }

            return response;
        }
        #endregion
    }
}
