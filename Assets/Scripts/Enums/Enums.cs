
using System;

public static class Enums
{
    public static string FormatEnumString(Enum targetEnum)
    {
        string unformatedString = targetEnum.ToString();
        string formatedString = "";

        for (int i = 0; i < unformatedString.Length; i++)
        {
            if (char.IsUpper(unformatedString[i]))
                formatedString += " ";

            formatedString += unformatedString[i];
        }

        return formatedString;
    }
}