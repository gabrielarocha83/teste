namespace Yara.AppService.Dtos
{
    public class ParametroSistemaDto : BaseDto
    {
        public string Grupo { get; set; }
        public string Tipo { get; set; }
        public string Chave { get; set; }
        public string Valor { get; set; }
        public bool Ativo { get; set; }
        public int? Ordem { get; set; }
        public string EmpresasID { get; set; }
        public virtual EmpresasDto EmpresasDto { get; set; }
    }
}
