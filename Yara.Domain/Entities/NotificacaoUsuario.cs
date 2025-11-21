using System;

namespace Yara.Domain.Entities
{
    public class NotificacaoUsuario
    {

        public Guid PropostaId { get; set; }
        public string IdProposta { get; set; }
        public Guid ContaClienteId { get; set; }
        public string CodigoPrincipal { get; set; }
        public string NomeCliente { get; set; }
        public string CG { get; set; }
        public string CTC { get; set; }
        public int ClienteNovo { get; set; }
        public int Atrasos { get; set; }
        public decimal Valor { get; set; }
        public decimal LCAtual { get; set; }
        public DateTime? VigenciaLC { get; set; }
        public string Status { get; set; }
        public int Leadtime { get; set; }
        public Guid ResponsavelId { get; set; }
        public string EmailResponsavel { get; set; }
        public string Responsavel { get; set; }
        public string Interacao { get; set; }

    }
}
