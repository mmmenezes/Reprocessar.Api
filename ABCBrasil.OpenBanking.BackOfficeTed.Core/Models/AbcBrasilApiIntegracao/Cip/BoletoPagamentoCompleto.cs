using Newtonsoft.Json;
using System;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.AbcBrasilApiIntegracao.CoreCip
{
    /// <summary>
    /// Classe espelho Tibco, tem a função de isolar o nuget, trazendo como um serviço direto (interno) sem a dependencia do mesmo via nuget.
    /// </summary>
    public class BoletoPagamentoCompleto
    {
        public BoletoPagamento boletoPagamento { get; set; }
        public string codigoMoedaCNAB { get; set; }
        public string indicadorBloqueioPagamento { get; set; }
        public string indicadorPagamentoParcial { get; set; }
        public DateTime? dataLimitePagamentoTitulo { get; set; }
        public decimal? valorAbatimentoTitulo { get; set; }
        public ListaJurosTitulo listaJurosTitulo { get; set; }
        public ListaMultaTitulo listaMultaTitulo { get; set; }
        public ListaDescontoTitulo[] listaDescontoTitulo { get; set; }
        public PercentualBoletoPagamento percentualBoletoPagamento { get; set; }
        public ListaBaixaOperacional[] listaBaixaOperacional { get; set; }
        public string situacaoTituloPagamento { get; set; }
        public DateTime? dataHoraDDA { get; set; }
        public DateTime? dataMovimento { get; set; }
        public ListaBaixaEfetiva[] listaBaixaEfetiva { get; set; }
    }

    public class BoletoPagamento
    {
        public string numeroControleParticipante { get; set; }
        public string ispbParticipanteRecebedorPrincipal { get; set; }
        public string ispbParticipanteRecebedorAdministrado { get; set; }
        public string numeroControleDDA { get; set; }
        public string numeroIdentificacaoTitulo { get; set; }
        public string numeroReferenciaAtualCadastroTitulo { get; set; }
        public string numeroSequenciaAtualizacaoCadastroTitulo { get; set; }
        public DateTime? dataHoraSituacaoTitulo { get; set; }
        public string ispbParticipanteDestinatario { get; set; }
        public string codigoParticipanteDestinatario { get; set; }
        public PessoaBeneficiarioOriginal pessoaBeneficiarioOriginal { get; set; }
        public PessoaBeneficiarioFinal pessoaBeneficiarioFinal { get; set; }
        public PessoaPagador pessoaPagador { get; set; }
        public PessoaSacadorAvalista pessoaSacadorAvalista { get; set; }
        public string numeroCodigoBarras { get; set; }
        public string numeroLinhaDigitavel { get; set; }
        public DateTime? dataVencimentoTitulo { get; set; }
        public decimal? valorTitulo { get; set; }
        public string codigoEspecieTitulo { get; set; }
        public ListaCalculoTitulo[] listaCalculoTitulo { get; set; }
        public string tipoValorPercentualMinimoTitulo { get; set; }
        public decimal? valorPercentualMinimoTitulo { get; set; }
        public string tipoValorPercentualMaximoTitulo { get; set; }
        public decimal? valorPercentualMaximoTitulo { get; set; }
        public int? quantidadePagamentoParcialRegistrado { get; set; }
        public decimal? valorSaldoAtualPagamentoTitulo { get; set; }
        public string quantidadeDiasProtesto { get; set; }
        public string quantidadePagamentoParcial { get; set; }
    }
    public class ListaEnderecoPessoa
    {
        public string cep { get; set; }
        public string logradouro { get; set; }
        public Cidade cidade { get; set; }
    }
    public class Cidade
    {
        public string nome { get; set; }
        public string uf { get; set; }
    }

    public class PessoaBeneficiarioOriginal
    {
        public string nome { get; set; }
        public string tipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public ListaEnderecoPessoa listaEnderecoPessoa { get; set; }
    }

    public class PessoaBeneficiarioFinal
    {
        public string tipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public string nome { get; set; }
    }

    public class ListaJurosTitulo
    {
        public string codigo { get; set; }
        public string data { get; set; }
        public decimal? valor { get; set; }
        public decimal? percentual { get; set; }
    }

    public class ListaMultaTitulo
    {
        public string codigo { get; set; }
        public string data { get; set; }
        public decimal? valor { get; set; }
        public decimal? percentual { get; set; }
    }

    public class ListaDescontoTitulo
    {
        public string codigo { get; set; }
        public string data { get; set; }
        public decimal? valor { get; set; }
        public decimal? percentual { get; set; }
    }

    public class PercentualBoletoPagamento
    {
        public string tipoModeloCalculo { get; set; }
        public string tipoAutorizacaoRecebimentoValorDivergente { get; set; }
    }

    public class ListaBaixaOperacional
    {
        public string numeroIdentificacao { get; set; }
        public string numeroReferenciaAtual { get; set; }
        public string numeroSequenciaAtualizacao { get; set; }
        public DateTime? dataProcessamento { get; set; }
        public DateTime? dataHoraProcessamento { get; set; }
        public DateTime? dataHoraSituacao { get; set; }
        public decimal? valor { get; set; }
        public string numeroCodigoBarras { get; set; }
    }

    public class ListaBaixaEfetiva
    {
        public string numeroIdentificacao { get; set; }
        public string numeroReferenciaAtual { get; set; }
        public string numeroSequenciaAtualizacao { get; set; }
        public DateTime? dataProcessamento { get; set; }
        public DateTime? dataHoraProcessamento { get; set; }
        public decimal? valor { get; set; }
        public string numeroCodigoBarras { get; set; }
        public string canalPagamento { get; set; }
        public string meioPagamento { get; set; }
        public DateTime? dataHoraSituacao { get; set; }
    }

    public class PessoaPagador
    {
        public string tipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public string nome { get; set; }
    }

    public class PessoaSacadorAvalista
    {
        public string tipoIdentificador { get; set; }
        public string identificadorAvalista { get; set; }
        public string nome { get; set; }
    }

    public class ListaCalculoTitulo
    {
        public decimal? valorCalculadoJuros { get; set; }
        public decimal? valorCalculadoMulta { get; set; }
        public decimal? valorCalculadoDesconto { get; set; }
        public decimal? valorTotalCobrar { get; set; }
        public DateTime? dataValidadeTitulo { get; set; }
    }

}
