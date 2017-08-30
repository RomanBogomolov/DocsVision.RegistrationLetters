namespace DocsVision.RegistrationLetters.DataAccess.Sql.Sql
{
    public sealed class SqlStrings
    {
        #region Message
        public static string SelectMessageById => "SELECT id, theme, date, text, senderId FROM uf_Select_message_info_by_id(@id)";
        public static string GetMessagesInFolder => "SELECT id, theme, date, text, senderId FROM uf_Select_message_info_by_id(@id)";
        #endregion

        #region User
        public static string FindUserByEmail => "SELECT id, name, secondname, email FROM uf_Select_user_info_by_email(@email)";
        public static string FindUserById => "SELECT id, name, secondname, email FROM uf_Select_user_info_by_id(@id)";
        public static string GetAllUsers => "SELECT id, name, secondname, email FROM up_Select_users()";
        #endregion

        #region UserFolders
        public static string GetUserFolders => "SELECT id, name, parentId, level FROM Select_user_folders(@userId)";
        #endregion

        #region Roles
        public static string GetRoles => "SELECT id, name FROM uf_Select_roles()";
        #endregion
    }
}