using Dapper;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Mobile.User;
using PNGEC.API.Domain.User;
using PNGEC.API.Helper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PNGEC.API.Repository.User
{
    public class UserRepository : IUserRepository
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The user
        /// </summary>
        private UserModel user = null;

        /// <summary>
        /// The lstuser
        /// </summary>
        private Result<UserModel> result = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public UserRepository(string connection)
        {
            connectionString = connection;
        }
        #endregion

        #region repository

        #region add update user
        /// <summary>
        /// Adds the update user.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<UserModel> AddUpdateUser(UserModel model)
        {
            user = new UserModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Add_Update_User;

                DynamicParameters param = new DynamicParameters();
                param.Add("@UserId", model.UserId);
                param.Add("@FirstName", model.FirstName);
                param.Add("@LastName", model.LastName);
                param.Add("@ProfileImageName", model.ProfileImageName);
                param.Add("@EmailAddress", model.EmailAddress);
                param.Add("@ContactNumber", model.ContactNumber);
                param.Add("@Password", model.Password);
                param.Add("@Status", model.Status);
                param.Add("@Role", model.Role);
                param.Add("@Region", model.Region);
                param.Add("@Province", model.Province);
                param.Add("@District", model.District);
                param.Add("@LLG", model.LLG);
                param.Add("@Ward", model.Ward);
                param.Add("@Location", model.Location);
                param.Add("@MKADistrict", model.MKADistrict);
                param.Add("@MKALLG", model.MKALLG);
                param.Add("@MKAWard", model.MKAWard);
                param.Add("@MKALocation", model.MKALocation);
                param.Add("@Permission", model.Permission);
                param.Add("@ActionBy", model.ActionBy);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                user = await con.QueryFirstOrDefaultAsync<UserModel>(sQuery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return user;
        }
        #endregion

        #region get user list
        /// <summary>
        /// Gets the user list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Result<UserModel>> GetUserList(PageList model)
        {
            result = new Result<UserModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Get_All_User_List;

                DynamicParameters param = new DynamicParameters();
                param.Add("@PageNo", model.PageNo);
                param.Add("@PageSize", model.PageSize);
                param.Add("@KeyWord", model.KeyWord);
                param.Add("@SortField", string.IsNullOrWhiteSpace(model.SortField) ? "UserId" : model.SortField);
                param.Add("@SortOrder", string.IsNullOrWhiteSpace(model.SortOrder) ? "DESC" : model.SortOrder);
                param.Add("@LoggedUserId", model.LoggedUserId);
                param.Add("@GeoName", model.GeoName);
                param.Add("@GeoId", model.GeoId);
                param.Add("@RoleId", model.RoleId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var lstuser = await con.QueryMultipleAsync(sQuery, param, commandType: CommandType.StoredProcedure);
                if (lstuser != null)
                {
                    result.ListItems = await lstuser.ReadAsync<UserModel>();
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

        #region update user status
        /// <summary>
        /// Updates the user status.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Result<UserModel>> UpdateUserStatus(CommonRequest model)
        {
            result = new Result<UserModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Update_User_Status;

                DynamicParameters param = new DynamicParameters();
                param.Add("@Status", model.status);
                param.Add("@UserIds", model.ids);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var lstuser = await con.QueryMultipleAsync(sQuery, param, commandType: CommandType.StoredProcedure);
                if (lstuser != null)
                {
                    result.ListItems = await lstuser.ReadAsync<UserModel>();
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

        #region user login
        /// <summary>
        /// Users the login.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Result<UserModel>> UserLogin(CommonRequest model)

        {
            result = new Result<UserModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.User_Login;

                DynamicParameters param = new DynamicParameters();
                param.Add("@UserName", model.username);
                param.Add("@Password", model.password);
                param.Add("@Database", model.database);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var lstuser = await con.QueryMultipleAsync(sql: sQuery, param: param, commandType: CommandType.StoredProcedure);
                if (lstuser != null)
                {
                    result.ListItems = await lstuser.ReadAsync<UserModel>();
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

        #region get super admin details
        /// <summary>
        /// Gets the super admin details.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<UserModel> GetSuperAdminDetails()
        {
            user = new UserModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Get_SuperAdmin_Details;

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                user = await con.QueryFirstOrDefaultAsync<UserModel>(sQuery, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return user;
        }
        #endregion

        #region reset password
        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<UserModel> ResetPassword(UserModel model)
        {
            user = new UserModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Reset_Password;

                DynamicParameters param = new DynamicParameters();
                param.Add("@UserId", model.UserId);
                param.Add("@EmailAddress", model.EmailAddress);
                param.Add("@Password", model.Password);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                user = await con.QueryFirstOrDefaultAsync<UserModel>(sQuery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return user;
        }
        #endregion

        #region mobileuserlogin        
        /// <summary>
        /// Mobiles the user login.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<UserModel> MobileUserLogin(LoginUserRequestModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string squery = StoredProcedure.Mobile_User_Login;

                    DynamicParameters param = new DynamicParameters();
                    param.Add("@UserName", model.UserName);
                    param.Add("@Password", model.Password);
                    param.Add("@DeviceNumber", model.DeviceNumber);

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    var res = await con.QueryFirstOrDefaultAsync<UserModel>(squery, param, commandType: CommandType.StoredProcedure);
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        #endregion

        #endregion
    }
}
