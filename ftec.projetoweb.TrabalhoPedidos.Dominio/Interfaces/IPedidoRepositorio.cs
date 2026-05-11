using ftec.projetoweb.TrabalhoPedidos.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ftec.projetoweb.TrabalhoPedidos.Dominio.Interfaces
{
    public interface IPedidoRepositorio
    {
        void Inserir(Pedido pedido);
        void Alterar(Pedido pedido);
        void Ecluir(Guid Id);
        void ExcluirPedidos(Guid usuarioid);
        Pedido Procurar(Guid Id);
        List<Pedido> ProcurarTodos();
        List<Pedido> BuscarPedidosUsuario(Guid usuarioId);
    }
}
