using AngleSharp.Text;
using System.Linq.Expressions;

namespace Data.Service;

public static class UtilityMethods
{
    public static string GetPropertyName(Expression expression)
    {
        if (expression is UnaryExpression unary)
        {
            return GetPropertyName(unary.Operand);
        }
        else if (expression is MemberExpression member)
        {
            return member.Member.Name;
        }
        else if (expression is ParameterExpression parameter)
        {
            return parameter.Name;
        }
        else
        {
            return string.Empty;
        }
    }

    public static string FixNumberForUnifonic(string number)
    {
        number = number.Replace(" ", "");
        number = number.Replace("(", "");
        number = number.Replace(")", "");
        number = number.Replace("?", "0");
        number = number.Replace("?", "1");
        number = number.Replace("?", "2");
        number = number.Replace("?", "3");
        number = number.Replace("?", "4");
        number = number.Replace("?", "5");
        number = number.Replace("?", "6");
        number = number.Replace("?", "7");
        number = number.Replace("?", "8");
        number = number.Replace("?", "9");
        number = number.Replace("+", "");


        if (number.StartsWith("00"))
        {
            number = number.ReplaceFirst("00", "");
        }
        else if (number.StartsWith("05"))
        {
            number = number.ReplaceFirst("05", "9665");
        }
        else if (number.StartsWith("5") && number.Length <= 9)
        {
            number = number.ReplaceFirst("5", "9665");
        }

        return number;
    }

}
