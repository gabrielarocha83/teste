using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yara.AppService.Dtos
{
    public class CobrancaFiltroDto
    {
        public string Tipo { get; set; }
        public string Chave { get; set; }
        public int Dias { get; set; }
        public string Diretoria { get; set; }
    }

    public class CobrancaContaDto
    {
        public Guid? PropostaAbonoID { get; set; }
        public string PropostaAbonoStatus { get; set; }
        public Guid? PropostaJuridicoID { get; set; }
        public string PropostaJuridicoStatus { get; set; }
        public Guid? PropostaProrrogacaoID { get; set; }
        public string PropostaProrrogacaoStatus { get; set; }
    }
}
