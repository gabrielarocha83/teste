using System;
using System.Collections.Generic;

namespace Yara.Domain.Entities
{
    public class Regiao
    {
        public Regiao()
        {

        }
        public Regiao(Guid id, string nome)
        {
            this.ID = id;
            this.Nome = nome;
        }
        public Guid ID { get; set; }
        public string Nome { get; set; }

        public ICollection<Estado> Estados { get; set; }

    }
}