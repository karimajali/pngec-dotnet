using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PNGEC.API.Domain;
using PNGEC.API.Domain.GeoStructure;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Setting;
using PNGEC.API.Helper;
using PNGEC.API.Repository.GeoStructure;
using PNGEC.API.Repository.Setting;

namespace PNGEC.API.Service.GeoStructure
{
    public class GeoStructureService : IGeoStructureService
    {
        #region variable declaration
        /// <summary>
        /// The i GeoStructure repository
        /// </summary>
        private readonly IGeoStructureRepository iGeoStructureRepository;

        /// <summary>
        /// The i setting repository
        /// </summary>
        private readonly ISettingRepository iSettingRepository;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The lstresponse
        /// </summary>
        private Response<GeoStructureModel> lstresponse = null;

        /// <summary>
        /// The result
        /// </summary>
        private Result<GeoStructureModel> result = null;

        /// <summary>
        /// The module name
        /// </summary>
        private const string ModuleName = "GeoStructure";
        #endregion

        #region constructor 
        /// <summary>
        /// Initializes a new instance of the <see cref="GeoStructureService"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public GeoStructureService(string connection)
        {
            iGeoStructureRepository = new GeoStructureRepository(connection);
            iSettingRepository = new SettingRepository(connection);
        }
        #endregion

        #region service

        #region Add Update GeoStructure
        /// <summary>
        /// Adds/update the GeoStructure
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> AddUpdateGeoStructure(GeoStructureModel model)
        {
            try
            {
                response = new Response();
                GeoStructureModel geoStructureModel = await iGeoStructureRepository.AddUpdateGeoStructure(model);
                if (geoStructureModel != null)
                {
                    response.Status = geoStructureModel.ResponseCode > 0;

                    response.Message = ResponseMessage.Record_Add_Update_Success;
                    response.Data = geoStructureModel;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("GeoStructureService", "AddUpdateGeoStructure", ex);
            }
            return response;
        }
        #endregion

        #region get GeoStructure list
        /// <summary>
        /// Gets the GeoStructure list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response<GeoStructureModel>> GetGeoStructureList(PageList model)
        {
            lstresponse = new Response<GeoStructureModel>();

            try
            {
                result = await iGeoStructureRepository.GetGeoStructureList(model);

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
                ErrorLog.Write("GeoStructureService", "GetGeoStructureList", ex);
            }

            return lstresponse;
        }
        #endregion

        #region GetGeoList
        /// <summary>
        /// Gets the geo list.
        /// </summary>
        /// <returns>
        /// return geo list
        /// </returns>
        public async Task<Response> GetGeoList(int userId)
        {
            Response response = new Response();

            try
            {
                var geolist = await iGeoStructureRepository.GetGeoList(userId);

                if (geolist != null)
                {
                    var setting = await iSettingRepository.GetSetting();
                    if (setting != null)
                    {
                        geolist.Version = setting.GeoVersion;
                    }

                    response.Data = geolist;
                    response.Status = true;
                }
                else
                {
                    response.Message = ResponseMessage.RecordNotExist;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("GeoStructureService", "GetGeoList", ex);
            }

            return response;
        }

        /// <summary>
        /// Gets the geo list and create file.
        /// </summary>
        public async Task GetGeoListAndCreateFile(int actionBy)
        {
            try
            {
                var geolist = await iGeoStructureRepository.GetGeoList(1);

                if (geolist != null)
                {
                    var setting = await iSettingRepository.UpdateSettingGeoVersion(new SettingModel { ActionBy = actionBy });
                    if (setting != null)
                    {
                        geolist.Version = setting.GeoVersion;
                    }

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.GeoFolderPath);

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    using (TextWriter writer = System.IO.File.CreateText($"{filePath}/geolist.json"))
                    {
                        var serializer = new JsonSerializer();
                        serializer.Formatting = Formatting.Indented;
                        serializer.NullValueHandling = NullValueHandling.Ignore;
                        serializer.Serialize(writer, geolist);
                    }

                    CommonFunction.CreateZipFile($"{filePath}\\geolist_{geolist.Version}.zip", Directory.GetFiles(filePath,"*.json"));
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write("GeoStructureService", "GetGeoListAndCreateFile", ex);
            }
        }
        #endregion

        #endregion
    }
}
