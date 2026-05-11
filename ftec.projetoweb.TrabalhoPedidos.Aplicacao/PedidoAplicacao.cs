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
                if (ValidadorPedido.ValidarPedido(pedidoDTO))
                {
                    Pedido pedido = PedidoAdapter.PedidoDTOTOPedido(pedidoDTO);
                    pedidoRepositorio.Inserir(pedido);
                }
                else
                {
                    throw new ApplicationException();
                }
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
                if (ValidadorPedido.ValidarPedido(pedidoDTO))
                {
                    Pedido pedido = PedidoAdapter.PedidoDTOTOPedido(pedidoDTO);
                    pedidoRepositorio.Alterar(pedido);
                }
                else
                {
                    throw new ApplicationException();
                }
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
    }
}
