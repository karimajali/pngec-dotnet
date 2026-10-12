namespace PNGEC.API.Helper
{
    public static class StoredProcedure
    {
        #region User
        /// <summary>
        /// Gets or sets the add update user.
        /// </summary>
        /// <value>
        /// The add update user.
        /// </value>
        public static string Add_Update_User { get; set; } = "[user].[sp_AddUpdateUser]";

        /// <summary>
        /// Gets or sets the get all user.
        /// </summary>
        /// <value>
        /// The get all user.
        /// </value>
        public static string Get_All_User_List { get; set; } = "[user].[sp_GetAllUser]";

        /// <summary>
        /// Gets or sets the update user status.
        /// </summary>
        /// <value>
        /// The update user status.
        /// </value>
        public static string Update_User_Status { get; set; } = "[user].[sp_UpdateUserStatus]";

        /// <summary>
        /// Gets or sets the user login.
        /// </summary>
        /// <value>
        /// The user login.
        /// </value>
        public static string User_Login { get; set; } = "[user].[sp_UserLogin]";

        /// <summary>
        /// Gets or sets the get super admin details.
        /// </summary>
        /// <value>
        /// The get super admin details.
        /// </value>
        public static string Get_SuperAdmin_Details { get; set; } = "[user].[sp_GetSuperAdminDetails]";

        /// <summary>
        /// Gets or sets the reset password.
        /// </summary>
        /// <value>
        /// The reset password.
        /// </value>
        public static string Reset_Password { get; set; } = "[user].[sp_ResetPassword]";

        #endregion

        #region Device
        /// <summary>
        /// Gets or sets the add update Device.
        /// </summary>
        /// <value>
        /// The add update Device.
        /// </value>
        public static string Add_Update_Device { get; set; } = "[device].[sp_AddUpdateDevice]";

        /// <summary>
        /// Gets or sets the get all device.
        /// </summary>
        /// <value>
        /// The get all device.
        /// </value>
        public static string Get_All_Device_List { get; set; } = "[device].[sp_GetAllDevice]";

        /// <summary>
        /// Gets or sets the update device status.
        /// </summary>
        /// <value>
        /// The update device status.
        /// </value>
        public static string Update_Device_Status { get; set; } = "[device].[sp_UpdateDeviceStatus]";

        #endregion

        #region GeoStructure
        /// <summary>
        /// Gets or sets the add update GeoStructure.
        /// </summary>
        /// <value>
        /// The add update GeoStructure.
        /// </value>
        public static string Add_Update_GeoStructure { get; set; } = "sp_AddUpdateGeoStructure";

        /// <summary>
        /// Gets or sets the get all geo structure.
        /// </summary>
        /// <value>
        /// The get all geo structure.
        /// </value>
        public static string Get_All_GeoStructure_List { get; set; } = "sp_GetAllGeoStructure";
        /// <summary>
        /// Gets or sets the get all geo structure.
        /// </summary>
        /// <value>
        /// The get all geo structure.
        /// </value>
        public static string Get_All_GeoMKAStructure_List { get; set; } = "sp_GetAllMKAGeoStructure";


        /// <summary>
        /// Gets or sets the get geo list.
        /// </summary>
        /// <value>
        /// The get geo list.
        /// </value>
        public static string Get_Geo_List { get; set; } = "[geo].[sp_GeoLists]";
        /// <summary>
        /// Gets or sets the get  mka geo list.
        /// </summary>
        /// <value>
        /// The get geo list.
        /// </value>
        public static string Get_MKAGeo_List { get; set; } = "[geo].[sp_MKAGeoLists]";
        /// <summary>
        /// Gets or sets the get district list.
        /// </summary>
        /// <value>
        /// The get district list.
        /// </value>
        public static string GetMKALLGList { get; set; } = "[geo].[sp_GetMKALLGList]";

        #endregion

        #region Role
        /// <summary>
        /// Gets or sets the add update Role.
        /// </summary>
        /// <value>
        /// The add update Role.
        /// </value>
        public static string Add_Update_Role { get; set; } = "sp_AddUpdateRole";

        /// <summary>
        /// Gets or sets the get all Role.
        /// </summary>
        /// <value>
        /// The get all Role.
        /// </value>
        public static string Get_All_Role_List { get; set; } = "sp_GetAllRole";
        #endregion

        #region Region
        /// <summary>
        /// Gets or sets the add update Region.
        /// </summary>
        /// <value>
        /// The add update Region.
        /// </value>
        public static string Add_Update_Region { get; set; } = "[geo].[sp_AddUpdateRegion]";
        /// <summary>
        /// Gets or sets the list Region.
        /// </summary>
        /// <value>
        /// The list Region.
        /// </value>
        public static string List_Region { get; set; } = "[geo].[sp_ListRegion]";

        #endregion
    
        /// <summary>
        /// Gets or sets the add update GeoStructure.
        /// </summary>
        /// <value>
        /// The add update GeoStructure.
        /// </value>
        public static string Add_Update_MKAGeoStructure { get; set; } = "sp_AddUpdateMKAGeoStructure";
       
        #region Province
        /// <summary>
        /// Gets or sets the add update Province.
        /// </summary>
        /// <value>
        /// The add update Province.
        /// </value>
        public static string Add_Update_Province { get; set; } = "[geo].[sp_AddUpdateProvince]";
        /// <summary>
        /// Gets or sets the list Province.
        /// </summary>
        /// <value>
        /// The list Province.
        /// </value>
        public static string List_Province { get; set; } = "[geo].[sp_ListProvince]";
        #endregion

        #region District
        /// <summary>
        /// Gets or sets the add update District.
        /// </summary>
        /// <value>
        /// The add update District.
        /// </value>
        public static string Add_Update_District { get; set; } = "[geo].[sp_AddUpdateDistrict]";
        /// <summary>
        /// Gets or sets the list District.
        /// </summary>
        /// <value>
        /// The list District.
        /// </value>
        public static string List_District { get; set; } = "[geo].[sp_ListDistrict]";
        #endregion

        #region MKADistrict
        /// <summary>
        /// Gets or sets the add update MKADistrict.
        /// </summary>
        /// <value>
        /// The add update MKADistrict.
        /// </value>
        public static string Add_Update_MKADistrict { get; set; } = "[geo].[sp_AddUpdateMKADistrict]";
        /// <summary>
        /// Gets or sets the list MKADistrict.
        /// </summary>
        /// <value>
        /// The list MKADistrict.
        /// </value>
        public static string List_MKADistrict { get; set; } = "[geo].[sp_ListMKADistrict]";
        /// <summary>
        /// Gets or sets the get district list.
        /// </summary>
        /// <value>
        /// The get district list.
        /// </value>
        public static string GetMKADistrictList { get; set; } = "[geo].[sp_GetMKADistrictList]";
        #endregion

        #region MKADistrict
        /// <summary>
        /// Gets or sets the add update MKAClan.
        /// </summary>
        /// <value>
        /// The add update MKAClan.
        /// </value>
        public static string Add_Update_MKAClan { get; set; } = "[geo].[sp_AddUpdateMKAClan]";
        /// <summary>
        /// Gets or sets the list MKAClan.
        /// </summary>
        /// <value>
        /// The list MKAClan.
        /// </value>
        public static string List_MKAClan { get; set; } = "[geo].[sp_ListMKAClan]";
        /// <summary>
        /// Gets or sets the get clan list.
        /// </summary>
        /// <value>
        /// The get clan list.
        /// </value>
        public static string GetMKAClanList { get; set; } = "[geo].[sp_GetMKAClanList]";
        #endregion

        #region LLG
        /// <summary>
        /// Gets or sets the add update LLG.
        /// </summary>
        /// <value>
        /// The add update LLG.
        /// </value>
        public static string Add_Update_LLG { get; set; } = "[geo].[sp_AddUpdateLLG]";
        /// <summary>
        /// Gets or sets the list LLG.
        /// </summary>
        /// <value>
        /// The list LLG.
        /// </value>
        public static string List_LLG { get; set; } = "[geo].[sp_ListLLG]";

        /// <summary>
        /// Gets or sets the LLG transfer.
        /// </summary>
        /// <value>
        /// The LLG transfer.
        /// </value>
        public static string LLG_Transfer { get; set; } = "[geo].[sp_LLGTransfer]";
        #endregion

        #region MKALLG
        /// <summary>
        /// Gets or sets the add update LLG.
        /// </summary>
        /// <value>
        /// The add update MKALLG.
        /// </value>
        public static string Add_Update_MKALLG { get; set; } = "[geo].[sp_AddUpdateMKALLG]";
        /// <summary>
        /// Gets or sets the list LLG.
        /// </summary>
        /// <value>
        /// The list LLG.
        /// </value>
        public static string List_MKALLG { get; set; } = "[geo].[sp_ListMKALLG]";

        /// <summary>
        /// Gets or sets the LLG transfer.
        /// </summary>
        /// <value>
        /// The LLG transfer.
        /// </value>
        public static string MKALLG_Transfer { get; set; } = "[geo].[sp_MKALLGTransfer]";
        #endregion

        #region Ward
        /// <summary>
        /// Gets or sets the add update Ward.
        /// </summary>
        /// <value>
        /// The add update Ward.
        /// </value>
        public static string Add_Update_Ward { get; set; } = "[geo].[sp_AddUpdateWard]";

        /// <summary>
        /// Gets or sets the list Ward.
        /// </summary>
        /// <value>
        /// The list Ward.
        /// </value>
        public static string List_Ward { get; set; } = "[geo].[sp_ListWard]";

        /// <summary>
        /// Gets or sets the ward transfer.
        /// </summary>
        /// <value>
        /// The ward transfer.
        /// </value>
        public static string Ward_Transfer { get; set; } = "[geo].[sp_WardTransfer]";
        #endregion

        #region MKAWard
        /// <summary>
        /// Gets or sets the add update Ward.
        /// </summary>
        /// <value>
        /// The add update Ward.
        /// </value>
        public static string Add_Update_MKAWard { get; set; } = "[geo].[sp_AddUpdateMKAWard]";

        /// <summary>
        /// Gets or sets the list Ward.
        /// </summary>
        /// <value>
        /// The list Ward.
        /// </value>
        public static string List_MKAWard { get; set; } = "[geo].[sp_ListMKAWard]";

        /// <summary>
        /// Gets or sets the ward transfer.
        /// </summary>
        /// <value>
        /// The ward transfer.
        /// </value>
        public static string MKAWard_Transfer { get; set; } = "[geo].[sp_MKAWardTransfer]";
        #endregion

        #region Location
        /// <summary>
        /// Gets or sets the add update Location.
        /// </summary>
        /// <value>
        /// The add update Location.
        /// </value>
        public static string Add_Update_Location { get; set; } = "[geo].[sp_AddUpdateLocation]";

        /// <summary>
        /// Gets or sets the list Location.
        /// </summary>
        /// <value>
        /// The list Location.
        /// </value>
        public static string List_Location { get; set; } = "[geo].[sp_ListLocation]";

        /// <summary>
        /// Gets or sets the list location by ward identifier.
        /// </summary>
        /// <value>
        /// The list location by ward identifier.
        /// </value>
        public static string List_Location_By_WardId { get; set; } = "[geo].[sp_ListLocationByWardId]";

        /// <summary>
        /// Gets or sets the location transfer.
        /// </summary>
        /// <value>
        /// The location transfer.
        /// </value>
        public static string Location_Transfer { get; set; } = "[geo].[sp_LocationTransfer]";
        #endregion

        #region MKALocation
        /// <summary>
        /// Gets or sets the add update Location.
        /// </summary>
        /// <value>
        /// The add update Location.
        /// </value>
        public static string Add_Update_MKALocation { get; set; } = "[geo].[sp_AddUpdateMKALocation]";

        /// <summary>
        /// Gets or sets the list Location.
        /// </summary>
        /// <value>
        /// The list Location.
        /// </value>
        public static string List_MKALocation { get; set; } = "[geo].[sp_ListMKALocation]";

        /// <summary>
        /// Gets or sets the list location by ward identifier.
        /// </summary>
        /// <value>
        /// The list location by ward identifier.
        /// </value>
        public static string List_MKALocation_By_WardId { get; set; } = "[geo].[sp_ListMKALocationByWardId]";

        /// <summary>
        /// Gets or sets the location transfer.
        /// </summary>
        /// <value>
        /// The location transfer.
        /// </value>
        public static string MKALocation_Transfer { get; set; } = "[geo].[sp_MKALocationTransfer]";
        #endregion

        #region Mobile        
        /// <summary>
        /// Gets or sets the mobile user login.
        /// </summary>
        /// <value>
        /// The mobile user login.
        /// </value>
        public static string Mobile_User_Login { get; set; } = "[dbo].[sp_UserMobileLogin]";

        /// <summary>
        /// Gets or sets the mobile water list.
        /// </summary>
        /// <value>
        /// The mobile water list.
        /// </value>
        public static string GetVoterListMobile { get; set; } = "[dbo].[sp_GetVoterListMobile]";

        /// <summary>
        /// Gets or sets the download voter list mobile.
        /// </summary>
        /// <value>
        /// The download voter list mobile.
        /// </value>
        public static string DownloadVoterListMobile { get; set; } = "[dbo].[sp_DownloadVoterListMobile]";
        
        /// <summary>
        /// Gets or sets the get voter list by province.
        /// </summary>
        /// <value>
        /// The get voter list by province.
        /// </value>
        public static string GetVoterListByProvince { get; set; } = "[dbo].[sp_GetVoterByProvince]";

        /// <summary>
        /// Gets or sets the get duplicate voter list.
        /// </summary>
        /// <value>
        /// The get duplicate voter list.
        /// </value>
        public static string GetDuplicateVoterList { get; set; } = "[dbo].[sp_GetDuplicateVoterList]";

        /// <summary>
        /// Gets or sets the get tranferin voter with condition.
        /// </summary>
        /// <value>
        /// The get tranferin voter with condition.
        /// </value>
        public static string GetTranferinVoterWithCondition { get; set; } = "[dbo].[sp_GetTranferinVoterWithCondition]";

        /// <summary>
        /// Gets or sets the get rejected voter list mobile.
        /// </summary>
        /// <value>
        /// The get rejected voter list mobile.
        /// </value>
        public static string GetRejectedVoterListMobile { get; set; } = "[dbo].[sp_GetRejectedVoterListMobile]";

        /// <summary>
        /// Gets or sets the get transfer in voter list.
        /// </summary>
        /// <value>
        /// The get transfer in voter list.
        /// </value>
        public static string GetTransferInVoterList { get; set; } = "[dbo].[spS_GetTransferInVoterList]";

        /// <summary>
        /// Gets or sets the synchronize electors.
        /// </summary>
        /// <value>
        /// The synchronize electors.
        /// </value>
        public static string SyncElectors { get; set; } = "[dbo].[sp_SyncElectors]";

        /// <summary>
        /// Gets or sets the synchronize electors images.
        /// </summary>
        /// <value>
        /// The synchronize electors images.
        /// </value>
        public static string SyncElectorsImages { get; set; } = "[dbo].[sp_SyncElectorsImages]";

        /// <summary>
        /// Gets or sets the import bio data details.
        /// </summary>
        /// <value>
        /// The import bio data details.
        /// </value>
        public static string ImportBioDataDetails { get; set; } = "[dbo].[sp_ImportBioDataDetails]";

        /// <summary>
        /// Gets or sets the validate enrolment number.
        /// </summary>
        /// <value>
        /// The validate enrolment number.
        /// </value>
        public static string ValidateEnrolmentNumber { get; set; } = "[dbo].[sp_ValidateEnrolmentNumber]";

        /// <summary>
        /// Gets or sets the get geo list mobile.
        /// </summary>
        /// <value>
        /// The get geo list mobile.
        /// </value>
        public static string GetGeoListMobile { get; set; } = "[geo].[sp_GetGeoListMobile]";
        /// <summary>
        /// Gets or sets the get geo list mobile.
        /// </summary>
        /// <value>
        /// The get geo list mobile.
        /// </value>
        public static string GetMKAGeoListMobile { get; set; } = "[geo].[sp_GetMKAGeoListMobile]";

        /// <summary>
        /// Gets or sets the get occupation list mobile.
        /// </summary>
        /// <value>
        /// The get occupation list mobile.
        /// </value>
        public static string GetOccupationListMobile { get; set; } = "[sp_GetOccupationListMobile]";

        /// <summary>
        /// Gets or sets the get all locations mobile.
        /// </summary>
        /// <value>
        /// The get all locations mobile.
        /// </value>
        public static string GetAllLocationsMobile { get; set; } = "[geo].[spS_GetAllLocationsMobile]";


        /// <summary>
        /// Gets or sets the get active province list mobile.
        /// </summary>
        /// <value>
        /// The get active province list mobile.
        /// </value>
        public static string GetActiveProvinceListMobile { get; set; } = "[geo].[sp_GetActiveProvinceList]";

        /// <summary>
        /// Gets or sets the get locations by ids mobile.
        /// </summary>
        /// <value>
        /// The get locations by ids mobile.
        /// </value>
        public static string GetLocationsByIdsMobile { get; set; } = "[geo].[spS_GetLocationsByIdsMobile]";

        /// <summary>
        /// Gets or sets the logout mobile.
        /// </summary>
        /// <value>
        /// The logout mobile.
        /// </value>
        public static string LogoutMobile { get; set; } = "[dbo].[sp_LogoutMobile]";

        /// <summary>
        /// Gets or sets the update geo structure mobile.
        /// </summary>
        /// <value>
        /// The update geo structure mobile.
        /// </value>
        public static string UpdateUserGeoStructureMobile { get; set; } = "[dbo].[sp_UpdateUserGeoStructure]";


        /// <summary>
        /// Gets or sets the get device operator mobile.
        /// </summary>
        /// <value>
        /// The get device operator mobile.
        /// </value>
        public static string GetDeviceOperatorMobile { get; set; } = "[dbo].[sp_GetDeviceOperatorMobile]";

        /// <summary>
        /// Gets or sets the get PDF device operator mobile.
        /// </summary>
        /// <value>
        /// The get PDF device operator mobile.
        /// </value>
        public static string GetPDFDeviceOperatorMobile { get; set; } = "[dbo].[sp_GetPDFDeviceOperatorMobile]";

        /// <summary>
        /// Gets or sets the get status and dob discrepancy mobile.
        /// </summary>
        /// <value>
        /// The get status and dob discrepancy mobile.
        /// </value>
        public static string GetStatusAndDOBDiscrepancyMobile { get; set; } = "[dbo].[sp_GetStatusAndDOBDiscrepancyMobile]";

        /// <summary>
        /// Gets or sets the get preliminary voter list mobile.
        /// </summary>
        /// <value>
        /// The get preliminary voter list mobile.
        /// </value>
        public static string GetPreliminaryVoterListMobile { get; set; } = "[dbo].[sp_GetPreliminaryVoterListMobile]";

        /// <summary>
        /// Gets or sets the download voters file by province.
        /// </summary>
        /// <value>
        /// The download voters file by province.
        /// </value>
        public static string DownloadVotersFileByProvince { get; set; } = "[dbo].[sp_DownloadVoterByProvince]";

        /// <summary>
        /// Gets or sets the get synchronize status.
        /// </summary>
        /// <value>
        /// The get synchronize status.
        /// </value>
        public static string GetSyncStatus { get; set; } = "[dbo].[sp_GetSyncStatus]";
        #endregion

        #region File
        /// <summary>
        /// Gets or sets the get voter list.
        /// </summary>
        /// <value>
        /// The get voter list.
        /// </value>
        public static string Get_All_Voter_List { get; set; } = "[dbo].[sp_GetAllVoter]";
        /// <summary>
        /// Gets or sets the get voter list.
        /// </summary>
        /// <value>
        /// The get voter list.
        /// </value>
        public static string Get_All_MKAVoter_List { get; set; } = "[dbo].[sp_GetMKAAllVoter]";

        /// <summary>
        /// Gets or sets the get all voter transfer out list.
        /// </summary>
        /// <value>
        /// The get all voter transfer out list.
        /// </value>
        public static string Get_All_Voter_TransferOut_List { get; set; } = "[dbo].[sp_GetAllVoterTransferOut]";

        /// <summary>
        /// Gets or sets the get all voter duplicate list.
        /// </summary>
        /// <value>
        /// The get all voter duplicate list.
        /// </value>
        public static string Get_All_Voter_Duplicate_List { get; set; } = "[dbo].[sp_GetAllVoterDuplicate]";

        /// <summary>
        /// Gets or sets the get all ward status list.
        /// </summary>
        /// <value>
        /// The get all ward status list.
        /// </value>
        public static string Get_All_WardStatus_List { get; set; } = "[dbo].[sp_GetAllVoterWardStatus]";

        /// <summary>
        /// Gets or sets the get all data migration report list.
        /// </summary>
        /// <value>
        /// The get all data migration report list.
        /// </value>
        public static string GetAllDataMigrationReportList { get; set; } = "[dbo].[sp_GetAllDataMigrationReportList]";
        #endregion

        #region EnrolmentForm
        /// <summary>
        /// Gets or sets the add EnrolmentForm.
        /// </summary>
        /// <value>
        /// The add EnrolmentForm.
        /// </value>
        public static string Add_EnrolmentForm { get; set; } = "[enrolment].[sp_AddUpdateEnrolmentForm]";
        /// <summary>
        /// Gets or sets the add MKAEnrolmentForm.
        /// </summary>
        /// <value>
        /// The add MKAEnrolmentForm.
        /// </value>
        public static string Add_MKAEnrolmentForm { get; set; } = "[enrolment].[sp_AddUpdateMKAEnrolmentForm]";

        /// <summary>
        /// Gets or sets the get all EnrolmentForm.
        /// </summary>
        /// <value>
        /// The get all EnrolmentForm.
        /// </value>
        public static string Get_All_EnrolmentForm_List { get; set; } = "[enrolment].[sp_GetAllEnrolmentForm]";
        /// <summary>
        /// Gets or sets the get all MKAEnrolmentForm.
        /// </summary>
        /// <value>
        /// The get all MKAEnrolmentForm.
        /// </value>
        public static string Get_All_MKAEnrolmentForm_List { get; set; } = "[enrolment].[sp_GetAllMKAEnrolmentForm]";

        /// <summary>
        /// Gets or sets the get ward quality check data.
        /// </summary>
        /// <value>
        /// The get ward quality check data.
        /// </value>
        public static string GetWardQualityCheckData { get; set; } = "[dbo].[sp_GetWardQualityCheckData]";
        /// <summary>
        /// Gets or sets the get ward quality check data.
        /// </summary>
        /// <value>
        /// The get ward quality check data.
        /// </value>
        public static string GetMKAWardQualityCheckData { get; set; } = "[dbo].[sp_GetMKAWardQualityCheckData]";

        /// <summary>
        /// Deletes the EnrolmentForm.
        /// </summary>
        /// <value>
        /// Deletes the EnrolmentForm.
        /// </value>
        public static string Delete_EnrolmentForm { get; set; } = "[enrolment].[sp_DeleteEnrolmentForm]";
        /// <summary>
        /// Deletes the EnrolmentForm.
        /// </summary>
        /// <value>
        /// Deletes the EnrolmentForm.
        /// </value>
        public static string Delete_MKAEnrolmentForm { get; set; } = "[enrolment].[sp_DeleteMKAEnrolmentForm]";
        #endregion

        #region Data Migration
        /// <summary>
        /// Gets or sets the manage voter status.
        /// </summary>
        /// <value>
        /// The manage voter status.
        /// </value>
        public static string Manage_Voter_Status { get; set; } = "[dbo].[sp_ManageVoterStatus]";

        /// <summary>
        /// Gets or sets the manage voter status transferred in out.
        /// </summary>
        /// <value>
        /// The manage voter status transferred in out.
        /// </value>
        public static string Manage_VoterStatus_Transferred_InOut { get; set; } = "[dbo].[sp_ManageVoterStatusTransferredInOut]";

        /// <summary>
        /// Gets or sets the get voter audit list.
        /// </summary>
        /// <value>
        /// The get voter audit list.
        /// </value>
        public static string Get_Voter_Audit_List { get; set; } = "[dbo].[sp_GetVoterAuditList]";

        /// <summary>
        /// Gets or sets the update voter by ro.
        /// </summary>
        /// <value>
        /// The update voter by ro.
        /// </value>
        public static string Update_Voter_By_RO { get; set; } = "[dbo].[sp_UpdateVoterByRO]";

        /// <summary>
        /// Gets or sets the preliminary update voter by admin.
        /// </summary>
        /// <value>
        /// The preliminary update voter by admin.
        /// </value>
        public static string Preliminary_Update_Voter_By_Admin { get; set; } = "[dbo].[sp_PreliminaryUpdateVoterByAdmin]";

        /// <summary>
        /// Gets or sets the transfer voter by ro.
        /// </summary>
        /// <value>
        /// The transfer voter by ro.
        /// </value>
        public static string Transfer_Voter_By_RO { get; set; } = "[dbo].[sp_TransferVoterByRO]";

        #endregion

        #region Roll Stat
        /// <summary>
        /// Gets or sets the get all roll stat list.
        /// </summary>
        /// <value>
        /// The get all roll stat list.
        /// </value>
        public static string Get_All_RollStat_List { get; set; } = "[dbo].[sp_GetAllRollStatList]";

        /// <summary>
        /// Gets or sets the get all mka roll stat list.
        /// </summary>
        /// <value>
        /// The get all mka roll stat list.
        /// </value>
        public static string Get_All_MKA_RollStat_List { get; set; } = "[dbo].[sp_GetAllMKARollStatList]";

        /// <summary>
        /// Gets or sets the get latest roll stat.
        /// </summary>
        /// <value>
        /// The get latest roll stat.
        /// </value>
        public static string GetLatestRollStat { get; set; } = "[dbo].[sp_GetLatestRollStat]";
        #endregion

        #region Ward Roll Book
        /// <summary>
        /// Gets or sets the get export records list.
        /// </summary>
        /// <value>
        /// The get export records list.
        /// </value>
        public static string Get_Export_Records_List { get; set; } = "[dbo].[sp_GetExportRecordsList]";

        /// <summary>
        /// Gets or sets the get export ward roll status count records list.
        /// </summary>
        /// <value>
        /// The get export ward roll status count records list.
        /// </value>
        public static string Get_Export_WardRollStatusCount_Records_List { get; set; } = "[dbo].[sp_GetExportWardRollStatusCountRecordsList]";

        /// <summary>
        /// Gets or sets the get MKA export ward roll status count records list.
        /// </summary>
        public static string Get_MKAExport_WardRollStatusCount_Records_List { get; set; } = "[dbo].[sp_GetMKAExportWardRollStatusCountRecordsList]";        

        /// <summary>
        /// Gets or sets the get export records CSV list.
        /// </summary>
        /// <value>
        /// The get export records CSV list.
        /// </value>
        public static string Get_Export_Records_Csv_List { get; set; } = "[dbo].[sp_GetExportRecordsCsvList]";
        #endregion

        #region Document
        /// <summary>
        /// Gets or sets the get all document list.
        /// </summary>
        /// <value>
        /// The get all document list.
        /// </value>
        public static string Get_All_Document_List { get; set; } = "[dbo].[sp_GetAllDocument]";

        /// <summary>
        /// Gets or sets the add delete document.
        /// </summary>
        /// <value>
        /// The add delete document.
        /// </value>
        public static string Add_Delete_Document { get; set; } = "[dbo].[sp_AddDeleteDocument]";
        #endregion

        /// <summary>
        /// Gets or sets the get region list.
        /// </summary>
        /// <value>
        /// The get region list.
        /// </value>
        public static string GetRegionList { get; set; } = "[geo].[sp_GetRegionList]";

        /// <summary>
        /// Gets or sets the get province list.
        /// </summary>
        /// <value>
        /// The get province list.
        /// </value>
        public static string GetProvinceList { get; set; } = "[geo].[sp_GetProvinceList]";

        /// <summary>
        /// Gets or sets the get district list.
        /// </summary>
        /// <value>
        /// The get district list.
        /// </value>
        public static string GetDistrictList { get; set; } = "[geo].[sp_GetDistrictList]";

        /// <summary>
        /// Gets or sets the get LLG list.
        /// </summary>
        /// <value>
        /// The get LLG list.
        /// </value>
        public static string GetLLGList { get; set; } = "[geo].[sp_GetLLGList]";

        /// <summary>
        /// Gets or sets the get ward list.
        /// </summary>
        /// <value>
        /// The get ward list.
        /// </value>
        public static string GetWardList { get; set; } = "[geo].[sp_GetWardList]";

        /// <summary>
        /// Gets or sets the get ward list.
        /// </summary>
        /// <value>
        /// The get ward list.
        /// </value>
        public static string GetMKAWardList { get; set; } = "[geo].[sp_GetMKAWardList]";

        /// <summary>
        /// Gets or sets the get location list.
        /// </summary>
        /// <value>
        /// The get location list.
        /// </value>
        public static string GetLocationList { get; set; } = "[geo].[sp_GetLocationList]";

        /// <summary>
        /// Gets or sets the get location list.
        /// </summary>
        /// <value>
        /// The get location list.
        /// </value>
        public static string GetMKALocationList { get; set; } = "[geo].[sp_GetMKALocationList]";

        #region Schedule Notification
        /// <summary>
        /// Gets or sets the schedule summary staging.
        /// </summary>
        /// <value>
        /// The schedule summary staging.
        /// </value>
        public static string Schedule_Summary_Staging { get; set; } = "[dbo].[sp_ScheduleSummaryStaging]";

        /// <summary>
        /// Gets or sets the schedule summary preliminary.
        /// </summary>
        /// <value>
        /// The schedule summary preliminary.
        /// </value>
        public static string Schedule_Summary_Preliminary { get; set; } = "[dbo].[sp_ScheduleSummaryPreliminary]";

        /// <summary>
        /// Gets or sets the schedule summary final.
        /// </summary>
        /// <value>
        /// The schedule summary final.
        /// </value>
        public static string Schedule_Summary_Final { get; set; } = "[dbo].[sp_ScheduleSummaryFinal]";

        /// <summary>
        /// Gets or sets the schedule summary rejected.
        /// </summary>
        /// <value>
        /// The schedule summary rejected.
        /// </value>
        public static string Schedule_Summary_Rejected { get; set; } = "[dbo].[sp_ScheduleSummaryForRejected]";

        /// <summary>
        /// Gets or sets the schedule summary rejected by rd.
        /// </summary>
        /// <value>
        /// The schedule summary rejected by rd.
        /// </value>
        public static string Schedule_Summary_RejectedByRD { get; set; } = "[dbo].[sp_ScheduleSummaryForRejectedByRD]";
        #endregion

        #region AugmentedRollStat
        /// <summary>
        /// Gets or sets the get all augmented roll stat list.
        /// </summary>
        /// <value>
        /// The get all augmented roll stat list.
        /// </value>
        public static string Get_All_Augmented_Roll_Stat_List { get; set; } = "[dbo].[sp_GetAllAugmentedRollStatList]";

        /// <summary>
        /// Gets or sets the add update augmented roll stat.
        /// </summary>
        /// <value>
        /// The add update augmented roll stat.
        /// </value>
        public static string Add_Update_AugmentedRollStat { get; set; } = "[dbo].[sp_AddUpdateAugmentedRollStat]";
        #endregion

        #region Kupiano Ward Roll
        /// <summary>
        /// Gets or sets the get kupiano ward roll list.
        /// </summary>
        /// <value>
        /// The get kupiano ward roll list.
        /// </value>
        public static string Get_Kupiano_Ward_Roll_List { get; set; } = "[dbo].[sp_GetKupianoWardRollList]";
        #endregion

        /// <summary>
        /// Gets or sets the get updated voter list.
        /// </summary>
        /// <value>
        /// The get updated voter list.
        /// </value>
        public static string GetUpdatedVoterList { get; set; } = "[dbo].[spS_GeUpdatedVoterList]";

        #region Report
        /// <summary>
        /// Gets or sets the get ward status report.
        /// </summary>
        /// <value>
        /// The get ward status report.
        /// </value>
        public static string GetWardStatusReport { get; set; } = "[dbo].[sp_GetWardStatusReport]";

        /// <summary>
        /// Gets or sets the get wards number of form11.
        /// </summary>
        /// <value>
        /// The get wards number of form11.
        /// </value>
        public static string GetWardsNumberOfForm11 { get; set; } = "[dbo].[sp_GetWardsNumberOfForm11]";
        /// <summary>
        /// Gets or sets the get wards number of form11.
        /// </summary>
        /// <value>
        /// The get wards number of form11.
        /// </value>
        public static string GetWardsNumberOfMKAForm11 { get; set; } = "[dbo].[sp_GetWardsNumberOfMKAForm11]";

        /// <summary>
        /// Gets or sets the get mka ward clan section summary list.
        /// </summary>
        /// <value>
        /// The get mka ward clan section summary list.
        /// </value>
        public static string GetMKAWardClanSectionSummaryList { get; set; } = "[dbo].[sp_GetMKAWardClanSectionSummarySheet]";


        /// <summary>
        /// Gets or sets the get ward clan section summary list.
        /// </summary>
        /// <value>
        /// The get ward clan section summary list.
        /// </value>
        public static string GetWardClanSectionSummaryList { get; set; } = "[dbo].[sp_GetWardClanSectionSummarySheet]";
               

        #endregion

        #region Occupation
        /// <summary>
        /// Gets or sets the get occupation list.
        /// </summary>
        /// <value>
        /// The get occupation list.
        /// </value>
        public static string GetOccupationList { get; set; } = "[dbo].[sp_GetOccupationList]";

        /// <summary>
        /// Gets or sets the add update occupation.
        /// </summary>
        /// <value>
        /// The add update occupation.
        /// </value>
        public static string AddUpdateOccupation { get; set; } = "[dbo].[sp_AddUpdateOccupation]";
        #endregion

        #region Setting
        /// <summary>
        /// Gets or sets the get occupation list.
        /// </summary>
        /// <value>
        /// The get occupation list.
        /// </value>
        public static string GetSetting { get; set; } = "[dbo].[sp_GetSetting]";

        /// <summary>
        /// Gets or sets the add update occupation.
        /// </summary>
        /// <value>
        /// The add update occupation.
        /// </value>
        public static string UpdateSetting { get; set; } = "[dbo].[sp_UpdateSetting]";

        /// <summary>
        /// Gets or sets the update setting geo version.
        /// </summary>
        /// <value>
        /// The update setting geo version.
        /// </value>
        public static string UpdateSettingGeoVersion { get; set; } = "[dbo].[sp_UpdateSettingGeoVersion]";
        /// <summary>
        /// Gets or sets the update setting mka geo version.
        /// </summary>
        /// <value>
        /// The update setting geo version.
        /// </value>
        public static string UpdateSettingMKAGeoVersion { get; set; } = "[dbo].[sp_UpdateSettingMKAGeoVersion]";
        #endregion

        #region Clan
        /// <summary>
        /// Gets or sets deleteClan
        /// </summary>
        /// <value>
        /// The Delete Clan.
        /// </value>
        public static string Delete_Clan { get; set; } = "[geo].[sp_DeleteClan]";


        /// <summary>
        /// Gets or sets the add update Clan.
        /// </summary>
        /// <value>
        /// The add update Clan.
        /// </value>
        public static string Add_Update_Clan { get; set; } = "[geo].[sp_AddUpdateClan]";

        /// <summary>
        /// Gets or sets the list Clan.
        /// </summary>
        /// <value>
        /// The list Clan.
        /// </value>
        public static string List_Clan { get; set; } = "[geo].[sp_ListClan]";

        /// <summary>
        /// Gets or sets the list Clan.
        /// </summary>
        /// <value>
        /// The list Clan.
        /// </value>
        public static string GetClanList { get; set; } = "[geo].[sp_GetClanList]";

        /// <summary>
        /// Gets or sets the list Clan by ward identifier.
        /// </summary>
        /// <value>
        /// The list Clan by ward identifier.
        /// </value>
        public static string List_Clan_By_WardId { get; set; } = "[geo].[sp_ListClanByWardId]";

        /// <summary>
        /// Gets or sets the Clan transfer.
        /// </summary>
        /// <value>
        /// The Clan transfer.
        /// </value>
        public static string Clan_Transfer { get; set; } = "[geo].[sp_ClanTransfer]";
        #endregion

        #region Clan
      


        /// <summary>
        /// Gets or sets the add update Clan.
        /// </summary>
        /// <value>
        /// The add update Clan.
        /// </value>
        public static string Add_Update_Writ { get; set; } = "[geo].[sp_AddUpdateWrit]";
              

        /// <summary>
        /// Gets or sets the list Clan.
        /// </summary>
        /// <value>
        /// The list Clan.
        /// </value>
        public static string GetWritList { get; set; } = "[geo].[sp_GetElectionWritList]";


        /// <summary>
        /// Gets RO list.
        /// </summary>
        /// <value>
        /// The list Clan.
        /// </value>
        public static string GetWritROList { get; set; } = "[geo].[sp_GetWritROList]";


        /// <summary>
        /// add update Writ RO.
        /// </summary>
        /// <value>
        /// The add update Clan.
        /// </value>
        public static string Add_Update_Writ_RO { get; set; } = "[geo].[sp_AddUpdateWritRO]";

        #endregion

        #region Activity Log

        /// <summary>
        /// Gets or sets the Add Activity Log.
        /// </summary>
        /// <value>
        /// The add Activity log.
        /// </value>
        public static string Add_Activity_Log { get; set; } = "[dbo].[sp_AddActivityLog]";


        /// <summary>
        /// Gets or sets the Get All Activity Log.
        /// </summary>
        /// <value>
        /// The Get all Activity log.
        /// </value>
        public static string Get_All_Activity_Log { get; set; } = "[dbo].[sp_GetAllActivityLog]";

        #endregion

        #region Production to Training

      
        public static string Backup_Pngec_Core { get; set; } = "[dbo].[sp_Backup_Pngec_Core]";
      
        public static string Backup_Pngec_Final { get; set; } = "[dbo].[sp_Backup_Pngec_Final]";

        public static string Backup_Pngec_Preliminary { get; set; } = "[dbo].[sp_Backup_Pngec_Preliminary]";

        public static string Backup_Pngec_Staging { get; set; } = "[dbo].[sp_Backup_Pngec_Staging]";


        public static string Restore_Pngec_Core { get; set; } = "[dbo].[sp_Restore_Pngec_Core]";

        public static string Restore_Pngec_Final { get; set; } = "[dbo].[sp_Restore_Pngec_Final]";

        public static string Restore_Pngec_Preliminary { get; set; } = "[dbo].[sp_Restore_Pngec_preliminary]";

        public static string Restore_Pngec_Staging { get; set; } = "[dbo].[sp_Restore_Pngec_Staging]";


        #endregion 
    }
}
