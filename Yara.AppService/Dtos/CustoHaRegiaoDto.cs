using System;


// ReSharper disable InconsistentNaming

namespace Yara.AppService.Dtos
{
    public class CustoHaRegiaoDto : BaseDto
    {

        public Guid CidadeID { get; set; }
        public virtual CidadeDto Cidade { get; set; }
        public decimal ValorHaCultivavel { get; set; }
        public decimal ValorHaNaoCultivavel { get; set; }
        public decimal ModuloRural { get; set; }
        public bool Ativo { get; set; }
    }
}