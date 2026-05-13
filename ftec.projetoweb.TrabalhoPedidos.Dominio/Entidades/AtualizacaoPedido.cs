using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ftec.projetoweb.TrabalhoPedidos.Dominio.Entidades
{
    public class AtualizacaoPedido
    {
        public AtualizacaoPedido()
        {
            this.PedidoId = Guid.Empty;
            this.StatusPedido = 0;
        }

        public Guid PedidoId { get; set; }
        public int StatusPedido { get; set; }
    }
}
