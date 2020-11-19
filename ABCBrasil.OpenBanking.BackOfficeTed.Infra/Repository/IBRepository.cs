using ABCBrasil.Core.Data;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Repository;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Infra.Repository
{
    public class IBRepository : DapperIBRepository, IIBRepository
    {
        public IBRepository(IConnectDataBase connectDataBase) : base(connectDataBase)
        {
        }

        public async Task<bool> Atualiza(TransferenciaInclui transferenciaInclui)
        {
            var param = new 
            {
                CD_TIPO_CLIENTE = transferenciaInclui.CdTipoCliente,
                CD_USUARIO = transferenciaInclui.CdUsuario,
                ID_TRANSACAO = transferenciaInclui.IdTransacao,
                CD_AGENCIA_DEBITO = transferenciaInclui.CdAgenciaDebito,
                DC_CONTA_DEBITO = transferenciaInclui.DcContaDebito,
                VL_VALOR = transferenciaInclui.VlValor,
                FL_APROVADO = transferenciaInclui.FlAprovado,
                CD_FINALIDADE = transferenciaInclui.CdFinalidade,
                CD_BANCO_CRED = transferenciaInclui.CdBancoCred,
                CD_AGENCIA_CRED = transferenciaInclui.CdAgenciaCred,
                DC_CONTA_CRED = transferenciaInclui.DcContaCred,
                CD_TIPO_CONTA_CRED = transferenciaInclui.CdTipoContaCred,
                CD_CNPJ_CPF_CLI_CRED = transferenciaInclui.CdCnpjCpfCliCred,
                DC_NOME_CLI_CRED = transferenciaInclui.DcNomeCliCred,
                CD_CNPJ_CPFCLI_CRED2 = transferenciaInclui.CdCnpjCpfCliCred2,
                DC_NOME_CLI_CRED2 = transferenciaInclui.DcNomeCliCred2,
                DT_TRANSFERENCIA = transferenciaInclui.DtTransferencia,
                CD_PROTOCOLO_API = transferenciaInclui.CdProtocoloApi,
                DC_URL_CALLBACK = transferenciaInclui.DcUrlCallBack,
                CD_TED_CLIENTE = transferenciaInclui.CdTedCliente
            };
            return await QueryFirstOrDefaultAsync<bool>(SqlProc.InsertTrans_Proc, param, System.Data.CommandType.StoredProcedure);
        }
    }
}
