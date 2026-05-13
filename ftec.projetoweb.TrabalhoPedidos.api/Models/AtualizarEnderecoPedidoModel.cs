namespace ftec.projetoweb.TrabalhoPedidos.api.Models
{
    public class AtualizarEnderecoPedidoModel
    {
        public AtualizarEnderecoPedidoModel()
        {
            this.PedidoId = Guid.Empty;
            this.CEPEnderecoEntrega = string.Empty;
            this.NumeroEnderecoEntrega = string.Empty;
        }

        public Guid PedidoId { get; set; }
        public string CEPEnderecoEntrega { get; set; }
        public string NumeroEnderecoEntrega { get; set; }
    }
}
