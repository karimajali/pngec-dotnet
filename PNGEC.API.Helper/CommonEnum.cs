using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PNGEC.API.Helper
{
    public static class CommonEnum
    {
        /// <summary>
        /// Database
        /// </summary>
        public enum Database
        {
            [Description("Select Database")]
            SelectDatabase = 0,

            [Description("Staging")]
            Staging = 1,

            [Description("Preliminary")]
            Preliminary = 2,

            [Description("Final")]
            Final = 3
        }

        /// <summary>
        /// Status
        /// </summary>
        public enum Status
        {
            Inactive = 0,
            Active = 1

        }

        /// <summary>
        /// Role
        /// </summary>
        public enum Role
        {
            [Description("Select Role")]
            SelectRole = 0,

            [Description("DPO")]
            DPO = 1,

            [Description("EM")]
            EM = 2,

            [Description("RD")]
            RD = 3,

            //[Description("Enrolment")]
            //Enrolment = 4,

            [Description("Guest")]
            Guest = 5,

            [Description("Admin")]
            Admin = 6,

            [Description("RO")]
            RO = 7,

            [Description("Management")]
            Management = 8,

            [Description("MKA DPO")]
            MKA = 9,

            [Description("Manage Clan")]
            ManageClan = 10
        }

        /// <summary>
        /// Server
        /// </summary>
        public enum Server
        {
            //[Description("Development")]
            //Development = 1,

            [Description("Training")]
            Training = 2,

            [Description("Production")]
            Production = 3
        }

        /// <summary>
        /// Log source system name
        /// </summary>
        public enum LogSource
        {

            [Description("ReportServer")]
            ReportServer = 1,

            [Description("Desktop")]
            Desktop = 2,

            [Description("Mobile")]
            Mobile = 3
        }


        /// <summary>
        /// Document Management
        /// </summary>
        public enum DocumentManagement
        {
            [Description("Select Type")]
            SelectDocumentType = 0,

            [Description("Data Centre Report")]
            DataCentreReport = 1,

            [Description("Management Report")]
            ManagementReport = 2,

            [Description("Training and Manuals")]
            TrainingAndManuals = 3,

            [Description("Others")]
            Others = 4,

            [Description("Form11")]
            Form11 = 5
        }
    }

    public static class EnumExtensionMethods
    {
        /// <summary>
        /// EnumerationMember class
        /// </summary>
        public class EnumerationMember
        {
            /// <summary>
            /// Gets or sets the description.
            /// </summary>
            /// <value>
            /// The description.
            /// </value>
            public string Description
            {
                get;
                set;
            }

            /// <summary>
            /// Gets or sets the value.
            /// </summary>
            /// <value>
            /// The value.
            /// </value>
            public object Value
            {
                get;
                set;
            }
        }

        /// <summary>
        /// Gets the enum description.
        /// </summary>
        /// <param name="enumValue">The enum value.</param>
        /// <returns></returns>
        public static string GetEnumDescription(this Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
        }

        /// <summary>
        /// Gets the enumeration member list.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>retrun list of enum with value and description pair</returns>
        public static List<EnumerationMember> GetEnumerationMemberList(Type type)
        {
            var enumValues = Enum.GetValues(type);

            return (from object enumValue in enumValues
                    select new EnumerationMember
                    {
                        Value = enumValue,
                        Description = GetDescription(type, enumValue)
                    }).ToList();
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <returns>return enum description</returns>
        public static string GetDescription(Type type, object value)
        {
            if (type != null && value != null)
            {
                var descriptionAttribute = type.GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
                string returnString = string.Empty;

                if (descriptionAttribute != null)
                {
                    returnString = descriptionAttribute.Description;
                }

                if (!string.IsNullOrEmpty(returnString))
                {
                    return returnString;
                }
                else
                {
                    return value.ToString();
                }
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
