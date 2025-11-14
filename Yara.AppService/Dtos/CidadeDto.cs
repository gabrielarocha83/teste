using System;
using Yara.Domain.Entities;

namespace Yara.AppService.Dtos
{
    public class CidadeDto
    {
        public Guid ID { get; set; }
        public string Nome { get; set; }
        public Guid EstadoID { get; set; }
        public virtual EstadoDto Estado { get; set; }
       
    }
}