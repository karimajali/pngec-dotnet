using Dapper;
using PNGEC.API.Domain.EnrolmentForm;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.MKAEnrolmentForm;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Helper;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PNGEC.API.Repository.EnrolmentForm
{
    public class EnrolmentFormRepository : IEnrolmentFormRepository
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The EnrolmentForm
        /// </summary>
        private EnrolmentFormModel EnrolmentForm = null;
        /// <summary>
        /// The EnrolmentForm
        /// </summary>
        private MKAEnrolmentFormModel MKAEnrolmentForm = null;

        /// <summary>
        /// The lstEnrolmentForm
        /// </summary>
        private Result<EnrolmentFormModel> result = null;
        /// <summary>
        /// The lstEnrolmentForm
        /// </summary>
        private Result<MKAEnrolmentFormModel> mkaresult = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="EnrolmentFormRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public EnrolmentFormRepository(string connection)
        {
            connectionString = connection;
        }
        #endregion

        #region repository

        #region Add EnrolmentForm
        /// <summary>
        /// Add the EnrolmentForm.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<EnrolmentFormModel> AddEnrolmentForm(EnrolmentFormModel model)
        {
            EnrolmentForm = new EnrolmentFormModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Add_EnrolmentForm;

                DynamicParameters param = new DynamicParameters();
                param.Add("@UserId", model.UserId);
                param.Add("@ProvinceId", model.ProvinceId);
                //param.Add("@StartRange", model.StartRange);
                //param.Add("@EndRange", model.EndRange);
                param.Add("@ActionBy", Globals.ActionBy);
                param.Add("@DistrictId", model.DistrictId);
                param.Add("@LLGId", model.LLGId);
                param.Add("@WardId", model.WardId);
                param.Add("@BatchName", model.BatchName);
                param.Add("@Attachment", model.Attachment);
                param.Add("@Comment", model.Comment);
                param.Add("@Comment", model.Comment);
                param.Add("@NumberOfBatch", model.NumberOfBatch);
                param.Add("@RegionId", model.RegionId);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                EnrolmentForm = await con.QueryFirstOrDefaultAsync<EnrolmentFormModel>(sQuery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return EnrolmentForm;
        }
        #endregion
        #region Add MKAEnrolmentForm
        /// <summary>
        /// Add the EnrolmentForm.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<MKAEnrolmentFormModel> AddMKAEnrolmentForm(MKAEnrolmentFormModel model)
        {
            MKAEnrolmentForm = new MKAEnrolmentFormModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Add_MKAEnrolmentForm;

                DynamicParameters param = new DynamicParameters();
                param.Add("@UserId", model.UserId);
                //param.Add("@StartRange", model.StartRange);
                //param.Add("@EndRange", model.EndRange);
                param.Add("@ActionBy", Globals.ActionBy);
                param.Add("@MKADistrictId", model.MKADistrictId);
                param.Add("@MKALLGId", model.MKALLGId);
                param.Add("@MKAWardId", model.MKAWardId);

                param.Add("@BatchName", model.BatchName);
                param.Add("@Attachment", model.Attachment);
                param.Add("@Comment", model.Comment);
                param.Add("@NumberOfBatch", model.NumberOfBatch);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                MKAEnrolmentForm = await con.QueryFirstOrDefaultAsync<MKAEnrolmentFormModel>(sQuery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return MKAEnrolmentForm;
        }
        #endregion
        #region Delete EnrolmentForm
        /// <summary>
        /// Delete Enrolment Form
        /// </summary>
        /// <param name="enrolmentFormId">The enrolmentFormId.</param>
        /// <returns></returns>
        public async Task<bool> DeleteEnrolmentForm(int enrolmentFormId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Delete_EnrolmentForm;

                DynamicParameters param = new DynamicParameters();
                param.Add("@EnrolmentFormId", enrolmentFormId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                await con.QueryAsync(sQuery, param, commandType: CommandType.StoredProcedure);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            //if(response true)
            return true;
        }
        #endregion
        #region Delete EnrolmentForm
        /// <summary>
        /// Delete Enrolment Form
        /// </summary>
        /// <param name="enrolmentFormId">The enrolmentFormId.</param>
        /// <returns></returns>
        public async Task<bool> DeleteMKAEnrolmentForm(int enrolmentFormId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Delete_MKAEnrolmentForm;

                DynamicParameters param = new DynamicParameters();
                param.Add("@EnrolmentFormId", enrolmentFormId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                await con.QueryAsync(sQuery, param, commandType: CommandType.StoredProcedure);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            //if(response true)
            return true;
        }
        #endregion
        #region get EnrolmentForm list
        /// <summary>
        /// Get the EnrolmentForm list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Result<EnrolmentFormModel>> GetEnrolmentFormList(PageList model)
        {
            result = new Result<EnrolmentFormModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Get_All_EnrolmentForm_List;

                DynamicParameters param = new DynamicParameters();
                param.Add("@PageNo", model.PageNo);
                param.Add("@PageSize", model.PageSize);
                param.Add("@KeyWord", model.KeyWord);
                param.Add("@SortField", string.IsNullOrWhiteSpace(model.SortField) ? "EnrolmentFormId" : model.SortField);
                param.Add("@SortOrder", string.IsNullOrWhiteSpace(model.SortOrder) ? "DESC" : model.SortOrder);
                param.Add("@GeoName", model.GeoName);
                param.Add("@GeoId", model.GeoId);

                param.Add("@LoggedUserId", model.LoggedUserId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var lsEnrolmentForm = await con.QueryMultipleAsync(sQuery, param, commandType: CommandType.StoredProcedure);
                if (lsEnrolmentForm != null)
                {
                    result.ListItems = await lsEnrolmentForm.ReadAsync<EnrolmentFormModel>();
                    result.TotalRecordCount = await lsEnrolmentForm.ReadFirstOrDefaultAsync<int>();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return result;
            }
        }
        #endregion
        #region get MKAEnrolmentForm list
        /// <summary>
        /// Get the EnrolmentForm list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Result<MKAEnrolmentFormModel>> GetMKAEnrolmentFormList(PageList model)
        {
            mkaresult = new Result<MKAEnrolmentFormModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Get_All_MKAEnrolmentForm_List;

                DynamicParameters param = new DynamicParameters();
                param.Add("@PageNo", model.PageNo);
                param.Add("@PageSize", model.PageSize);
                param.Add("@KeyWord", model.KeyWord);
                param.Add("@SortField", string.IsNullOrWhiteSpace(model.SortField) ? "EnrolmentFormId" : model.SortField);
                param.Add("@SortOrder", string.IsNullOrWhiteSpace(model.SortOrder) ? "DESC" : model.SortOrder);
                param.Add("@GeoName", model.GeoName);
                param.Add("@GeoId", model.GeoId);

                param.Add("@LoggedUserId", model.LoggedUserId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var lsEnrolmentForm = await con.QueryMultipleAsync(sQuery, param, commandType: CommandType.StoredProcedure);
                if (lsEnrolmentForm != null)
                {
                    mkaresult.ListItems = await lsEnrolmentForm.ReadAsync<MKAEnrolmentFormModel>();
                    mkaresult.TotalRecordCount = await lsEnrolmentForm.ReadFirstOrDefaultAsync<int>();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return mkaresult;
            }
        }
        #endregion

        #region get ward quality check data

        /// <summary>
        /// Gets the ward quality check data.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<WardQualityCheckSPResponseModel> GetWardQualityCheckData(EnrolmentFormModel model)
        {
            WardQualityCheckSPResponseModel wardQualityCheckSPResponseModel = new WardQualityCheckSPResponseModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.GetWardQualityCheckData;

                DynamicParameters param = new DynamicParameters();
                param.Add("@WardId", model.WardId);
                param.Add("@LLGId", model.LLGId);
                param.Add("@DistrictId", model.DistrictId);
                param.Add("@ProvinceId", model.ProvinceId);
                param.Add("@RegionId", model.RegionId);
                param.Add("@BatchName", model.BatchName);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var wardQualityCheckData = await con.QueryMultipleAsync(sQuery, param, commandType: CommandType.StoredProcedure);
                if (wardQualityCheckData != null)
                {
                    wardQualityCheckSPResponseModel.voterModels = await wardQualityCheckData.ReadAsync<VoterModel>();
                    wardQualityCheckSPResponseModel.enrolmentFormModels = await wardQualityCheckData.ReadAsync<EnrolmentFormModel>();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return wardQualityCheckSPResponseModel;
            }
        }
        #endregion
        #region get ward quality check data

        /// <summary>
        /// Gets the ward quality check data.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<WardQualityCheckSPResponseModel> GetMKAWardQualityCheckData(MKAEnrolmentFormModel model)
        {
            WardQualityCheckSPResponseModel wardQualityCheckSPResponseModel = new WardQualityCheckSPResponseModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.GetMKAWardQualityCheckData;

                DynamicParameters param = new DynamicParameters();
                param.Add("@MKAWardId", model.MKAWardId);
                param.Add("@MKALLGId", model.MKALLGId);
                param.Add("@MKADistrictId", model.MKADistrictId);
                param.Add("@BatchName", model.BatchName);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var wardQualityCheckData = await con.QueryMultipleAsync(sQuery, param, commandType: CommandType.StoredProcedure);
                if (wardQualityCheckData != null)
                {
                    wardQualityCheckSPResponseModel.voterModels = await wardQualityCheckData.ReadAsync<VoterModel>();
                    wardQualityCheckSPResponseModel.mkaenrolmentFormModels = await wardQualityCheckData.ReadAsync<MKAEnrolmentFormModel>();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return wardQualityCheckSPResponseModel;
            }
        }
        #endregion
        #endregion
    }
}
