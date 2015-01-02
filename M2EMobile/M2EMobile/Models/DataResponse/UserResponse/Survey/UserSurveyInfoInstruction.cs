using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M2EMobile.Models.DataResponse.UserResponse.Survey;

namespace M2EMobile.Models.DataResponse.UserResponse.Survey
{
    public class UserSurveyInfoInstruction
    {
        public string type { get; set; }
        public string subType { get; set; }
        public List<UserSurveyInfoInnerInstructionListData> data { get; set; }
    }
}