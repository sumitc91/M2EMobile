using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M2EMobile.Models.Constants;
using M2EMobile.SSO;

namespace M2EMobile.Services
{
    public class UserDetailService
    {
        public async Task<string> GetUserDetailsAsync()
        {
            var SQLiteInfo = M2ESSOClient.GetUserInfoFromSQLite();

            var userDetailAsync =
                M2ESSOClient.MakePostRequestWithHeaders(
                    Constants.serverContextUrl + "/Client/GetClientDetails?userType=user", null, null, SQLiteInfo.UTMZK,
                    SQLiteInfo.TokenId, SQLiteInfo.UTMZV);
            return await userDetailAsync;
        }

        public async Task<string> GetUserAllTaskDetailsAsync()
        {
            var SQLiteInfo = M2ESSOClient.GetUserInfoFromSQLite();

            var userAllTaskDetailAsync =
                M2ESSOClient.MakePostRequestWithHeaders(
                    Constants.serverContextUrl + "/User/GetAllTemplateInformation", null, null, SQLiteInfo.UTMZK,
                    SQLiteInfo.TokenId, SQLiteInfo.UTMZV);
            return await userAllTaskDetailAsync;
        }
    }
}
