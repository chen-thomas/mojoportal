/// Author:					
/// Created:				2007-11-03
/// Last Modified:			2011-01-19
/// 
/// The use and distribution terms for this software are covered by the 
/// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)  
/// which can be found in the file CPL.TXT at the root of this distribution.
/// By using this software in any fashion, you are agreeing to be bound by 
/// the terms of this license.
///
/// You must not remove this notice, or any other, from this software.


using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using FirebirdSql.Data.FirebirdClient;


namespace mojoPortal.Data
{
    
    public static class DBUserPage
    {
        
        public static String DBPlatform()
        {
            return "FirebirdSql";
        }

        private static String GetConnectionString()
        {
            return ConfigurationManager.AppSettings["FirebirdConnectionString"];

        }



        public static int AddUserPage(
            Guid userPageId,
            Guid siteGuid,
            int siteId,
            Guid userGuid,
            string pageName,
            string pagePath,
            int pageOrder)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("INSERT INTO mp_UserPages ");
            sqlCommand.Append("( ");
            sqlCommand.Append("UserPageID, ");
            sqlCommand.Append("SiteID, ");
            sqlCommand.Append("UserGuid, ");
            sqlCommand.Append("PageName, ");
            sqlCommand.Append("PagePath, ");
            sqlCommand.Append("PageOrder, ");
            sqlCommand.Append("SiteGuid )"); 

            sqlCommand.Append(" VALUES (");
            sqlCommand.Append("@UserPageID, ");
            sqlCommand.Append("@SiteID, ");
            sqlCommand.Append("@UserGuid, ");
            sqlCommand.Append("@PageName, ");
            sqlCommand.Append("@PagePath, ");
            sqlCommand.Append("@PageOrder, ");
            sqlCommand.Append("@SiteGuid )");
            sqlCommand.Append(";");


            FbParameter[] arParams = new FbParameter[7];

            arParams[0] = new FbParameter("@UserPageID", FbDbType.VarChar, 36);
            arParams[0].Direction = ParameterDirection.Input;
            arParams[0].Value = userPageId.ToString();

            arParams[1] = new FbParameter("@SiteID", FbDbType.Integer);
            arParams[1].Direction = ParameterDirection.Input;
            arParams[1].Value = siteId;

            arParams[2] = new FbParameter("@UserGuid", FbDbType.VarChar, 36);
            arParams[2].Direction = ParameterDirection.Input;
            arParams[2].Value = userGuid.ToString();

            arParams[3] = new FbParameter("@PageName", FbDbType.VarChar, 255);
            arParams[3].Direction = ParameterDirection.Input;
            arParams[3].Value = pageName;

            arParams[4] = new FbParameter("@PagePath", FbDbType.VarChar, 255);
            arParams[4].Direction = ParameterDirection.Input;
            arParams[4].Value = pagePath;

            arParams[5] = new FbParameter("@PageOrder", FbDbType.Integer);
            arParams[5].Direction = ParameterDirection.Input;
            arParams[5].Value = pageOrder;

            arParams[6] = new FbParameter("@SiteGuid", FbDbType.Char, 36);
            arParams[6].Direction = ParameterDirection.Input;
            arParams[6].Value = siteGuid.ToString();

            int rowsAffected = FBSqlHelper.ExecuteNonQuery(
                GetConnectionString(),
                sqlCommand.ToString(),
                arParams);

            return rowsAffected;

        }

        public static bool UpdateUserPage(
            Guid userPageId,
            string pageName,
            int pageOrder)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("UPDATE mp_UserPages ");
            sqlCommand.Append("SET ");
            sqlCommand.Append("PageName = @PageName, ");
            sqlCommand.Append("PageOrder = @PageOrder ");
            sqlCommand.Append("WHERE UserPageID = @UserPageID; ");

            FbParameter[] arParams = new FbParameter[3];

            arParams[0] = new FbParameter("@UserPageID", FbDbType.VarChar, 36);
            arParams[0].Direction = ParameterDirection.Input;
            arParams[0].Value = userPageId.ToString();

            arParams[1] = new FbParameter("@PageName", FbDbType.VarChar, 255);
            arParams[1].Direction = ParameterDirection.Input;
            arParams[1].Value = pageName;

            arParams[2] = new FbParameter("@PageOrder", FbDbType.Integer);
            arParams[2].Direction = ParameterDirection.Input;
            arParams[2].Value = pageOrder;

            int rowsAffected = FBSqlHelper.ExecuteNonQuery(
                GetConnectionString(),
                sqlCommand.ToString(),
                arParams);

            return (rowsAffected > 0);

        }

        public static bool DeleteUserPage(Guid userPageId)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("DELETE FROM mp_UserPages ");
            sqlCommand.Append("WHERE UserPageID = @UserPageID; ");

            FbParameter[] arParams = new FbParameter[1];

            arParams[0] = new FbParameter("@UserPageID", FbDbType.VarChar, 36);
            arParams[0].Direction = ParameterDirection.Input;
            arParams[0].Value = userPageId.ToString();

            int rowsAffected = FBSqlHelper.ExecuteNonQuery(
                GetConnectionString(),
                sqlCommand.ToString(),
                arParams);

            return (rowsAffected > 0);

        }

        public static bool DeleteByUser(Guid userGuid)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("DELETE FROM mp_UserPages ");
            sqlCommand.Append("WHERE UserGuid = @UserGuid; ");

            FbParameter[] arParams = new FbParameter[1];

            arParams[0] = new FbParameter("@UserGuid", FbDbType.VarChar, 36);
            arParams[0].Direction = ParameterDirection.Input;
            arParams[0].Value = userGuid.ToString();

            int rowsAffected = FBSqlHelper.ExecuteNonQuery(
                GetConnectionString(),
                sqlCommand.ToString(),
                arParams);

            return (rowsAffected > 0);

        }

        public static IDataReader GetUserPage(Guid userPageId)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("SELECT  * ");
            sqlCommand.Append("FROM	mp_UserPages ");
            sqlCommand.Append("WHERE ");
            sqlCommand.Append("UserPageID = @UserPageID ;");

            FbParameter[] arParams = new FbParameter[1];

            arParams[0] = new FbParameter("@UserPageID", FbDbType.VarChar, 36);
            arParams[0].Direction = ParameterDirection.Input;
            arParams[0].Value = userPageId.ToString();

            return FBSqlHelper.ExecuteReader(
                GetConnectionString(),
                sqlCommand.ToString(),
                arParams);

        }

        public static IDataReader SelectByUser(Guid userGuid)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("SELECT  * ");
            sqlCommand.Append("FROM	mp_UserPages ");
            sqlCommand.Append("WHERE ");
            sqlCommand.Append("UserGuid = @UserGuid ");
            sqlCommand.Append("ORDER BY PageOrder ;");

            FbParameter[] arParams = new FbParameter[1];

            arParams[0] = new FbParameter("@UserGuid", FbDbType.VarChar, 36);
            arParams[0].Direction = ParameterDirection.Input;
            arParams[0].Value = userGuid.ToString();

            return FBSqlHelper.ExecuteReader(
                GetConnectionString(),
                sqlCommand.ToString(),
                arParams);

        }

        public static int GetNextPageOrder(Guid userGuid)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("SELECT COALESCE(MAX(PageOrder),-1)  ");
            sqlCommand.Append("FROM	mp_UserPages ");

            sqlCommand.Append("WHERE UserGuid = @UserGuid ; ");

            FbParameter[] arParams = new FbParameter[1];

            arParams[0] = new FbParameter("@UserGuid", FbDbType.VarChar, 36);
            arParams[0].Direction = ParameterDirection.Input;
            arParams[0].Value = userGuid.ToString();

            int nextPageOrder = Convert.ToInt32(FBSqlHelper.ExecuteScalar(
                GetConnectionString(),
                sqlCommand.ToString(),
                arParams)) + 2;
            if (nextPageOrder == 1)
            {
                nextPageOrder = 3;
            }

            return nextPageOrder;

        }

        public static bool UpdatePageOrder(Guid userPageId, int pageOrder)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("UPDATE mp_UserPages ");
            sqlCommand.Append("SET ");
            sqlCommand.Append("PageOrder = @PageOrder ");
            sqlCommand.Append("WHERE UserPageID = @UserPageID; ");

            FbParameter[] arParams = new FbParameter[2];

            arParams[0] = new FbParameter("@UserPageID", FbDbType.VarChar, 36);
            arParams[0].Direction = ParameterDirection.Input;
            arParams[0].Value = userPageId.ToString();

            arParams[1] = new FbParameter("@PageOrder", FbDbType.Integer);
            arParams[1].Direction = ParameterDirection.Input;
            arParams[1].Value = pageOrder;

            int rowsAffected = FBSqlHelper.ExecuteNonQuery(
                GetConnectionString(),
                sqlCommand.ToString(),
                arParams);

            return (rowsAffected > 0);

        }

        

    }
}
