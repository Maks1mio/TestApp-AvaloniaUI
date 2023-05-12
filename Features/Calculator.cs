using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace TestApp_AvaloniaUI.Features;

public class Calculator
{
    public static object Resultator(string e)
    {
        var expression = e.Replace(',', '.');
        var table = new DataTable();
        var result = table.Compute(expression, default);
        return result;
    }

    public static string Comma(string e, char[] _os)
    {
        if (e.Contains('.'))
            return string.Empty;

        if (!string.IsNullOrEmpty(e) && !_os.Contains(e.Last()))
        {
            var parts = e.Split(_os, StringSplitOptions.RemoveEmptyEntries);
            var lastPart = parts.Last();

            if (!lastPart.Contains(','))
            {
                return ",";
            }
        }
        return string.Empty;
    }
}