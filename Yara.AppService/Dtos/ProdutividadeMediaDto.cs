using System;

namespace Yara.AppService.Dtos
{
    public class ProdutividadeMediaDto : BaseDto
    {
        public string Nome { get; set; }
        public Guid RegiaoID { get; set; }
        public virtual RegiaoDto Regiao { get; set; }
        public bool Ativo { get; set; }
    }
}
