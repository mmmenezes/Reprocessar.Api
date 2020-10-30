using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Issuer;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer
{
    public class AbcBrasilEnvironment
    {
        const string MODULE_NAME = "API_OB_PAGAMENTO";
        const int MODULE_NUMBER = 1;
        public ABCBrasilSystems System { get; set; }
        public Module Module { get; set; }
        public string Prefix { get; set; }

        public AbcBrasilEnvironment()
        {
            System = ABCBrasilSystems.OpenBanking;
            Module = new Module { Id = MODULE_NUMBER, Name = MODULE_NAME };
        }
    }
}
