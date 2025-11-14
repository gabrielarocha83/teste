using System;

namespace Yara.AppService.Dtos
{
    public class RegiaoDto
    {
        public RegiaoDto()
        {
            
        }
        public RegiaoDto(Guid id, string nome)
        {
            this.ID = id;
            this.Nome = nome;
        }
        public Guid ID { get; set; }
        public string Nome { get; set; }
    }
}