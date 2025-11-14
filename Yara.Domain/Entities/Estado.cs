using System;
using System.Collections.Generic;

namespace Yara.Domain.Entities
{
    public class Estado
    {
        public Guid ID { get; set; }
        public string Nome  { get; set; }
        public string Sigla { get; set; }
        public Guid RegiaoID { get; set; }


        public Regiao Regiao { get; set; }
        public ICollection<Cidade> Cidades { get; set; }

    }
}