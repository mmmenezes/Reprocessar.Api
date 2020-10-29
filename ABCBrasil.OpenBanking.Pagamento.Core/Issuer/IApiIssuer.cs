namespace ABCBrasil.OpenBanking.Pagamento.Core.Issuer
{
    public interface IApiIssuer
    {
        string Prefix { get; }
        string Maker(Issues issue);
        string MakerCode(Issues issue);
    }
}
