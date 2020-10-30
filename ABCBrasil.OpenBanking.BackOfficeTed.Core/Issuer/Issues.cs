namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer
{
    /// <summary>
    /// Enumeradores para instrumentação de log
    /// Não repetir o código, pois cada um deve ser único e exclusivo.
    /// </summary>
    public enum Issues
    {
        None,
        //Range suggestions

        //Range from 0001 to 0100
        //VALIDATOR INFORMATIONS 
        vi0001,
        vi0002,
        vi0003,
        vi0004,
        vi0005,
        vi0006,
        vi0007,
        vi0008,
        vi0009,
        vi0010,
        vi0011,
        vi0012,
        vi0013,
        vi0014,
        vi0015,
        vi0016,
        vi0017,
        vi0018,
        vi0019,
        vi0020,
        vi0021,
        vi0022,
        vi0023,
        vi0024,
        vi0025,
        vi0026,


        vi0030,
        vi0031,
        vi0032,

        //Range from 01001 to 02000
        //FILTER ERROS
        fe1001,
        fe1002,
        fe1003,
        fe1004,

        //Range from 02001 to 03000
        //CONTROLLER ERRORS
        ce2001,
        ce2002,
        ce2003,
        ce2004,
        ce2005,
        ce2006,

        ce2010,//CoreCipController - ObterSituacaoBoleto - GeneralFail
        ce2020,//PagamentosController - ObterSituacaoProtocolo - GeneralFail
        ce2021,//PagamentosController - ObterSituacaoIdentificador - GeneralFail
        ce2022,//ComprovantesController - ObterComprovante - GeneralFail


        //CONTROLLER INFORMATIONS
        ci2010, 
        ci2011, 
        ci2012,
        ci2013,
        ci2014,
        ci2015,
        ci2016,
        ci2030,
        ci2031,
        ci2032,
        ci2033,
        ci2034,

        //Range from 03001 to 03800
        //SERVICE ERRORS
        se3001,//ClientService - CreateAsync - ServiceErrorPostClient
        se3002,//ClientService - FindAsync - ServiceErrorFindClient
        se3003,//ClientService - SearchAsync - ServiceErrorFindClients
        se3004,//ClientService - DeleteAsync - ServiceErrorDeleteClients
        se3005,//ClientService - UpdateAsync - ServiceErrorUpdateClients
        se3006,//ClientService - DeleteAsync - ServiceErrorClientNotFound
        se3007,//ClientService - FindAsync - ServiceErrorClientNotFound
        se3008,//ClientService - UpdateAsync - ServiceErrorClientNotFound
        se3009,//AbcBrasilApiIntegracaoService - ObterProximoDiaUtil - ServiceErrorExternalApi - Calendário
        se3010,//AbcBrasilApiIntegracaoService - ObterBoletoAsync - ServiceErrorExternalApi - Core CIP
        se3011,//AbcBrasilApiIntegracaoService - EnviarCorePagamento - ErroServicoEnviarParaCorePagamento
        se3012,//AbcBrasilApiIntegracaoService - AbrirLoteCorePagamento - ErroServicoAbrirCorePagamento
        se3013,//AbcBrasilApiIntegracaoService - FecharLoteCorePagamento - ErroServicoFecharCorePagamento
        se3014,//PagamentoService - Incluir - ErroServicoIncluirPagamento
        se3015,//AbcBrasilApiIntegracaoService - AbrirLoteCorePagamento - ErroServicoAbrirCorePagamento
        se3016,//AbcBrasilApiIntegracaoService - EnviarCorePagamento - ErroServicoEnviarParaCorePagamento
        se3017,//AbcBrasilApiIntegracaoService - FecharLoteCorePagamento - ErroServicoFecharCorePagamento
        se3018,//RegistroEventoService - IncluirEvento - 
        se3019,//AbcBrasilApiIntegracaoService - CalcularTituloAsync - ErroServicoCalcularPagamento
        se3020,//CipService - ObterBoletoAsync - ServiceErrorBoletoNotFound
        se3022,//CipService - ObterBoletoAsync - ServiceErrorObterSituacaoBoleto
        se3023,//CipService - ObterBoletoAsync - Erro Core Cálculo CIP
        se3024,//AbcBrasilApiIntegracaoService - ObterBoletoAsync - GeneralFail - Core CIP
        se3025,//PagamentoService - Incluir - ErroServicoTituloJaRegistrado
        se3026,//PagamentoService - Incluir - ErroServicoContaCliente
        se3027,//PagamentoService - Incluir - ErroServicoInaptoNConsistido
        se3028,//CipService - ObterBoletoAsync - ErrorValidCodigoClienteUsuario
        se3029,//PagamentoValida - ValidaDataHoraProcessamento - ErroServicoIncluirPagamento
        se3030,//IAbcBrasilApiIntegracaoService
        se3031,//IAbcBrasilApiIntegracaoService
        se3032,//IAbcBrasilApiIntegracaoService
        se3033,//PagamentoService - Incluir - ErroValidaForaHorario
        se3034,//PagamentoService - Incluir - ErroServicoAplicaValidacoesTitulo
        se3035,//PagamentoValida - AplicaValidacoesTitulo - InfoServicoTituloExcedeDataLimite
        se3036,//PagamentoValida - AplicaValidacoesTitulo - InfoServicoTituloInapto
        se3037,//PagamentoValida - AplicaValidacoesTitulo - InfoServicoTituloExcedeQtdPagtos
        se3038,//PagamentoService - Incluir - ErroServicoTituloValorDivergente
        se3039,//PagamentoValida - ValidaTipoAutenticacaoValorDivergente - ErroServicoTpAutcRecbtVlrDivgte
        se3040,//AbcBrasilApiIntegracaoService - IncluirPagamentoTibcoAsync - ???
        se3041,//PagamentoService - Incluir - ???
        se3042,//PagamentoService - ObterSituacaoProtocolo - ErroServicoPagamentoNaoEncontrado
        se3043,//PagamentoService - ObterSituacaoProtocolo - ErroServicoPagamentoNaoEncontrado
        se3044,//PagamentoService - ObterSituacaoIdentificador - ErroServicoPagamentoNaoEncontrado
        se3045,//PagamentoService - ObterSituacaoProtocolo - ErroServicoPagamentoNaoEncontrado
        se3046,//CallbackCoreService - AtualizarSituacaoPagamento - ServiceErrorCallbackCoreAtualizarSituacao Pgto não encontrado
        se3047,//CallbackCoreService - AtualizarSituacaoPagamento - ServiceErrorCallbackCoreAtualizarSituacao Pgto Exeption
        se3048,//ComprovanteService - ObterSituacaoProtocolo - Sem comprovante de pagamento de boleto neste protocolo
        se3049,//ComprovanteService - ObterSituacaoProtocolo - ErroServicoPagamentoNaoEncontrado

        se3050,//PagamentoService - PagamentoExiste - ???
        se3051,//PagamentoService - PagamentoExiste - 
        
        //Range from 03801 to 03900
        //SERVICE INFOS


        //Range from 03901 to 04000
        //SERVICE WARNINGS
        sw5001,

        //Range from 04001 to 05000
        //MIDDLEWARE ERRORS
        me4001
    }
}
