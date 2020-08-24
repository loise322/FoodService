namespace TravelLine.Food.Core.Reports
{
    internal class TransferReportItem
    {
        public string UserName { get; set; }

        public decimal SumTransfer { get; set; }

        public int CountTransfer { get; set; }

        public decimal BalanceTransfer { get; set; }

        public decimal SumBasic { get; set; }

        public int CountBasic { get; set; }

        public decimal BalanceBasic { get; set; }

        public int CountTotal => CountTransfer + CountBasic;

        public decimal BalanceTotal => BalanceTransfer + BalanceBasic;
    }
}
