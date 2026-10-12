using PNGEC.API.Domain.AugmentedRollStat;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Helper;
using PNGEC.API.Repository.AugmentedRollStat;
using System;
using System.Threading.Tasks;

namespace PNGEC.API.Service.AugmentedRollStat
{
    public class AugmentedRollStatService : IAugmentedRollStatService
    {
        #region variable declaration

        /// <summary>
        /// The i augmented roll stat repository
        /// </summary>
        private readonly IAugmentedRollStatRepository iAugmentedRollStatRepository;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The lstresponse
        /// </summary>
        private Response<AugmentedRollStatModel> lstresponse = null;

        /// <summary>
        /// The result
        /// </summary>
        private Result<AugmentedRollStatModel> result = null;


        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="AugmentedRollStatService"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public AugmentedRollStatService(string connection)
        {
            iAugmentedRollStatRepository = new AugmentedRollStatRepository(connection);
        }
        #endregion

        #region service

        #region get augmented roll stat list
        /// <summary>
        /// Gets the augmented roll stat list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response<AugmentedRollStatModel>> GetAugmentedRollStatList(PageList model)
        {
            lstresponse = new Response<AugmentedRollStatModel>();

            try
            {
                result = await iAugmentedRollStatRepository.GetAugmentedRollStatList(model);

                if (result != null && result.ListItems != null)
                {
                    lstresponse.Data = result;
                    lstresponse.Status = true;
                }
                else
                {
                    lstresponse.Message = ResponseMessage.RecordNotExist;
                }
            }
            catch (Exception ex)
            {
                lstresponse.Message = ex.Message;
                ErrorLog.Write("AugmentedRollStatService", "GetAugmentedRollStatList", ex);
            }

            return lstresponse;
        }
        #endregion

        #region add update augmented roll stat
        /// <summary>
        /// Adds the update augmented roll stat.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Response> AddUpdateAugmentedRollStat(AugmentedRollStatModel model)
        {
            try
            {
                response = new Response();
                AugmentedRollStatModel augmentedRollStatModel = await iAugmentedRollStatRepository.AddUpdateAugmentedRollStat(model);

                response.Status = augmentedRollStatModel.ResponseCode > 0;
                if (augmentedRollStatModel.ResponseCode > 0)
                {
                    response.Message = string.Format(ResponseMessage.RecordUpdateSuccess, "AugmentedRollStat");
                    response.Data = augmentedRollStatModel;
                }
                else
                {
                    response.Message = ResponseMessage.SomethingWentWrong;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("AugmentedRollStatService", "AddUpdateAugmentedRollStat", ex);
            }
            return response;
        }
        #endregion

        #endregion
    }
}
