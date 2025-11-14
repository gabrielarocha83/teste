using System;

namespace Yara.AppService.Dtos
{
    public class PropostaProrrogacaoComiteDto
    {
        public Guid ID { get; set; }

        public Guid PropostaProrrogacaoID { get; set; }
        public virtual PropostaProrrogacaoDto PropostaProrrogacao { get; set; }

        public Guid PropostaProrrogacaoComiteSolicitanteID { get; set; }
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
        public bool NovasLiberacoes { get; set; }
        public float TaxaJuros { get; set; }

        public bool Aprovado { get; set; }
        public string Comentario { get; set; }
        public bool Ativo { get; set; }

        public string EmpresaID { get; set; }
        public bool AprovadorLogado { get; set; }

        public Guid FluxoLiberacaoProrrogacaoID { get; set; }
    }
}