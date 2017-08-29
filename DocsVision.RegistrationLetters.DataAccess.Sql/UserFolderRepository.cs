using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DocsVision.RegistrationLetters.DataAccess.Sql.Exceptions;
using DocsVision.RegistrationLetters.DataAccess.Sql.SQLHelper;
using DocsVision.RegistrationLetters.Log;
using DocsVision.RegistrationLetters.Model;

namespace DocsVision.RegistrationLetters.DataAccess.Sql
{
    public class UserFolderRepository : IUserFolderRepository
    {
        public void CreateFolder(Guid userId, string name, int? parentId = null)
        {
            using (var logger = new LogWrapper())
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    logger.Error(ExceptionDescribed.UserFolderNameIsNull);
                    return;
                }
                if (userId == Guid.Empty)
                {
                    logger.Error(ExceptionDescribed.GuidIsEmpty);
                    return;
                }
                try
                {
                    SqlParameter[] param =
                    {
                        new SqlParameter("@userId", userId),
                        new SqlParameter("@name", name),
                        new SqlParameter("@parentId", parentId),
                    };
                    using (var connection = new SqlConnection(SqlHelper.GetConnectionString()))
                    {
                        SqlHelper.ExecuteNonQuery(
                            connection,
                            CommandType.StoredProcedure,
                            "up_Create_user_folder",
                            param);
                    }
                }
                catch (Exception e)
                {
                    logger.Error(e.StackTrace);
                }
            }
        }

        public IEnumerable<UserFolders> GetUserFolders(Guid userId)
        {
            using (var logger = new LogWrapper())
            {
                if (userId == Guid.Empty)
                {
                    logger.Error(ExceptionDescribed.GuidIsEmpty);
                    return null;
                }
                try
                {
                    List<UserFolders> userFolders = new List<UserFolders>();
                    SqlParameter[] param ={new SqlParameter("@userId", userId)};
                    string query = "SELECT id, name, parentId, level FROM [dbo].[Select_user_folders](@userId)";
                    using (var connection = new SqlConnection(SqlHelper.GetConnectionString()))
                    {
                        SqlDataReader data = SqlHelper.ExecuteReader(
                            connection,
                            CommandType.Text,
                            query,
                            param);

                        while (data.Read())
                        {
                            userFolders.Add(new UserFolders
                            {
                                Id = (int) data["id"],
                                ParentId = data["parentId"] as int?,
                                FolderName = data["name"].ToString(),
                                Level = (int) data["level"]
                            });
                        }
                        return userFolders;
                    }
                }
                catch (Exception e)
                {
                    logger.Error(e.StackTrace);
                    return null;
                }

            }
        }

        public void DeleteFolder(int folderId, Guid userId)
        {
            using (var logger = new LogWrapper())
            {
                if (folderId < 0)
                {
                    logger.Error(ExceptionDescribed.IdIsNegative);
                    return;
                }
                try
                {
                    SqlParameter[] param =
                    {
                        new SqlParameter("@folderId", folderId),
                        new SqlParameter("@userId", userId)
                    };

                    using (var connection = new SqlConnection(SqlHelper.GetConnectionString()))
                    {
                        SqlHelper.ExecuteNonQuery(
                            connection,
                            CommandType.StoredProcedure,
                            "Delete_userfolder_by_id",
                            param);
                    }
                }
                catch (Exception e)
                {
                    logger.Error(e.StackTrace);
                }
            }
        }
    }
}