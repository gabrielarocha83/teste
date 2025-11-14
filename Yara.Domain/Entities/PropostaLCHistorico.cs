using System;

namespace Yara.Domain.Entities
{
    public class PropostaLCHistorico
    {
        public Guid ID { get; set; }

        public DateTime DataCriacao { get; set; }

        public Usuario Usuario { get; set; }
        public Guid UsuarioID { get; set; }

        public Guid PropostaLCID { get; set; }
        public PropostaLC PropostaLC { get; set; }

        public string PropostaLCStatusID { get; set; }
        public PropostaLCStatus PropostaLCStatus { get; set; }

        public string Descricao { get; set; }


    }
}