namespace Security_Services_Dev_Env.Services.Security_Services.Security_Table.Data_Models
{
    public abstract class Security_Data_Model_Abtraction
    {
        public override string ToString()
        {
            var properties = GetType().GetProperties();

            string result = string.Join(", ", properties.Select(prop => $"{prop.Name}: {prop.GetValue(this)}"));

            return result;
        }
    }
}
