using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Cache
{
    public interface ICipCache
    {
        Task<bool> Create(object request, string codigoDeBarras, int ttl);
    }
}
