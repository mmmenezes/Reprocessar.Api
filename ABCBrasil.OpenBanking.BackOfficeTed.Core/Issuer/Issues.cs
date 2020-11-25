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
        ce2001,//ReprocessaTed
        ce2002,//ReprocessaTed
        ce2003,//ReprocessaTed
        ce2004,
        ce2005,
        ce2006,

        ce2010,//CoreCipController - ObterSituacaoBoleto - GeneralFail
        ce2020,//PagamentosController - ObterSituacaoProtocolo - GeneralFail
        ce2021,//PagamentosController - ObterSituacaoIdentificador - GeneralFail
        ce2022,//ComprovantesController - ObterComprovante - GeneralFail


        //CONTROLLER INFORMATIONS
        ci2010, 
        ci2011, //ReprocessaTed
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
        se3001,//ReprocessaTed - TedService - Processaarquivo
        se3002,//ReprocessaTed - TedService - ProcessaTed
        se3003,//ReprocessaTed - TedService - ProcessaTed
        se3004,
        se3005,
        se3006,
        se3007,
        se3008,
        se3009,
        se3010,
        se3011,
        se3012,
        se3013,
        se3014,
        se3015,
        se3016,
        se3017,
        se3018,
        se3019,
        se3020,
        se3022,
        se3023,
        se3024,
        se3025,
        se3026,
        se3027,
        se3028,
        se3029,
        se3030,
        se3031,
        se3032,
        se3033,
        se3034,
        se3035,
        se3036,
        se3037,
        se3038,
        se3039,
        se3040,
        se3041,
        se3042,
        se3043,
        se3044,
        se3045,
        se3046,
        se3047,
        se3048,
        se3049,

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
