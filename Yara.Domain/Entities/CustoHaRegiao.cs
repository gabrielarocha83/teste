using System;
// ReSharper disable InconsistentNaming

namespace Yara.Domain.Entities
{
    public class CustoHaRegiao:Base
    {

        public Guid CidadeID { get; set; }
        public virtual Cidade Cidade { get; set; }
        public decimal ValorHaCultivavel { get; set; }
        public decimal ValorHaNaoCultivavel { get; set; }
        public decimal ModuloRural { get; set; }
        public bool Ativo { get; set; }
    }
}