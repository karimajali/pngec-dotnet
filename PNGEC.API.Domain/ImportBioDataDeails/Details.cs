using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNGEC.API.Domain.ImportBioDataDeails
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class fingerprintRecords
    {
        public string _0 { get; set; }
        public string _1 { get; set; }
        public string _2 { get; set; }
        public string _3 { get; set; }
        public string _4 { get; set; }
        public string _5 { get; set; }
        public string _6 { get; set; }
        public string _7 { get; set; }
        public string _8 { get; set; }
        public string _9 { get; set; }
    }

    public class Staticfield
    {
        [DisplayName("Electoral Id")]
        public string mElectoralId { get; set; }

        [DisplayName("Enrollment Type")]
        public string mEnrollmentType { get; set; }

        [DisplayName("First Name")]
        public string mFirstName { get; set; }

        [DisplayName("Middle Name")]
        public string mMiddleName { get; set; }

        [DisplayName("Last Name")]
        public string mLastName { get; set; }

        [DisplayName("Country")]
        public string mCountry { get; set; }

        [DisplayName("State")]
        public string mState { get; set; }

        [DisplayName("City")]
        public string mCity { get; set; }

        [DisplayName("Nationality")]
        public string mNationality { get; set; }

        [DisplayName("Email Id")]
        public string mEmail { get; set; }

        [DisplayName("Phone Number")]
        public string mphone { get; set; }

        [DisplayName("National Id")]
        public string mNationalyID { get; set; }

        [DisplayName("Province")]
        public string mprovince { get; set; }

        [DisplayName("Llg")]
        public string mllg { get; set; }

        [DisplayName("Ward")]
        public string mward { get; set; }

        [DisplayName("District")]
        public string mdistrict { get; set; }

        [DisplayName("Location")]
        public string mLocation { get; set; }

        [DisplayName("LocationFK")]
        public string mLocationFK { get; set; }

        [DisplayName("BirthProvinceFK")]
        public string mBirthProvinceFK { get; set; }

        [DisplayName("OccupationFK")]
        public string mOccupationFK { get; set; }

        [DisplayName("Disability")]
        public string mdisability { get; set; }

        [DisplayName("Occupation")]
        public string mOccupation { get; set; }

        [DisplayName("Street Address 1")]
        public string streetAddressOne { get; set; }

        [DisplayName("Street Address 2")]
        public string streetAddressTWo { get; set; }

        [DisplayName("Date of Birth")]
        public string mDateOfBirth { get; set; }

        [DisplayName("Gender")]
        public string mGender { get; set; }

        [DisplayName("GPS")]
        public string mGPS { get; set; }

        [DisplayName("Face Image")]
        public string mFaceImage { get; set; }

        [DisplayName("Face Template")]
        public string mFaceTemplate { get; set; }

        [DisplayName("Signature Image")]
        public string mSignatureImage { get; set; }

        [DisplayName("Temp License")]
        public string mlsTempLicense { get; set; }

        [DisplayName("Updated Time")]
        public string updatedTime { get; set; }

        [DisplayName("Biometeric ID")]
        public int biometricID { get; set; }

        [DisplayName("Created Time")]
        public string createdTime { get; set; }

        [DisplayName("Fingerprint Match")]
        public bool mFingerprintMatch { get; set; }

        [DisplayName("Polling GPS")]
        public string mPollingGps { get; set; }

        [DisplayName("Polling Start Date")]
        public string mPollingStartDate { get; set; }

        [DisplayName("Fingerprint Template")]
        public string mFingerprintsFmdTemplate { get; set; }

        [DisplayName("Place of Birth")]
        public string mPlaceOfBirth { get; set; }

        [DisplayName("Father First Name")]
        public string mFatherFirstName { get; set; }

        [DisplayName("Father Last Name")]
        public string mFatherLastName { get; set; }

        [DisplayName("Mother First Name")]
        public string mMotherFirstName { get; set; }

        [DisplayName("Mother Last Name")]
        public string mMotherLastName { get; set; }

        [DisplayName("Clan")]
        public string mClan { get; set; }

        [DisplayName("Custom Field Values")]
        public List<CustomField> mCustomFieldValues { get; set; }

        [DisplayName("Religion")]
        public string mReligion { get; set; }

        [DisplayName("Form 11")]
        public string mEnrolmentFormNumber { get; set; }
    }

    public class CustomField
    {
        public int ID { get; set; }
        public int bioInfoID { get; set; }
        public string headerName { get; set; }
        public string headerValues { get; set; }
    }

    public class Details
    {
        public List<Staticfield> staticfield { get; set; }
        public List<object> customfield { get; set; }
    }
}
