using PNGEC.API.Domain.DataMigration;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Helper;
using PNGEC.API.Repository.DataMigration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PNGEC.API.Service.DataMigration
{
    public class DataMigrationService : IDataMigrationService
    {
        #region variable declaration
        /// <summary>
        /// The i data migration repository
        /// </summary>
        private readonly IDataMigrationRepository iDataMigrationRepository;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The lstresponse
        /// </summary>
        private Response<VoterAuditModel> lstresponse = null;

        /// <summary>
        /// The result
        /// </summary>
        private Result<VoterAuditModel> result = null;

        /// <summary>
        /// The migration transferred in out
        /// </summary>
        private MigrationTransferredInOut migrationTransferredInOut = null;

        /// <summary>
        /// The API URL
        /// </summary>
        private string apiURL = string.Empty;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DataMigrationService"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public DataMigrationService(string connection, string apiURL)
        {
            this.iDataMigrationRepository = new DataMigrationRepository(connection);
            this.apiURL = apiURL;
        }
        #endregion

        #region service

        #region manage voter status
        /// <summary>
        /// Manages the voter status.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> ManageVoterStatus(CommonRequest model)
        {
            response = new Response();

            try
            {
                bool result = false;

                if (model.recordType == "TRANSFERRED OUT" || model.recordType == "TRANSFER OUT with FORM 11" || model.recordType == "TRANSFER OUT without FORM 11" || model.recordType == "TRANSFERRED IN")
                {

                    ////if (!string.IsNullOrWhiteSpace(model.ids))
                    if (model.VoterList != null && model.VoterList.Count > 0)
                    {
                        List<MigrationTransferredInOut> migrationTransferreds = new List<MigrationTransferredInOut>();

                        ////int[] ids = model.ids.Split(',').Select(int.Parse).ToArray();
                        ////foreach (var item in ids)
                        ////{
                        ////    model.ids = Convert.ToString(item);
                        ////    migrationTransferredInOut = await iDataMigrationRepository.ManageVoterStatusTransferredInOut(model);
                        ////    if (migrationTransferredInOut != null & migrationTransferredInOut.EmEmailAddress != null)
                        ////    {
                        ////        migrationTransferreds.Add(migrationTransferredInOut);
                        ////    }
                        ////}
                        
                        foreach (var item in model.VoterList)
                        {
                            migrationTransferredInOut = await iDataMigrationRepository.ManageVoterStatusTransferredInOut(model, item);
                            if (migrationTransferredInOut != null && migrationTransferredInOut.EmEmailAddress != null)
                            {
                                migrationTransferreds.Add(migrationTransferredInOut);
                            }
                        }

                        //If status updated then set true and display popup while mail is send or not
                        result = true;

                        //If stagin then only send a main
                        if (model.database == "Staging")
                        {
                            if (migrationTransferreds != null && migrationTransferreds.Count > 0)
                            {
                                try
                                {
                                    var groupingList = migrationTransferreds.GroupBy(item => item.ProvinceId)
                                    .Select(grouping => grouping.FirstOrDefault())
                                    .OrderByDescending(item => item.ProvinceId)
                                    .ToList();

                                    foreach (var item in groupingList)
                                    {
                                        string voterDetails = string.Empty;

                                        var Transferreds = migrationTransferreds.Where(x => x.ProvinceId == item.ProvinceId).ToList();
                                        foreach (var subitem in Transferreds)
                                        {
                                            voterDetails = voterDetails + "<tr>" +
                                                "<td>" + subitem.ElectorId + "</td>" +
                                                "<td>" + subitem.LName + "</td>" +
                                                "<td>" + subitem.FName + "</td>" +
                                                "<td>" + subitem.DOB + "</td>" +
                                            "</tr>";
                                        }

                                        if (!string.IsNullOrWhiteSpace(voterDetails))
                                        {
                                            #region mail Template
                                            string mailTemplate = @"
                                            <table border='1' cellspacing='0' cellpadding='0' width='100%'>
                                                <tr>
                                                    <th>ElectorId</th>
                                                    <th>LName</th>
                                                    <th>FName</th>
                                                    <th>DOB</th>
                                                </tr>
                                                {0}
                                            </table>";
                                            #endregion
                                            mailTemplate = string.Format(mailTemplate, voterDetails);

                                            string content = string.Format("Hello {0}{1}{2} {3}{4} {5} {6}{7} Thanks & Regards {8} PNGEC",
                                            Environment.NewLine,
                                            Environment.NewLine,
                                            "Below all voter transferreds other area.",
                                            Environment.NewLine,
                                            Environment.NewLine,
                                            mailTemplate,
                                            Environment.NewLine,
                                            Environment.NewLine,
                                            Environment.NewLine);

                                            bool IsSuccess = Email.SendMail(item.EmEmailAddress, ("PNGEC Voter " + model.recordType), content);
                                        }
                                    }                                    
                                }
                                catch (Exception ex)
                                {                                    
                                    response.Message = ex.Message;
                                    ErrorLog.Write("DataMigrationService", "ManageVoterStatus - ManageVoterStatusTransferredInOut", ex);
                                }
                            }
                        }                                                
                    }
                }
                else
                {
                    foreach (var item in model.VoterList)
                    {
                        result = await iDataMigrationRepository.ManageVoterStatus(model, item);
                    }
                }

                if (result)
                {
                    response.Status = true;
                    response.Message = "Successfully voter details updated";
                }
                else
                {
                    response.Status = false;
                    response.Message = "something went wrong, try again later";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("DataMigrationService", "ManageVoterStatus", ex);
            }

            return response;
        }
        #endregion

        #region get voter audit list
        /// <summary>
        /// Gets the voter audit list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Response<VoterAuditModel>> GetVoterAuditList(PageList model)
        {
            lstresponse = new Response<VoterAuditModel>();

            try
            {
                result = await iDataMigrationRepository.GetVoterAuditList(model);

                if (result != null && result.ListItems != null)
                {
                    foreach (var item in result.ListItems)
                    {
                        item.VoterName = item.FName + " " + item.LName;
                        item.SetImageUrl(apiURL);
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
                ErrorLog.Write("DataMigrationService", "GetVoterAuditList", ex);
            }

            return lstresponse;
        }
        #endregion

        #region update voter
        /// <summary>
        /// Updates the voter.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Response> UpdateVoter(VoterModel model)
        {
            response = new Response();
            try
            {
                int result = await iDataMigrationRepository.UpdateVoter(model);
                if(result > 0)
                {
                    response.Status = true;
                    response.Message = string.Format(ResponseMessage.RecordUpdateSuccess, "Update Voter");
                }
                else
                {
                    response.Status = false;
                    response.Message = ResponseMessage.SomethingWentWrong;
                }
                
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("DataMigrationService", "UpdateVoter", ex);
            }
            return response;
        }
        #endregion

        #endregion
    }
}
