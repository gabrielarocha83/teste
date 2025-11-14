using System.Collections.Generic;

namespace Yara.Domain.Entities
{
    public class Permissao
    {

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Processo { get; set; }
        public bool Ativo { get; set; }

        public ICollection<Grupo> Grupos { get; set; }

        public Permissao()
        {
            Grupos = new List<Grupo>();
        }
    }
}