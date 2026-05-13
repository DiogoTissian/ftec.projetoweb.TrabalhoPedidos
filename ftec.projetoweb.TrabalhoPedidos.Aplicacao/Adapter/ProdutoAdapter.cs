using ftec.projetoweb.TrabalhoPedidos.Aplicacao.DTO;
using ftec.projetoweb.TrabalhoPedidos.Dominio.Entidades;

namespace ftec.projetoweb.TrabalhoPedidos.Aplicacao.Adapter
{
    public static class ProdutoAdapter
    {
        public static List<Produto> ProdutoDTOTOProduto(List<ProdutoDTO> produtosDTO, Guid pedidoId)
        {
            List<Produto> produtos = new List<Produto>();

            foreach (ProdutoDTO produtoDTO in produtosDTO)
            {
                produtos.Add(new Produto()
                {
                    Id = produtoDTO.Id,
                    PedidoId = pedidoId,
                    ProdutoId = produtoDTO.ProdutoId,
                    Quantidade = produtoDTO.Quantidade
                });
            }

            return produtos;
        }

        public static List<ProdutoDTO> ProdutoTOProdutoDTO(List<Produto> produtos, Guid pedidoId)
        {
            List<ProdutoDTO> produtosDTO = new List<ProdutoDTO>();

            foreach (Produto produto in produtos)
            {
                produtosDTO.Add(new ProdutoDTO()
                {
                    Id = produto.Id,
                    PedidoId = pedidoId,
                    ProdutoId = produto.ProdutoId,
                    Quantidade = produto.Quantidade
                });
            }

            return produtosDTO;
        }
    }
}
