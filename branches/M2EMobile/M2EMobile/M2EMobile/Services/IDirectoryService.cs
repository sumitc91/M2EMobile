using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace M2EMobile.Services
{
    public interface IDirectoryService : IDisposable
    {
        Task LoginAsync(string username, string password, string userType, CancellationToken cancellationToken);
    }
}
