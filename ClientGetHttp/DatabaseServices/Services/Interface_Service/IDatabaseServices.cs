using ClientGetHttp.DatabaseServices.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGetHttp.DatabaseServices.Services.Interface_Service
{
    public interface IDatabaseServices
    {
        Task<List<object>> GetDataServiceAsync();
    }
}
