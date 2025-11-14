using System;
using Yara.AppService.Dtos;

namespace Yara.AppService.Dtos
{
    public class UnidadeMedidaCulturaDto : BaseDto
    {
        public UnidadeMedidaCulturaDto()
        {
            
        }
        public UnidadeMedidaCulturaDto(Guid id, string nome, string sigla, bool ativo, Guid usuarioidcriacao, Guid? usuarioidalteracao)
        {
            ID = id;
            Nome = nome;
            Sigla = sigla;
            Ativo = ativo;
            DataCriacao = DateTime.Now;
            UsuarioIDCriacao = usuarioidcriacao;
            if (usuarioidalteracao.HasValue)
            {
                DataAlteracao = DateTime.Now;
                UsuarioIDAlteracao = usuarioidalteracao;
            }
        }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public bool Ativo { get; set; }
        
    }
}