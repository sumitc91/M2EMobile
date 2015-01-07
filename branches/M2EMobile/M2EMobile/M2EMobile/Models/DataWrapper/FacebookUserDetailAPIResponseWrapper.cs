using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2EMobile.Models.DataWrapper
{
    public class FacebookUserDetailAPIResponseWrapper
    {
        //public FacebookUserDetailAPIResponseData data { get; set; }
    }

    public class FacebookUserDetails
    {
        public string username { get; set; }
        //Password = EncryptionClass.Md5Hash(Guid.NewGuid().ToString()),                        
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string sex { get; set; }
        public string pic_big_with_log { get; set; } 
    }
}
