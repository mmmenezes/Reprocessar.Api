

using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer
{
    public class ApiIssuer : IssuerBase, IApiIssuer
    {
        public ApiIssuer()
        {
            var environment = new AbcBrasilEnvironment();
            System = environment?.System ?? ABCBrasilSystems.None;
            Module = environment?.Module ?? default(Module);
        }

        public string Maker(Issues issue) => Make(issue.ToString().Substring(2));
        public string MakerCode(Issues issue) => issue.ToString().Substring(2);
    }
}
