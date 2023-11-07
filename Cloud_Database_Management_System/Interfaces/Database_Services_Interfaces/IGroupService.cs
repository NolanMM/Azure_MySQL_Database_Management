namespace Cloud_Database_Management_System.Interfaces.Database_Services_Interfaces
{
    public interface IGroupService
    {
        bool ProcessPostRequestDataCorrespondGroupID(object data, string Tablename);

        bool ProcessGetRequestDataCorrespondGroupID(object data, string Tablename);

    }
}