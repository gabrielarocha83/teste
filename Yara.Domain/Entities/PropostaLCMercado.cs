using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yara.Domain.Entities
{
    public class PropostaLCMercado
    {

        public Guid ID { get; set; }
        public Guid PropostaLCID { get; set; }
        public Guid? CulturaID { get; set; }

        public PropostaLC PropostaLC { get; set; }
        public virtual Cultura Cultura { get; set; }
    }
}
