using ClientGetHttp.DatabaseServices.Services.Models.Interfaces;

namespace ClientGetHttp.DatabaseServices.Services.Model
{
    public class Feedback : Group_1_Record_Interface
    {
        public  int FeedbackId { get; set; }
        public  string UserId { get; set; }
        public  string ProductId { get; set; }
        public  decimal StarRating { get; set; }

        public Feedback()
        {
            FeedbackId = 0;
            UserId = string.Empty;
            ProductId = string.Empty;
            StarRating = 0;
        }
    }
}
