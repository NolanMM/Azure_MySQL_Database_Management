using ClientGetHttp.DatabaseServices.Services.Model;
using ClientGetHttp.DatabaseServices.Services.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGetHttp.DatabaseServices.Services.Interface_Service
{
    public interface IDatabaseServices
    {
        Task<List<Group_1_Record_Abstraction>?> GetDataServiceAsync();
    }
}
