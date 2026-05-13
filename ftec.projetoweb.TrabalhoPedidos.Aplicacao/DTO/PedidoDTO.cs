using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ftec.projetoweb.TrabalhoPedidos.Aplicacao.DTO
{
    public class PedidoDTO
    {
        public PedidoDTO()
        {
            this.Id = Guid.Empty;
            this.UsuarioId = Guid.Empty;
            this.ProdutosDTO = new List<ProdutoDTO>();
            this.DataPedido = DateTime.MinValue;
            this.StatusPedido = 0;
            this.CEPEnderecoEntrega = string.Empty;
            this.NumeroEnderecoEntrega = string.Empty;
        }

        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public List<ProdutoDTO> ProdutosDTO { get; set; }
        public DateTime DataPedido { get; set; }
        public int StatusPedido { get; set; }
        public string CEPEnderecoEntrega { get; set; }
        public string NumeroEnderecoEntrega { get; set; }
    }
}
