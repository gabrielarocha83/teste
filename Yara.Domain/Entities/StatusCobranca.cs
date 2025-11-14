using System.Collections.Generic;

namespace Yara.Domain.Entities
{
    public class StatusCobranca : Base
    {
        public bool Ativo { get; set; }
        public string Descricao { get; set; }
        public bool CobrancaEfetiva { get; set; }
        public bool Padrao { get; set; }
        public bool NaoCobranca { get; set; }
        public bool ContaExposicao { get; set; }
        public bool BloqueioOrdem { get; set; }
        public ICollection<Titulo> Titulos { get; set; }

        public StatusCobranca()
        {
            CobrancaEfetiva = false;
            Padrao = false;
            NaoCobranca = false;
            ContaExposicao = false;
            BloqueioOrdem = false;
        }
    }
}
