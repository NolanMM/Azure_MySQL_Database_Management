using ClientGetHttp.DatabaseServices.Services;
using ClientGetHttp.DatabaseServices.Services.Interface_Service;

namespace ClientGetHttp.DatabaseServices
{
    public abstract class Database_Centre
    {
        private static string? Id { get; set; }
        private static readonly Random random = new Random();
        private static readonly HashSet<short> usedSessionIDs = new HashSet<short>();
        private static IDatabaseServices? databaseServices { get; set; }
        public static async Task<List<object>> GetDataForDatabaseServiceID(int tablenumber)
        {
            Id = GenerateUniqueSessionID();
            List<object> processedData = new List<object>();
            switch (tablenumber)
            {
                case 0:
                    databaseServices = new UserViewTableService();
                    processedData = await databaseServices.GetDataServiceAsync();
                    break;
                case 1:
                    databaseServices = new PageViewTableService();
                    processedData = await databaseServices.GetDataServiceAsync();
                    break;
                case 2:
                    databaseServices = new SaleTransactionTableService();
                    processedData = await databaseServices.GetDataServiceAsync();
                    break;
                case 3:
                    databaseServices = new FeedbackTableService();
                    processedData = await databaseServices.GetDataServiceAsync();
                    break;
                default:
                    throw new ArgumentException("Invalid table number");
            }
            return processedData;
        }
        private static string GenerateUniqueSessionID()
        {
            short sessionId;
            do
            {
                sessionId = (short)random.Next(0, 65536);
            } while (usedSessionIDs.Contains(sessionId));
            usedSessionIDs.Add(sessionId);
            return sessionId.ToString();
        }
    }
}
