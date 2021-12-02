namespace moneymanager.Models
{
    public class Transaction
    {
        private double _amount;
        public long Id { get; set; }
        public string ItemName { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
        public double Amount { get { return Qty * Price; } }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}