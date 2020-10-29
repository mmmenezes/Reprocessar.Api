namespace ABCBrasil.OpenBanking.Pagamento.Core.Models.Cip
{
    public class CipBaseResult
    {
        public bool? Sucesso { get; set; }
        public string MensagemErro { get; set; }
        public string Codigo { get; set; }
        public string Mensagem { get; set; }
        public MensagemDesenvolvedor Desenvolvedor { get; set; }
    }
}
