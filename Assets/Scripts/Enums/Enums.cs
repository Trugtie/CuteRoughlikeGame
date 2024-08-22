
using System;

public static class Enums
{
    public static string FormatEnumString(Enum targetEnum)
    {
        string unformatedString = targetEnum.ToString();
        string formatedString = "";

        if (unformatedString.Length <= 0)
        {
            return "Unvalid Enum Unformat String";
        }

        formatedString += unformatedString[0];

        for (int i = 1; i < unformatedString.Length; i++)
        {
            if (char.IsUpper(unformatedString[i]))
                formatedString += " ";

            formatedString += unformatedString[i];
        }

        return formatedString;
    }
}