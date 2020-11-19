
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Common
{
    public static class EnumHelper
    {
        public static string GetEnumDescription(System.Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
    }
}
