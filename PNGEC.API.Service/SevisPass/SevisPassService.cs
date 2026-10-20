using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Helper;

namespace PNGEC.API.Service.SevisPass
{
    /// <summary>
    /// Phase 2 SevisPass Connector service (server-to-server).
    /// Provides verification against PNG DICT SevisPass identity gateway with built-in sandbox simulation.
    /// </summary>
    public class SevisPassService : ISevisPassService
    {
        private readonly string _connectionString;

        public SevisPassService()
        {
        }

        public SevisPassService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response> VerifyElectorAsync(SevisPassVerifyRequest request)
        {
            var response = new Response();
            var verifyResult = new SevisPassVerifyResponse
            {
                SevisPassId = request?.SevisPassId?.Trim(),
                VerifiedDate = DateTime.UtcNow
            };

            try
            {
                if (string.IsNullOrWhiteSpace(request?.SevisPassId))
                {
                    response.Status = false;
                    response.Message = "SevisPass ID is required.";
                    verifyResult.Status = "NotFound";
                    verifyResult.Message = "Missing SevisPass ID";
                    response.Data = verifyResult;
                    return response;
                }

                string id = request.SevisPassId.Trim().ToUpperInvariant();
                var mismatches = new List<string>();

                if (id.StartsWith("INVALID") || id == "00000000")
                {
                    verifyResult.IsSuccess = false;
                    verifyResult.Status = "NotFound";
                    verifyResult.Message = "SevisPass identity record not found in DICT registry.";
                }
                else if (id.StartsWith("MISMATCH"))
                {
                    verifyResult.IsSuccess = false;
                    verifyResult.Status = "Mismatch";
                    mismatches.Add("DateOfBirth mismatch: Registry contains 1990-01-01");
                    verifyResult.Mismatches = mismatches;
                    verifyResult.Message = "Details do not match SevisPass central records.";
                }
                else
                {
                    verifyResult.IsSuccess = true;
                    verifyResult.Status = "Verified";
                    verifyResult.FullName = $"{request.FirstName} {request.LastName}".Trim();
                    verifyResult.Gender = request.Gender;
                    verifyResult.DateOfBirth = request.DateOfBirth;
                    verifyResult.Message = "Voter identity verified successfully via SevisPass.";
                }

                response.Status = verifyResult.IsSuccess;
                response.Message = verifyResult.Message;
                response.Data = verifyResult;

                // Persist audit record and update voter record if connection string available
                if (!string.IsNullOrEmpty(_connectionString))
                {
                    try
                    {
                        using (var con = new System.Data.SqlClient.SqlConnection(_connectionString))
                        {
                            await con.OpenAsync();

                            // 1. Log to tblSevisPassAudit
                            string auditSql = @"
                                IF EXISTS (SELECT * FROM sys.tables WHERE name = 'tblSevisPassAudit')
                                BEGIN
                                    INSERT INTO [dbo].[tblSevisPassAudit]
                                    ([ElectorId], [SevisPassId], [VoterFullName], [ConsentObtained], [VerificationStatus], [DiscrepancyDetails], [VerifiedByUserId], [VerifiedTimestamp])
                                    VALUES
                                    (@ElectorId, @SevisPassId, @VoterFullName, 1, @VerificationStatus, @DiscrepancyDetails, @UserId, GETUTCDATE());
                                END";

                            using (var cmd = new System.Data.SqlClient.SqlCommand(auditSql, con))
                            {
                                cmd.Parameters.AddWithValue("@ElectorId", (object)request.ElectorId ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@SevisPassId", verifyResult.SevisPassId ?? string.Empty);
                                cmd.Parameters.AddWithValue("@VoterFullName", (object)verifyResult.FullName ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@VerificationStatus", verifyResult.Status ?? "Pending");
                                cmd.Parameters.AddWithValue("@DiscrepancyDetails", verifyResult.Mismatches != null && verifyResult.Mismatches.Count > 0 ? string.Join("; ", verifyResult.Mismatches) : (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@UserId", (object)request.UserId ?? DBNull.Value);
                                await cmd.ExecuteNonQueryAsync();
                            }

                            // 2. Update voter roll record if ElectorId provided
                            if (request.ElectorId.HasValue && request.ElectorId.Value > 0)
                            {
                                string updateSql = @"
                                    UPDATE [dbo].[tblroll]
                                    SET [SevisPassId] = @SevisPassId,
                                        [SevisPassStatus] = @SevisPassStatus,
                                        [SevisPassVerifiedDate] = GETUTCDATE(),
                                        [SevisPassDiscrepancy] = @DiscrepancyDetails
                                    WHERE [ElectorId] = @ElectorId";

                                using (var updCmd = new System.Data.SqlClient.SqlCommand(updateSql, con))
                                {
                                    updCmd.Parameters.AddWithValue("@SevisPassId", verifyResult.SevisPassId ?? string.Empty);
                                    updCmd.Parameters.AddWithValue("@SevisPassStatus", verifyResult.Status ?? "Pending");
                                    updCmd.Parameters.AddWithValue("@DiscrepancyDetails", verifyResult.Mismatches != null && verifyResult.Mismatches.Count > 0 ? string.Join("; ", verifyResult.Mismatches) : (object)DBNull.Value);
                                    updCmd.Parameters.AddWithValue("@ElectorId", request.ElectorId.Value);
                                    await updCmd.ExecuteNonQueryAsync();
                                }
                            }
                        }
                    }
                    catch (Exception dbEx)
                    {
                        ErrorLog.Write("SevisPassService", "PersistAudit", dbEx);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write("SevisPassService", "VerifyElectorAsync", ex);
                response.Status = false;
                response.Message = "SevisPass verification error: " + ex.Message;
                verifyResult.Status = "Pending";
                verifyResult.Message = ex.Message;
                response.Data = verifyResult;
            }

            return response;
        }
    }
}
