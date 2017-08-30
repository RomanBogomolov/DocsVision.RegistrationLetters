namespace DocsVision.RegistrationLetters.DataAccess.Sql.Sql
{
    public class StoreProcedures
    {
        #region User
        public static string CreateNewUser => "up_Create_new_user";
        public static string DeleteUser => "up_Delete_user";
        #endregion

        #region UserFolders
        public static string CreateUserFolder => "up_Create_user_folder";
        public static string DeleteUserFolderById => "Delete_userfolder_by_id";
        #endregion

        #region Messages
        public static string GetUserMessagesInFolder => "up_Select_user_messages_in_folder";
        public static string DeleteUserMessages => "up_Delete_user_messages";
        public static string RemoveUserMessages => "up_Remove_user_messages";
        public static string SendMessage => "up_Send_new_Message";
        public static string UpdateStatusInMessage => "up_Update_read_in_message";
        public static string MoveUserMessageInFolder => "up_Move_user_message_in_folder";
        public static string SelectMessageByid => "up_Select_message_info_by_id";
        #endregion

        #region Roles
        public static string CreateRole => "up_Create_role";
        public static string DeleteRole => "up_Delete_role";
        public static string AddUserToRole => "up_Add_user_to_role";
        #endregion
    }
}