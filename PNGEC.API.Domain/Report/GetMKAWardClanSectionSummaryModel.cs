namespace PNGEC.API.Domain.Report
{
    public class GetMKAWardClanSectionSummaryModel
    {
        public int RowNo { get; set; }

        public int WardId { get; set; }
        public string WardName { get; set; }

        public int LocationId { get; set; }
        public string LocationName { get; set; }

        public int ClanId { get; set; }
        public string ClanName { get; set; }

        public int ElectorCount { get; set; }
    }

    public class GetWardClanSectionSummaryModel
    {
        public int RowNo { get; set; }

        public int WardId { get; set; }
        public string WardName { get; set; }

        public int LocationId { get; set; }
        public string LocationName { get; set; }

        public int ClanId { get; set; }
        public string ClanName { get; set; }

        public int ElectorCount { get; set; }
    }
}
