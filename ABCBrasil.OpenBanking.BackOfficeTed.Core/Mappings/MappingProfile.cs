using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Repository;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Arguments.Pagamento;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Commands;
using AutoMapper;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<Client, ClientViewModel>();
            //CreateMap<IncluirPagamentoRequest, Models.Pagamento>();
            //CreateMap<RegisterClientCommand, Client>();
            //CreateMap<UpdateClientCommand, Client>();
            //CreateMap<IncluirPagamentoRequest, PagamentoExisteRequest>();
            //CreateMap<Models.Pagamento, IncluirPagamentoTibcoRequest>()
            //    .ForMember(x => x.Crc, opt => opt.MapFrom(y => y.CodigoCliente))
            //    .ForMember(x => x.ColigadaDebito, opt => opt.MapFrom(y => y.ColigadaDebito))
            //    .ForMember(x => x.AgenciaDebito, opt => opt.MapFrom(y => y.Agencia))
            //    .ForMember(x => x.ContaDebito, opt => opt.MapFrom(y => y.Conta))
            //    .ForMember(x => x.Canal, opt => opt.MapFrom(y => y.Canal))
            //    .ForMember(x => x.DataRequisicao, opt => opt.MapFrom(y => y.DataRequisicao)) //
            //    .ForMember(x => x.Reagendado, opt => opt.MapFrom(y => y.Reagendado))        //
            //    .ForMember(x => x.Protocolo, opt => opt.MapFrom(y => y.Protocolo))
            //    .ForMember(x => x.IdPagamento, opt => opt.MapFrom(y => y.IdentificacaoPagamento))
            //    .ForMember(x => x.PagamentoSituacao, opt => opt.MapFrom(y => y.PagamentoSituacao))
            //    .ForMember(x => x.UrlCallbackCliente, opt => opt.MapFrom(y => y.UrlNotificacao))
            //    .ForMember(x => x.ApiKeyNameCallbackCliente, opt => opt.MapFrom(y => y.ApiKeyNotificacao))
            //    .ForMember(x => x.ApiKeyValueCallbackCliente, opt => opt.MapFrom(y => y.ApivalueNotificacao))
            //    .ForMember(x => x.ApiKeyValueCallbackCliente, opt => opt.MapFrom(y => y.ApivalueNotificacao))
            //    .ForMember(x => x.ApiKeyValueCallbackCliente, opt => opt.MapFrom(y => y.ApivalueNotificacao))
            //    .ForMember(x => x.ApiKeyValueCallbackCliente, opt => opt.MapFrom(y => y.ApivalueNotificacao))
            //    .ForMember(x => x.LoginOpenBanking, opt => opt.MapFrom(y => y.LoginOpenBanking))
            //    .ForMember(x => x.Boletos, opt => opt.MapFrom(y => y.Boleto))
            //    .ForMember(x => x.Trincons, opt => opt.MapFrom(y => y.TriCon))
            //;
            //CreateMap<Models.Boleto, BoletoTibcoRequest>()
            //    .ForMember(x => x.CodigoBarras, opt => opt.MapFrom(y => y.CodigoBarras))
            //    .ForMember(x => x.CodigoEntidade, opt => opt.MapFrom(y => y.CodigoEntidade))
            //    .ForMember(x => x.CpfCnpjBeneficiario, opt => opt.MapFrom(y => y.CpfCnpjBeneficiario))
            //    .ForMember(x => x.CpfCnpjBeneficiarioFinal, opt => opt.MapFrom(y => y.CpfCnpjBeneficiarioFinal))
            //    .ForMember(x => x.CpfCnpjPagador, opt => opt.MapFrom(y => y.CpfCnpjPagador))
            //    .ForMember(x => x.DataPagamento, opt => opt.MapFrom(y => y.DataPagamento))
            //    .ForMember(x => x.DataVencimento, opt => opt.MapFrom(y => y.DataVencimento))
            //    .ForMember(x => x.IdentificacaoCanal, opt => opt.MapFrom(y => y.IdentificacaoCanal))
            //    .ForMember(x => x.LinhaDigitavel, opt => opt.MapFrom(y => y.LinhaDigitavel))
            //    .ForMember(x => x.NomeBeneficiario, opt => opt.MapFrom(y => y.NomeBeneficiario))
            //    .ForMember(x => x.NomeBeneficiarioFinal, opt => opt.MapFrom(y => y.NomeBeneficiarioFinal))
            //    .ForMember(x => x.NomeEntidade, opt => opt.MapFrom(y => y.NomeEntidade))
            //    .ForMember(x => x.NomePagador, opt => opt.MapFrom(y => y.NomePagador))
            //    .ForMember(x => x.NumeroDocumento, opt => opt.MapFrom(y => y.NumeroDocumento))
            //    .ForMember(x => x.TipoPagamento, opt => opt.MapFrom(y => y.TipoPagamento))
            //    .ForMember(x => x.ValorAbatimento, opt => opt.MapFrom(y => y.ValorAbatimento))
            //    .ForMember(x => x.ValorDesconto, opt => opt.MapFrom(y => y.ValorDesconto))
            //    .ForMember(x => x.ValorJuros, opt => opt.MapFrom(y => y.ValorJuros))
            //    .ForMember(x => x.ValorMulta, opt => opt.MapFrom(y => y.ValorMulta))
            //    .ForMember(x => x.ValorPagamento, opt => opt.MapFrom(y => y.ValorPagamento))
            //    .ForMember(x => x.ValorTitulo, opt => opt.MapFrom(y => y.ValorTitulo))
            //;
            //CreateMap<Models.TriCon, TrinConTibcoRequest>()
            //    .ForMember(x => x.CodigoBarras, opt => opt.MapFrom(y => y.CodigoBarras))
            //    .ForMember(x => x.CpfCnpjPagador, opt => opt.MapFrom(y => y.CpfCnpjPagador))
            //    .ForMember(x => x.DataPagamento, opt => opt.MapFrom(y => y.DataPagamento))
            //    .ForMember(x => x.DataVencimento, opt => opt.MapFrom(y => y.DataVencimento))
            //    .ForMember(x => x.IdentificacaoCanal, opt => opt.MapFrom(y => y.IdentificacaoCanal))
            //    .ForMember(x => x.LinhaDigitavel, opt => opt.MapFrom(y => y.LinhaDigitavel))
            //    .ForMember(x => x.NomePagador, opt => opt.MapFrom(y => y.NomePagador))
            //    .ForMember(x => x.TipoPagamento, opt => opt.MapFrom(y => y.TipoPagamento))
            //    .ForMember(x => x.ValorPagamento, opt => opt.MapFrom(y => y.ValorPagamento))
            //;



            ////callback core de pagamento
            //CreateMap<Models.SituacaoPagamento, CallbackCoreDataRequest>()
            //    .ForMember(x => x.Idenficador, opt => opt.MapFrom(y => y.Protocolo))
            //    .ForMember(x => x.CdStatus, opt => opt.MapFrom(y => y.Situacao))
            //    .ReverseMap();
        }
    }
}