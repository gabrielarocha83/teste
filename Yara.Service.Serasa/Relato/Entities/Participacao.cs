using System.Collections.Generic;

namespace Yara.Service.Serasa.Relato.Entities
{
    public class Participacao
    {
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public List<Membro> Membros { get; set; }

        public Participacao()
        {
            Membros = new List<Membro>();
        }
    }
}