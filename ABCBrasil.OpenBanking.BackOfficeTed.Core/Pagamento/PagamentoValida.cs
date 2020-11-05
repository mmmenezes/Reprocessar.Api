
//using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Notification;
//using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Tracer;
//using ABCBrasil.OpenBanking.API.Pagamento.Core.Entities.Model;
//using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
//using ABCBrasil.OpenBanking.BackOfficeTed.Core.Enum;
//using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Common;
//using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
//using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
//using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.AbcBrasilApiIntegracao.Calculo;
//using ABCBrasil.OpenBanking.BackOfficeTed.Core.Services;
//using Microsoft.Extensions.Options;
//using Newtonsoft.Json;
//using System;
//using System.Diagnostics;
//using System.Threading.Tasks;

//namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Pagamento
//{
//    //public class PagamentoValida : ServiceBase, IPagamentoValida
//    public class PagamentoValida
//    {
//        //readonly IAbcBrasilApiIntegracaoService _integraApiRepository;
//        ////readonly IValidacaoPagamentoService _libService;
//        //decimal? _limiteValorAcima;

//        //public PagamentoValida(
//        //    IAbcBrasilApiIntegracaoService integraApiRepository,
//        //    ITraceHandler traceHandler,
//        //    INotificationHandler notificationHandler,
//        //    IApiIssuer issuer,
//        //    IOptions<PagamentoConfig> options)

//        //{
//        //    _integraApiRepository = integraApiRepository;
//        //    //_libService = libService;
//        //    SetConfig(options?.Value);

//        //    if (_integraApiRepository != null)
//        //    {
//        //        _integraApiRepository.SetNoticationHandle(notificationHandler);
//        //        _integraApiRepository.SetTraceHandle(traceHandler);
//        //    }
//        //}

//        //public void SetConfig(PagamentoConfig pagamentoConfig)
//        //{
//        //    _limiteValorAcima = pagamentoConfig?.LimiteValorAcima;
//        //}

//        //public async Task<Models.Pagamento> ValidaDataHoraProcessamento(Models.Pagamento pagamento)
//        //{
//        //    try
//        //    {
//        //        // 1- Verificar na api de calendário se é dia útil
//        //        var proximoDiaUtil = await _integraApiRepository.ObterProximoDiaUtil(pagamento.DataPagamento, pagamento.Protocolo.ToString());

//        //        if (proximoDiaUtil.Status && proximoDiaUtil.Result.Date.Date > DateTime.Now.Date)
//        //        {
//        //            // 2- Se o passo 1 possuir dia superior ao atual, é feriado ou fds, pagto será agendado
//        //            pagamento.DataPagamento = proximoDiaUtil.Result.Date;
//        //            pagamento.PagamentoSituacao = (int)SituacaoPagamento.AguardandoDiaPagto;
//        //        }
//        //        else if (proximoDiaUtil.Status && proximoDiaUtil.Result.Date.Date == DateTime.Now.Date)
//        //        {
//        //            // 3- Se ñ é para o futuro, consultar data de exceção (/api/v1/dataexcecao/verificadataexcecao)
//        //            var dataExcecao = await _integraApiRepository.ConsultarDataExcecao(pagamento);

//        //            if (dataExcecao.Success && dataExcecao.Data != null &&
//        //                DateTime.Now.Hour < DateTime.Parse(dataExcecao.Data.HoraInicial).Hour &&
//        //                DateTime.Now.Hour > DateTime.Parse(dataExcecao.Data.HoraFinal).Hour)
//        //            {
//        //                pagamento.HorarioNegado = true;
//        //                pagamento.MensagemHorarioNegado = string.Format(Resources.FriendlyMessages.InfoServicoForaHorario,
//        //                    dataExcecao.Data.HoraInicial,
//        //                    dataExcecao.Data.HoraFinal);
//        //                pagamento.PagamentoSituacao = (int)SituacaoPagamento.TituloNaoProcessado;
//        //            }
//        //            else
//        //            {
//        //                // 4- Validar transação por valor, se está dentro do horário para o dia (/api/v1/horas/horatransacional/{canalOrigem})
//        //                pagamento.PagamentoSituacao = (int)SituacaoPagamento.EmProcessamento;
//        //                pagamento = await ValidaHorarioTransacao(pagamento);
//        //            }
//        //        }
//        //        return pagamento;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        AddTrace(Issues.se3029, Resources.FriendlyMessages.ErroServicoIncluirPagamento, ex);
//        //        throw;
//        //    }
//        //    finally
//        //    {
//        //        AddTrace("Datas e horários checados na api de calendário");
//        //    }
//        //}

//        ////public async Task<Models.Pagamento> ValidaDataHoraProcessamento(Models.Pagamento pagamento)
//        ////{
//        ////    try
//        ////    {
//        ////        // 1- Verificar na api de calendário se é dia útil (/api/v1/dataferiados/verificadatahora)
//        ////        var dataHora = await _integraApiRepository.ConsultarDiaUtil(pagamento);

//        ////        if (dataHora.Success && !dataHora.Data.DiaHoraEncontrada)
//        ////        {
//        ////            // 2- Se o passo 1 não encontrou o dia, é feriado ou fds, validar proximo dia útil (/api/v1/dataferiados/proximo-dia-util)
//        ////            var proximoDiaUtil = await _integraApiRepository.ObterProximoDiaUtil(pagamento.DataPagamento, pagamento.Protocolo.ToString());

//        ////            if (proximoDiaUtil.Status)
//        ////            {
//        ////                pagamento.DataPagamento = proximoDiaUtil.Result.Date;
//        ////                pagamento.PagamentoSituacao = (int)SituacaoPagamento.AguardandoDiaPagto;
//        ////            }
//        ////            else
//        ////            {
//        ////                pagamento.PagamentoSituacao = (int)SituacaoPagamento.TituloNaoProcessado;
//        ////                pagamento.HorarioNegado = true;
//        ////                pagamento.MensagemHorarioNegado = Resources.FriendlyMessages.InfoServicoProximoDiaUtil;
//        ////            }
//        ////        }
//        ////        else if (dataHora.Success && dataHora.Data.DiaHoraEncontrada)
//        ////        {
//        ////            // 3- Verificar se é pro futuro, se for futuro não valida exceção e nem hora
//        ////            if(dataHora.Data.DiaRecebido.Date > DateTime.Now.Date)
//        ////            {
//        ////                pagamento.PagamentoSituacao = (int)SituacaoPagamento.AguardandoDiaPagto;
//        ////            }
//        ////            else
//        ////            {
//        ////                // 4- Se o passo 1 encontrou dia, verifica data de exceção (/api/v1/dataexcecao/verificadataexcecao)
//        ////                var dataExcecao = await _integraApiRepository.ConsultarDataExcecao(pagamento);

//        ////                if (dataExcecao.Success && dataExcecao.Data != null &&
//        ////                    DateTime.Now.Hour < DateTime.Parse(dataExcecao.Data.HoraInicial).Hour &&
//        ////                    DateTime.Now.Hour > DateTime.Parse(dataExcecao.Data.HoraFinal).Hour)
//        ////                {
//        ////                    pagamento.HorarioNegado = true;
//        ////                    pagamento.MensagemHorarioNegado = string.Format(Resources.FriendlyMessages.InfoServicoForaHorario,
//        ////                        dataExcecao.Data.HoraInicial,
//        ////                        dataExcecao.Data.HoraFinal);
//        ////                    pagamento.PagamentoSituacao = (int)SituacaoPagamento.TituloNaoProcessado;
//        ////                }
//        ////                else
//        ////                {
//        ////                    // 5- Validar transação por valor, se está dentro do horário para o dia (/api/v1/horas/horatransacional/{canalOrigem})
//        ////                    pagamento.PagamentoSituacao = (int)SituacaoPagamento.EmProcessamento;
//        ////                    pagamento = await ValidaHorarioTransacao(pagamento);
//        ////                }
//        ////            }
//        ////        }
//        ////        return pagamento;
//        ////    }
//        ////    catch (Exception ex)
//        ////    {
//        ////        AddTrace(Issues.se3029, Resources.FriendlyMessages.ErroServicoIncluirPagamento, ex);
//        ////        throw;
//        ////    }
//        ////    finally
//        ////    {
//        ////        AddTrace("Datas e horários checados na api de calendário");
//        ////    }
//        ////}

//        //public async Task<Models.Pagamento> ValidaHorarioTransacao(Models.Pagamento pagamento)
//        //{
//        //    AddTrace("Checando horários de transação na api de calendário");

//        //    if (pagamento.ValorPagamento >= _limiteValorAcima)
//        //        pagamento.TipoPagamento = TipoPagamentoValor.PAGTOA;
//        //    else
//        //        pagamento.TipoPagamento = TipoPagamentoValor.PAGTO;

//        //    var horaTransacao = await _integraApiRepository.ConsultarHoraTransacao(pagamento);

//        //    TimeSpan horaCorrente = TimeSpan.Parse(DateTime.Now.ToShortTimeString());
//        //    TimeSpan horaTransacaoInicial = TimeSpan.Parse(horaTransacao.Data.InitialHour);
//        //    TimeSpan horaTransacaoFinal = TimeSpan.Parse(horaTransacao.Data.FinalHour);

//        //    /*
//        //        -1	t1 é menor do que t2.
//        //        0	t1 é igual a t2.
//        //        1	t1 é maior que t2.
//        //    */
//        //    if (TimeSpan.Compare(horaCorrente, horaTransacaoInicial) == -1 ||
//        //        TimeSpan.Compare(horaCorrente, horaTransacaoFinal) == 1)
//        //    {
//        //        // horário não permitido
//        //        pagamento.HorarioNegado = true;
//        //        pagamento.MensagemHorarioNegado = string.Format(Resources.FriendlyMessages.InfoServicoForaHorario,
//        //                    horaTransacao.Data.InitialHour,
//        //                    horaTransacao.Data.FinalHour);
//        //    }
//        //    return pagamento;
//        //}

//        //public bool AplicaValidacoesTitulo(Dda0110R1 xmlCip)
//        //{
//        //    try
//        //    {
//        //        var retorno = true;

//        //        if (xmlCip.SitTitPgto == 12 && DateTime.Compare(xmlCip.DtLimPgtoTit, DateTime.Now.AddDays(-1)) <= 0) // data de pagamento prevista
//        //        {
//        //            // REJEITA quando ultrapassar a data limite de pagamento
//        //            AddNotification(Issues.se3035, Resources.FriendlyMessages.InfoServicoTituloExcedeDataLimite, NotificationType.Error);
//        //            retorno = false;
//        //        }

//        //        if (xmlCip.SitTitPgto != 12)
//        //        {
//        //            // REJEITA pagamento inapto
//        //            AddNotification(Issues.se3036, Resources.FriendlyMessages.InfoServicoTituloInapto, NotificationType.Error);
//        //            retorno = false;
//        //        }

//        //        if (xmlCip.SitTitPgto == 12 && xmlCip.IndrPgtoParcl.Equals("S") && xmlCip.QtdPgtoParcl == 0)
//        //        {
//        //            // REJEITA pagamento quando o limite de pagtos for esgotado
//        //            AddNotification(Issues.se3037, Resources.FriendlyMessages.InfoServicoTituloExcedeQtdPagtos, NotificationType.Error);
//        //            retorno = false;
//        //        }
//        //        return retorno;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        AddTrace(Issues.se3034, Resources.FriendlyMessages.ErroServicoAplicaValidacoesTitulo, ex);
//        //        throw;
//        //    }
//        //}

//        ////public bool ValidaTipoAutenticacaoValorDivergente(Models.Pagamento pagamento, CalculaTituloResponse retornoCalculo)
//        ////{
//        ////    var sw = Stopwatch.StartNew();
//        ////    try
//        ////    {
//        ////        CalculoResponse calculoCip = new CalculoResponse()
//        ////        {
//        ////            ValorAbatimento = retornoCalculo.CalculoValorCobrarReturn.ValorAbatimento,
//        ////            ValorDescontoCalculado = retornoCalculo.CalculoValorCobrarReturn.ValorDescontoCalculado,
//        ////            ValorJurosCalculado = retornoCalculo.CalculoValorCobrarReturn.ValorJurosCalculado,
//        ////            ValorMultaCalculado = retornoCalculo.CalculoValorCobrarReturn.ValorMultaCalculado,
//        ////            ValorTituloOriginal = retornoCalculo.CalculoValorCobrarReturn.ValorTituloOriginal,
//        ////            ValorTotalCobrar = retornoCalculo.CalculoValorCobrarReturn.ValorTotalCobrar
//        ////        };

//        ////        CipXml xmlConvertido = _libService.ConverteObjetoCipxCalculo(retornoCalculo.RespostaConsultaCip.XmlR1);
//        ////        xmlConvertido.Autbank = JsonConvert.DeserializeObject<ABCBrasil.Core.BackOfficeTed.Dsl.LIB.Models.CIP.RespostaAutbank>(JsonConvert.SerializeObject(calculoCip));
//        ////        ResultadoValidacao resultadoTpRecebAutDiv = _libService.FluxoValidacaoPagamento(new DadosPagamento() { Valor = pagamento.ValorPagamento }, xmlConvertido);

//        ////        if (resultadoTpRecebAutDiv.Valido)
//        ////            return true;
//        ////        else
//        ////        {
//        ////            AddTrace($"{resultadoTpRecebAutDiv.Codigo}-{resultadoTpRecebAutDiv.Mensagem}");
//        ////            return false;
//        ////        }

//        ////    }
//        ////    catch (Exception ex)
//        ////    {
//        ////        AddTrace(Issues.se3039, Resources.FriendlyMessages.ErroServicoTpAutcRecbtVlrDivgte, ex);
//        ////        throw;
//        ////    }
//        ////    finally
//        ////    {
//        ////        sw.Stop();
//        ////        AddTrace("Método TpAutcRecbtVlrDivgte, tempo total gasto: {@InformationData}", sw.Elapsed);
//        ////    }
//        ////}
//        //public bool AplicaValidacoesTitulo(Dda0110R1 xmlCip)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //public void SetConfig(PagamentoConfig pagamentoConfig)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //public Task<Models.Pagamento> ValidaDataHoraProcessamento(Models.Pagamento pagamento)
//        //{
//        //    throw new NotImplementedException();
//        //}
//    }
//}
