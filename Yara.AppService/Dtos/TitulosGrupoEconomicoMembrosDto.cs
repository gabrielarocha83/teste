using System;

namespace Yara.AppService.Dtos
{
    public class TitulosGrupoEconomicoMembrosDto
    {

        public Guid? ContaClienteId { get; set; }
        public string Documento { get; set; }
        public string CodigoPrincipal { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public Guid? TipoClienteId { get; set; }
        public Guid? SegmentoId { get; set; }
        public int? QtdVencido { get; set; }
        public int? QtdAVencer { get; set; }
        public decimal? TitulosVencidos { get; set; }
        public decimal? TitulosAVencer { get; set; }

    }
}
