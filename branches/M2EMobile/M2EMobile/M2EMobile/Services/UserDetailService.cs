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

        public async Task<string> AcceptUserTaskByRefIdAsync(string refKey)
        {
            var SQLiteInfo = M2ESSOClient.GetUserInfoFromSQLite();

            var acceptUserTaskByRefIdResponse =
                M2ESSOClient.MakePostRequestWithHeaders(
                    Constants.serverContextUrl + "/User/AllocateThreadToUserByRefKey?refKey=" + refKey, null, null, SQLiteInfo.UTMZK,
                    SQLiteInfo.TokenId, SQLiteInfo.UTMZV);
            return await acceptUserTaskByRefIdResponse;
        }
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

        public async Task<string> GetUserActiveTaskDetailsAsync()
        {
            var SQLiteInfo = M2ESSOClient.GetUserInfoFromSQLite();

            var userAllTaskDetailAsync =
                M2ESSOClient.MakePostRequestWithHeaders(
                    Constants.serverContextUrl + "/User/GetUserActiveThreads?status=active", null, null, SQLiteInfo.UTMZK,
                    SQLiteInfo.TokenId, SQLiteInfo.UTMZV);
            return await userAllTaskDetailAsync;
        }

        public async Task<string> GetUserActiveSurveyDetailsUsingRefKeyAsync(string refKey)
        {
            var SQLiteInfo = M2ESSOClient.GetUserInfoFromSQLite();

            var userActiveSurveyTaskDetailAsync =
                M2ESSOClient.MakePostRequestWithHeaders(
                    Constants.serverContextUrl + "/User/GetTemplateSurveyQuestionsByRefKey?refKey="+refKey, null, null, SQLiteInfo.UTMZK,
                    SQLiteInfo.TokenId, SQLiteInfo.UTMZV);
            return await userActiveSurveyTaskDetailAsync;
        }
    }
}
