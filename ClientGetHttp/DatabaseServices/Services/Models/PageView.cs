using ClientGetHttp.DatabaseServices.Services.Models.Interfaces;

namespace ClientGetHttp.DatabaseServices.Services.Model
{
    public class PageView : Group_1_Record_Interface
    {
        public string SessionId { get; set; }
        public string UserId { get; set; }
        public string PageUrl { get; set; }
        public string PageInfo { get; set; }
        public string ProductId { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime Start_Time { get; set; }
        public DateTime End_Time { get; set; }

        public PageView() {
            SessionId = string.Empty;
            UserId = string.Empty;
            PageInfo = string.Empty;
            PageUrl = string.Empty;
            ProductId = string.Empty;
            DateTime = DateTime.MinValue;
            Start_Time = DateTime.MinValue;
            End_Time = DateTime.MinValue;
        }
    }
}
