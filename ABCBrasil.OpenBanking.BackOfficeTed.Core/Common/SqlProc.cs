namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Common
{
    public static class SqlProc
    {

        //Select
        public const string ValidaCliente_Proc = "SPR_VALIDAR_USUARIO_SEL";
        public const string BuscaContasCliente_Proc = "SPR_CONTA_CLIENTE_SEL";
        public const string BuscaPagamento_Proc = "SPR_PAGAMENTO_EXISTE_SEL";
        public const string BuscaTedsReprocessar_Proc = "SPR_REPROCESSA_TED_API_SEL";
        public const string FindClientPagination_Proc = "SPR_CLIENT_TEMPLATE_PAG_SEL";
        public const string FindClient_Proc = "SPR_CLIENT_TEMPLATE_SEL";
        public const string FindClientDocument_Proc = "SPR_CLIENT_TEMPLATE_SEL_DOCUMENT";

        public const string BuscaPagamentoPorProtocolo_Proc = "SPR_PAGAMENTO_SITUACAO_PROTOCOLO_SEL";
        public const string BuscaPagamentoPorIdentificador_Proc = "SPR_PAGAMENTO_SITUACAO_IDENTIFICADOR_SEL";

        //Insert
        public const string InsertClient_Proc = "SPR_CLIENT_TEMPLATE_INS";
        public const string InsertTeds_Proc = "SPR_REPROCESSA_TED_API_INS";
        public const string InsertTrans_Proc = "SPR_TRANSFERENCIA_API_INCLUI";


        //Update
        public const string UpdateClient_Proc = "SPR_CLIENT_TEMPLATE_UPD";
        public const string UpdateSituacaoPagamento_Proc = "SPR_PAGAMENTO_SITUACAO_UPD";
        public const string UpdateSituacaoPagamentoCallbackCore_Proc = "SPR_CALLBACK_CORE_PAGAMENTO_SITUACAO_UPD";
        public const string UpdateTrans_Proc = "SPR_REPROCESSA_TED_API_UPD";

        //Delete
        public const string DeleteClient_Proc = "SPR_CLIENT_TEMPLATE_DEL";

    }
}
