using Microsoft.AspNetCore.Mvc;
using OTP_Centre.DataModel;
using OTP_Centre.Tables;
using System.Text.Json;

namespace OTP_Centre.Services
{
    public class OTP_Centre_Services
    {
        private static readonly string otp_table_name = "otp_table";
        public async Task<bool> OTP_Table_Record_Process(OTP_Record? OTP_record)
        {
            if (OTP_record == null)
            {
                return false;
            }
            bool create_otp_Record = await OTP_Centre_Services.CreateAsyncTablename(OTP_record, otp_table_name);
            return create_otp_Record;
        }
        public static async Task<bool> CreateAsyncTablename(OTP_Record dataModel, string tablename)
        {
            try
            {
                switch (tablename)
                {
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
    }
}
