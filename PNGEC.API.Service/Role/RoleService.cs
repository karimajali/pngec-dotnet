using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Role;
using PNGEC.API.Helper;
using PNGEC.API.Repository.Role;
using System;
using System.Threading.Tasks;

namespace PNGEC.API.Service.Role
{
    public class RoleService : IRoleService
    {
        #region variable declaration
        /// <summary>
        /// The i Role repository
        /// </summary>
        private readonly IRoleRepository iRoleRepository;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The lstresponse
        /// </summary>
        private Response<RoleModel> lstresponse = null;

        /// <summary>
        /// The result
        /// </summary>
        private Result<RoleModel> result = null;


        /// <summary>
        /// The module name
        /// </summary>
        private const string ModuleName = "Role";
        #endregion

        #region constructor 
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleService"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public RoleService(string connection)
        {
            iRoleRepository = new RoleRepository(connection);
        }
        #endregion

        #region service

        #region add update role
        /// <summary>
        /// Adds/update the Role
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> AddUpdateRole(RoleModel model)
        {
            try
            {
                response = new Response();
                RoleModel Role = await iRoleRepository.AddUpdateRole(model);

                response.Status = Role.ResponseCode > 0;

                response.Message = ResponseMessage.Record_Add_Update_Success;
                response.Data = Role;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("RoleService", "AddUpdateRole", ex);
            }
            return response;
        }
        #endregion

        #region get Role list
        /// <summary>
        /// Gets the Role list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response<RoleModel>> GetRoleList(PageList model)
        {
            lstresponse = new Response<RoleModel>();

            try
            {
                result = await iRoleRepository.GetRoleList(model);

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
                ErrorLog.Write("RoleService", "GetRoleList", ex);
            }

            return lstresponse;
        }
        #endregion

        #endregion
    }
}
