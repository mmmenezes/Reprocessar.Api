using ABCBrasil.OpenBanking.BackOfficeTed.Core.Enum;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Commands
{
    public class BaseCommand
    {
        public string Name { get; set; }
        public TypeGenre Genre { get; set; }
        public string Document { get; set; }
        public TypeDocument TypeDocument { get; set; }
        public TypeMaritalStatus MaritalStatus { get; set; }
        public long Contact { get; set; }
    }
}
