using ABCBrasil.OpenBanking.Pagamento.Core.Models.AbcBrasilApiIntegracao.CoreCip;
using ABCBrasil.OpenBanking.Pagamento.Core.Models.Cip;

namespace ABCBrasil.OpenBanking.Pagamento.Infra.Mocks
{
    internal static class BoletoCipMock
    {
        internal static BoletoCipResult Get()
        {
            var str = @"{
                  'BoletoPagamento': {
                    'NumeroControleParticipante': 'DDA20200609001754670',
                    'ispbParticipanteRecebedorPrincipal': '28195667',
                    'ispbParticipanteRecebedorAdministrado': '28195667',
                    'numeroControleDDA': '20200609000204007829',
                    'numeroIdentificacaoTitulo': '2020041803030574280',
                    'numeroReferenciaAtualCadastroTitulo': '1587236387715000418',
                    'numeroSequenciaAtualizacaoCadastroTitulo': '1',
                    'dataHoraSituacaoTitulo': '18/04/2020',
                    'ispbParticipanteDestinatario': '28195667',
                    'codigoParticipanteDestinatario': '246',
                    'pessoaBeneficiarioOriginal': {
                      'nome': '014303 PJ COR CLIENTE 022040404',
                      'tipoDocumento': 'CNPJ',
                      'numeroDocumento': '1982584000100',
                      'listaEnderecoPessoa': {
                        'cep': '13877780',
                        'logradouro': 'RUA 014303 PJ COR CLIENTE',
                        'cidade': {
                          'nome': 'SAO JOAO DA BOA VISTA',
                          'uf': 'SP'
                        }
                      }
                    },
                    'pessoaBeneficiarioFinal': {
                      'tipoDocumento': 'CNPJ',
                      'numeroDocumento': '1982584000100',
                      'nome': '014303 PJ COR CLIENTE 022040404'
                    },
                    'pessoaPagador': {
                      'tipoDocumento': 'CPF',
                      'numeroDocumento': '21882558529',
                      'nome': 'Nome Cliente'
                    },
                    'pessoaSacadorAvalista': {
                      'tipoIdentificador': '1',
                      'identificadorAvalista': '62909973000',
                      'nome': 'Nome Sacador'
                    },
                    'numeroCodigoBarras': '24691827300000069000001112502108300284080233',
                    'numeroLinhaDigitavel': '24690001171250210830602840802330182730000006900',
                    'dataVencimentoTitulo': '01/06/2020',
                    'valorTitulo': 69,
                    'codigoEspecieTitulo': '2',
                    'listaCalculoTitulo': null,
                    'tipoValorPercentualMinimoTitulo': null,
                    'valorPercentualMinimoTitulo': null,
                    'tipoValorPercentualMaximoTitulo': null,
                    'valorPercentualMaximoTitulo': null,
                    'quantidadePagamentoParcialRegistrado': null,
                    'valorSaldoAtualPagamentoTitulo': null,
                    'quantidadeDiasProtesto': null,
                    'quantidadePagamentoParcial': null
                  },
                  'codigoMoedaCNAB': '09',
                  'indicadorBloqueioPagamento': 'N',
                  'indicadorPagamentoParcial': 'N',
                  'dataLimitePagamentoTitulo': '05/08/2020',
                  'valorAbatimentoTitulo': 0,
                  'listaJurosTitulo': {
                    'codigo': '5',
                    'data': null,
                    'valor': 0,
                    'percentual': 0
                  },
                  'listaMultaTitulo': {
                    'Codigo': '3',
                    'Data': null,
                    'Valor': 0,
                    'Percentual': 0
                  },
                  'listaDescontoTitulo': [
                    {
                      'codigo': '0',
                      'data': null,
                      'valor': 0,
                      'percentual': 0
                    }
                  ],
                  'percentualBoletoPagamento': {
                    'tipoModeloCalculo': '01',
                    'tipoAutorizacaoRecebimentoValorDivergente': '3'
                  },
                  'listaBaixaOperacional': null,
                  'situacaoTituloPagamento': '12',
                  'dataHoraDDA': '09/06/2020',
                  'dataMovimento': '09/06/2020',
                  'listaBaixaEfetiva': null
                }";
            return new BoletoCipResult() { 
                ConsultaCIP = "S",  
                Sucesso = true,
                BoletoPagamentoCompleto = new BoletoPagamentoCompleto(),
            };
        }
        
    }
}
