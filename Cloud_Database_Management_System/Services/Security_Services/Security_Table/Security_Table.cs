using Cloud_Database_Management_System.Security_Services.Security_Table.Security_Tables;
using Cloud_Database_Management_System.Services.Security_Services.Security_Table.Data_Models;
using Cloud_Database_Management_System.Services.Security_Services.Security_Table.Security_Tables;

namespace Cloud_Database_Management_System.Services.Security_Services.Security_Table
{
    public static class Security_Table_DB_Control
    {
        private static readonly string user_id_table_name = "security_userid";
        private static readonly string password_table_name = "security_password";
        private static readonly string otp_table_name = "otp_table";

        public static List<Security_Data_Model_Abtraction>? Security_Data_Model_Abtraction_List = new List<Security_Data_Model_Abtraction>();

        public static async Task<List<Security_Data_Model_Abtraction>?> ReadAllAsyncTablename(string tablename)
        {
            Security_Data_Model_Abtraction_List = new List<Security_Data_Model_Abtraction>();
            try
            {
                switch (tablename)
                {
                    case var _ when tablename == user_id_table_name:
                        security_userid_table security_Userid_Table = new security_userid_table();
                        Security_Data_Model_Abtraction_List = await security_Userid_Table.ReadAllAsync_Security_Table();
                        return Security_Data_Model_Abtraction_List;
                    case var _ when tablename == password_table_name:
                        security_password_table security_Password_Table = new security_password_table();
                        Security_Data_Model_Abtraction_List = await security_Password_Table.ReadAllAsync_Security_Table();
                        return Security_Data_Model_Abtraction_List;
                    case var _ when tablename == otp_table_name:
                        security_otp_table security_otp_Table = new security_otp_table();
                        Security_Data_Model_Abtraction_List = await security_otp_Table.ReadAllAsync_Security_Table();
                        return Security_Data_Model_Abtraction_List;
                    default:
                        return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public static async Task<bool> CreateAsyncTablename(Security_Data_Model_Abtraction dataModel, string tablename)
        {
            try
            {
                switch (tablename)
                {
                    case var _ when tablename == user_id_table_name:
                        security_userid_table security_Userid_Table = new security_userid_table();
                        return await security_Userid_Table.CreateAsync_Security_Table(dataModel, user_id_table_name);

                    case var _ when tablename == password_table_name:
                        security_password_table security_Password_Table = new security_password_table();
                        return await security_Password_Table.CreateAsync_Security_Table(dataModel, password_table_name);
                    case var _ when tablename == otp_table_name:
                        security_otp_table security_otp_Table = new security_otp_table();
                        return await security_otp_Table.CreateAsync_Security_Table(dataModel, otp_table_name);
                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public static bool Test_Connection_To_Table(string tablename)
        {
            try
            {
                switch (tablename)
                {
                    case var _ when tablename == user_id_table_name:
                        security_userid_table security_Userid_Table = new security_userid_table();
                        return security_Userid_Table.Test_Connection_To_Table();

                    case var _ when tablename == password_table_name:
                        security_password_table security_Password_Table = new security_password_table();
                        return security_Password_Table.Test_Connection_To_Table();
                    case var _ when tablename == otp_table_name:
                        security_otp_table security_otp_Table = new security_otp_table();
                        return security_otp_Table.Test_Connection_To_Table();
                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

        }


        public static void Delete()
        {
            throw new NotImplementedException();
        }

        public static void Insert()
        {
            throw new NotImplementedException();
        }
    }
}
