using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Interfaces.Cache
{
    public interface ICipCache
    {
        Task<bool> Create(object request, string codigoDeBarras, int ttl);
    }
}
