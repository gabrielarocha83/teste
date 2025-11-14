using System;
using Yara.AppService.Dtos;

namespace Yara.AppService.Dtos
{
    public class EstadoDto
    {
        public Guid ID { get; set; }
        public string Nome  { get; set; }
        public string Sigla { get; set; }

        public Guid RegiaoID { get; set; }
        public RegiaoDto Regiao { get; set; }
       
    }
}