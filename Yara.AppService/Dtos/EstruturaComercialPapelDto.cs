using System;
using Newtonsoft.Json;

namespace Yara.AppService.Dtos
{
    public class EstruturaComercialPapelDto
    {
        public string ID { get; set; }
        public string Papel { get; set; }
        public bool Ativo { get; set; }
    }
}