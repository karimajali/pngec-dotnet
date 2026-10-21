using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Helper;

namespace PNGEC.API.Repository.Voter
{
    public class VoterRepository : IVoterRepository
    {
        #region variable        
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;
        #endregion

        #region constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="VoterRepository"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public VoterRepository(string connection)
        {
            connectionString = connection;
        }
        #endregion

        #region repository     

        #region get voter list mobile
        /// <summary>
        /// Gets the voter list mobile.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<Result<VoterModel>> GetVoterListMobile(VoterRequestModel voterReqModel)
        {
            Result<VoterModel> result = new Result<VoterModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.GetVoterListMobile;

                DynamicParameters param = new DynamicParameters();
                param.Add("@UserId", voterReqModel.UserId);
                param.Add("@PageNo", voterReqModel.PageNo);
                param.Add("@PageSize", voterReqModel.PageSize);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                var res = await con.QueryMultipleAsync(squery, param, commandType: CommandType.StoredProcedure);
                if (res != null)
                {
                    result.ListItems = await res.ReadAsync<VoterModel>();
                    result.TotalRecordCount = await res.ReadFirstOrDefaultAsync<int>();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return result;
            }
        }

        /// <summary>
        /// Downloads the voter list mobile.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns></returns>
        public async Task<Result<VoterModel>> DownloadVoterListMobile(VoterRequestModel voterReqModel)
        {
            Result<VoterModel> result = new Result<VoterModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.DownloadVoterListMobile;

                DynamicParameters param = new DynamicParameters();
                param.Add("@UserId", voterReqModel.UserId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var queryTimeoutInSeconds = 1800;
                result.ListItems = await con.QueryAsync<VoterModel>(squery, param, commandType: CommandType.StoredProcedure, commandTimeout: queryTimeoutInSeconds);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return result;
            }
        }
        #endregion

        #region get voter list by province mobile
        /// <summary>
        /// Gets the voter list by province mobile.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns></returns>
        public async Task<Result<VoterModel>> GetVoterListByProvinceMobile(VoterRequestModel voterReqModel)
        {
            Result<VoterModel> result = new Result<VoterModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.GetVoterListByProvince;

                DynamicParameters param = new DynamicParameters();
                param.Add("@ProvinceId", voterReqModel.ProvinceId);
                param.Add("@PageNo", voterReqModel.PageNo);
                param.Add("@PageSize", voterReqModel.PageSize);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                var res = await con.QueryMultipleAsync(squery, param, commandType: CommandType.StoredProcedure);
                if (res != null)
                {
                    result.ListItems = await res.ReadAsync<VoterModel>();
                    result.TotalRecordCount = await res.ReadFirstOrDefaultAsync<int>();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return result;
            }
        }
        #endregion

        #region get rejected voter list mobile
        /// <summary>
        /// Gets the rejected voter list mobile.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns>
        /// return rejected voter list
        /// </returns>
        public async Task<Result<VoterModel>> GetRejectedVoterListMobile(VoterRequestModel voterReqModel)
        {
            Result<VoterModel> result = new Result<VoterModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.GetRejectedVoterListMobile;

                DynamicParameters param = new DynamicParameters();
                param.Add("@UserId", voterReqModel.UserId);
                param.Add("@PageNo", voterReqModel.PageNo);
                param.Add("@PageSize", voterReqModel.PageSize);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                var res = await con.QueryMultipleAsync(squery, param, commandType: CommandType.StoredProcedure);
                if (res != null)
                {
                    result.ListItems = await res.ReadAsync<VoterModel>();
                    result.TotalRecordCount = await res.ReadFirstOrDefaultAsync<int>();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return result;
            }
        }
        #endregion

        #region get transfer in voter list
        /// <summary>
        /// Gets the transfer in voter list.
        /// </summary>
        /// <param name="transferInVoterRequestModel">The transfer in voter request model.</param>
        /// <returns>
        /// return voter list
        /// </returns>
        public async Task<Result<VoterModel>> GetTransferInVoterList(TransferInVoterRequestModel transferInVoterRequestModel)
        {
            if (!string.IsNullOrEmpty(transferInVoterRequestModel.FName))
            {
                transferInVoterRequestModel.FName = transferInVoterRequestModel.FName.Replace("'", "''");
            }
            if (!string.IsNullOrEmpty(transferInVoterRequestModel.LName))
            {
                transferInVoterRequestModel.LName = transferInVoterRequestModel.LName.Replace("'", "''");
            }
            Result<VoterModel> result = new Result<VoterModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.GetTransferInVoterList;

                DynamicParameters param = new DynamicParameters();
                param.Add("@RegionId", transferInVoterRequestModel.RegionId);
                param.Add("@ProvinceId", transferInVoterRequestModel.ProvinceId);
                param.Add("@DistrictId", transferInVoterRequestModel.DistrictId);
                param.Add("@LLGId", transferInVoterRequestModel.LLGId);
                param.Add("@WardId", transferInVoterRequestModel.WardId);
                param.Add("@MKADistrictId", transferInVoterRequestModel.MKADistrictId);
                param.Add("@MKALLGId", transferInVoterRequestModel.MKALLGId);
                param.Add("@MKAWardId", transferInVoterRequestModel.MKAWardId);
                param.Add("@KeyWord", transferInVoterRequestModel.KeyWord);
                param.Add("@FName", transferInVoterRequestModel.FName);
                param.Add("@LName", transferInVoterRequestModel.LName);
                param.Add("@Gender", transferInVoterRequestModel.Gender);
                param.Add("@PageNo", transferInVoterRequestModel.PageNo);
                param.Add("@PageSize", transferInVoterRequestModel.PageSize);
                param.Add("@DatabaseName", transferInVoterRequestModel.DatabaseName);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                var lstVoter = await con.QueryMultipleAsync(squery, param, commandType: CommandType.StoredProcedure);
                if (lstVoter != null)
                {
                    result.ListItems = await lstVoter.ReadAsync<VoterModel>();
                    result.TotalRecordCount = await lstVoter.ReadFirstOrDefaultAsync<int>();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return result;
            }
        }
        #endregion

        #region sync electors
        /// <summary>
        /// Synchronizes the electors.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// return elector id
        /// </returns>
        public async Task<long> SyncElectors(VoterModel model)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.SyncElectors;

               
                DynamicParameters param = new DynamicParameters();
                param.Add("@ElectorId", model.ElectorId);
                param.Add("@OldElectorID", model.OldElectorID);
                param.Add("@LocationId", model.LocationId);
                param.Add("@MKALocationId", model.MKALocationId);
                param.Add("@WardId", model.WardId);
                param.Add("@MKAWardId", model.MKAWardId);
                param.Add("@LLGId", model.LLGId);
                param.Add("@MKALLGId", model.MKALLGId);
                param.Add("@DistrictId", model.DistrictId);
                param.Add("@MKADistrictId", model.MKADistrictId);
                param.Add("@MKAClanId", model.MKAClanId);
                param.Add("@ProvinceId", model.ProvinceId);
                param.Add("@RegionId", model.RegionId);
                param.Add("@LName", model.LName);
                param.Add("@FName", model.FName);
                param.Add("@Gender", model.Gender);
                param.Add("@DOB", model.DOB);
                param.Add("@isYOB", model.isYOB);
                param.Add("@isPWD", model.isPWD);
                param.Add("@FatherLName", model.FatherLName);
                param.Add("@FatherFName", model.FatherFName);
                param.Add("@MotherLName", model.MotherLName);
                param.Add("@MotherFName", model.MotherFName);
                param.Add("@ProvOfBirthFK", model.ProvOfBirthFK);
                param.Add("@PlaceOfBirth", model.PlaceOfBirth);
                param.Add("@OccupationFK", model.OccupationFK);
                param.Add("@ClanFK", model.ClanFK);
                param.Add("@SubClan", model.SubClan);
                param.Add("@Address", model.Address);
                param.Add("@NatID", model.NatID);
                param.Add("@NatIDRecordNo", model.NatIDRecordNo);
                param.Add("@Status", model.Status);
                param.Add("@HouseFK", model.HouseFK);
                param.Add("@HouseSortOrder", model.HouseSortOrder);
                param.Add("@FamilyFK", model.FamilyFK);
                param.Add("@FamilySortOrder", model.FamilySortOrder);
                param.Add("@dpo", model.dpo);
                param.Add("@ClanId", model.ClanId);


                ////This static ward id for KUPIANO TOWN(750) as per client requirements
                ///If any KUPIANO ward records update from mobile, lat long should be static given by client
                ///-10.07419329398167, 148.18397820765685
                if (model.WardId == 750)
                {
                    //model.Lat = Convert.ToDecimal(-10.07419329398167);
                    //model.Long = Convert.ToDecimal(148.18397820765685);
                    model.Lat = Convert.ToDecimal(-10.072770);
                    model.Long = Convert.ToDecimal(148.18413);
                }

                param.Add("@Lat", model.Lat);
                param.Add("@Long", model.Long);
                param.Add("@FamilyNumber", model.FamilyNumber);
                param.Add("@FamilyName", model.FamilyName);
                param.Add("@OpenElectorate", model.OpenElectorate);
                param.Add("@Clan", model.Clan);
                param.Add("@RestHouse", model.RestHouse);
                param.Add("@SectionNo", model.SectionNo);
                param.Add("@LotNo", model.LotNo);
                param.Add("@StreetName", model.StreetName);
                param.Add("@SuburbTownSettlement", model.SuburbTownSettlement);
                param.Add("@Comment", model.Comment);
                param.Add("@ElectorProfileImage", model.ElectorProfileImage);
                param.Add("@CreatedBy", model.CreatedBy);
                param.Add("@ElectorFormImage", model.ElectorFormImage);
                param.Add("@EnrolmentFormNumber", model.EnrolmentFormNumber);
                param.Add("@EnrolmentFormType", model.EnrolmentFormType);
                param.Add("@TempElectorId", model.TempElectorId);
                param.Add("@DuplicateId", model.DuplicateId);
                param.Add("@AuditDescription", model.AuditDescription);
                param.Add("@HasTransferInForm11", model.HasTransferInForm11);

                param.Add("@TransferInRegionId", model.TransferInRegionId);
                param.Add("@TransferInProvinceId", model.TransferInProvinceId);
                param.Add("@TransferInDistrictId", model.TransferInDistrictId);
                param.Add("@TransferInLLGId", model.TransferInLLGId);
                param.Add("@TransferInWardId", model.TransferInWardId);
                param.Add("@TransferInLocationId", model.TransferInLocationId);

                param.Add("@TransferOutRegionId", model.TransferOutRegionId);
                param.Add("@TransferOutProvinceId", model.TransferOutProvinceId);
                param.Add("@TransferOutDistrictId", model.TransferOutDistrictId);
                param.Add("@TransferOutLLGId", model.TransferOutLLGId);
                param.Add("@TransferOutWardId", model.TransferOutWardId);
                param.Add("@TransferOutLocationId", model.TransferOutLocationId);

                //MKA Fields             
                param.Add("@TransferInMKADistrictId", model.TransferInMKADistrictId);
                param.Add("@TransferInMKALLGId", model.TransferInMKALLGId);
                param.Add("@TransferInMKAWardId", model.TransferInMKAWardId);
                param.Add("@TransferInMKALocationId", model.TransferInMKALocationId);
             
                param.Add("@TransferOutMKADistrictId", model.TransferOutMKADistrictId);
                param.Add("@TransferOutMKALLGId", model.TransferOutMKALLGId);
                param.Add("@TransferOutMKAWardId", model.TransferOutMKAWardId);
                param.Add("@TransferOutMKALocationId", model.TransferOutMKALocationId);

                param.Add("@Religion", model.Religion);
                param.Add("@IsRejected", model.IsRejected);
                param.Add("@OldLocationId", model.OldLocationId);
                param.Add("@IsManualFormNumber", model.IsManualFormNumber);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var electorId = await con.QueryFirstOrDefaultAsync<long?>(squery, param, commandType: CommandType.StoredProcedure) ?? 0;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return electorId;
            }
        }
        #endregion

        #region sync electors images
        /// <summary>
        /// Synchronizes the electors images.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// return elector id
        /// </returns>
        public async Task<long> SyncElectorsImages(VoterModel model)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.SyncElectorsImages;

                DynamicParameters param = new DynamicParameters();
                param.Add("@ElectorId", model.ElectorId);
                param.Add("@LocationId", model.LocationId);
                param.Add("@WardId", model.WardId);
                param.Add("@LLGId", model.LLGId);
                param.Add("@DistrictId", model.DistrictId);
                param.Add("@ProvinceId", model.ProvinceId);
                param.Add("@RegionId", model.RegionId);
                param.Add("@ElectorProfileImage", model.ElectorProfileImage);
                param.Add("@CreatedBy", model.CreatedBy);
                param.Add("@ElectorFormImage", model.ElectorFormImage);
                param.Add("@EnrolmentFormNumber", model.EnrolmentFormNumber);
                param.Add("@TempElectorId", model.TempElectorId);
                param.Add("@Status", model.Status);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                var electorId = await con.QueryFirstOrDefaultAsync<long>(squery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return electorId;
            }
        }
        #endregion

        #region import biodata details
        /// <summary>
        /// Imports the bio data details.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<long> ImportBioDataDetails(VoterModel model)
        {
            try
            {
                byte[] fingerPrintByteData = Encoding.UTF8.GetBytes(model.ElectorFingerPrintImage);
                //string decrypted = Encoding.UTF8.GetString(fingerPrintByteData);

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string squery = StoredProcedure.ImportBioDataDetails;

                    DynamicParameters param = new DynamicParameters();
                    param.Add("@ElectorId", model.ElectorId);
                    param.Add("@FName", model.FName);
                    param.Add("@LName", model.LName);

                    param.Add("@RegionId", model.RegionId);
                    param.Add("@ProvinceId", model.ProvinceId);
                    param.Add("@LLGId", model.LLGId);
                    param.Add("@DistrictId", model.DistrictId);
                    param.Add("@WardId", model.WardId);
                    param.Add("@LocationId", model.LocationId);

                    if (!string.IsNullOrWhiteSpace(model.NatID))
                    {
                        param.Add("@NatID", model.NatID);
                    }

                    if (!string.IsNullOrWhiteSpace(model.ElectorProfileImage))
                    {
                        param.Add("@ElectorProfileImage", model.ElectorProfileImage);
                    }
                    param.Add("@OccupationFK", model.OccupationFK);
                    param.Add("@ProvOfBirthFK", model.ProvOfBirthFK);
                    param.Add("@isPWD", model.isPWD);
                    param.Add("@Occupation", model.Occupation);
                    param.Add("@Address", model.Address);
                    param.Add("@DOB", model.DOB);
                    param.Add("@Gender", model.Gender);
                    param.Add("@Status", model.Status);
                    param.Add("@CreatedBy", model.CreatedBy);

                    param.Add("@PlaceOfBirth", model.PlaceOfBirth);
                    param.Add("@FatherFName", model.FatherFName);
                    param.Add("@FatherLName", model.FatherLName);
                    param.Add("@MotherFName", model.MotherFName);
                    param.Add("@MotherLName", model.MotherLName);
                    param.Add("@Clan", model.Clan);
                    param.Add("@Religion", model.Religion);
                    param.Add("@Lat", model.Lat);
                    param.Add("@Long", model.Long);
                    if (!string.IsNullOrWhiteSpace(model.ElectorSignatureImage))
                    {
                        param.Add("@ElectorSignatureImage", model.ElectorSignatureImage);
                    }
                    param.Add("@ElectorFingerPrintImage", model.ElectorFingerPrintImage);
                    param.Add("@EnrolmentFormNumber", model.EnrolmentFormNumber);
                    param.Add("@ElectorFingerPrintImageByte", fingerPrintByteData);
                    param.Add("@ElectorProfileImageBinary", model.ElectorProfileImageBinary);
                    param.Add("@ElectorSignatureImageBinary", model.ElectorSignatureImageBinary);

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    var electorId = await con.QueryFirstOrDefaultAsync<long>(squery, param, commandType: CommandType.StoredProcedure);

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    return electorId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region get updated voter list
        /// <summary>
        /// Gets the updated voter list.
        /// </summary>
        /// <param name="transferInVoterRequestModel">The updated voter request model.</param>
        /// <returns>
        /// return voter list
        /// </returns>
        public async Task<Result<VoterModel>> GetUpdatedVoterList(TransferInVoterRequestModel transferInVoterRequestModel)
        {
            Result<VoterModel> result = new Result<VoterModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.GetUpdatedVoterList;

                DynamicParameters param = new DynamicParameters();
                param.Add("@RegionId", transferInVoterRequestModel.RegionId);
                param.Add("@ProvinceId", transferInVoterRequestModel.ProvinceId);
                param.Add("@DistrictId", transferInVoterRequestModel.DistrictId);
                param.Add("@LLGId", transferInVoterRequestModel.LLGId);
                param.Add("@WardId", transferInVoterRequestModel.WardId);
                param.Add("@PageNo", transferInVoterRequestModel.PageNo);
                param.Add("@PageSize", transferInVoterRequestModel.PageSize);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                var lstVoter = await con.QueryMultipleAsync(squery, param, commandType: CommandType.StoredProcedure);
                if (lstVoter != null)
                {
                    result.ListItems = await lstVoter.ReadAsync<VoterModel>();
                    result.TotalRecordCount = await lstVoter.ReadFirstOrDefaultAsync<int>();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return result;
            }
        }
        #endregion

        #region TransferVoterByRO
        /// <summary>
        /// Transfers the voter by ro.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<int> TransferVoterByRO(VoterModel model)
        {
            int result = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string squery = StoredProcedure.Transfer_Voter_By_RO;

                DynamicParameters param = new DynamicParameters();
                param.Add("@ElectorId", model.ElectorId);
                param.Add("@LocationId", model.LocationId);
                param.Add("@RegionId", model.RegionId);
                param.Add("@ProvinceId", model.ProvinceId);
                param.Add("@DistrictId", model.DistrictId);
                param.Add("@LLGId", model.LLGId);
                param.Add("@WardId", model.WardId);
                param.Add("@DatabaseName", model.Database);

                param.Add("@TransferOutRegionId", model.TransferOutRegionId);
                param.Add("@TransferOutProvinceId", model.TransferOutProvinceId);
                param.Add("@TransferOutDistrictId", model.TransferOutDistrictId);
                param.Add("@TransferOutLLGId", model.TransferOutLLGId);
                param.Add("@TransferOutWardId", model.TransferOutWardId);
                param.Add("@TransferOutLocationId", model.TransferOutLocationId);

                result = await con.QueryFirstOrDefaultAsync<int>(squery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return result;
        }
        #endregion

        #region Unused repository method of finger print

        //public async Task<Result<FingerPrintModel>> GetFingerPrintByte()
        //{
        //    Result<FingerPrintModel> result = new Result<FingerPrintModel>();
        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        string squery = "sp_GetFingerPrintData";

        //        DynamicParameters param = new DynamicParameters();

        //        if (con.State == ConnectionState.Closed)
        //        {
        //            con.Open();
        //        }
        //        var res = await con.QueryMultipleAsync(squery, param, commandType: CommandType.StoredProcedure);
        //        if (res != null)
        //        {
        //            result.ListItems = await res.ReadAsync<FingerPrintModel>();
        //        }

        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }
        //        return result;
        //    }
        //}
        #endregion

        #region get voter list mobile

        /// <summary>
        /// Gets the preliminary voter list mobile.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns>
        /// return voter list
        /// </returns>
        public async Task<Result<VoterModel>> GetPreliminaryVoterListMobile(VoterRequestModel voterReqModel)
        {
            Result<VoterModel> result = new Result<VoterModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.GetPreliminaryVoterListMobile;

                DynamicParameters param = new DynamicParameters();
                param.Add("@ProvinceId", voterReqModel.ProvinceId);
                param.Add("@DistrictId", voterReqModel.DistrictId != 0 ? voterReqModel.DistrictId : voterReqModel.MKADistrictId);
                param.Add("@LLGId", voterReqModel.LLGId != 0 ? voterReqModel.LLGId : voterReqModel.MKALLGId);
                param.Add("@WardId", voterReqModel.WardId != 0 ? voterReqModel.WardId : voterReqModel.MKAWardId);
                param.Add("@LocationId", voterReqModel.LocationId != 0 ? voterReqModel.LocationId : voterReqModel.MKALocationId);
                param.Add("@MKADistrictId", voterReqModel.MKADistrictId != 0 ? voterReqModel.MKADistrictId : voterReqModel.DistrictId);
                param.Add("@MKALLGId", voterReqModel.MKALLGId != 0 ? voterReqModel.MKALLGId : voterReqModel.LLGId);
                param.Add("@MKAWardId", voterReqModel.MKAWardId != 0 ? voterReqModel.MKAWardId : voterReqModel.WardId);
                param.Add("@MKALocationId", voterReqModel.MKALocationId != 0 ? voterReqModel.MKALocationId : voterReqModel.LocationId);
                param.Add("@PageNo", voterReqModel.PageNo);
                param.Add("@PageSize", voterReqModel.PageSize);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                var res = await con.QueryMultipleAsync(squery, param, commandType: CommandType.StoredProcedure);
                if (res != null)
                {
                    result.ListItems = await res.ReadAsync<VoterModel>();
                    result.TotalRecordCount = await res.ReadFirstOrDefaultAsync<int>();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return result;
            }
        }
        #endregion
        #endregion
    }
}
