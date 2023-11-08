using Cloud_Database_Management_System.Interfaces.Database_Services_Interfaces;
using Cloud_Database_Management_System.Interfaces.Repositories_Interfaces;
using Cloud_Database_Management_System.Models.Group_Data_Models;
using Cloud_Database_Management_System.Models.Group_Data_Models.Group_1_Data_Model_Tables;
using Cloud_Database_Management_System.Repositories.Repository_Group_1;
using Cloud_Database_Management_System.Repositories.Repository_Group_1.Table_Interface;
using System.Text.Json;

namespace Cloud_Database_Management_System.Services.Group_Data_Services
{
    public class Group1Service : IGroupService
    {
        private IGroupRepository _Group1Repository;
        private Input_Tables_Template _Group1_Data_Model;
        private DateTime _Created;

        public Group1Service(DateTime created, object data)
        {
            _Group1Repository = new Group1Repository(created);
            _Created = created;
        }

        public Group1Service(DateTime created)
        {
            _Group1Repository = new Group1Repository(created);
            _Created = created;
        }

        public bool ProcessGetRequestDataCorrespondGroupID(int tablenumber)
        {
            try
            {
                _Group1Repository.ReadAllTables();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ProcessPostRequestDataCorrespondGroupID(object data,int tablenumber)
        {
            try
            {
                _Group1_Data_Model = ProcessDataForGroup1(data, tablenumber);
                Table_Group_1_Dictionary tableInfo = Table_Group_1_Dictionary.Tablesname_List_with_Data_Type.FirstOrDefault(info => info.Index == tablenumber);
                if (tableInfo == null)
                {
                    return false;
                }
                if (_Group1_Data_Model != null)
                {
                    _Group1Repository.Create(_Group1_Data_Model, _Created, tableInfo.TableName);
                    return true;
                }else { return false; }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private Input_Tables_Template? ProcessDataForGroup1(object data, int tableNumber)
        {
            if (data == null) { return null; }

            Table_Group_1_Dictionary tableInfo = Table_Group_1_Dictionary.Tablesname_List_with_Data_Type.FirstOrDefault(info => info.Index == tableNumber);

            if (tableInfo == null)
            {
                return null;
            }

            Type dataType = tableInfo.DataType;
            string tableName = tableInfo.TableName;

            try
            {
                switch (tableNumber)
                {
                    case 0:
                        if (dataType == typeof(UserView))
                        {
                            return (Input_Tables_Template?)JsonSerializer.Deserialize<UserView>(data.ToString());
                        }
                        break;

                    case 1:
                        if (dataType == typeof(PageView))
                        {
                            return (Input_Tables_Template?)JsonSerializer.Deserialize<PageView>(data.ToString());
                        }
                        break;

                    case 2:
                        if (dataType == typeof(SaleTransaction))
                        {
                            return (Input_Tables_Template?)JsonSerializer.Deserialize<SaleTransaction>(data.ToString());
                        }
                        break;

                    case 3:
                        if (dataType == typeof(Feedback))
                        {
                            return (Input_Tables_Template?)JsonSerializer.Deserialize<Feedback>(data.ToString());
                        }
                        break;

                    default:
                        break;
                }
            }
            catch (JsonException)
            {
                return null;
            }

            return null;
        }
    }
}
