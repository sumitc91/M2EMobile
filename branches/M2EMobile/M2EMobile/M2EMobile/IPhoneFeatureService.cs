using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2EMobile
{
    public interface IPhoneFeatureService
    {
        bool Email(string emailAddress);

        bool Browse(string websiteUrl);

        bool Tweet(string twitterName);

        bool Call(string phoneNumber);

        bool Map(string address);
    }
}
