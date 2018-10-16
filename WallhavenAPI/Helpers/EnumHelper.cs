using System;
using System.Collections.Generic;
using System.Text;

namespace WallhavenAPI.Helpers
{
    public static class EnumHelper
    {
        public static string ToBitString<T>(this T value) where T : struct, IConvertible
        {
            var type = typeof(T);
            if (!type.IsEnum)
                throw new ArgumentException("T must be an enumerable type");

            return Convert.ToString((byte)(object)value, 2).PadLeft(Enum.GetValues(type).Length, '0');
        }
    }
}
