using ABCBrasil.OpenBanking.BackOfficeTed.Core.Enum;
using System;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Models
{
    public class Client
    {
        public Guid Key { get; set; }
        public string Name { get; set; }
        public TypeGenre Genre { get; set; }
        public string Document { get; set; }
        public TypeDocument TypeDocument { get; set; }
        public TypeMaritalStatus MaritalStatus { get; set; }
        public long Contact { get; set; }
    }
}
