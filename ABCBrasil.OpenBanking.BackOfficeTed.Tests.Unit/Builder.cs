using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Repository;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Arguments.Cip;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Arguments.Comprovante;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Arguments.Pagamento;
using Castle.Windsor;
using Moq;
using System;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Tests.Unit
{
    public class Builder
    {
        public static void ArrangeServiceIssuer(IWindsorContainer container, Issues issue)
        {
            container.Resolve<Mock<IApiIssuer>>()
                               .Setup(x => x.Maker(issue))
                               .Returns(issue.ToString());

            container.Resolve<Mock<IApiIssuer>>()
                               .Setup(x => x.MakerCode(issue))
                               .Returns(issue.ToString());
        }

        internal static class CipBuilder
        {
            internal static ConsultaCipRequest BuildCip()
            {
                return new ConsultaCipRequest() { codigoCliente = 45, codigoPagamento = "24691827300000069000001112502108300284080233" };
            }
            internal static ConsultaCipRequest BuildConsultaCipDefault()
            {
                return new ConsultaCipRequest
                {
                    codigoCliente = default,
                    codigoPagamento = default
                };
            }
        }
        internal static class PagamentoBuilder
        {
            internal static SituacaoPagamentoProtocoloRequest BuildConsultaProtocoloPagamento()
            {
                return new SituacaoPagamentoProtocoloRequest() { protocolo = "24691827300000069000001112502108300284080233" };
            }
            internal static SituacaoPagamentoProtocoloRequest BuildConsultaProtocoloPagamentoDefault()
            {
                return new SituacaoPagamentoProtocoloRequest
                {
                    protocolo = default
                };
            }
            internal static SituacaoPagamentoIdentificadorRequest BuildConsultaIdentificadorPagamento()
            {
                return new SituacaoPagamentoIdentificadorRequest() { identificadorPagamento = "ID_TESTE" };
            }
            internal static SituacaoPagamentoIdentificadorRequest BuildConsultaIdentificadorPagamentoDefault()
            {
                return new SituacaoPagamentoIdentificadorRequest
                {
                    identificadorPagamento = default
                };
            }
        }
        internal static class ComprovanteBuilder
        {
            internal static string buildComprovante()
            {
                return "cbad3766-0ecc-49f2-b14a-5112262addf1";
            }

        }

        internal static BuscaTedRequest Reprocessar64Builder()
        {
            return new BuscaTedRequest { DTINI = Convert.ToDateTime("2020-12-01"), DTFIM = Convert.ToDateTime("2020-12-20"), CDCliente = 45 };
        }

        //internal static UploadViewModel ReprocessarArquivoBuilder()
        //{
        //    return new UploadViewModel { Teds = Teds.csv };
        //}
    }
    
}
