namespace Cloud_Database_Management_System.Interfaces.Database_Services_Interfaces
{
    public interface IGroupService
    {
        bool ProcessPostRequestDataCorrespondGroupID(object data, int tablenumber);

        Task<object> ProcessGetRequestDataCorrespondGroupID(int tablenumber);

        Task<object> ProcessGetRequestAllDataTablesCorrespondGroupID();

    }
}