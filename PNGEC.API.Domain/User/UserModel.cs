using Microsoft.AspNetCore.Http;
using PNGEC.API.Domain.Base;
using System;

namespace PNGEC.API.Domain.User
{
    public class UserModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the profile file.
        /// </summary>
        /// <value>
        /// The profile file.
        /// </value>
        public IFormFile ProfileImage { get; set; }

        /// <summary>
        /// Gets or sets the profile image.
        /// </summary>
        /// <value>
        /// The profile image.
        /// </value>
        public string ProfileImageName { get; set; }

        /// <summary>
        /// Gets or sets the profile image URL.
        /// </summary>
        /// <value>
        /// The profile image URL.
        /// </value>
        public string ProfileImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the contact number.
        /// </summary>
        /// <value>
        /// The contact number.
        /// </value>
        public string ContactNumber { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="UserModel"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        public Boolean Status { get; set; }

        /// <summary>
        /// Gets or sets the name of the status.
        /// </summary>
        /// <value>
        /// The name of the status.
        /// </value>
        public string StatusName { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public int Role { get; set; }

        /// <summary>
        /// Gets or sets the name of the role.
        /// </summary>
        /// <value>
        /// The name of the role.
        /// </value>
        public string RoleName { get; set; }

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        /// <value>
        /// The region.
        /// </value>
        public int Region { get; set; }

        /// <summary>
        /// Gets or sets the name of the region.
        /// </summary>
        /// <value>
        /// The name of the region.
        /// </value>
        public string RegionName { get; set; }

        /// <summary>
        /// Gets or sets the province.
        /// </summary>
        /// <value>
        /// The province.
        /// </value>
        public int Province { get; set; }

        /// <summary>
        /// Gets or sets the name of the province.
        /// </summary>
        /// <value>
        /// The name of the province.
        /// </value>
        public string ProvinceName { get; set; }

        /// <summary>
        /// Gets or sets the district.
        /// </summary>
        /// <value>
        /// The district.
        /// </value>
        public int District { get; set; }

        /// <summary>
        /// Gets or sets the name of the district.
        /// </summary>
        /// <value>
        /// The name of the district.
        /// </value>
        public string DistrictName { get; set; }

        /// <summary>
        /// Gets or sets the LLG.
        /// </summary>
        /// <value>
        /// The LLG.
        /// </value>
        public int LLG { get; set; }

        /// <summary>
        /// Gets or sets the name of the LLG.
        /// </summary>
        /// <value>
        /// The name of the LLG.
        /// </value>
        public string LLGName { get; set; }

        /// <summary>
        /// Gets or sets the ward.
        /// </summary>
        /// <value>
        /// The ward.
        /// </value>
        public int Ward { get; set; }

        /// <summary>
        /// Gets or sets the name of the ward.
        /// </summary>
        /// <value>
        /// The name of the ward.
        /// </value>
        public string WardName { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public int Location { get; set; }

        /// <summary>
        /// Gets or sets the name of the location.
        /// </summary>
        /// <value>
        /// The name of the location.
        /// </value>
        public string LocationName { get; set; }
        /// <summary>
        /// Gets or sets the district.
        /// </summary>
        /// <value>
        /// The district.
        /// </value>
        public int MKADistrict { get; set; }

        /// <summary>
        /// Gets or sets the name of the district.
        /// </summary>
        /// <value>
        /// The name of the district.
        /// </value>
        public string MKADistrictName { get; set; }

        /// <summary>
        /// Gets or sets the LLG.
        /// </summary>
        /// <value>
        /// The LLG.
        /// </value>
        public int MKALLG { get; set; }

        /// <summary>
        /// Gets or sets the name of the LLG.
        /// </summary>
        /// <value>
        /// The name of the LLG.
        /// </value>
        public string MKALLGName { get; set; }

        /// <summary>
        /// Gets or sets the ward.
        /// </summary>
        /// <value>
        /// The ward.
        /// </value>
        public int MKAWard { get; set; }

        /// <summary>
        /// Gets or sets the name of the ward.
        /// </summary>
        /// <value>
        /// The name of the ward.
        /// </value>
        public string MKAWardName { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public int MKALocation { get; set; }

        /// <summary>
        /// Gets or sets the name of the location.
        /// </summary>
        /// <value>
        /// The name of the location.
        /// </value>
        public string MKALocationName { get; set; }
        /// <summary>
        /// Gets or sets the permission.
        /// </summary>
        /// <value>
        /// The permission.
        /// </value>
        public string Permission { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is database write.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is database write; otherwise, <c>false</c>.
        /// </value>
        public bool IsDBWrite { get; set; }

        /// <summary>
        /// Gets or sets the response code.
        /// </summary>
        /// <value>
        /// The response code.
        /// </value>
        public int ResponseCode { get; set; }

        /// <summary>
        /// Gets or sets the geo file URL.
        /// </summary>
        /// <value>
        /// The geo file URL.
        /// </value>
        public string GeoFileUrl { get; set; }
        /// <summary>
        /// Gets or sets the geo file URL.
        /// </summary>
        /// <value>
        /// The geo file URL.
        /// </value>
        public string MKAGeoFileUrl { get; set; }

        /// <summary>
        /// Gets or sets the geo version.
        /// </summary>
        /// <value>
        /// The geo version.
        /// </value>
        public int GeoVersion { get; set; }
    }
}
