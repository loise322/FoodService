namespace TravelLine.Food.Core.Reports
{
    internal class ComplexReportItem
    {
        public string LegalName { get; set; }

        public string UserName { get; set; }

        public decimal Sum { get; set; }

        public decimal SumQuota { get; set; }

        public int Count { get; set; }

        public int WorkDays { get; set; }

        public decimal Balance => SumQuota - Sum;
    }
}
