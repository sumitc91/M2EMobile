using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace M2EMobile.Services
{
    public class DirectoryService :IDirectoryService
    {
        public void Dispose()
        {
        }

        public Task LoginAsync(string username, string password,string userType, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                //Just for testing
                //HACK: Thread.Sleep (2000);
            });
        }
    }
}
