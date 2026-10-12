using Dapper;
using PNGEC.API.Domain.Device;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Helper;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PNGEC.API.Repository.Device
{
    public class DeviceRepository : IDeviceRepository
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The device
        /// </summary>
        private DeviceModel device = null;

        /// <summary>
        /// The lstdevice
        /// </summary>
        private Result<DeviceModel> result = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public DeviceRepository(string connection)
        {
            connectionString = connection;
        }
        #endregion

        #region repository

        #region add Update Device
        /// <summary>
        /// Add/update the Device.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<DeviceModel> AddUpdateDevice(DeviceModel model)
        {
            device = new DeviceModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Add_Update_Device;

                DynamicParameters param = new DynamicParameters();
                param.Add("@DeviceId", model.DeviceId);
                param.Add("@DeviceNumber", model.DeviceNumber);
                param.Add("@DeviceName", model.DeviceName);
                param.Add("@DeviceOS", model.DeviceOS);
                param.Add("@DeviceType", model.DeviceType);
                param.Add("@AssignedTo", model.AssignedTo);
                param.Add("@Status", model.Status);
                param.Add("@ActionBy", Globals.ActionBy);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                device = await con.QueryFirstOrDefaultAsync<DeviceModel>(sQuery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return device;
        }
        #endregion

        #region get device list
        /// <summary>
        /// Get the Device list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Result<DeviceModel>> GetDeviceList(PageList model)
        {
            result = new Result<DeviceModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Get_All_Device_List;

                DynamicParameters param = new DynamicParameters();
                param.Add("@PageNo", model.PageNo);
                param.Add("@PageSize", model.PageSize);
                param.Add("@KeyWord", model.KeyWord);
                param.Add("@SortField", string.IsNullOrWhiteSpace(model.SortField) ? "DeviceId" : model.SortField);
                param.Add("@SortOrder", string.IsNullOrWhiteSpace(model.SortOrder) ? "DESC" : model.SortOrder);
                param.Add("@LoggedUserId", model.LoggedUserId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var lsdevice = await con.QueryMultipleAsync(sQuery, param, commandType: CommandType.StoredProcedure);
                if (lsdevice != null)
                {
                    result.ListItems = await lsdevice.ReadAsync<DeviceModel>();
                    result.TotalRecordCount = await lsdevice.ReadFirstOrDefaultAsync<int>();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return result;
            }
        }
        #endregion

        #region update device status
        /// <summary>
        /// Updates the device status.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Result<DeviceModel>> UpdateDeviceStatus(CommonRequest model)
        {
            result = new Result<DeviceModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Update_Device_Status;

                DynamicParameters param = new DynamicParameters();
                param.Add("@Status", model.status);
                param.Add("@DeviceIds", model.ids);
                
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var lstuser = await con.QueryMultipleAsync(sQuery, param, commandType: CommandType.StoredProcedure);
                if (lstuser != null)
                {
                    result.ListItems = await lstuser.ReadAsync<DeviceModel>();
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

        #endregion
    }
}
