using System;

namespace Yara.Domain.Entities
{
    public class CulturaEstado : Base
    {
        public Guid EstadoID { get; set; }
        public virtual Estado Estado { get; set; }
        public Guid CulturaID { get; set; }
        public virtual Cultura Cultura { get; set; }
        public decimal ProdutividadeMedia { get; set; }
        public decimal Preco { get; set; }
        public decimal PorcentagemFertilizanteCusto { get; set; }
        public decimal MediaFertilizante { get; set; }
        public decimal Custo { get; set; }
        public bool Ativo { get; set; }
    }
}