using Cloud_Database_Management_System.Models.Group_Data_Models.Group_1_Data_Model_Tables;
using System.Diagnostics.Metrics;
using System.Net.NetworkInformation;

namespace Cloud_Database_Management_System.Repositories.Repository_Group_1.Table_Interface
{
    public class Table_Group_1_Dictionary
    {
        public int Index { get; set; }
        public string TableName { get; set; }
        public Type DataType { get; set; }

        public Table_Group_1_Dictionary(int index, string tableName, Type dataType)
        {
            Index = index;
            TableName = tableName;
            DataType = dataType;
        }

        public static List<Table_Group_1_Dictionary> Tablesname_List_with_Data_Type = new List<Table_Group_1_Dictionary>
        {
            new Table_Group_1_Dictionary(0, "userview", typeof(UserView)),
            new Table_Group_1_Dictionary(1, "pageview", typeof(PageView)),
            new Table_Group_1_Dictionary(2, "sales_transaction_table", typeof(SaleTransaction)),
            new Table_Group_1_Dictionary(3, "feedback_table", typeof(Feedback))
        };
    }


}
