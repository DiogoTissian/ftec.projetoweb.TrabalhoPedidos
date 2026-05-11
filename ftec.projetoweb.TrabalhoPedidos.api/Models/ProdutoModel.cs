namespace ftec.projetoweb.TrabalhoPedidos.api.Models
{
    public class ProdutoModel
    {
        public ProdutoModel()
        {
            this.Id = Guid.Empty;
            this.PedidoId = Guid.Empty;
            this.ProdutoId = Guid.Empty;
            this.Quantidade = 0;
            this.Valor = 0;
        }

        public Guid Id { get; set; }
        public Guid PedidoId { get; set; }
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
    }
}
