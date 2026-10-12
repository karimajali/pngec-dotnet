using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Helper;
using PNGEC.API.Repository.Location;
using PNGEC.API.Repository.MKALocation;

namespace PNGEC.API.Service.MKALocation
{
    public class MKALocationService : IMKALocationService
    {
        #region variable declaration
        /// <summary>
        /// The i Location repository
        /// </summary>
        private readonly IMKALocationRepository iMKALocationRepository;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The lstresponse
        /// </summary>
        private Response<MKALocationModel> lstresponse = null;

        /// <summary>
        /// The result
        /// </summary>
        private Result<MKALocationModel> result = null;

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
        public MKALocationService(string connection)
        {
            iMKALocationRepository = new MKALocationRepository(connection);
        }
        #endregion

        #region service

        #region add update location
        /// <summary>
        /// Adds/update the Location
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> AddUpdateMKALocation(MKALocationModel model)
        {
            try
            {
                response = new Response();
                MKALocationModel location = await iMKALocationRepository.AddUpdateMKALocation(model);

                response.Status = location.ResponseCode > 0;
                if (location.ResponseCode == -1)
                {
                    response.Message = ResponseMessage.LocationError_MKALocationName_Exists;
                }
                else if (location.ResponseCode == -2)
                {
                    response.Message = ResponseMessage.LocationError_MKALocationIdUsed;
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
        public async Task<Response> ListMKALocation(int LocationId)
        {
            var res = new Response();
            try
            {

                var locations = await iMKALocationRepository.ListMKALocation(LocationId);
                if (locations != null && locations.ListItems != null && locations.ListItems.Count() > 0)
                {
                    res.Data = locations.ListItems;
                    res.Message = "Data Get Successfully";
                    res.Status = true;
                }
                else
                {
                    res.Data = null;
                    res.Message = "Data Does Not Exist";
                    res.Status = false;
                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("LocationService", "ListLocation", ex);
            }
            return res;
        }

        #endregion

        #region get location list
        /// <summary>
        /// Gets the location list.
        /// </summary>
        /// <returns></returns>
        public async Task<Response> GetMKALocationList()
        {
             var res = new Response();
            try
            {
               var locations = await iMKALocationRepository.GetMKALocationList();
                if (locations != null && locations.ListItems != null && locations.ListItems.Count() > 0)
                {
                    res.Data = locations.ListItems;
                    res.Message = "Data Get Successfully";
                    res.Status = true;
                }
                else
                {
                    res.Data = null;
                    res.Message = "Data Does Not Exist";
                    res.Status = false;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("LocationService", "GetLocationList", ex);
            }
            return res;
        }
        #endregion

        #region ListLocationByWardId
        /// <summary>
        /// Lists the location by ward identifier.
        /// </summary>
        /// <param name="wardId">The ward identifier.</param>
        /// <returns></returns>
        public async Task<Response> ListMKALocationByWardId(int wardId)
        {
            var res = new Response();
            try
            {
                var locations = await iMKALocationRepository.ListMKALocationByWardId(wardId);
                if (locations != null && locations.ListItems != null && locations.ListItems.Count() > 0)
                {
                    res.Data = locations.ListItems;
                    res.Message = "Data Get Successfully";
                    res.Status = true;
                }
                else
                {
                    res.Data = null;
                    res.Message = "Data Does Not Exist";
                    res.Status = false;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("LocationService", "ListLocationByWardId", ex);
            }
            return res;
        }
        #endregion

        #region Location Transfer

        /// <summary>
        /// Locations the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> MKALocationTransfer(MKALocationModel model)
        {
            try
            {
                response = new Response();
                int result = await iMKALocationRepository.MKALocationTransfer(model);

                if (result > 0)
                {
                    response.Status = true;
                    response.Message = ResponseMessage.MKALocation_Transfer_Success;
                }
                else
                {
                    response.Message = ResponseMessage.MKALocation_Transfer_Failure;
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
