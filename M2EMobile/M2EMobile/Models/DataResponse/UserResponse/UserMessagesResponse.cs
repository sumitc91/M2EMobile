using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2EMobile.Models.DataResponse.UserResponse
{
    public class UserMessagesResponse
    {
        public string UnreadMessages { get; set; }
        public string CountLabelType { get; set; }
        public List<UserMessageList> MessageList { get; set; }
    }
}
