using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PNGEC.API.Domain;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Helper;
using PNGEC.API.Repository.Voter;

namespace PNGEC.API.Service.Voter
{
    public class SyncV2Service : ISyncV2Service
    {
        private readonly string _connectionString;
        private readonly string _apiURL;
        private readonly IVoterRepository _voterRepository;

        public SyncV2Service(string connectionString, string apiURL)
        {
            _connectionString = connectionString;
            _apiURL = apiURL;
            _voterRepository = new VoterRepository(connectionString);
        }

        private async Task EnsureSyncLogTableAsync(SqlConnection con)
        {
            try
            {
                string sql = @"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'tblSyncLog')
                BEGIN
                    CREATE TABLE [dbo].[tblSyncLog](
                        [SyncLogId] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                        [UploadId] NVARCHAR(100) NOT NULL,
                        [ElectorId] BIGINT NULL,
                        [RecordNo] BIGINT NULL,
                        [ActionType] NVARCHAR(50) NULL,
                        [UserId] INT NULL,
                        [DeviceId] NVARCHAR(100) NULL,
                        [Status] NVARCHAR(50) NOT NULL,
                        [ErrorMessage] NVARCHAR(MAX) NULL,
                        [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE()
                    );
                    CREATE UNIQUE INDEX [IX_tblSyncLog_UploadId] ON [dbo].[tblSyncLog]([UploadId]);
                END";
                using (var cmd = new SqlCommand(sql, con))
                {
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write("SyncV2Service", "EnsureSyncLogTableAsync", ex);
            }
        }

        public async Task<SyncV2BatchResponse> ProcessBatchAsync(SyncV2BatchRequest request, string defaultActionType)
        {
            var response = new SyncV2BatchResponse();
            if (request == null || request.Voters == null || request.Voters.Count == 0)
            {
                response.Status = false;
                response.Message = "No voter records received.";
                return response;
            }

            string actionType = !string.IsNullOrEmpty(request.ActionType) ? request.ActionType : defaultActionType;
            response.ProcessedCount = request.Voters.Count;

            using (var con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                await EnsureSyncLogTableAsync(con);

                foreach (var voter in request.Voters)
                {
                    if (string.IsNullOrWhiteSpace(voter.UploadId))
                    {
                        voter.UploadId = Guid.NewGuid().ToString("N");
                    }

                    var recResult = new SyncV2RecordResult
                    {
                        UploadId = voter.UploadId,
                        ElectorId = voter.ElectorId
                    };

                    try
                    {
                        // 1. Idempotency Check: Was this UploadId already processed?
                        string checkSql = "SELECT TOP 1 ElectorId, RecordNo, Status, ErrorMessage FROM tblSyncLog WHERE UploadId = @UploadId";
                        bool alreadyProcessed = false;
                        using (var checkCmd = new SqlCommand(checkSql, con))
                        {
                            checkCmd.Parameters.AddWithValue("@UploadId", voter.UploadId);
                            using (var reader = await checkCmd.ExecuteReaderAsync())
                            {
                                if (await reader.ReadAsync())
                                {
                                    alreadyProcessed = true;
                                    string existingStatus = reader["Status"]?.ToString();
                                    recResult.IsSuccess = (existingStatus == "SUCCESS");
                                    recResult.IsDuplicateOrRetried = true;
                                    recResult.Status = "ALREADY_PROCESSED";
                                    recResult.ElectorId = reader["ElectorId"] != DBNull.Value ? Convert.ToInt64(reader["ElectorId"]) : voter.ElectorId;
                                    recResult.RecordNo = reader["RecordNo"] != DBNull.Value ? Convert.ToInt64(reader["RecordNo"]) : 0;
                                    recResult.ErrorMessage = reader["ErrorMessage"]?.ToString();
                                }
                            }
                        }

                        if (alreadyProcessed)
                        {
                            if (recResult.IsSuccess)
                            {
                                response.SuccessCount++;
                            }
                            else
                            {
                                response.FailedCount++;
                            }

                            response.Results.Add(recResult);
                            continue;
                        }

                        // 2. Process record via Modular Status Procedures
                        string cleanAction = (actionType ?? "NEW").ToUpperInvariant();
                        if (cleanAction == "BATCH")
                        {
                            string vStatus = (voter.Status ?? "").ToUpperInvariant().Trim();
                            if (vStatus == "TRANSFERRED IN" || vStatus == "TRANSFER_IN")
                            {
                                cleanAction = "TRANSFER_IN";
                            }
                            else if (vStatus == "UPDATE" || vStatus == "EDIT")
                            {
                                cleanAction = "UPDATE";
                            }
                            else if (vStatus == "NEW" || vStatus == "INSERT" || voter.ElectorId == 0)
                            {
                                cleanAction = "NEW";
                            }
                            else
                            {
                                cleanAction = "STATUS_CHANGE";
                            }
                        }

                        if (cleanAction == "NEW" || cleanAction == "INSERT")
                        {
                            int wardId = voter.WardId > 0 ? voter.WardId : (voter.MKAWardId > 0 ? voter.MKAWardId : 0);
                            int locationId = voter.LocationId > 0 ? voter.LocationId : (voter.MKALocationId > 0 ? voter.MKALocationId : 0);

                            using (var cmd = new SqlCommand("dbo.sp_Sync_New", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@UploadId", voter.UploadId);
                                cmd.Parameters.AddWithValue("@FName", (object)voter.FName ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@LName", (object)voter.LName ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@LocationId", locationId);
                                cmd.Parameters.AddWithValue("@WardId", wardId);
                                cmd.Parameters.AddWithValue("@DOB", (object)voter.DOB ?? DateTime.UtcNow.AddYears(-25));
                                cmd.Parameters.AddWithValue("@Gender", (object)voter.Gender ?? "M");
                                cmd.Parameters.AddWithValue("@UserId", request.UserId);
                                cmd.Parameters.AddWithValue("@DeviceId", (object)request.DeviceId ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@SevisPassId", (object)voter.SevisPassId ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@SevisPassStatus", (object)voter.SevisPassStatus ?? DBNull.Value);

                                using (var reader = await cmd.ExecuteReaderAsync())
                                {
                                    if (await reader.ReadAsync())
                                    {
                                        int isSuccess = reader["IsSuccess"] != DBNull.Value ? Convert.ToInt32(reader["IsSuccess"]) : 0;
                                        long electorId = reader["ElectorId"] != DBNull.Value ? Convert.ToInt64(reader["ElectorId"]) : 0;
                                        long recordNo = reader["RecordNo"] != DBNull.Value ? Convert.ToInt64(reader["RecordNo"]) : 0;
                                        string msg = reader["Message"]?.ToString();

                                        recResult.IsSuccess = (isSuccess == 1);
                                        recResult.Status = (msg == "ALREADY_PROCESSED") ? "ALREADY_PROCESSED" : (recResult.IsSuccess ? "QUEUED" : "FAILED");
                                        recResult.IsDuplicateOrRetried = (msg == "ALREADY_PROCESSED");
                                        recResult.ElectorId = electorId;
                                        recResult.RecordNo = recordNo;
                                        recResult.ErrorMessage = recResult.IsSuccess ? null : msg;

                                        if (recResult.IsSuccess) response.SuccessCount++;
                                        else response.FailedCount++;
                                    }
                                }
                            }

                            if (recResult.IsSuccess && recResult.ElectorId == 0)
                            {
                                await Task.Delay(250);
                                using (var checkCmd = new SqlCommand("SELECT TOP 1 ElectorId, RecordNo, Status FROM dbo.tblSyncLog WHERE UploadId = @UpId", con))
                                {
                                    checkCmd.Parameters.AddWithValue("@UpId", voter.UploadId);
                                    using (var checkReader = await checkCmd.ExecuteReaderAsync())
                                    {
                                        if (await checkReader.ReadAsync())
                                        {
                                            if (checkReader["ElectorId"] != DBNull.Value)
                                            {
                                                recResult.ElectorId = Convert.ToInt64(checkReader["ElectorId"]);
                                                recResult.RecordNo = Convert.ToInt64(checkReader["RecordNo"]);
                                                recResult.Status = checkReader["Status"]?.ToString() ?? "SUCCESS";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (cleanAction == "UPDATE")
                        {
                            using (var cmd = new SqlCommand("dbo.sp_Sync_Update", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@UploadId", voter.UploadId);
                                cmd.Parameters.AddWithValue("@ElectorId", voter.ElectorId);
                                cmd.Parameters.AddWithValue("@FName", (object)voter.FName ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@LName", (object)voter.LName ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@DOB", (object)voter.DOB ?? DateTime.UtcNow.AddYears(-25));
                                cmd.Parameters.AddWithValue("@Gender", (object)voter.Gender ?? "M");
                                cmd.Parameters.AddWithValue("@LocationId", voter.LocationId);
                                cmd.Parameters.AddWithValue("@UserId", request.UserId);
                                cmd.Parameters.AddWithValue("@DeviceId", (object)request.DeviceId ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@SevisPassId", (object)voter.SevisPassId ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@SevisPassStatus", (object)voter.SevisPassStatus ?? DBNull.Value);

                                using (var reader = await cmd.ExecuteReaderAsync())
                                {
                                    if (await reader.ReadAsync())
                                    {
                                        int isSuccess = reader["IsSuccess"] != DBNull.Value ? Convert.ToInt32(reader["IsSuccess"]) : 0;
                                        long electorId = reader["ElectorId"] != DBNull.Value ? Convert.ToInt64(reader["ElectorId"]) : voter.ElectorId;
                                        string msg = reader["Message"]?.ToString();

                                        recResult.IsSuccess = (isSuccess == 1);
                                        recResult.Status = recResult.IsSuccess ? "SUCCESS" : "FAILED";
                                        recResult.ElectorId = electorId;
                                        recResult.RecordNo = electorId;
                                        recResult.ErrorMessage = recResult.IsSuccess ? null : msg;

                                        if (recResult.IsSuccess) response.SuccessCount++;
                                        else response.FailedCount++;
                                    }
                                }
                            }
                        }
                        else if (cleanAction == "TRANSFER_IN")
                        {
                            using (var cmd = new SqlCommand("dbo.sp_Sync_TransferIn", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@UploadId", voter.UploadId);
                                cmd.Parameters.AddWithValue("@ElectorId", voter.ElectorId);
                                cmd.Parameters.AddWithValue("@FromWardId", voter.OldLocationId);
                                cmd.Parameters.AddWithValue("@ToWardId", voter.WardId);
                                cmd.Parameters.AddWithValue("@ToLocationId", voter.LocationId);
                                cmd.Parameters.AddWithValue("@UserId", request.UserId);
                                cmd.Parameters.AddWithValue("@DeviceId", (object)request.DeviceId ?? DBNull.Value);

                                using (var reader = await cmd.ExecuteReaderAsync())
                                {
                                    if (await reader.ReadAsync())
                                    {
                                        int isSuccess = reader["IsSuccess"] != DBNull.Value ? Convert.ToInt32(reader["IsSuccess"]) : 0;
                                        long electorId = reader["ElectorId"] != DBNull.Value ? Convert.ToInt64(reader["ElectorId"]) : voter.ElectorId;
                                        string msg = reader["Message"]?.ToString();

                                        recResult.IsSuccess = (isSuccess == 1);
                                        recResult.Status = recResult.IsSuccess ? "SUCCESS" : "FAILED";
                                        recResult.ElectorId = electorId;
                                        recResult.RecordNo = electorId;
                                        recResult.ErrorMessage = recResult.IsSuccess ? null : msg;

                                        if (recResult.IsSuccess) response.SuccessCount++;
                                        else response.FailedCount++;
                                    }
                                }
                            }
                        }
                        else if (cleanAction == "STATUS_CHANGE")
                        {
                            using (var cmd = new SqlCommand("dbo.sp_Sync_StatusChange", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@UploadId", voter.UploadId);
                                cmd.Parameters.AddWithValue("@ElectorId", voter.ElectorId);
                                cmd.Parameters.AddWithValue("@NewStatus", (object)voter.Status ?? "STATUS_CHANGE");
                                cmd.Parameters.AddWithValue("@Comment", (object)voter.Comment ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@UserId", request.UserId);
                                cmd.Parameters.AddWithValue("@DeviceId", (object)request.DeviceId ?? DBNull.Value);

                                using (var reader = await cmd.ExecuteReaderAsync())
                                {
                                    if (await reader.ReadAsync())
                                    {
                                        int isSuccess = reader["IsSuccess"] != DBNull.Value ? Convert.ToInt32(reader["IsSuccess"]) : 0;
                                        long electorId = reader["ElectorId"] != DBNull.Value ? Convert.ToInt64(reader["ElectorId"]) : voter.ElectorId;
                                        string msg = reader["Message"]?.ToString();

                                        recResult.IsSuccess = (isSuccess == 1);
                                        recResult.Status = recResult.IsSuccess ? "SUCCESS" : "FAILED";
                                        recResult.ElectorId = electorId;
                                        recResult.RecordNo = electorId;
                                        recResult.ErrorMessage = recResult.IsSuccess ? null : msg;

                                        if (recResult.IsSuccess) response.SuccessCount++;
                                        else response.FailedCount++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            // Fallback to legacy repository for arbitrary bulk formats
                            long syncResult = await _voterRepository.SyncElectors(voter);
                            if (syncResult > 0)
                            {
                                recResult.IsSuccess = true;
                                recResult.Status = "SUCCESS";
                                recResult.ElectorId = syncResult;
                                recResult.RecordNo = syncResult;
                                response.SuccessCount++;
                            }
                            else
                            {
                                recResult.IsSuccess = false;
                                recResult.Status = "FAILED";
                                recResult.ErrorMessage = "Voter sync failed with status " + syncResult;
                                response.FailedCount++;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.Write("SyncV2Service", "ProcessBatchAsync voter " + voter.UploadId, ex);
                        recResult.IsSuccess = false;
                        recResult.Status = "FAILED";
                        recResult.ErrorMessage = ex.Message;
                        response.FailedCount++;
                    }

                    response.Results.Add(recResult);
                }
            }

            response.Status = (response.SuccessCount > 0);
            response.Message = $"Processed {response.ProcessedCount} records: {response.SuccessCount} successful, {response.FailedCount} failed.";
            return response;
        }

        public async Task<Response> ProcessSingleVoterAsync(VoterModel voter, string actionType, int userId, string deviceId)
        {
            var response = new Response();
            var batchReq = new SyncV2BatchRequest
            {
                UserId = userId,
                DeviceId = deviceId,
                ActionType = actionType,
                Voters = new List<VoterModel> { voter }
            };

            var batchRes = await ProcessBatchAsync(batchReq, actionType);
            if (batchRes != null && batchRes.Results.Count > 0)
            {
                var first = batchRes.Results[0];
                response.Status = first.IsSuccess;
                response.Message = first.IsSuccess ? "Success" : first.ErrorMessage;
                response.Data = first;
            }
            else
            {
                response.Status = false;
                response.Message = batchRes?.Message ?? "Sync failed.";
            }

            return response;
        }

        public async Task<Response> UploadVoterImageAsync(IFormFile file, string imageType, long electorId, string uploadId)
        {
            var response = new Response();

            try
            {
                if (file == null || file.Length == 0)
                {
                    response.Status = false;
                    response.Message = "No file received.";
                    return response;
                }

                string subFolder = imageType?.ToLowerInvariant() == "form"
                    ? Path.Combine(Constants.UploadFolderPath, Constants.ElectorFormImageFolderPath, electorId.ToString())
                    : Path.Combine(Constants.UploadFolderPath, Constants.ElectorProfileImageFolderPath, electorId.ToString());

                string targetDir = Path.Combine(Directory.GetCurrentDirectory(), subFolder);
                if (!Directory.Exists(targetDir))
                {
                    Directory.CreateDirectory(targetDir);
                }

                string ext = Path.GetExtension(file.FileName);
                if (string.IsNullOrEmpty(ext)) ext = ".jpg";
                string fileName = $"{Guid.NewGuid():N}{ext}";
                string fullPath = Path.Combine(targetDir, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    await stream.FlushAsync();
                }

                if (electorId > 0)
                {
                    using (var con = new SqlConnection(_connectionString))
                    {
                        await con.OpenAsync();
                        string updateColumn = imageType?.ToLowerInvariant() == "form" ? "ElectorFormImage" : "ElectorProfileImage";
                        string sql = $"UPDATE [dbo].[tblRoll] SET [{updateColumn}] = @FileName WHERE [ElectorId] = @ElectorId";
                        using (var cmd = new SqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("@FileName", fileName);
                            cmd.Parameters.AddWithValue("@ElectorId", electorId);
                            await cmd.ExecuteNonQueryAsync();
                        }
                    }
                }

                response.Status = true;
                response.Message = "Image uploaded successfully.";
                response.Data = $"{_apiURL}/{subFolder.Replace('\\', '/')}/{fileName}";
            }
            catch (Exception ex)
            {
                ErrorLog.Write("SyncV2Service", "UploadVoterImageAsync", ex);
                response.Status = false;
                response.Message = "Error uploading image: " + ex.Message;
            }

            return response;
        }

        public async Task<Response> GetSyncStatusByUploadIdAsync(string uploadId)
        {
            var response = new Response();

            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    await con.OpenAsync();
                    await EnsureSyncLogTableAsync(con);

                    string sql = "SELECT TOP 1 UploadId, ElectorId, RecordNo, Status, ErrorMessage FROM tblSyncLog WHERE UploadId = @UploadId";
                    using (var cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@UploadId", uploadId);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                string st = reader["Status"]?.ToString();
                                var result = new SyncV2RecordResult
                                {
                                    UploadId = reader["UploadId"]?.ToString(),
                                    ElectorId = reader["ElectorId"] != DBNull.Value ? Convert.ToInt64(reader["ElectorId"]) : 0,
                                    RecordNo = reader["RecordNo"] != DBNull.Value ? Convert.ToInt64(reader["RecordNo"]) : 0,
                                    IsSuccess = (st == "SUCCESS"),
                                    Status = st,
                                    ErrorMessage = reader["ErrorMessage"]?.ToString()
                                };
                                response.Status = true;
                                response.Message = "Found";
                                response.Data = result;
                            }
                            else
                            {
                                response.Status = false;
                                response.Message = "UploadId not found";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write("SyncV2Service", "GetSyncStatusByUploadIdAsync", ex);
                response.Status = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
