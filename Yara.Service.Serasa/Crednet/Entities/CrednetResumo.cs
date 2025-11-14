namespace Yara.Service.Serasa.Crednet.Entities
{
    public class CrednetResumo
    {
        public int Quantidade { get; set; }
        public string DescricaoQuantidade { get; set; }
        public string Discriminacao { get; set; }
        public string Periodo { get; set; }
        public decimal Valor { get; set; }

        public decimal Total => Valor;
    }
}
