using ftec.projetoweb.TrabalhoPedidos.api.Models;
using ftec.projetoweb.TrabalhoPedidos.Aplicacao.DTO;

namespace ftec.projetoweb.TrabalhoPedidos.api.Adapter
{
    public static class PedidoAdapter
    {
        public static List<PedidoModel> PedidoDTOTOPedidoModel(List<PedidoDTO> pedidosDTO)
        {
            List<PedidoModel> pedidosModel = new List<PedidoModel>();

            foreach (PedidoDTO pedidoDTO in pedidosDTO)
            {
                pedidosModel.Add(
                    new PedidoModel()
                    {
                        Id = pedidoDTO.Id,
                        UsuarioId = pedidoDTO.UsuarioId,
                        ProdutosModel = ProdutoAdapter.ProdutoDTOTOProdutoModel(pedidoDTO.ProdutosDTO, pedidoDTO.Id),
                        DataPedido = pedidoDTO.DataPedido,
                        StatusPedido = pedidoDTO.StatusPedido
                    }
                );
            }

            return pedidosModel;
        }

        public static PedidoModel PedidoDTOTOPedidoModel(PedidoDTO pedidoDTO)
        {
            PedidoModel pedidoModel = new PedidoModel();

            pedidoModel.Id = pedidoDTO.Id;
            pedidoModel.UsuarioId = pedidoDTO.UsuarioId;
            pedidoModel.ProdutosModel = ProdutoAdapter.ProdutoDTOTOProdutoModel(pedidoDTO.ProdutosDTO, pedidoDTO.Id);
            pedidoModel.DataPedido = pedidoDTO.DataPedido;
            pedidoModel.StatusPedido = pedidoDTO.StatusPedido;

            return pedidoModel;
        }

        public static List<PedidoDTO> PedidoModelTOPedidoDTO(List<PedidoModel> pedidosModel)
        {
            List<PedidoDTO> pedidosDTO = new List<PedidoDTO>();

            foreach (PedidoModel pedidoModel in pedidosModel)
            {
                pedidosDTO.Add(
                    new PedidoDTO()
                    {
                        Id = pedidoModel.Id,
                        UsuarioId = pedidoModel.UsuarioId,
                        ProdutosDTO = ProdutoAdapter.ProdutoModelTOProdutoDTO(pedidoModel.ProdutosModel, pedidoModel.Id),
                        DataPedido = pedidoModel.DataPedido,
                        StatusPedido = pedidoModel.StatusPedido
                    }
                );
            }

            return pedidosDTO;
        }

        public static PedidoDTO PedidoModelTOPedidoDTO(PedidoModel pedidoModel)
        {
            PedidoDTO pedidoDTO = new PedidoDTO();

            pedidoDTO.Id = pedidoModel.Id;
            pedidoDTO.UsuarioId = pedidoModel.UsuarioId;
            pedidoDTO.ProdutosDTO = ProdutoAdapter.ProdutoModelTOProdutoDTO(pedidoModel.ProdutosModel, pedidoModel.Id);
            pedidoDTO.DataPedido = pedidoModel.DataPedido;
            pedidoDTO.StatusPedido = pedidoModel.StatusPedido;

            return pedidoDTO;
        }
    }
}
