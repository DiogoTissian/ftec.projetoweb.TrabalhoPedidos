using ftec.projetoweb.TrabalhoPedidos.Aplicacao.DTO;
using ftec.projetoweb.TrabalhoPedidos.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ftec.projetoweb.TrabalhoPedidos.Aplicacao.Adapter
{
    public static class AtualizarEnderecoPedidoAdapter
    {
        public static AtualizarEnderecoPedidoDTO AtualizarEnderecoPedidoTOAtualizarEnderecoPedidoDTO(AtualizarEnderecoPedido atualizarEnderecoPedido)
        {
            return new AtualizarEnderecoPedidoDTO()
            {
                PedidoId = atualizarEnderecoPedido.PedidoId,
                CEPEnderecoEntrega = atualizarEnderecoPedido.CEPEnderecoEntrega,
                NumeroEnderecoEntrega = atualizarEnderecoPedido.NumeroEnderecoEntrega
            };
        }

        public static AtualizarEnderecoPedido AtualizarEnderecoPedidoDTOTOAtualizarEnderecoPedido(AtualizarEnderecoPedidoDTO atualizarEnderecoPedidoDTO)
        {
            return new AtualizarEnderecoPedido()
            {
                PedidoId = atualizarEnderecoPedidoDTO.PedidoId,
                CEPEnderecoEntrega = atualizarEnderecoPedidoDTO.CEPEnderecoEntrega,
                NumeroEnderecoEntrega = atualizarEnderecoPedidoDTO.NumeroEnderecoEntrega
            };
        }
    }
}
