using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Notification;
using ABCBrasil.OpenBanking.Pagamento.Core.Interfaces.Repository;
using ABCBrasil.OpenBanking.Pagamento.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.Pagamento.Core.Issuer;
using ABCBrasil.OpenBanking.Pagamento.Core.Models;
using ABCBrasil.OpenBanking.Pagamento.Core.ViewModels.Commands;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Services
{
    public class ClientService : ServiceBase, IClientService
    {
        readonly IClientRepository _clientRepository;
        readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, IApiIssuer issuer, IMapper mapper)
            : base(issuer)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }
        public async Task<ClientViewModel> CreateAsync(RegisterClientCommand command)
        {
            var result = default(ClientViewModel);
            try
            {
                AddTrace("Criando o cliente", command);

                var model = _mapper.Map<Client>(command);
                model.Key = Guid.NewGuid();
                var resulRepo = await _clientRepository.InsertAsync(model);
                if (resulRepo)
                    result = _mapper.Map<ClientViewModel>(model);

                AddTrace("Resultado da criação do cliente", result);
            }
            catch (Exception ex)
            {
                AddError(Issues.se3001, Resources.FriendlyMessages.ServiceErrorPostClient, ex);
            }
            return result;
        }
        public async Task<ClientViewModel> UpdateAsync(Guid key, UpdateClientCommand command)
        {
            var result = default(ClientViewModel);
            try
            {
                AddTrace("Localizando cliente para atualização", new { key, command });

                var model = await _clientRepository.FindAsync(key);
                if (model is null)
                {
                    AddNotification(Issues.se3008, Resources.FriendlyMessages.ServiceErrorClientNotFound, NotificationType.Error);
                }
                else
                {
                    AddTrace("Atualizando o cliente", command);

                    model = Set(model, command);

                    var resultCache = await _clientRepository.UpdateAsync(model);

                    if (resultCache)
                        result = _mapper.Map<ClientViewModel>(model);
                    else
                        result = default;

                    AddTrace("Resultado da atualização do cliente", result);
                }
            }
            catch (Exception ex)
            {
                AddError(Issues.se3005, Resources.FriendlyMessages.ServiceErrorUpdateClients, ex);
            }
            return result;
        }
        public async Task<ClientViewModel> FindAsync(Guid key)
        {
            var result = default(ClientViewModel);
            try
            {
                AddTrace($"Localizando dados do cliente: {key}");

                var model = await _clientRepository.FindAsync(key);
                if (model is null)
                {
                    AddNotification(Issues.se3007, Resources.FriendlyMessages.ServiceErrorClientNotFound, NotificationType.Error);
                }
                else
                {
                    result = _mapper.Map<ClientViewModel>(model);
                    AddTrace("Dados localizados do cliente", result);
                }
            }
            catch (Exception ex)
            {
                AddError(Issues.se3002, Resources.FriendlyMessages.ServiceErrorFindClient.Replace("[KEY]", key.ToString()), ex);
            }
            return result;
        }
        public async Task<IList<ClientViewModel>> SearchAsync(short pageNumber, short rowsPerPage)
        {
            var result = default(IList<ClientViewModel>);
            try
            {
                AddTrace("Localizando clientes");

                var models = await _clientRepository.SearchAsync(pageNumber, rowsPerPage);
                result = _mapper.Map<IList<ClientViewModel>>(models);

                AddTrace($"Clientes localizados: {result?.Count()}");
            }
            catch (Exception ex)
            {
                AddError(Issues.se3003, Resources.FriendlyMessages.ServiceErrorFindClients, ex);
            }
            return result;
        }
        public async Task<bool> DeleteAsync(Guid key)
        {
            var result = false;
            try
            {
                var model = await _clientRepository.FindAsync(key);
                if (model is null)
                {
                    AddNotification(Issues.se3006, Resources.FriendlyMessages.ServiceErrorClientNotFound, NotificationType.Error);
                }
                else 
                { 
                    AddTrace($"Excluido dados do cliente {key}");

                    result = await _clientRepository.DeleteAsync(key);

                    AddTrace("Resultado da exclusão do cliente", result);
                }
            }
            catch (Exception ex)
            {
                AddError(Issues.se3004, Resources.FriendlyMessages.ServiceErrorDeleteClients, ex);
            }
            return result;
        }
        Client Set(Client current, UpdateClientCommand command)
        {
            current.Name = !string.IsNullOrEmpty(command.Name) ? command.Name : current.Name;
            current.MaritalStatus = !string.IsNullOrEmpty(command.MaritalStatus.ToString()) ? command.MaritalStatus : current.MaritalStatus;
            current.Genre = !string.IsNullOrEmpty(command.Genre.ToString()) ? command.Genre : current.Genre;
            current.TypeDocument = !string.IsNullOrEmpty(command.TypeDocument.ToString()) ? command.TypeDocument : current.TypeDocument;
            current.Document = !string.IsNullOrEmpty(command.Document) ? command.Document : current.Document;
            current.Contact = command.Contact > 0 ? command.Contact : current.Contact;
            return current;
        }
    }
}
