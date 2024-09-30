using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cofidis.Data.Models.External;

namespace Cofidis.Manager.Interfaces
{
    public interface IHttpClientManager
    {
        Task<User> GetExternalUserByNIF(string nif);
    }
}
