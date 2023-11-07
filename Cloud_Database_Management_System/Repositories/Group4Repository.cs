using Cloud_Database_Management_System.Interfaces.Repositories_Interfaces;

namespace Cloud_Database_Management_System.Repositories
{
    public class Group4Repository : IGroupRepository
    {
        private DateTime _created;
        public Group4Repository(DateTime created)
        {
            _created = created;
        }

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
