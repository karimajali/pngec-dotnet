using Dapper;
using PNGEC.API.Domain.DataMigration;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Helper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PNGEC.API.Repository.DataMigration
{
    public class DataMigrationRepository : IDataMigrationRepository
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The result
        /// </summary>
        private bool bitResult = false;

        /// <summary>
        /// The result
        /// </summary>
        private Result<VoterAuditModel> result = null;

        /// <summary>
        /// The migration transferred in out
        /// </summary>
        private MigrationTransferredInOut migrationTransferredInOut = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DataMigrationRepository"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public DataMigrationRepository(string connection)
        {
            connectionString = connection;
        }
        #endregion

        #region repository

        #region manage voter status
        /// <summary>
        /// Manages the voter status.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> ManageVoterStatus(CommonRequest model, VoterModel voterModel)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Manage_Voter_Status;

                DynamicParameters param = new DynamicParameters();
                param.Add("@DatabaseName", model.database);
                param.Add("@RecordIds", Convert.ToString(voterModel.ElectorId));
                param.Add("@Type", model.recordType);
                param.Add("@ActionBy", model.id);
                param.Add("@IsApproved", model.isApproved);
                param.Add("@IsRejected", model.isRejected);

                param.Add("@RegionId", voterModel.RegionId);
                param.Add("@ProvinceId", voterModel.ProvinceId);
                param.Add("@DistrictId", voterModel.DistrictId);
                param.Add("@LLGId", voterModel.LLGId);
                param.Add("@WardId", voterModel.WardId);
                param.Add("@LocationId", voterModel.LocationId);

                //MKA Fields                
                param.Add("@MKADistrictId", voterModel.MKADistrictId);
                param.Add("@MKALLGId", voterModel.MKALLGId);
                param.Add("@MKAWardId", voterModel.MKAWardId);
                param.Add("@MKALocationId", voterModel.MKALocationId);
                param.Add("@MKAClanId", voterModel.MKAClanId);

                param.Add("@RejectedReason", voterModel.RejectedReason);
                param.Add("@ARLat", model.VoterList.FirstOrDefault().ARLat);
                param.Add("@ARLong", model.VoterList.FirstOrDefault().ARLong);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                bitResult = await con.QueryFirstOrDefaultAsync<bool>(sQuery, param, commandType: CommandType.StoredProcedure, commandTimeout: 120);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return bitResult;
        }
        #endregion

        #region manage voter status transferred in out
        /// <summary>
        /// Manages the voter status transferred in out.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<MigrationTransferredInOut> ManageVoterStatusTransferredInOut(CommonRequest model, VoterModel voterModel)
        {
            migrationTransferredInOut = new MigrationTransferredInOut();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Manage_VoterStatus_Transferred_InOut;

                DynamicParameters param = new DynamicParameters();
                param.Add("@DatabaseName", model.database);
                param.Add("@RecordIds", model.ids);
                param.Add("@Type", model.recordType);
                param.Add("@ActionBy", model.id);
                param.Add("@IsApproved", model.isApproved);
                param.Add("@IsRejected", model.isRejected);
                param.Add("@ElectorId", voterModel.ElectorId);
                param.Add("@RegionId", voterModel.RegionId);
                param.Add("@ProvinceId", voterModel.ProvinceId);
                param.Add("@DistrictId", voterModel.DistrictId);
                param.Add("@LLGId", voterModel.LLGId);
                param.Add("@WardId", voterModel.WardId);
                param.Add("@LocationId", voterModel.LocationId);

                #region unused
                //param.Add("@TransferInRegionId", voterModel.TransferInRegionId);
                //param.Add("@TransferInProvinceId", voterModel.TransferInProvinceId);
                //param.Add("@TransferInDistrictId", voterModel.TransferInDistrictId);
                //param.Add("@TransferInLLGId", voterModel.TransferInLLGId);
                //param.Add("@TransferInWardId", voterModel.TransferInWardId);
                //param.Add("@TransferInLocationId", voterModel.TransferInLocationId);
                #endregion

                param.Add("@TransferOutRegionId", voterModel.TransferOutRegionId);
                param.Add("@TransferOutProvinceId", voterModel.TransferOutProvinceId);
                param.Add("@TransferOutDistrictId", voterModel.TransferOutDistrictId);
                param.Add("@TransferOutLLGId", voterModel.TransferOutLLGId);
                param.Add("@TransferOutWardId", voterModel.TransferOutWardId);
                param.Add("@TransferOutLocationId", voterModel.TransferOutLocationId);

                //MKA Fields             
                param.Add("@MKADistrictId", voterModel.MKADistrictId);
                param.Add("@MKALLGId", voterModel.MKALLGId);
                param.Add("@MKAWardId", voterModel.MKAWardId);
                param.Add("@MKALocationId", voterModel.MKALocationId);

                param.Add("@TransferOutMKADistrictId", voterModel.TransferOutMKADistrictId);
                param.Add("@TransferOutMKALLGId", voterModel.TransferOutMKALLGId);
                param.Add("@TransferOutMKAWardId", voterModel.TransferOutMKAWardId);
                param.Add("@TransferOutMKALocationId", voterModel.TransferOutMKALocationId);
                param.Add("@TransferType", voterModel.EnrolmentFormType);

                param.Add("@RejectedReason", voterModel.RejectedReason);
                param.Add("@ARLat", model.VoterList.FirstOrDefault().ARLat);
                param.Add("@ARLong", model.VoterList.FirstOrDefault().ARLong);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                migrationTransferredInOut = await con.QueryFirstOrDefaultAsync<MigrationTransferredInOut>(sQuery, param, commandType: CommandType.StoredProcedure, commandTimeout: 120);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return migrationTransferredInOut;
        }
        #endregion

        #region get voter audit list
        /// <summary>
        /// Gets the voter audit list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Result<VoterAuditModel>> GetVoterAuditList(PageList model)
        {
            result = new Result<VoterAuditModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Get_Voter_Audit_List;

                DynamicParameters param = new DynamicParameters();
                param.Add("@DatabaseName", model.DatabaseName);

                param.Add("@PageNo", model.PageNo);
                param.Add("@PageSize", model.PageSize);
                param.Add("@KeyWord", model.KeyWord);
                param.Add("@SortField", string.IsNullOrWhiteSpace(model.SortField) ? "ElectorId" : model.SortField);
                param.Add("@SortOrder", string.IsNullOrWhiteSpace(model.SortOrder) ? "DESC" : model.SortOrder);

                param.Add("@ElectorId", model.Id);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var lstuser = await con.QueryMultipleAsync(sQuery, param, commandType: CommandType.StoredProcedure);
                if (lstuser != null)
                {
                    result.ListItems = await lstuser.ReadAsync<VoterAuditModel>();
                    result.TotalRecordCount = await lstuser.ReadFirstOrDefaultAsync<int>();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return result;
            }
        }
        #endregion

        #region update voter
        /// <summary>
        /// Updates the voter.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<int> UpdateVoter(VoterModel model)
        {
            int result = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // RO, Admin or EM in staging
                string sQuery = StoredProcedure.Update_Voter_By_RO;
                if (model.Database == Convert.ToString(CommonEnum.Database.Preliminary))
                {
                    // Admin or EM user in preliminary
                    sQuery = StoredProcedure.Preliminary_Update_Voter_By_Admin;
                }

                DynamicParameters param = new DynamicParameters();
                param.Add("ElectorId", model.ElectorId);
                param.Add("LocationId", model.LocationId);
                param.Add("WardId", model.WardId);
                param.Add("LLGId", model.LLGId);
                param.Add("DistrictId", model.DistrictId);
                //DB Changes for MKA Fields
                param.Add("MKALocationId", model.MKALocationId);
                param.Add("MKAWardId", model.MKAWardId);
                param.Add("MKALLGId", model.MKALLGId);
                param.Add("MKADistrictId", model.MKADistrictId);
                param.Add("MKAClanId", model.MKAClanId);
                param.Add("ProvinceId", model.ProvinceId);
                //param.Add("RegionId", model.RegionId);
                param.Add("LName", model.LName);
                param.Add("FName", model.FName);
                param.Add("Gender", model.Gender);
                param.Add("DOB", model.DOB);
                param.Add("isPWD", model.isPWD);
                param.Add("FatherLName", model.FatherLName);
                param.Add("FatherFName", model.FatherFName);
                param.Add("MotherLName", model.MotherLName);
                param.Add("MotherFName", model.MotherFName);
                param.Add("ProvOfBirthFK", model.ProvOfBirthFK);
                param.Add("PlaceOfBirth", model.PlaceOfBirth);
                param.Add("OccupationFK", model.OccupationFK);
                param.Add("SubClan", model.SubClan);
                param.Add("NatIDRecordNo", model.NatIDRecordNo);
                param.Add("Status", model.Status);
                param.Add("FamilySortOrder", model.FamilySortOrder);
                param.Add("Lat", model.Lat);
                param.Add("Long", model.Long);
                param.Add("FamilyNumber", model.FamilyNumber);
                param.Add("FamilyName", model.FamilyName);
                param.Add("Clan", model.Clan);
                param.Add("ClanId", model.GeoClanID);
                param.Add("RestHouse", model.RestHouse);
                param.Add("SectionNo", model.SectionNo);
                param.Add("LotNo", model.LotNo);
                param.Add("StreetName", model.StreetName);
                param.Add("SuburbTownSettlement", model.SuburbTownSettlement);
                param.Add("Comment", model.Comment);
                param.Add("Religion", model.Religion);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                result = await con.QueryFirstOrDefaultAsync<int>(sQuery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return result;
        }
        #endregion

        #endregion
    }
}
