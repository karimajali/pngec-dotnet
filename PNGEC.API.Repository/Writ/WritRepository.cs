using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Mobile.General;
using PNGEC.API.Domain.Writ;
using PNGEC.API.Helper;

namespace PNGEC.API.Repository.Writ
{
    public class WritRepository : IWritRepository
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The Writ
        /// </summary>
        private WritModel Writ = null;

        /// <summary>
        /// The lstWrit
        /// </summary>
        private Result<WritModel> result = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="WritRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public WritRepository(string connection)
        {
            connectionString = connection;
        }
        #endregion

        #region repository

        #region add update Writ
        /// <summary>
        /// Add/update the Writ.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<WritModel> AddUpdateWrit(WritModel model)
        {
            Writ = new WritModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Add_Update_Writ;

                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", model.Id);
                param.Add("@WritName", model.WritName);
                param.Add("@RegionId", model.RegionId);
                param.Add("@ProvinceId", model.ProvinceId);
                param.Add("@DistrictId", model.DistrictId);
                param.Add("@LLGId", model.LLGId);
                param.Add("@WardId", model.WardId);
                param.Add("@Type", model.Type);
                param.Add("@Scop", model.Scop);
                param.Add("@IsLocked", model.IsLocked);
                param.Add("@NominationStartDate", model.NominationStartDate);
                param.Add("@NominationEndDate", model.NominationEndDate);
                param.Add("@FirstDayOfPolling", model.FirstDayOfPolling);
                param.Add("@LastDayOfPolling", model.LastDayOfPolling);
                param.Add("@ReturnOfWritDate", model.ReturnOfWritDate);
                param.Add("@IssueDate", model.IssueDate);
                param.Add("@IssuerName", model.IssuerName);
                param.Add("@IssuerTitle", model.IssuerTitle);
                param.Add("@CreatedBy", model.CreatedBy);
                param.Add("@ModifiedBy", model.ModifiedBy);


                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                Writ = await con.QueryFirstOrDefaultAsync<WritModel>(sQuery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return Writ;
        }
        #endregion

        #region Get Writ List
        /// <summary>
        /// Gets the Writ list.
        /// </summary>
        /// <returns></returns>
        public async Task<List<WritModel>> GetWritList(PageList model)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.GetWritList;
                DynamicParameters param = new DynamicParameters();
                param.Add("@KeyWord", model.KeyWord);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                var res = await con.QueryAsync<WritModel>(squery, param, commandType: CommandType.StoredProcedure);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return res.ToList();
            }
        }

        #endregion       

        #endregion

    }
}
