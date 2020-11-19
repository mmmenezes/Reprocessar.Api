using System;
using System.Collections.Generic;
using System.Text;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Mappings
{
    public static class AutoMapperExtensions
    {
        public static T MapTo<T>(this object value)
        {
            return MappingProfile.Mapper.Map<T>(value);
        }
        public static IEnumerable<T> IEnumerableTo<T>(this object value)
        {
            return MappingProfile.Mapper.Map<IEnumerable<T>>(value);
        }
        public static IList<T> IListTo<T>(this object value)
        {
            return MappingProfile.Mapper.Map<IList<T>>(value);
        }
        public static List<T> ListTo<T>(this object value)
        {
            return MappingProfile.Mapper.Map<List<T>>(value);
        }
        public static ICollection<T> ICollectionTo<T>(this object value)
        {
            return MappingProfile.Mapper.Map<ICollection<T>>(value);
        }
    }
}
