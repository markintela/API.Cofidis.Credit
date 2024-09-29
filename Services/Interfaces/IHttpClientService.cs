using Cofidis.Data.Models.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.Services.Interfaces
{
    public interface IHttpClientService
    {
        Task<User> GetExternalUserByNIF(string nif);

    }
}
