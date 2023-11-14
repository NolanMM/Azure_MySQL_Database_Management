namespace Cloud_Database_Management_System.Models.Group_Data_Models.Group_1_Data_Model_Tables
{
    public abstract class Group_1_Record_Interface : Group_Data_Model
    {
        public override string ToString()
        {
            var properties = GetType().GetProperties();

            string result = string.Join(", ", properties.Select(prop => $"{prop.Name}: {prop.GetValue(this)}"));

            return result;
        }
    }
}
