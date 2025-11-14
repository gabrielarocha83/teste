using System;
using Newtonsoft.Json;

namespace Yara.AppService.Dtos
{
    public class LiberacaoOrdemVendaFluxoDto : BaseDto
    {
        public Guid SolicitanteFluxoID  { get; set; }
        public int Nivel { get; set; }
        public string Perfil { get; set; }
        public Guid? UsuarioID { get; set; }
        public string Usuario { get; set; }
        public Guid StatusOrdemVendasID { get; set; }
        public String StatusOrdemVendaNome { get; set; }
        public string Sigla { get; set; }
        [JsonIgnore]
        public string EmpresasId { get; set; }
        [JsonIgnore]
        public string CodigoSap { get; set; }

        public string Comentario { get; set; }
    }
}