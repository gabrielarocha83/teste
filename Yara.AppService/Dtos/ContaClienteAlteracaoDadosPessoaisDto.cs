using System;

namespace Yara.AppService.Dtos
{
    public class ContaClienteAlteracaoDadosPessoaisDto : BaseDto
    {
        public Guid TipoClienteID { get; set; }
        public Guid SegmentoID { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string CEP { get; set; }
        public string Endereco { get; set; }
        public string Email { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Pais { get; set; }
        public string Telefone { get; set; }
        public bool AdiantamentoLC { get; set; }
        public bool ClientePremium { get; set; }
        public bool TOP10 { get; set; }
        public bool LiberacaoManual { get; set; }
        public DateTime DataNascimentoFundacao { get; set; }
        public Guid? PropostaAlcadaID { get; set; }
        public string PropostaAlcadaStatusID { get; set; }
    }
}