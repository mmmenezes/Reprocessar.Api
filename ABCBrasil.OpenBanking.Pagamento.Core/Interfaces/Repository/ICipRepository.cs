using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Interfaces.Repository
{
    public interface ICipRepository
    {
        Task<bool> ValidarCrcUsuario(int CodigoCrc);
    }
}
