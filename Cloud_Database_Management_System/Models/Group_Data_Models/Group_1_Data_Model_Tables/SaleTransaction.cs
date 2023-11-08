namespace Cloud_Database_Management_System.Models.Group_Data_Models.Group_1_Data_Model_Tables
{
    public class SaleTransaction : Group_1_Record_Interface
    {
        public string TransactionId { get; set; }
        public string UserId { get; set; }
        public decimal TransactionValue { get; set; }
        public DateTime Date { get; set; }

        public SaleTransaction() { 
            TransactionId = string.Empty;
            UserId = string.Empty;
            TransactionValue = 0;
            Date = DateTime.MinValue;
        }
    }
}
