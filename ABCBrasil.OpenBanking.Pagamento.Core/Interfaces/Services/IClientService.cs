using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Notification;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Tracer;
using ABCBrasil.OpenBanking.Pagamento.Core.Models;
using ABCBrasil.OpenBanking.Pagamento.Core.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Interfaces.Services
{
    public interface IClientService
    {
        void SetNoticationHandle(INotificationHandler notificationHandler);
        void SetTraceHandle(ITraceHandler traceHandler);
        Task<ClientViewModel> CreateAsync(RegisterClientCommand command);
        Task<ClientViewModel> UpdateAsync(Guid key, UpdateClientCommand command);
        Task<ClientViewModel> FindAsync(Guid key);
        Task<IList<ClientViewModel>> SearchAsync(short pageNumber, short rowsPerPage);
        Task<bool> DeleteAsync(Guid key);
    }
}
