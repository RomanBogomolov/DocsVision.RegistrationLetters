namespace DocsVision.RegistrationLetters.DataAccess.Sql.SqlSrtring
{
    public sealed class SqlStrings
    {
        public static string SelectMessageById => "SELECT id, theme, date, text, senderId FROM uf_Select_message_info_by_id(@id)";
        public static string GetMessagesInFolder => "SELECT id, theme, date, text, senderId FROM uf_Select_message_info_by_id(@id)";


    }
}