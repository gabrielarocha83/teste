using Newtonsoft.Json;
using System;

namespace Yara.AppService.Dtos
{
    public class AnexoArquivoDto : BaseDto
    {
        public Guid? ContaClienteID { get; set; }
        public Guid? PropostaLCID { get; set; }
        public Guid? PropostaLCAdicionalID { get; set; }
        public Guid AnexoID { get; set; }
        public string NomeArquivo { get; set; }
        public string ExtensaoArquivo { get; set; }
        public bool Ativo { get; set; }

        public string EmpresaID { get; set; }
        [JsonIgnore]
        public byte[] Arquivo { get; set; }

        //0 = Invalido, 1 = Não Valido, 2 = Valido
        public int? Status { get; set; }
        public DateTime? DataValidade { get; set; }
        public string Comentario { get; set; }

        public string Complemento { get; set; }
        
        [JsonIgnore]
        public ContaClienteDto ContaCliente { get; set; }
        [JsonIgnore]
        public PropostaLCDto PropostaLC { get; set; }
        [JsonIgnore]
        public PropostaLCDto PropostaLCAdicional { get; set; }
        public virtual AnexoDto Anexo { get; set; }
        public new DateTime? DataCriacao { get; set; }
    }
}
