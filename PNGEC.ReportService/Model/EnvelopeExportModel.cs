using PNGEC.API.Domain.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNGEC.ReportService.Model
{
    public class EnvelopeExportModel
    {
        public string RegionName { get; set; }
        public string ProvinceName { get; set; }
        public string DistrictName { get; set; }
        public string LLGName { get; set; }
        public string WardName { get; set; }
        public int WardNo { get; set; }
        public List<LocationModel> LocationNameList { get; set; }
    }
}
