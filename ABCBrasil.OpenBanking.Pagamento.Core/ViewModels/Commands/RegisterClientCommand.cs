using System;

namespace ABCBrasil.OpenBanking.Pagamento.Core.ViewModels.Commands
{
    public class RegisterClientCommand : BaseCommand
    {
        public Guid Key { get; set; }
    }
}
