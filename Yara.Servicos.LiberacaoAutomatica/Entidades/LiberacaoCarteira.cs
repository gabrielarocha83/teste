using System;

namespace Yara.Servicos.LiberacaoAutomatica.Entidades
{
    public class LiberacaoCarteira
    {
        public Guid ID { get; set; }
        public Guid UsuarioIDCriacao { get; set; }
        public Guid? UsuarioIDAlteracao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public Guid ProcessamentoCarteiraID { get; set; }
        public ProcessamentoCarteira ProcessamentoCarteira { get; set; }
        public int Divisao { get; set; }
        public int Item { get; set; }
        public string Numero { get; set; }
        public bool EnviadoSAP { get; set; }

    }
}