﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2EMobile.Models.DataResponse.UserResponse.Survey
{
    public class UserSurveyInfoSingleAnswerQueston
    {
        public string type { get; set; }
        public string subType { get; set; }
        public List<UserSurveyInfoInnerListData> data { get; set; }
    }
}