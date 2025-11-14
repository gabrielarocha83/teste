using System;

namespace Yara.AppService.Dtos
{
    public class AnexoArquivoCobrancaWithOutFileDto : BaseDto
    {
        public Guid PropostaCobranca { get; set; }
        public Guid? ContaClienteID { get; set; }

        //0 = Proposta Prorrogação, 1 = Proposta Abono, 2 = Proposta Juridico
        public int TipoProposta { get; set; }
        
        public string NomeArquivo { get; set; }
        public string ExtensaoArquivo { get; set; }
        public bool Ativo { get; set; }
        public string Descricao { get; set; }
        public ContaClienteDto ContaCliente { get; set; }
    }
}
