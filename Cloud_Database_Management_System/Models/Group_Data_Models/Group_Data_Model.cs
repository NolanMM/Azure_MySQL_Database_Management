namespace Cloud_Database_Management_System.Models.Group_Data_Models
{
    public abstract class Group_Data_Model
    {
        public override string ToString()
        {
            var properties = GetType().GetProperties();

            string result = string.Join(", ", properties.Select(prop => $"{prop.Name}: {prop.GetValue(this)}"));

            return result;
        }
    }
}
