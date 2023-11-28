using ClientGetHttp.DatabaseServices.Services;
using ClientGetHttp.DatabaseServices.Services.Interface_Service;
using ClientGetHttp.DatabaseServices.Services.Model;
using ClientGetHttp.DatabaseServices.Services.Models.Interfaces;
using System.Collections.Generic;

namespace ClientGetHttp.DatabaseServices
{
    public abstract class Database_Centre
    {
        private static string? Id { get; set; }
        private static readonly Random random = new Random();
        private static readonly HashSet<short> usedSessionIDs = new HashSet<short>();

        private static List<UserView>? Valid_User_Views_Table;
        private static List<PageView>? Website_logs_table;
        private static List<SaleTransaction>? SalesTransactionsTable;
        private static List<Feedback>? FeedbackTable;
        private static IDatabaseServices? databaseServices { get; set; }
        public static async Task<List<Group_1_Record_Abstraction>?> GetDataForDatabaseServiceID(int tablenumber)
        {
            Id = GenerateUniqueSessionID();
            List<Group_1_Record_Abstraction>? processedData = new List<Group_1_Record_Abstraction>();
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


        // Return list Dictionary<string,( string, string, string)              [Product name, Today sales total corresponding to that product id, total view corresponding to that product ID]

        private async Task<Dictionary<string,( string, string, string)>?> ProcessDataForGetTableCorrespondingUserID(string UserID)
        {
            try
            {
                // Reset the data for each request
                Valid_User_Views_Table = new List<UserView>();
                Website_logs_table = new List<PageView>();
                SalesTransactionsTable = new List<SaleTransaction>();
                FeedbackTable = new List<Feedback>();

                // Define the Return Data
                Dictionary<string, (string, string, string)> return_List = new Dictionary<string, (string, string, string)>();

                // Collect the Userview data
                UserViewTableService userViewTableService = new UserViewTableService();
                List<Group_1_Record_Abstraction>? Userview_table_Data = await userViewTableService.GetDataServiceAsync();

                PageViewTableService pageViewTableService = new PageViewTableService();
                List<Group_1_Record_Abstraction>? pageView_table_Data = await pageViewTableService.GetDataServiceAsync();

                SaleTransactionTableService saleTransactionTableService = new SaleTransactionTableService();
                List<Group_1_Record_Abstraction>? saleTransaction_table_Data = await saleTransactionTableService.GetDataServiceAsync();

                Process_And_Print_Table_DataAsync(Userview_table_Data);
                Process_And_Print_Table_DataAsync(pageView_table_Data);
                Process_And_Print_Table_DataAsync(saleTransaction_table_Data);
                FeedbackTableService userVieTableService = new FeedbackTableService();

                userVieTableService.ProcessFeedbackList(FeedbackTable);
                pageViewTableService.ProcessPageViewList(Website_logs_table, UserID);
                return return_List;

            }
            catch (Exception e)
            {
                return null;
            }
        }
        private bool Process_And_Print_Table_DataAsync(List<Group_1_Record_Abstraction> dataAsList)
        {
            try
            {
                foreach (var Myobject in dataAsList)
                {
                    if (Myobject is UserView userView)
                    {
                        Valid_User_Views_Table.Add(userView);
                        //Console.WriteLine($"User_Id: {userView.User_Id}, Timestamp: {userView.Timestamp}, End_Date: {userView.End_Date}, Start_Date: {userView.Start_Date}");
                    }
                    else if (Myobject is PageView pageView)
                    {
                        Website_logs_table.Add(pageView);
                        //Console.WriteLine($"SessionId: {pageView.SessionId}, UserId: {pageView.UserId}, PageUrl: {pageView.PageUrl}, PageInfo: {pageView.PageInfo}, ProductId: {pageView.ProductId}, DateTime: {pageView.DateTime}, Start_Time: {pageView.Start_Time}, End_Time: {pageView.End_Time}");
                    }
                    else if (Myobject is SaleTransaction saleTransaction)
                    {
                        SalesTransactionsTable.Add(saleTransaction);
                        //Console.WriteLine($"TransactionId: {saleTransaction.TransactionId}, UserId: {saleTransaction.UserId}, TransactionValue: {saleTransaction.TransactionValue}, Date: {saleTransaction.Date}");
                    }
                    else if (Myobject is Feedback feedback)
                    {
                        FeedbackTable.Add(feedback);
                        //Console.WriteLine($"FeedbackId: {feedback.FeedbackId}, UserId: {feedback.UserId}, ProductId: {feedback.ProductId}, StarRating: {feedback.StarRating}");
                    }
                    else
                    {
                        Console.WriteLine("Unknown object type");
                    }
                }
                
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
