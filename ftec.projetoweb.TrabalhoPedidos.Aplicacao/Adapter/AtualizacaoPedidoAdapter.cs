using ftec.projetoweb.TrabalhoPedidos.Aplicacao.DTO;
using ftec.projetoweb.TrabalhoPedidos.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ftec.projetoweb.TrabalhoPedidos.Aplicacao.Adapter
{
    public static class AtualizacaoPedidoAdapter
    {
        public static AtualizacaoPedidoDTO AtualizacaoPedidoTOAtualizacaoPedidoDTO(AtualizacaoPedido atualizacaoPedido)
        {
            return new AtualizacaoPedidoDTO()
            {
                PedidoId = atualizacaoPedido.PedidoId,
                StatusPedido = atualizacaoPedido.StatusPedido
            };
        }

        public static AtualizacaoPedido AtualizacaoPedidoDTOTOAtualizacaoPedido(AtualizacaoPedidoDTO atualizacaoPedidoDTO)
        {
            return new AtualizacaoPedido()
            {
                PedidoId = atualizacaoPedidoDTO.PedidoId,
                StatusPedido = atualizacaoPedidoDTO.StatusPedido
            };
        }
    }
}
