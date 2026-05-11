using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ftec.projetoweb.TrabalhoPedidos.Dominio.Entidades
{
    public class Pedido
    {
        public Pedido()
        {
            this.Id = Guid.Empty;
            this.UsuarioId = Guid.Empty;
            this.Produtos = new List<Produto>();
            this.DataPedido = DateTime.MinValue;
            this.StatusPedido = 0;
        }

        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public List<Produto> Produtos { get; set; }
        public DateTime DataPedido { get; set; }
        public int StatusPedido { get; set; }
    }
}
