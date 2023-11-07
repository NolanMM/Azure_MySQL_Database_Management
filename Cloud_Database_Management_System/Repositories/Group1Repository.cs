using Cloud_Database_Management_System.Interfaces.Repositories_Interfaces;

namespace Cloud_Database_Management_System.Repositories
{
    public class Group1Repository : IGroupRepository
    {
        private DateTime _created;
        public Group1Repository(DateTime created)
        {
            _created = created;
        }
        
        // private Group1_Data_Model? ProcessDataForGroup1(object data)
        // {
        //     if (data == null) { return null; }
        //     try
        //     {
        //         return JsonSerializer.Deserialize<Group1_Data_Model>(data.ToString());
        //     }
        //     catch (JsonException)
        //     {
        //         return null;
        //     }
        // }

        public bool Create()
        {
            throw new NotImplementedException();
        }

        public bool Delete()
        {
            throw new NotImplementedException();
        }

        public bool Read()
        {
            throw new NotImplementedException();
        }

        public bool Test_Connection_To_Table()
        {
            throw new NotImplementedException();
        }

        public bool Update()
        {
            throw new NotImplementedException();
        }
    }
}
