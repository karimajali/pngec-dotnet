using Newtonsoft.Json;
using PNGEC.API.Domain.Clan;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PNGEC.API.Helper
{
    [DataContract]
    public class Response
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        [DataMember(Name = "message")]
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Response"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "status")]
        public bool Status { get; set; } = false;

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        [DataMember(Name = "data")]
        public Object Data { get; set; }

       
    }

    [DataContract]
    public class Response<T>
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Response{T}"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "status")]
        public bool Status { get; set; } = false;

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        [DataMember(Name = "message")]
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        [DataMember(Name = "data")]
        public Result<T> Data { get; set; }
    }

    [DataContract]
    public class Result<T>
    {
        /// <summary>
        /// Gets or sets the list items.
        /// </summary>
        /// <value>
        /// The list items.
        /// </value>
        [DataMember(Name = "result")]
        public IEnumerable<T> ListItems { get; set; }

        /// <summary>
        /// Gets or sets the total record count.
        /// </summary>
        /// <value>
        /// The total record count.
        /// </value>
        [DataMember(Name = "total")]
        public int TotalRecordCount { get; set; }
    }

    /// <summary>
    /// Common response model class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CommonResponse<T>
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CommonResponse{T}"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("status")]
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
