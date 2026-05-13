using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ftec.projetoweb.TrabalhoPedidos.Aplicacao.DTO
{
    public class AtualizarEnderecoPedidoDTO
    {
        public AtualizarEnderecoPedidoDTO()
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
