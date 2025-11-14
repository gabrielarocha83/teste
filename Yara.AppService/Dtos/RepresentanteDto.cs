using System.Collections.Generic;
using Newtonsoft.Json;

namespace Yara.AppService.Dtos
{
    public class RepresentanteDto:BaseDto
    {
        public string CodigoSap { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
       
        [JsonIgnore]
        public ICollection<ContaClienteDto> ContaClientes { get; set; }
        [JsonIgnore]
        public virtual ICollection<UsuarioDto> Usuarios { get; set; }

        public RepresentanteDto()
        {
           
        }
    }
}