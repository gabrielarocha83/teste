using System;
using Newtonsoft.Json;

namespace Yara.AppService.Dtos
{
    public class PropostaAbonoComiteDto
    {
        public Guid ID { get; set; }

        public Guid PropostaAbonoID { get; set; }
        public virtual PropostaAbonoDto PropostaAbono { get; set; }

        public Guid PropostaAbonoComiteSolicitanteID { get; set; }
        public string NomeSolicitante { get; set; }

        public DateTime DataCriacao { get; set; }

        public int Nivel { get; set; }

        public int Round { get; set; }

        public string NomePerfil { get; set; }
        public Guid PerfilID { get; set; }

        public string NomeUsuario { get; set; }
        public Guid UsuarioID { get; set; }

        public DateTime? DataAcao { get; set; }

        public decimal ValorDe { get; set; }
        public decimal ValorAte { get; set; }

        // Campos específicos
        public bool ConceitoH { get; set; }

        public bool Aprovado { get; set; }
        public string Comentario { get; set; }
        public bool Ativo { get; set; }

        public string EmpresaID { get; set; }
        public bool AprovadorLogado { get; set; }
        
        public Guid FluxoLiberacaoAbonoID { get; set; }
    }
}