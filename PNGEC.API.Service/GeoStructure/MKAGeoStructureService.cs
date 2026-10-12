using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PNGEC.API.Domain;
using PNGEC.API.Domain.GeoStructure;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Domain.Setting;
using PNGEC.API.Helper;
using PNGEC.API.Repository.GeoStructure;
using PNGEC.API.Repository.MKAGeoStructure;
using PNGEC.API.Repository.Setting;

namespace PNGEC.API.Service.GeoStructure
{
    public class MKAGeoStructureService : IMKAGeoStructureService
    {
        #region variable declaration
        /// <summary>
        /// The i GeoStructure repository
        /// </summary>
        private readonly IMKAGeoStructureRepository iMKAGeoStructureRepository;

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
        private Response<MKAGeoStructureModel> lstresponse = null;

        /// <summary>
        /// The result
        /// </summary>
        private Result<MKAGeoStructureModel> result = null;

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
        public MKAGeoStructureService(string connection)
        {
            iMKAGeoStructureRepository = new MKAGeoStructureRepository(connection);
            iSettingRepository = new SettingRepository(connection);
        }
        #endregion
        #region service

        
        #region GetGeoList
        /// <summary>
        /// Gets the geo list.
        /// </summary>
        /// <returns>
        /// return geo list
        /// </returns>
        public async Task<Response> GetMKAGeoList(int userId)
        {
            Response response = new Response();

            try
            {
                var geolist = await iMKAGeoStructureRepository.GetMKAGeoList(userId);

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
                ErrorLog.Write("MKAGeoStructureService", "GetMKAGeoList", ex);
            }

            return response;
        }
        #region get MKAGeoStructure list
        /// <summary>
        /// Gets the GeoStructure list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response<MKAGeoStructureModel>> GetMKAGeoStructureList(PageList model)
        {
            lstresponse = new Response<MKAGeoStructureModel>();

            try
            {
                result = await iMKAGeoStructureRepository.GetMKAGeoStructureList(model);

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
        /// <summary>
        /// Gets the geo list and create file.
        /// </summary>
        public async Task GetMKAGeoListAndCreateFile(int actionBy)
        {
            try
            {
                var geolist = await iMKAGeoStructureRepository.GetMKAGeoList(1);

                if (geolist != null)
                {
                    var setting = await iSettingRepository.UpdateSettingMKAGeoVersion(new SettingModel { ActionBy = actionBy });
                    if (setting != null)
                    {
                        geolist.Version = setting.GeoVersion;
                    }

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.MKAGeoFolderPath);

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    using (TextWriter writer = System.IO.File.CreateText($"{filePath}/mkageolist.json"))
                    {
                        var serializer = new JsonSerializer();
                        serializer.Formatting = Formatting.Indented;
                        serializer.NullValueHandling = NullValueHandling.Ignore;
                        serializer.Serialize(writer, geolist);
                    }

                    CommonFunction.CreateZipFile($"{filePath}\\mkageolist_{geolist.Version}.zip", Directory.GetFiles(filePath, "*.json"));
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write("MKAGeoStructureService", "GetMKAGeoListAndCreateFile", ex);
            }
        }
        #endregion
        #endregion
    }
}
