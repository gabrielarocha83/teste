using System;

namespace Yara.Domain.Entities
{
    public class Feriado : Base
    {
        public DateTime DataFeriado { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
    }
}
