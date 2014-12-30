using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2EMobile.Models.DataWrapper
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string KeepMeSignedInCheckBox { get; set; }
        public string Type { get; set; }
    }
}
