using System;
using System.Collections.Generic;

namespace PNGEC.API.Domain.Mobile.Voter
{
    /// <summary>
    /// Batch request model for Phase 1 modular sync
    /// </summary>
    public class SyncV2BatchRequest
    {
        public int UserId { get; set; }
        public string DeviceId { get; set; }
        public string ActionType { get; set; } // NEW, UPDATE, TRANSFER_IN, STATUS_CHANGE
        public List<VoterModel> Voters { get; set; } = new List<VoterModel>();
    }

    /// <summary>
    /// Individual record result for Phase 1 idempotency
    /// </summary>
    public class SyncV2RecordResult
    {
        public string UploadId { get; set; }
        public long ElectorId { get; set; }
        public long RecordNo { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsDuplicateOrRetried { get; set; }
        public string Status { get; set; } // SUCCESS, ALREADY_PROCESSED, FAILED
        public string ErrorMessage { get; set; }
    }

    /// <summary>
    /// Overall response for batch sync
    /// </summary>
    public class SyncV2BatchResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public int ProcessedCount { get; set; }
        public int SuccessCount { get; set; }
        public int FailedCount { get; set; }
        public List<SyncV2RecordResult> Results { get; set; } = new List<SyncV2RecordResult>();
    }

    /// <summary>
    /// Phase 2 SevisPass verification request model
    /// </summary>
    public class SevisPassVerifyRequest
    {
        public string SevisPassId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; } // yyyy-MM-dd
        public string Gender { get; set; } // M / F
        public long? ElectorId { get; set; }
        public int? UserId { get; set; }
    }

    /// <summary>
    /// Phase 2 SevisPass verification response model
    /// </summary>
    public class SevisPassVerifyResponse
    {
        public bool IsSuccess { get; set; }
        public string Status { get; set; } // Verified, Mismatch, NotFound, Simulated
        public string SevisPassId { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public DateTime VerifiedDate { get; set; }
        public string Message { get; set; }
        public List<string> Mismatches { get; set; } = new List<string>();
    }
}
