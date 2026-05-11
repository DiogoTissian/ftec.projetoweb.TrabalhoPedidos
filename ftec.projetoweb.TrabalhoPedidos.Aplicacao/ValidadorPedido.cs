using ftec.projetoweb.TrabalhoPedidos.Aplicacao.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ftec.projetoweb.TrabalhoPedidos.Aplicacao
{
    public static class ValidadorPedido
    {
        public static bool ValidarPedido(PedidoDTO pedidoDTO)
        {
            if (pedidoDTO == null)
            {
                return false;
            }
            else
            {
                if (pedidoDTO.UsuarioId == Guid.Empty)
                {
                    return false;
                }

                if (pedidoDTO.ProdutosDTO.Count == 0)
                {
                    return false;
                }
                else
                {
                    if (pedidoDTO.ProdutosDTO.Where(a => a.ProdutoId == null || a.ProdutoId == Guid.Empty).Count() > 0)
                    {
                        return false;
                    }

                    if (pedidoDTO.ProdutosDTO.Where(a => a.Quantidade < 1).Count() > 0)
                    {
                        return false;
                    }
                }

                if (pedidoDTO.StatusPedido < -1 || pedidoDTO.StatusPedido > 1)
                {
                    return false;
                }

                return true;
            }
        }
    }
}
