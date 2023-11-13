namespace Cloud_Database_Management_System.Models.Group_Data_Models.Group_1_Data_Model_Tables
{
    public class SaleTransaction : Group_1_Record_Interface
    {
        public string Transaction_ID { get; set; }
        public string User_ID { get; set; }
        public decimal Order_Value { get; set; }
        public DateTime date { get; set; }

        public SaleTransaction() {
            Transaction_ID = string.Empty;
            User_ID = string.Empty;
            Order_Value = 0;
            date = DateTime.MinValue;
        }
        public override string ToString()
        {
            return $"Transaction_ID: {Transaction_ID}, User_ID: {User_ID}, Order_Value: {Order_Value}, date: {date}";
        }
    }
}
