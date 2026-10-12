using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNGEC.API.Domain.Base;

namespace PNGEC.API.Domain.Mobile.General
{
    public class MKAClanModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the modifiedDate
        /// </summary>
        /// <value>
        /// The modifiedDate
        /// </value>
        private DateTime modifiedDate { get; set; }



        /// <summary>
        /// Gets or sets the MKAClan identifier.
        /// </summary>
        /// <value>
        /// The MKAClan identifier.
        /// </value>
        public int ClanId { get; set; }

        /// <summary>
        /// Gets or sets the MKAClan name.
        /// </summary>
        /// <value>
        /// The MKAClan name.
        /// </value>
        public string ClanName { get; set; }

        /// <summary>
        /// Gets or sets the Comment
        /// </summary>
        /// <value>
        /// The Comment
        /// </value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the response code.
        /// </summary>
        /// <value>
        /// The response code.
        /// </value>
        public int ResponseCode { get; set; }

   

        /// <summary>
        /// Gets or sets the Created Date
        /// </summary>
        /// <value>
        /// The Created Date.
        /// </value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the ModifiedDate
        /// </summary>
        /// <value>
        /// The Modified date.
        /// </value>
        public DateTime ModifiedDate
        {
            get { return modifiedDate; }
            set
            {
                modifiedDate = value;
                LastChangeDate = String.Format("{0:dd-MM-yyyy}", value);
            }
        }

        /// <summary>
        /// Gets or sets the LastChangeDate
        /// </summary>
        /// <value>
        /// The LastChangeDate
        /// </value>
        public string LastChangeDate { get; set; }
       
        /// <summary>
        /// Gets or sets the Update By UserName
        /// </summary>
        /// <value>
        /// The Update By UserName
        /// </value>
        public string UpdateByUserName { get; set; }

    
    }
}
