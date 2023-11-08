using ClientGetHttp.DatabaseServices.Services.Models.Interfaces;

namespace ClientGetHttp.DatabaseServices.Services.Model
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
