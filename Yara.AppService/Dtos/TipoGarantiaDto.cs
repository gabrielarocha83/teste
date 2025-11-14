using System;

namespace Yara.AppService.Dtos
{
    public class TipoGarantiaDto:BaseDto
    {
        public TipoGarantiaDto()
        {

        }
        public TipoGarantiaDto(Guid id, string nome, bool ativo, DateTime datacriacao, Guid usuarioidcriacao)
        {
            ID = id;
            Nome = nome;
            Ativo = ativo;
            DataCriacao = datacriacao;
            UsuarioIDCriacao = usuarioidcriacao;
        }
        public string Nome { get; set; }
        public Boolean Ativo { get; set; }    
        
    }
}