using Dapper;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Role;
using PNGEC.API.Helper;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PNGEC.API.Repository.Role
{
    public class RoleRepository : IRoleRepository
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The Role
        /// </summary>
        private RoleModel Role = null;

        /// <summary>
        /// The lstRole
        /// </summary>
        private Result<RoleModel> result = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public RoleRepository(string connection)
        {
            connectionString = connection;
        }
        #endregion

        #region repository

        #region add update role
        /// <summary>
        /// Add/update the Role.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<RoleModel> AddUpdateRole(RoleModel model)
        {
            Role = new RoleModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Add_Update_Role;

                DynamicParameters param = new DynamicParameters();
                param.Add("@RoleId", model.RoleId);
                param.Add("@RoleName", model.RoleName);
                param.Add("@About", model.About);
                param.Add("@Status", model.Status);
                param.Add("@ActionBy", Globals.ActionBy);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                Role = await con.QueryFirstOrDefaultAsync<RoleModel>(sQuery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return Role;
        }
        #endregion

        #region get Role list
        /// <summary>
        /// Get the Role list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Result<RoleModel>> GetRoleList(PageList model)
        {
            result = new Result<RoleModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Get_All_Role_List;

                DynamicParameters param = new DynamicParameters();
                param.Add("@PageNo", model.PageNo);
                param.Add("@PageSize", model.PageSize);
                param.Add("@KeyWord", model.KeyWord);
                param.Add("@SortField", string.IsNullOrWhiteSpace(model.SortField) ? "RoleId" : model.SortField);
                param.Add("@SortOrder", string.IsNullOrWhiteSpace(model.SortOrder) ? "DESC" : model.SortOrder);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var lsRole = await con.QueryMultipleAsync(sQuery, param, commandType: CommandType.StoredProcedure);
                if (lsRole != null)
                {
                    result.ListItems = await lsRole.ReadAsync<RoleModel>();
                    result.TotalRecordCount = await lsRole.ReadFirstOrDefaultAsync<int>();
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
