using System.ComponentModel.DataAnnotations;

namespace Cloud_Database_Management_System.Models.Group_Data_Models.Group_1_Data_Model_Tables
{
    public class DateNotDefaultAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                return date != DateTime.MinValue;
            }
            return false;
        }
    }

    public class SaleTransaction : Group_1_Record_Interface
    {
        [Required]
        public string Transaction_ID { get; set; }
        [Required]
        public string User_ID { get; set; }
        [Required(ErrorMessage = "Order Value is required")]
        [Range(0.00001, double.MaxValue, ErrorMessage = "Required Order value > 0")]
        public decimal Order_Value { get; set; }
        [Required(ErrorMessage = "Date is required")]
        [DateNotDefault(ErrorMessage = "Date must be filled")]
        public DateTime date { get; set; }

        //public SaleTransaction() {
        //    Transaction_ID = string.Empty;
        //    User_ID = string.Empty;
        //    Order_Value = 0;
        //    date = DateTime.MinValue;
        //}
        public override string ToString()
        {
            return $"Transaction_ID: {Transaction_ID}, User_ID: {User_ID}, Order_Value: {Order_Value}, date: {date}";
        }
    }
}
