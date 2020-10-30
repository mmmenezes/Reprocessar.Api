using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Repository
{
    public interface ICipRepository
    {
        Task<bool> ValidarCrcUsuario(int CodigoCrc);
    }
}
