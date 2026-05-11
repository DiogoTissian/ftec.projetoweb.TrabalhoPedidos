using ftec.projetoweb.TrabalhoPedidos.api.Models;
using ftec.projetoweb.TrabalhoPedidos.Aplicacao.DTO;

namespace ftec.projetoweb.TrabalhoPedidos.api.Adapter
{
    public static class ProdutoAdapter
    {
        public static List<ProdutoModel> ProdutoDTOTOProdutoModel(List<ProdutoDTO> produtosDTO, Guid pedidoId)
        {
            List<ProdutoModel> produtosModel = new List<ProdutoModel>();

            foreach (ProdutoDTO produtoDTO in produtosDTO)
            {
                produtosModel.Add(new ProdutoModel()
                {
                    Id = produtoDTO.Id,
                    PedidoId = pedidoId,
                    ProdutoId = produtoDTO.ProdutoId,
                    Quantidade = produtoDTO.Quantidade,
                    Valor = produtoDTO.Valor
                });
            }

            return produtosModel;
        }

        public static List<ProdutoDTO> ProdutoModelTOProdutoDTO(List<ProdutoModel> produtosModel, Guid pedidoId)
        {
            List<ProdutoDTO> produtosDTO = new List<ProdutoDTO>();

            foreach (ProdutoModel produtoModel in produtosModel)
            {
                produtosDTO.Add(new ProdutoDTO()
                {
                    Id= produtoModel.Id,
                    PedidoId = pedidoId,
                    ProdutoId = produtoModel.ProdutoId,
                    Quantidade = produtoModel.Quantidade,
                    Valor = produtoModel.Valor
                });
            }

            return produtosDTO;
        }
    }
}
