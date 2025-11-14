using System;

namespace Yara.AppService.Dtos
{
    public class IdadeMediaCanavialDto:BaseDto
    {
        public IdadeMediaCanavialDto()
        {
            
        }

        public IdadeMediaCanavialDto(Guid id, string nome, bool ativo, Guid usuariocriacao, Guid? usuarioalteracao)
        {
            ID = id;
            Nome = nome;
            Ativo = ativo;
            DataCriacao = DateTime.Now;
            UsuarioIDCriacao = usuariocriacao;

            if (!usuarioalteracao.HasValue) return;
            DataAlteracao = DateTime.Now;
            UsuarioIDAlteracao = usuarioalteracao;
        }

        public string Nome { get; set; }
        public bool Ativo { get; set; }
       
        
    }
}