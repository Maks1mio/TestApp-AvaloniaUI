using System;
using System.Collections.Generic;

namespace TestApp_AvaloniaUI.Views
{
    public static class RangeExtensions
    {
        public static IEnumerator<int> GetEnumerator(this Range range)
        {
            for (var i = range.Start.Value; i <= range.End.Value; i++)
                yield return i;
        }
    }
}