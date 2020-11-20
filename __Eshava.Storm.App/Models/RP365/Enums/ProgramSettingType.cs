namespace Eshava.RP365.Models.Data.Enums
{
    public enum ProgramSettingType
    {
        Menu = 1,
        Number = 2,           /* Integer */
        Guid = 3,
        Bool = 4,
        ComboBoxSQLGuidString = 5, /* Key->Guid, Value->String */
        TextEncrypted = 6,         /* byte Array */
        Text = 7,            /* String */
        ComboBoxStringString = 8, /* Key->String, Value->String */
        ComboBoxString = 9, /* List with Strings */
        TextCompressed = 10, /* byte Array */
    }
}