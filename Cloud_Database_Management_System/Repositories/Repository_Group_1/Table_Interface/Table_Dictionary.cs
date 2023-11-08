using Cloud_Database_Management_System.Models.Group_Data_Models.Group_1_Data_Model_Tables;
using Cloud_Database_Management_System.Repositories.Repository_Group_1.Raw_Data_Tables_Class;

namespace Cloud_Database_Management_System.Repositories.Repository_Group_1.Table_Interface
{
    public class Table_Group_1_Dictionary
    {
        private static Random random = new Random();
        private static HashSet<short> usedSessionIDs = new HashSet<short>();
        public int Index { get; set; }
        public string TableName { get; set; }
        public Type DataType { get; set; }
        public Input_Tables_Template? TableType { get; set; }

        public Table_Group_1_Dictionary(int index, string tableName, Type dataType)
        {
            Index = index;
            TableName = tableName;
            DataType = dataType;
        }
        public Table_Group_1_Dictionary(int index, string tableName, Type dataType, Input_Tables_Template tabletype)
        {
            Index = index;
            TableName = tableName;
            DataType = dataType;
            TableType = tabletype;
        }
        public static List<Table_Group_1_Dictionary> Tablesname_List_with_Data_Type = new List<Table_Group_1_Dictionary>
        {
            new Table_Group_1_Dictionary(0, "userview", typeof(UserView)),
            new Table_Group_1_Dictionary(1, "pageview", typeof(PageView)),
            new Table_Group_1_Dictionary(2, "sales_transaction_table", typeof(SaleTransaction)),
            new Table_Group_1_Dictionary(3, "feedback_table", typeof(Feedback))
        };
        public static List<Table_Group_1_Dictionary> Tablesname_List_with_Data_Type_Table_Type = new List<Table_Group_1_Dictionary>
        {
            new Table_Group_1_Dictionary(0, "userview", typeof(UserView), Userview_table.SetUp(GenerateUniqueSessionID())),
            new Table_Group_1_Dictionary(1, "pageview", typeof(PageView), Pageview_table.SetUp(GenerateUniqueSessionID())),
            new Table_Group_1_Dictionary(2, "sales_transaction_table", typeof(SaleTransaction), Salestransaction_table.SetUp(GenerateUniqueSessionID())),
            new Table_Group_1_Dictionary(3, "feedback_table", typeof(Feedback), Feedback_table.SetUp(GenerateUniqueSessionID()))
        };

        private static string GenerateUniqueSessionID()
        {
            short sessionId;

            do
            {
                sessionId = (short)random.Next(0, 65536);
            } while (usedSessionIDs.Contains(sessionId));

            usedSessionIDs.Add(sessionId);

            // Convert the short session ID to a string
            return sessionId.ToString();
        }
    }
}