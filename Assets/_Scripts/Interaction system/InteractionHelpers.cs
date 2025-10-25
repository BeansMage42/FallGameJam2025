using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

internal static class InteractionHelpers
{
    public static T[] GetFlags<T>(this T flagsEnumValue) where T : Enum
    {
        return Enum
            .GetValues(typeof(T))
            .Cast<T>()
            .Where(e => flagsEnumValue.HasFlag(e))
            .ToArray();
    }
}