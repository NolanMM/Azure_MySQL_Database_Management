using ClientGetHttp.DatabaseServices.Services;
using ClientGetHttp.DatabaseServices.Services.Interface_Service;
using ClientGetHttp.DatabaseServices.Services.Model;
using ClientGetHttp.DatabaseServices.Services.Models.Interfaces;
using ClientGetHttp.DatabaseServices.Services.Network_Database_Services;
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
                    return null;
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


        // Return list Dictionary<string,( string, string, string, string)              [Product name, Today sales total corresponding to that product id, total view corresponding to that product ID]

        public static async Task<Dictionary<string, (string, string, string, string, string, string)>?> ProcessDataForGetTableCorrespondingUserID(string UserID)        // pid,(sid == UserID, Name, TodaySale, TodayViews, Product Prices, Date)
        {
            try
            {
                Dictionary<string, (string, string, string, string, string, string)> ReturnData = new Dictionary<string, (string, string, string, string, string, string)>();   // pid,(sid == UserID, Name, TodaySale, TodayViews, Product Prices, Date)

                // Retrieve data from the database
                Dictionary<string, (string, string, string, string)>? return_Product_List_Database = await ProcessDataForGetTableCorrespondingUserID_Database("None");   // ProductId (pid), PageViewNumberPageViewNumber (TodayViews), UserViewNumber, TotalQuantity(TodaySale), Date(Date) for all userID

                // Retrieve data from the product module
                Dictionary<string, (string, string, string, string)>? return_Product_List_Product_Module = await Product_Group_Database_Services.ProcessGetTableRequestByUserIDAsync(UserID);   // (pid, (sid == UserID (all the values the same in column)), Name, Price, Date)) specific for input userID

                // Iterate through the product IDs in the database
                foreach (var pid in return_Product_List_Database.Keys)
                {
                    // Check if the product ID is present in the product module data
                    if (return_Product_List_Product_Module.TryGetValue(pid, out var productModuleData))
                    {
                        // Check if the sid (UserID) and the date match
                        if (return_Product_List_Database[pid].Item4 == productModuleData.Item4)  // Minh note same day
                        {
                        // Combine data from both sources into the ReturnData dictionary
                        return_Product_List_Database.TryGetValue(pid, out var productModuleDatabase);
                            int.TryParse(productModuleDatabase.Item2, out int int_TodayView);
                            if (int_TodayView == 0)
                            {
                                int.TryParse(productModuleDatabase.Item3, out int IntTodaySale);
                                int_TodayView = IntTodaySale;
                            }
                        ReturnData[pid] = (                     // ProductID
                            productModuleData.Item1,            // UserSeller
                            productModuleData.Item2,            // Name of product
                            productModuleDatabase.Item3,        // TodaySale
                            int_TodayView.ToString(),           // TodayViews
                            productModuleData.Item3,            // ProducPrices
                            productModuleDatabase.Item4         // Date
                        );
                        }
                    }
                }

                return ReturnData;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public static async Task<Dictionary<string, (string, string, string, string)>?> ProcessDataForGetTableCorrespondingUserID_Database(string UserID)
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

                Dictionary<string, (string, string)>? return_Data_userView = new Dictionary<string, (string, string)>(); // Product ID, (Count, Date)
                Dictionary<string, (string, string)>? return_Data_PageView = new Dictionary<string, (string, string)>(); // Product ID, (Count, Date)
                Dictionary<string, (string, string)>? return_Data_SaleTransactionTable = new Dictionary<string, (string, string)>(); // Product ID, (Total Quantity, Date)

                return_Data_userView = userViewTableService.ProcessUserViewList(Valid_User_Views_Table, UserID);
                return_Data_PageView = pageViewTableService.ProcessPageViewList(Website_logs_table, UserID);
                return_Data_SaleTransactionTable = saleTransactionTableService.ProcessSaleTransactionList(SalesTransactionsTable, UserID);

                Dictionary<string, (string, string, string, string)> combinedData = FullOuterJoin(return_Data_userView, return_Data_PageView, return_Data_SaleTransactionTable);

                return combinedData;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        private static Dictionary<string, (string, string, string, string)> FullOuterJoin(
                Dictionary<string, (string, string)>? userViewData,
                Dictionary<string, (string, string)>? pageViewData,
                Dictionary<string, (string, string)>? saleTransactionData)
        {
            userViewData ??= new Dictionary<string, (string, string)>();
            pageViewData ??= new Dictionary<string, (string, string)>();
            saleTransactionData ??= new Dictionary<string, (string, string)>();

            // Combine all unique ProductIDs from the three dictionaries
            HashSet<string> allProductIDs = new HashSet<string>(userViewData.Keys);
            allProductIDs.UnionWith(pageViewData.Keys);
            allProductIDs.UnionWith(saleTransactionData.Keys);

            Dictionary<string, (string, string, string, string)> result = new Dictionary<string, (string, string, string, string)>();

            foreach (string productId in allProductIDs)
            {
                // Get data from each dictionary, defaulting to ("0", "") if not present
                string? date_process = null;
                (string userViewCount, string userViewDate) = userViewData.TryGetValue(productId, out var userViewValue)
                    ? userViewValue
                    : ("0", "");

                (string pageViewCount, string pageViewDate) = pageViewData.TryGetValue(productId, out var pageViewValue)
                    ? pageViewValue
                    : ("0", "");

                (string totalQuantity, string saleTransactionDate) = saleTransactionData.TryGetValue(productId, out var saleTransactionValue)
                    ? saleTransactionValue
                    : ("0", "");

                if (!string.IsNullOrEmpty(userViewDate))
                {
                    date_process = userViewDate;
                }
                else if (!string.IsNullOrEmpty(pageViewDate))
                {
                    date_process = pageViewDate;
                }
                else if (!string.IsNullOrEmpty(saleTransactionDate))
                {
                    date_process = saleTransactionDate;
                }
                else
                {
                    date_process = "0";
                }
                // Add combined data to the result dictionary
                result.Add(productId, (userViewCount, pageViewCount, totalQuantity, date_process));
            }

            return result;
        }

        private static bool Process_And_Print_Table_DataAsync(List<Group_1_Record_Abstraction> dataAsList)
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
