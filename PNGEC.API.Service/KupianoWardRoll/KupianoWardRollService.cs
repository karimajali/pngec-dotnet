using PNGEC.API.Domain.KupianoWardRoll;
using PNGEC.API.Helper;
using PNGEC.API.Repository.KupianoWardRoll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNGEC.API.Service.KupianoWardRoll
{
    public class KupianoWardRollService : IKupianoWardRollService
    {
        #region variable declaration
        /// <summary>
        /// The i file repository
        /// </summary>
        private readonly IKupianoWardRollRepository iKupianoWardRollRepository;

        /// <summary>
        /// The lstresponse
        /// </summary>
        private Response<KupianoWardRollModel> lstresponse = null;

        /// <summary>
        /// The result
        /// </summary>
        private Result<KupianoWardRollModel> result = null;

        /// <summary>
        /// The API URL
        /// </summary>
        private string apiURL = string.Empty;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="KupianoWardRollService"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public KupianoWardRollService(string connection, string apiURL)
        {
            iKupianoWardRollRepository = new KupianoWardRollRepository(connection);
            this.apiURL = apiURL;
        }
        #endregion

        #region service

        #region get kupiano ward roll list
        /// <summary>
        /// Gets the kupiano ward roll list.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Response<KupianoWardRollModel>> GetKupianoWardRollList()
        {
            lstresponse = new Response<KupianoWardRollModel>();

            try
            {
                result = await iKupianoWardRollRepository.GetKupianoWardRollList();

                if (result != null && result.ListItems != null)
                {
                    foreach (var item in result.ListItems)
                    {
                        item.VoterName = item.LName + " " + item.FName;
                        item.SetImageUrl(this.apiURL);
                        item.SetFormImageUrl(this.apiURL);
                    }

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
                ErrorLog.Write("KupianoWardRollService", "GetKupianoWardRollList", ex);
            }

            return lstresponse;
        }
        #endregion

        #endregion
    }
}
