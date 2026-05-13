using ftec.projetoweb.TrabalhoPedidos.api.Models;
using ftec.projetoweb.TrabalhoPedidos.Aplicacao.DTO;

namespace ftec.projetoweb.TrabalhoPedidos.api.Adapter
{
    public static class AtualizacaoPedidoAdapter
    {
        public static AtualizacaoPedidoModel AtualizacaoPedidoDTOTOAtualizacaoPedidoModel(AtualizacaoPedidoDTO atualizacaoPedidoDTO)
        {
            return new AtualizacaoPedidoModel()
            {
                PedidoId = atualizacaoPedidoDTO.PedidoId,
                StatusPedido = atualizacaoPedidoDTO.StatusPedido
            };
        }

        public static AtualizacaoPedidoDTO AtualizacaoPedidoModelTOAtualizacaoPedidoDTO(AtualizacaoPedidoModel atualizacaoPedidoModel)
        {
            return new AtualizacaoPedidoDTO()
            {
                PedidoId = atualizacaoPedidoModel.PedidoId,
                StatusPedido = atualizacaoPedidoModel.StatusPedido
            };
        }
    }
}
