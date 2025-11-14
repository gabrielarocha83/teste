using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yara.AppService.Dtos
{
    public class LimiteYaraGalvaniDto
    {
        public Guid ContaClienteId { get; set; }

        public decimal? LimiteYara { get; set; }

        public decimal? LimiteGalvani { get; set; }
    }
}
