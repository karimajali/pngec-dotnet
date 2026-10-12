namespace PNGEC.API.Helper
{
    public static class ResponseMessage
    {
        #region common
        /// <summary>
        /// The record add success
        /// </summary>
        public static string RecordAddSuccess = "{0} added successfully";

        /// <summary>
        /// The record update success
        /// </summary>
        public static string RecordUpdateSuccess = "{0} updated successfully";

        /// <summary>
        /// The record delete success
        /// </summary>
        public static string RecordDeleteSuccess = "{0} deleted successfully";

        /// <summary>
        /// Gets or sets the record add update success.
        /// </summary>
        /// <value>
        /// The record add update success.
        /// </value>
        public static string Record_Add_Update_Success { get; set; } = "Success";

        /// <summary>
        /// Gets or sets the requesr data not valid.
        /// </summary>
        /// <value>
        /// The requesr data not valid.
        /// </value>
        public static string Invalid_Requests_Model { get; set; } = "Request model is not valid";

        /// <summary>
        /// Gets or sets the record not exist.
        /// </summary>
        /// <value>
        /// The record not exist.
        /// </value>
        public static string RecordNotExist { get; set; } = "Record Not Exist";

        /// <summary>
        /// Gets or sets something went wrong.
        /// </summary>
        /// <value>
        /// Something went wrong.
        /// </value>
        public static string SomethingWentWrong { get; set; } = "Something Went Wrong";

        /// <summary>
        /// Gets or sets the report generated success.
        /// </summary>
        /// <value>
        /// The report generated success.
        /// </value>
        public static string ReportGeneratedSuccess { get; set; } = "{0} report generated successfully";

        /// <summary>
        /// The data not found
        /// </summary>
        public static string DataNotFound = "Data not found";
        #endregion

        #region User
        /// <summary>
        /// Gets or sets the user error email address exists.
        /// </summary>
        /// <value>
        /// The user error email address exists.
        /// </value>
        public static string UserError_EmailAddress_Exists { get; set; } = "Email Address already exists";

        /// <summary>
        /// Gets or sets the user error contact number exists.
        /// </summary>
        /// <value>
        /// The user error contact number exists.
        /// </value>
        public static string UserError_ContactNumber_Exists { get; set; } = "Contact Number already exists";

        /// <summary>
        /// Gets or sets the success user logout.
        /// </summary>
        /// <value>
        /// The success user logout.
        /// </value>
        public static string SuccessUserLogout { get; set; } = "Logout successfully";

        /// <summary>
        /// Gets or sets the failure user logout.
        /// </summary>
        /// <value>
        /// The failure user logout.
        /// </value>
        public static string FailureUserLogout { get; set; } = "Error in logout";
        #endregion

        #region Device
        /// <summary>
        /// Gets or sets the device number exists.
        /// </summary>
        /// <value>
        /// The device error device number exists
        /// </value>
        public static string DeviceError_DeviceNumber_Exists { get; set; } = "Device Number already exists";
        
        /// <summary>
        /// Gets or sets the device already assigned.
        /// </summary>
        /// <value>
        /// The device error device already assigned to
        /// </value>
        public static string DeviceError_DeviceAssignedTo_Exists { get; set; } = "Device is already assigned to this user";

        /// <summary>
        /// Gets or sets the device name exists.
        /// </summary>
        /// <value>
        /// The device error device name exists
        /// </value>
        public static string DeviceError_DeviceName_Exists { get; set; } = "Device Name already exists";

        /// <summary>
        /// Gets or sets the geo structure updated.
        /// </summary>
        /// <value>
        /// The geo structure updated.
        /// </value>
        public static string Updated_successfully { get; set; } = "Updated successfully";
        
        /// <summary>
        /// Gets or sets the failed to update geo structure.
        /// </summary>
        /// <value>
        /// The failed to update geo structure.
        /// </value>
        public static string Not_updated_successfully { get; set; } = "Not updated successfully";
        #endregion

        #region Region
        /// <summary>
        /// Gets or sets the region name exists.
        /// </summary>
        /// <value>
        /// The region name exists.
        /// </value>
        public static string RegionError_RegionName_Exists { get; set; } = "Region Name already exists";

        /// <summary>
        /// Gets or sets the region error region identifier used.
        /// </summary>
        /// <value>
        /// The region error region identifier used.
        /// </value>
        public static string RegionError_RegionIdUsed { get; set; } = "Unable to make the selected region Inactive - One or more region have active registered voters";
        #endregion

        #region Province
        /// <summary>
        /// Gets or sets the Province name exists.
        /// </summary>
        /// <value>
        /// The Province name exists.
        /// </value>
        public static string ProvinceError_ProvinceName_Exists { get; set; } = "Province Name already exists";

        /// <summary>
        /// Gets or sets the ward error ward identifier used.
        /// </summary>
        /// <value>
        /// The ward error ward identifier used.
        /// </value>
        public static string ProvinceError_ProvinceIdUsed { get; set; } = "Unable to make the selected Province Inactive - One or more Province have active registered voters";
        #endregion

        #region District
        /// <summary>
        /// Gets or sets the Batch ID exists.
        /// </summary>
        /// <value>
        /// The Batch ID exists.
        /// </value>
        public static string DistrictError_BatchID_Exists { get; set; } = "Batch ID already exists";
        
        /// <summary>
        /// Gets or sets the District name exists.
        /// </summary>
        /// <value>
        /// The District name exists.
        /// </value>
        public static string DistrictError_DistrictName_Exists { get; set; } = "District Name already exists";

        /// <summary>
        /// Gets or sets the district error district identifier used.
        /// </summary>
        /// <value>
        /// The district error district identifier used.
        /// </value>
        public static string DistrictError_DistrictIdUsed { get; set; } = "Unable to make the selected district Inactive - One or more district have active registered voters";
        #endregion

        #region LLG
        /// <summary>
        /// Gets or sets the LLG name exists.
        /// </summary>
        /// <value>
        /// The LLG name exists.
        /// </value>
        public static string LLGError_LLGName_Exists { get; set; } = "LLG Name already exists";

        /// <summary>
        /// Gets or sets the LLG error LLG identifier used.
        /// </summary>
        /// <value>
        /// The LLG error LLG identifier used.
        /// </value>
        public static string LLGError_LLGId_Used { get; set; } = "Unable to make the selected llg Inactive - One or more llg have active registered voters";

        /// <summary>
        /// Gets or sets the LLG transfer success.
        /// </summary>
        /// <value>
        /// The LLG transfer success.
        /// </value>
        public static string LLG_Transfer_Success { get; set; } = "LLG transfer successfully";

        /// <summary>
        /// Gets or sets the LLG transfer failure.
        /// </summary>
        /// <value>
        /// The LLG transfer failure.
        /// </value>
        public static string LLG_Transfer_Failure { get; set; } = "Get some error while LLG transfer";
        #endregion

        #region MKALLG
        /// <summary>
        /// Gets or sets the LLG name exists.
        /// </summary>
        /// <value>
        /// The LLG name exists.
        /// </value>
        public static string LLGError_MKALLGName_Exists { get; set; } = "LLG Name already exists";

        /// <summary>
        /// Gets or sets the LLG error LLG identifier used.
        /// </summary>
        /// <value>
        /// The LLG error LLG identifier used.
        /// </value>
        public static string LLGError_MKALLGId_Used { get; set; } = "Unable to make the selected llg Inactive - One or more llg have active registered voters";

        /// <summary>
        /// Gets or sets the LLG transfer success.
        /// </summary>
        /// <value>
        /// The LLG transfer success.
        /// </value>
        public static string MKALLG_Transfer_Success { get; set; } = "LLG transfer successfully";

        /// <summary>
        /// Gets or sets the LLG transfer failure.
        /// </summary>
        /// <value>
        /// The LLG transfer failure.
        /// </value>
        public static string MKALLG_Transfer_Failure { get; set; } = "Get some error while LLG transfer";
        #endregion

        #region Ward
        /// <summary>
        /// Gets or sets the Ward number exists.
        /// </summary>
        /// <value>
        /// The Ward number exists.
        /// </value>
        public static string WardError_WardNumber_Exists { get; set; } = "Ward Number already exists";

        /// <summary>
        /// Gets or sets the Ward name exists.
        /// </summary>
        /// <value>
        /// The ward name exists.
        /// </value>
        public static string WardError_WardName_Exists { get; set; } = "Ward Name already exists";

        /// <summary>
        /// Gets or sets the ward error ward identifier used.
        /// </summary>
        /// <value>
        /// The ward error ward identifier used.
        /// </value>
        public static string WardError_WardIdUsed { get; set; } = "Unable to make the selected ward Inactive - One or more ward have active registered voters";

        /// <summary>
        /// Gets or sets the ward transfer success.
        /// </summary>
        /// <value>
        /// The ward transfer success.
        /// </value>
        public static string Ward_Transfer_Success { get; set; } = "Ward transfer successfully";

        /// <summary>
        /// Gets or sets the ward transfer failure.
        /// </summary>
        /// <value>
        /// The ward transfer failure.
        /// </value>
        public static string Ward_Transfer_Failure { get; set; } = "Get some error while ward transfer";
        #endregion

        #region MKAWard
        /// <summary>
        /// Gets or sets the Ward number exists.
        /// </summary>
        /// <value>
        /// The Ward number exists.
        /// </value>
        public static string WardError_MKAWardNumber_Exists { get; set; } = "Ward Number already exists";

        /// <summary>
        /// Gets or sets the Ward name exists.
        /// </summary>
        /// <value>
        /// The ward name exists.
        /// </value>
        public static string WardError_MKAWardName_Exists { get; set; } = "Ward Name already exists";

        /// <summary>
        /// Gets or sets the ward error ward identifier used.
        /// </summary>
        /// <value>
        /// The ward error ward identifier used.
        /// </value>
        public static string WardError_MKAWardIdUsed { get; set; } = "Unable to make the selected ward Inactive - One or more ward have active registered voters";

        /// <summary>
        /// Gets or sets the ward transfer success.
        /// </summary>
        /// <value>
        /// The ward transfer success.
        /// </value>
        public static string MKAWard_Transfer_Success { get; set; } = "Ward transfer successfully";

        /// <summary>
        /// Gets or sets the ward transfer failure.
        /// </summary>
        /// <value>
        /// The ward transfer failure.
        /// </value>
        public static string MKAWard_Transfer_Failure { get; set; } = "Get some error while ward transfer";
        #endregion

        #region Location
        /// <summary>
        /// Gets or sets the Location name exists.
        /// </summary>
        /// <value>
        /// The Location name exists.
        /// </value>
        public static string LocationError_LocationName_Exists { get; set; } = "Location Name already exists";

        /// <summary>
        /// Gets or sets the location error location identifier used.
        /// </summary>
        /// <value>
        /// The location error location identifier used.
        /// </value>
        public static string LocationError_LocationIdUsed { get; set; } = "Unable to make the selected location Inactive - One or more location have active registered voters";

        /// <summary>
        /// Gets or sets the location transfer success.
        /// </summary>
        /// <value>
        /// The location transfer success.
        /// </value>
        public static string Location_Transfer_Success { get; set; } = "Location transfer successfully";

        /// <summary>
        /// Gets or sets the location transfer failure.
        /// </summary>
        /// <value>
        /// The location transfer failure.
        /// </value>
        public static string Location_Transfer_Failure { get; set; } = "Get some error while location transfer";
        #endregion

        #region MKALocation
        /// <summary>
        /// Gets or sets the Location name exists.
        /// </summary>
        /// <value>
        /// The Location name exists.
        /// </value>
        public static string LocationError_MKALocationName_Exists { get; set; } = "Location Name already exists";

        /// <summary>
        /// Gets or sets the location error location identifier used.
        /// </summary>
        /// <value>
        /// The location error location identifier used.
        /// </value>
        public static string LocationError_MKALocationIdUsed { get; set; } = "Unable to make the selected location Inactive - One or more location have active registered voters";

        /// <summary>
        /// Gets or sets the location transfer success.
        /// </summary>
        /// <value>
        /// The location transfer success.
        /// </value>
        public static string MKALocation_Transfer_Success { get; set; } = "Location transfer successfully";

        /// <summary>
        /// Gets or sets the location transfer failure.
        /// </summary>
        /// <value>
        /// The location transfer failure.
        /// </value>
        public static string MKALocation_Transfer_Failure { get; set; } = "Get some error while location transfer";
        #endregion

        #region EnrolmentForm
        /// <summary>
        /// Gets or sets the range overlapping.
        /// </summary>
        /// <value>
        /// The Start/end range overlapping 
        /// </value>
        public static string EnrolmentFormError_Range_Overlapping { get; set; } = "Start or End range is overlapping";

        /// <summary>
        /// Gets or sets the enrolment form error validation.
        /// </summary>
        /// <value>
        /// The enrolment form error validation.
        /// </value>
        public static string EnrolmentFormError_Validation { get; set; } = "This enrolment number is already assigned";

        /// <summary>
        /// Gets or sets the enrolment form not exist error validation.
        /// </summary>
        /// <value>
        /// The enrolment form not exist error validation.
        /// </value>
        public static string EnrolmentFormNotExistError_Validation { get; set; } = "This enrolment number does not exist or wrong";

        /// <summary>
        /// Gets or sets the location not match error validation.
        /// </summary>
        /// <value>
        /// The location not match error validation.
        /// </value>
        public static string LocationNotMatchError_Validation { get; set; } = "This voters location is not matched. Please update the location from rejected list and sync again.{0}-{1}";

        /// <summary>
        /// Gets or sets the Elector not Exists error.
        /// </summary>
        /// <value>
        /// The location not match error validation.
        /// </value>
        public static string ElectorNotExist_Validation { get; set; } = "This voter is not present in Staging Database.";

        /// <summary>
        /// Gets or sets the Elector Other data not match error validation.
        /// </summary>
        /// <value>
        /// The location not match error validation.
        /// </value>
        public static string ElectorOtherDataMismatch_Validation { get; set; } = "This voter details are not matched.";

        /// <summary>
        /// Gets or sets the enrolment form error same batch record exist.
        /// </summary>
        /// <value>
        /// The enrolment form error same batch record exist.
        /// </value>
        public static string EnrolmentFormError_Same_BatchRecordExist { get; set; } = "Record is already exist with same batch";
        #endregion

        #region Occupation
        /// <summary>
        /// Gets or sets the occupation already exist.
        /// </summary>
        /// <value>
        /// The occupation already exist.
        /// </value>
        public static string OccupationAlreadyExist{ get; set; } = "Occupation already exists";
        #endregion

        #region MKADistrict
        /// <summary>
        /// Gets or sets the region name exists.
        /// </summary>
        /// <value>
        /// The region name exists.
        /// </value>
        public static string MKADistrictError_DistrictName_Exists { get; set; } = "District Name already exists";

        /// <summary>
        /// Gets or sets the region error region identifier used.
        /// </summary>
        /// <value>
        /// The region error region identifier used.
        /// </value>
        public static string MKADistrictError_DistrictIdUsed { get; set; } = "Unable to make the selected district Inactive - One or more district have active registered voters";
        #endregion

        #region MKAClan
        /// <summary>
        /// Gets or sets the region name exists.
        /// </summary>
        /// <value>
        /// The region name exists.
        /// </value>
        public static string MKAClanError_ClanName_Exists { get; set; } = "Clan Name already exists";

        /// <summary>
        /// Gets or sets the region error region identifier used.
        /// </summary>
        /// <value>
        /// The region error region identifier used.
        /// </value>
        public static string MKAClanError_ClanIdUsed { get; set; } = "Unable to Update clan details - One or more voters in used this clan";
        #endregion

        #region Clan
        /// <summary>
        /// Gets or sets the Clan name exists.
        /// </summary>
        /// <value>
        /// The Clan name exists.
        /// </value>
        public static string ClanError_ClanName_Exists { get; set; } = "Clan Name already exists";

        /// <summary>
        /// Gets or sets the Clan error Clan identifier used.
        /// </summary>
        /// <value>
        /// The Clan error Clan identifier used.
        /// </value>
        public static string ClanError_ClanIdUsed { get; set; } = "Unable to make the selected Clan Inactive - One or more Clan have active registered voters";

        /// <summary>
        /// Gets or sets the Clan transfer success.
        /// </summary>
        /// <value>
        /// The Clan transfer success.
        /// </value>
        public static string Clan_Transfer_Success { get; set; } = "Clan transfer successfully";

        /// <summary>
        /// Gets or sets the Clan transfer failure.
        /// </summary>
        /// <value>
        /// The Clan transfer failure.
        /// </value>
        public static string Clan_Transfer_Failure { get; set; } = "Get some error while Clan transfer";
        #endregion

        #region Writ
        /// <summary>
        /// Gets or sets the Writ name exists.
        /// </summary>
        /// <value>
        /// The Writ name exists.
        /// </value>
        public static string WritError_WritName_Exists { get; set; } = "Writ Name already exists";

        /// <summary>
        /// Gets or sets the Writ error Writ identifier used.
        /// </summary>
        /// <value>
        /// The Writ error Writ identifier used.
        /// </value>
        public static string WritError_WritIdUsed { get; set; } = "Unable to make the selected Writ Inactive - One or more Writ have active registered voters";

        /// <summary>
        /// Gets or sets the Writ transfer success.
        /// </summary>
        /// <value>
        /// The Writ transfer success.
        /// </value>
        public static string Writ_Transfer_Success { get; set; } = "Writ transfer successfully";

        /// <summary>
        /// Gets or sets the Writ transfer failure.
        /// </summary>
        /// <value>
        /// The Writ transfer failure.
        /// </value>
        public static string Writ_Transfer_Failure { get; set; } = "Get some error while Writ transfer";
        #endregion
    }
}
