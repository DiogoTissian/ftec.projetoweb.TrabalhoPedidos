using ftec.projetoweb.TrabalhoPedidos.api.Models;
using ftec.projetoweb.TrabalhoPedidos.Aplicacao.DTO;

namespace ftec.projetoweb.TrabalhoPedidos.api.Adapter
{
    public static class AtualizarEnderecoPedidoAdapter
    {
        public static AtualizarEnderecoPedidoDTO AtualizarEnderecoPedidoModelTOAtualizarEnderecoPedidoDTO(AtualizarEnderecoPedidoModel atualizarEnderecoPedidoModel)
        {
            return new AtualizarEnderecoPedidoDTO()
            {
                PedidoId = atualizarEnderecoPedidoModel.PedidoId,
                CEPEnderecoEntrega = atualizarEnderecoPedidoModel.CEPEnderecoEntrega,
                NumeroEnderecoEntrega = atualizarEnderecoPedidoModel.NumeroEnderecoEntrega
            };
        }

        public static AtualizarEnderecoPedidoModel AtualizarEnderecoPedidoDTOTOAtualizarEnderecoPedidoModel(AtualizarEnderecoPedidoDTO atualizarEnderecoPedidoDTO)
        {
            return new AtualizarEnderecoPedidoModel()
            {
                PedidoId = atualizarEnderecoPedidoDTO.PedidoId,
                CEPEnderecoEntrega = atualizarEnderecoPedidoDTO.CEPEnderecoEntrega,
                NumeroEnderecoEntrega = atualizarEnderecoPedidoDTO.NumeroEnderecoEntrega
            };
        }
    }
}
