
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Notification;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Tracer;
using ABCBrasil.OpenBanking.API.Pagamento.Core.Entities.Model;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Enum;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Repository;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.AbcBrasilApiIntegracao.Calculo;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.AbcBrasilApiIntegracao.Tibco;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Repository;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Arguments.Pagamento;
using AutoMapper;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Services
{
    public class PagamentoService : ServiceBase, IPagamentoService
    {
        readonly IAbcBrasilApiIntegracaoService _integraApiRepository;
        readonly IMapper _mapper;
        readonly IPagamentoRepository _pagamentoRepository;
        readonly IPagamentoValida _pagamentoValida;
        

        public PagamentoService(IAbcBrasilApiIntegracaoService integraApiRepository,
            ITraceHandler traceHandler,
            INotificationHandler notificationHandler,
            IApiIssuer issuer,
            IMapper mapper,
            IPagamentoRepository pagamentoRepository,
            IPagamentoValida pagamentoValida)
            : base(issuer)
        {
            _mapper = mapper;
            _integraApiRepository = integraApiRepository;
            _pagamentoRepository = pagamentoRepository;
            _pagamentoValida = pagamentoValida;

            if(_pagamentoValida != null)
            {
                _pagamentoValida.SetNoticationHandle(notificationHandler);
                _pagamentoValida.SetTraceHandle(traceHandler);
            }
        }

        //
        // Todo Gameiro: Método pendente de finalização
        // Consumir api TIBCO
        public async Task<IncluirPagamentoResponse> Incluir(Models.Pagamento modelPagamento)
        {
            var retorno = default(IncluirPagamentoResponse);
            var sw = Stopwatch.StartNew();
            try
            {
                AddTrace("Validando o pagamento");

                //
                // Validar se a conta pertence ao cliente
                var contaCliente = await _pagamentoRepository.ContaPertenceCliente(modelPagamento);
                if (!contaCliente)
                {
                    AddNotification(Issues.se3026, Resources.FriendlyMessages.ErroServicoContaCliente, NotificationType.Error);
                    return retorno;
                }

                //
                // Validar se é dia útil, exceção e se pode processar no horário por transacao
                var pagamentoValidado = await _pagamentoValida.ValidaDataHoraProcessamento(modelPagamento);
                if(pagamentoValidado.HorarioNegado)
                {
                    AddNotification(Issues.se3033, pagamentoValidado.MensagemHorarioNegado, NotificationType.Error);
                    return retorno;
                }

                modelPagamento.PagamentoSituacao = pagamentoValidado.PagamentoSituacao;
                modelPagamento.DataPagamento = pagamentoValidado.DataPagamento;

                //
                // Extair dados do condigo de barras
                //var dadosPagto = new PaymentCodeExtractorService().Make(modelPagamento.CodigoPagamento);

                //
                // Se for tipo Boleto, calcula titulo e aplica validações 
                var retornoCalculo = default(CalculaTituloResponse);
                //if (dadosPagto.DocumentType == ABCBrasil.Core.Pagamento.Dsl.LIB.Common.DocumentType.Boleto)
                //{
                //    //
                //    // Calcula o título na autbank e monta objeto xml cip
                //    retornoCalculo = await _integraApiRepository.CalcularTituloAsync(modelPagamento);

                //    var xmlCip = default(Dda0110R1);
                //    if (retornoCalculo != null && retornoCalculo.RespostaConsultaCip?.CodErro == 0 && retornoCalculo.Sucesso)
                //    {
                //        XmlSerializer xmlSer = new XmlSerializer(typeof(Dda0110R1));
                //        using (TextReader reader = new StringReader(retornoCalculo.RespostaConsultaCip.XmlR1))
                //        {
                //            xmlCip = (Dda0110R1)xmlSer.Deserialize(reader);
                //        }

                //        //
                //        // Aplicar as validações
                //        var validacaoTitulo = _pagamentoValida.AplicaValidacoesTitulo(xmlCip);
                //        if (!validacaoTitulo)
                //        {
                //            return retorno;
                //        }

                //        //
                //        // Tipo autenticação de recebimento valor divergente
                //        if (!_pagamentoValida.ValidaTipoAutenticacaoValorDivergente(modelPagamento, retornoCalculo))
                //        {
                //            AddNotification(Issues.se3038, Resources.FriendlyMessages.ErroServicoTituloValorDivergente, NotificationType.Error);
                //            return retorno;
                //        }

                //        var boleto = new Boleto {
                //            CodigoBarras = dadosPagto.BarCode,
                //            LinhaDigitavel = dadosPagto.Ipte,
                //            IdentificacaoCanal = modelPagamento.Protocolo,
                //            CodigoEntidade = null, // todo: saber o q é
                //            NomeEntidade = null, // todo: saber o q é
                //            CpfCnpjBeneficiario = xmlCip.CNPJ_CPFBenfcrioOr,
                //            CpfCnpjBeneficiarioFinal = xmlCip.CNPJ_CPFBenfcrioFinl,
                //            CpfCnpjPagador = xmlCip.CNPJ_CPFPagdr,
                //            DataPagamento = modelPagamento.DataPagamento,
                //            DataVencimento = xmlCip.DtVencTit,
                //            NomeBeneficiario = xmlCip.NomFantsBenfcrioOr,
                //            NomeBeneficiarioFinal = xmlCip.CNPJ_CPFBenfcrioFinl,
                //            NomePagador = xmlCip.NomFantsPagdr,
                //            NumeroDocumento = xmlCip.CNPJ_CPFBenfcrioOr,
                //            TipoPagamento = modelPagamento.ValorPagamento >= 250000 ? (int)TipoPagamento.BoletoMaior : (int)TipoPagamento.BoletoMenor, // todo: criar enum com os tipos de pagamentos
                //            ValorAbatimento = retornoCalculo.CalculoValorCobrarReturn.ValorAbatimento,
                //            ValorDesconto = retornoCalculo.CalculoValorCobrarReturn.ValorDescontoCalculado,
                //            ValorJuros = retornoCalculo.CalculoValorCobrarReturn.ValorJurosCalculado,
                //            ValorMulta = retornoCalculo.CalculoValorCobrarReturn.ValorMultaCalculado,
                //            ValorPagamento = modelPagamento.ValorPagamento,
                //            ValorTitulo = retornoCalculo.CalculoValorCobrarReturn.ValorTituloOriginal
                //        };
                //        modelPagamento.Boleto.Add(boleto);
                //    }
                //    else
                //    {
                //        AddNotification(Issues.se3027, Resources.FriendlyMessages.ErroServicoInaptoNConsistido, NotificationType.Error);
                //        return retorno;
                //    }
                //}
                //else
                //{
                //    var tricon = new TriCon
                //    {
                //        CodigoBarras = dadosPagto.BarCode,
                //        LinhaDigitavel = dadosPagto.Ipte,
                //        CpfCnpjPagador = "", // todo: verificar
                //        DataPagamento = modelPagamento.DataPagamento,
                //        //DataVencimento = ; // todo: verificar
                //        IdentificacaoCanal = modelPagamento.Protocolo,
                //        NomePagador = "",
                //        TipoPagamento = dadosPagto.BarCode.StartsWith("81") ? (int)TipoPagamento.Tributo : (int)TipoPagamento.Concessionaria, // todo: 2 ou 3
                //        ValorPagamento = modelPagamento.ValorPagamento
                //    };
                //    modelPagamento.TriCon.Add(tricon);
                //}

                //
                // Fazer o mapper do objeto para o TIBCO
                var objTibco = _mapper.Map<IncluirPagamentoTibcoRequest>(modelPagamento);

                AddTrace("Tempo da opeação: {@InformationData}", sw.Elapsed);
                //
                // Checar o tempo processamento
                if (sw.Elapsed > TimeSpan.FromSeconds(25))
                {
                    AddNotification(Issues.se3025, Resources.FriendlyMessages.ErroServicoTempoLimite, NotificationType.Error);
                    return retorno;
                }

                //
                // Enviar para o Tibco processar (incluir na base e enviar para o core de processamento)
                if(await _integraApiRepository.IncluirPagamentoTibcoAsync(objTibco))
                    retorno = new IncluirPagamentoResponse {  Protocolo = modelPagamento.Protocolo, Mensagem = Resources.FriendlyMessages.ServicoInfoPagamentoSucesso };
                else
                {
                    AddNotification(Issues.se3041, Resources.FriendlyMessages.ErroServicoIncluirPagamento, NotificationType.Error);
                    return retorno;
                }














                // 0 - Envia para o tibco inserir na base

                //AddTrace("Abrindo lote de pagamento");
                //// 1 - Abre lote pagamento Api Core
                //var retornoAbrir = await _integraApiRepository.AbrirLoteCorePagamento(protocolo);

                //// 2 - Envia pagamento Api Core
                //if (retornoAbrir != null && retornoAbrir.Status)
                //{
                //    AddTrace("Enviando lote de pagamento");
                //    var retornoEnvio = await _integraApiRepository.EnviarCorePagamento(retornoAbrir, request, protocolo);

                //    if (retornoEnvio != null && retornoEnvio.Status)
                //    {
                //        AddTrace("Fechando lote de pagamento");
                //        // 3 - Fecha lote de pagamento Api Core
                //        retornoFechar = await _integraApiRepository.FecharLoteCorePagamento(retornoEnvio, protocolo);
                //    }
                //}

                //if (retornoFechar != null && retornoFechar.Status)
                //{
                //    AddTrace("Pagamento enviado");
                //    retorno = new IncluirPagamentoResponse { Protocolo = Guid.Parse(protocolo), DataEnvio = DateTime.Now, Mensagem = "Pagamento enviado com sucesso." };
                //}
            }
            catch (Exception ex)
            {
                AddError(Issues.se3014, Resources.FriendlyMessages.ErroServicoIncluirPagamento, ex);
            }
            finally
            {
                sw.Stop();
            }
            return retorno;
        }

        /// <summary>
        /// Método que checa se o pagamento existe utilizando o id unico do cliente
        /// </summary>
        /// <param name="modelExisteRequest"></param>
        /// <returns></returns>
        public async Task<PagamentoExiste> PagamentoExiste(PagamentoExisteRequest modelExisteRequest)
        {
            try
            {
                var modelExisteResponse = await _pagamentoRepository.PagamentoExiste(modelExisteRequest);

                if (!string.IsNullOrEmpty(modelExisteResponse?.Protocolo.ToString()))
                {
                    if (modelExisteRequest.Equals(modelExisteResponse))
                    {
                        return new PagamentoExiste
                        {
                            Sucesso = true,
                            IdExiste = true,
                            IncluirPagamentoResponse = new IncluirPagamentoResponse
                            {
                                Protocolo = modelExisteResponse.Protocolo,
                                Mensagem = Resources.FriendlyMessages.ServicoInfoPagamentoSucesso
                            }
                        };
                    }
                    else
                    {
                        AddNotification(Issues.se3050, String.Format(Resources.FriendlyMessages.ServicoInfoIdPagtoExiste, modelExisteResponse.IdentificacaoPagamento), NotificationType.Error);
                        return new PagamentoExiste
                        {
                            Sucesso = false,
                            IdExiste = true,
                            IncluirPagamentoResponse = null
                        };
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                AddTrace(Issues.se3051, "Erro ao validar pagamento se existe", ex);
                throw;
            }
            finally
            {
                AddTrace("Checagem idempotencia finalizada");
            }
        }

        /// <summary>
        /// Metodo para Retornar a Situação do Pagamento usando o Protocolo.
        /// </summary>
        /// <param name="situacaoPagamentoProtocolo">SituacaoPagamentoProtocoloRequest</param>
        /// <returns>SituacaoPagamentoResponse</returns>
        public async Task<SituacaoPagamentoResponse> ObterSituacaoProtocolo(SituacaoPagamentoProtocoloRequest situacaoPagamentoProtocolo)
        {
            AddTrace("Service ObterSituacaoProtocolo");
            var retorno = default(SituacaoPagamentoResponse);
            try
            {
                AddTrace("Consulta o Protocolo", situacaoPagamentoProtocolo.protocolo);

                retorno = await _pagamentoRepository.SituacaoProtocolo(situacaoPagamentoProtocolo.protocolo);
                if (retorno == null)
                {
                    AddNotification(Issues.se3045, Resources.FriendlyMessages.ErroServicoPagamentoNaoEncontrado, NotificationType.Error);
                    return retorno;
                }
                AddTrace("Situação do pagamento", retorno);
                return _mapper.Map<SituacaoPagamentoResponse>(retorno);
            }
            catch (Exception ex)
            {
                AddError(Issues.se3014, Resources.FriendlyMessages.ErroServicoConsultarPagamento, ex);
            }
            return retorno;
        }

        /// <summary>
        /// Metodo para Retornar a Situação do Pagamento  usando o Identificador Único.
        /// </summary>
        /// <param name="situacaoPagamentoIdentificador">SituacaoPagamentoIdentificadorRequest</param>
        /// <returns></returns>
        public async Task<SituacaoPagamentoResponse> ObterSituacaoIdentificador(SituacaoPagamentoIdentificadorRequest situacaoPagamentoIdentificador)
        {
            AddTrace("Service ObterSituacaoIdentificador");
            var retorno = default(SituacaoPagamentoResponse);
            try
            {
                AddTrace("Consulta identificador", situacaoPagamentoIdentificador.identificadorPagamento);

                retorno = await _pagamentoRepository.SituacaoIdentificador(situacaoPagamentoIdentificador.identificadorPagamento);
                if (retorno == null)
                {
                    AddNotification(Issues.se3044, Resources.FriendlyMessages.ErroServicoPagamentoNaoEncontrado, NotificationType.Error);
                    return retorno;
                }
                AddTrace("Situação do pagamento", retorno);
                return _mapper.Map<SituacaoPagamentoResponse>(retorno);
            }
            catch (Exception ex)
            {
                AddError(Issues.se3014, Resources.FriendlyMessages.ErroServicoConsultarPagamento, ex);
            }
            return retorno;
        }
    }
}
