using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PNGEC.API.Domain.Location;
using PNGEC.API.Helper;
using PNGEC.API.Repository.Location;

namespace PNGEC.API.Service.Location
{
    public class LocationService : ILocationService
    {
        #region variable declaration
        /// <summary>
        /// The i Location repository
        /// </summary>
        private readonly ILocationRepository iLocationRepository;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The lstresponse
        /// </summary>
        private Response<LocationModel> lstresponse = null;

        /// <summary>
        /// The result
        /// </summary>
        private Result<LocationModel> result = null;

        /// <summary>
        /// The module name
        /// </summary>
        private const string ModuleName = "Location";
        #endregion

        #region constructor 
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationService"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public LocationService(string connection)
        {
            iLocationRepository = new LocationRepository(connection);
        }
        #endregion

        #region service

        #region add update location
        /// <summary>
        /// Adds/update the Location
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> AddUpdateLocation(LocationModel model)
        {
            try
            {
                response = new Response();
                LocationModel location = await iLocationRepository.AddUpdateLocation(model);

                response.Status = location.ResponseCode > 0;
                if (location.ResponseCode == -1)
                {
                    response.Message = ResponseMessage.LocationError_LocationName_Exists;
                }
                else if (location.ResponseCode == -2)
                {
                    response.Message = ResponseMessage.LocationError_LocationIdUsed;
                }
                else
                {
                    if (model.LocationId <= 0)
                    {
                        response.Message = string.Format(ResponseMessage.RecordAddSuccess, "Location");
                    }
                    else
                    {
                        response.Message = string.Format(ResponseMessage.RecordUpdateSuccess, "Location");
                    }

                    response.Data = location;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("LocationService", "AddUpdateLocation", ex);
            }
            return response;
        }
        #endregion

        #region list location
        /// <summary>
        /// List the Location
        /// </summary>
        /// <returns></returns>
        public async Task<List<LocationModel>> ListLocation(int LocationId)
        {
            List<LocationModel> Locations = new List<LocationModel>();
            try
            {
                Locations = await iLocationRepository.ListLocation(LocationId);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("LocationService", "ListLocation", ex);
            }
            return Locations;
        }

        #endregion

        #region get location list
        /// <summary>
        /// Gets the location list.
        /// </summary>
        /// <returns></returns>
        public async Task<List<LocationModel>> GetLocationList()
        {
            List<LocationModel> Locations = new List<LocationModel>();
            try
            {
                Locations = await iLocationRepository.GetLocationList();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("LocationService", "GetLocationList", ex);
            }
            return Locations;
        }
        #endregion

        #region ListLocationByWardId
        /// <summary>
        /// Lists the location by ward identifier.
        /// </summary>
        /// <param name="wardId">The ward identifier.</param>
        /// <returns></returns>
        public async Task<List<LocationModel>> ListLocationByWardId(int wardId)
        {
            List<LocationModel> Locations = new List<LocationModel>();
            try
            {
                Locations = await iLocationRepository.ListLocationByWardId(wardId);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("LocationService", "ListLocationByWardId", ex);
            }
            return Locations;
        }
        #endregion

        #region Location Transfer

        /// <summary>
        /// Locations the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> LocationTransfer(LocationModel model)
        {
            try
            {
                response = new Response();
                int result = await iLocationRepository.LocationTransfer(model);

                if (result > 0)
                {
                    response.Status = true;
                    response.Message = ResponseMessage.Location_Transfer_Success;
                }
                else
                {
                    response.Message = ResponseMessage.Location_Transfer_Failure;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("LocationService", "LocationTransfer", ex);
            }
            return response;
        }
        #endregion

        #endregion
    }
}
