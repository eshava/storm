namespace Eshava.RP365.Models.Data.Enums
{
    public enum JobActionType
    {
        None = 0,
        Add = 1, /* Only number und string */
        Subtract = 2, /* Only number */
        Multiply = 3, /* Only number */
        Divide = 4, /* Only number */
        Replace = 5,    /* All data types */
        ReplaceSourceNotEmpty = 6,    /* Only string, nullable */
        ReplaceTargetEmpty = 7,    /* Only string, nullable */
        DynamicCode = 8
    }
}