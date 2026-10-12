using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNGEC.API.Domain.Report
{
    public class FieldTallySummarySheetReportModel
    {
        public string Wards { get; set; }

        public string TransferinWithinProvince { get; set; }

        public string TransferinFromDifferentProvince { get; set; }

        public string Ghost { get; set; }

        public string Duplicates { get; set; }

        public string TransferOut { get; set; }

        public string Deceased { get; set; }

        public string UnderAged { get; set; }

        public string Updated { get; set; }
    }
}
