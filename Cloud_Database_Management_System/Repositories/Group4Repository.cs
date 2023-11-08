using Cloud_Database_Management_System.Interfaces.Repositories_Interfaces;
using Cloud_Database_Management_System.Models.Group_Data_Models;

namespace Cloud_Database_Management_System.Repositories
{
    public class Group4Repository : IGroupRepository
    {
        private DateTime _created;
        public Group4Repository(DateTime created)
        {
            _created = created;
        }

        public Task<bool> Create(Group_Data_Model group_Data_Model, DateTime _Created, string tablename)
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

        public Task<List<Group_Data_Model>> ReadTable(string tablename)
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<string, object>> ReadAllTables()
        {
            throw new NotImplementedException();
        }
    }
}
