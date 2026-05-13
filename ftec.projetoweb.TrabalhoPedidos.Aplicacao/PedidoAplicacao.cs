using ftec.projetoweb.TrabalhoPedidos.Aplicacao.Adapter;
using ftec.projetoweb.TrabalhoPedidos.Aplicacao.DTO;
using ftec.projetoweb.TrabalhoPedidos.Dominio.Entidades;
using ftec.projetoweb.TrabalhoPedidos.Dominio.Interfaces;
using ftec.projetoweb.TrabalhoPedidos.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ftec.projetoweb.TrabalhoPedidos.Aplicacao
{
    public class PedidoAplicacao
    {
        IPedidoRepositorio pedidoRepositorio;

        public PedidoAplicacao(string strConexao)
        {
            pedidoRepositorio = new PedidoRepositorio(strConexao);
        }

        public List<PedidoDTO> BuscarPedidos()
        {
            try
            {
                List<Pedido> pedidos = pedidoRepositorio.ProcurarTodos();
                return PedidoAdapter.PedidoTOPedidoDTO(pedidos);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PedidoDTO BuscarPedido(Guid pedidoId)
        {
            try
            {
                Pedido pedido = pedidoRepositorio.Procurar(pedidoId);
                return PedidoAdapter.PedidoTOPedidoDTO(pedido);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<PedidoDTO> BuscarPedidosUsuario(Guid usuarioId)
        {
            try
            {
                List<Pedido> pedidos = pedidoRepositorio.BuscarPedidosUsuario(usuarioId);
                return PedidoAdapter.PedidoTOPedidoDTO(pedidos);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AdicionarPedido(PedidoDTO pedidoDTO)
        {
            try
            {
                Pedido pedido = PedidoAdapter.PedidoDTOTOPedido(pedidoDTO);
                pedidoRepositorio.Inserir(pedido);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AlterarPedido(PedidoDTO pedidoDTO)
        {
            try
            {
                Pedido pedido = PedidoAdapter.PedidoDTOTOPedido(pedidoDTO);
                pedidoRepositorio.Alterar(pedido);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AtualizarStatusPedido(AtualizacaoPedidoDTO atualizacaoPedidoDTO)
        {
            try
            {
                AtualizacaoPedido atualizacaoPedido = AtualizacaoPedidoAdapter.AtualizacaoPedidoDTOTOAtualizacaoPedido(atualizacaoPedidoDTO);
                pedidoRepositorio.AtualizarStatusPedido(atualizacaoPedido);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AtualizarEnderecoEntregaPedido(AtualizarEnderecoPedidoDTO atualizarEnderecoPedidoDTO)
        {
            try
            {
                AtualizarEnderecoPedido atualizarEnderecoPedido = AtualizarEnderecoPedidoAdapter.AtualizarEnderecoPedidoDTOTOAtualizarEnderecoPedido(atualizarEnderecoPedidoDTO);
                pedidoRepositorio.AtualizarEnderecoEntregaPedido(atualizarEnderecoPedido);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ExcluirPedido(Guid pedidoId)
        {
            try
            {
                pedidoRepositorio.Ecluir(pedidoId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ExcluirPedidos(Guid usuarioid)
        {
            try
            {
                pedidoRepositorio.ExcluirPedidos(usuarioid);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteProdutoPedido(Guid pedidoId, Guid produtoId)
        {
            try
            {
                pedidoRepositorio.DeleteProdutoPedido(pedidoId, produtoId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
