

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Common
{
    public class IssuerBase
    {
        protected ABCBrasilSystems System { get; set; }
        protected Module Module { get; set; }

        public string Prefix => $"{EnumHelper.GetEnumDescription(System)}.{Module.Name}";

        public string Make(string issue)
        {
            var systemID = (int)System;
            return $"{systemID}.{Module.Id}.{issue}";
        }
    }
}