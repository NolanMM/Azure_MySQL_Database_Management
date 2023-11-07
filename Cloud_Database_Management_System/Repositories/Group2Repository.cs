using Cloud_Database_Management_System.Interfaces.Repositories_Interfaces;
using Cloud_Database_Management_System.Models.Group_Data_Models;

namespace Cloud_Database_Management_System.Repositories
{
    public class Group2Repository : IGroupRepository
    {
        private DateTime _created;
        public Group2Repository(DateTime created)
        {
            _created = created;
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

        Task<bool> IGroupRepository.Create(Group_Data_Model group_Data_Model, DateTime _Created)
        {
            throw new NotImplementedException();
        }

        Task<List<Group_Data_Model>> IGroupRepository.Read()
        {
            throw new NotImplementedException();
        }
    }
}
