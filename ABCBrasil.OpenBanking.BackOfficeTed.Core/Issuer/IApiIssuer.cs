namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer
{
    public interface IApiIssuer
    {
        string Prefix { get; }
        string Maker(Issues issue);
        string MakerCode(Issues issue);
    }
}
