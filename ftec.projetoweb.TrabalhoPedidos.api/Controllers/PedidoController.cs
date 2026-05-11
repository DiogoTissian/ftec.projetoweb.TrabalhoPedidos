using ftec.projetoweb.TrabalhoPedidos.api.Adapter;
using ftec.projetoweb.TrabalhoPedidos.api.Models;
using ftec.projetoweb.TrabalhoPedidos.Aplicacao;
using ftec.projetoweb.TrabalhoPedidos.Aplicacao.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ftec.projetoweb.TrabalhoPedidos.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        PedidoAplicacao pedidoAplicacao;

        public PedidoController(IConfiguration config)
        {
            pedidoAplicacao = new PedidoAplicacao(config["strConexao"]);
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<PedidoDTO> pedidosDTO = pedidoAplicacao.BuscarPedidos();
                List<PedidoModel> pedidosModel = PedidoAdapter.PedidoDTOTOPedidoModel(pedidosDTO);
                return Ok(pedidosModel);
            }
            catch (Exception)
            {
                return BadRequest("Erro ao listar os pedidos");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                if (id == null || id == Guid.Empty)
                {
                    throw new ApplicationException();
                }

                PedidoDTO pedidoDTO = pedidoAplicacao.BuscarPedido(id);
                PedidoModel pedidoModel = PedidoAdapter.PedidoDTOTOPedidoModel(pedidoDTO);
                return Ok(pedidoModel);
            }
            catch (Exception)
            {
                return BadRequest("Erro ao retornar o pedido pesquisado");
            }
        }

        [HttpGet("GetPedidosUsuario/{usuarioId}")]
        public IActionResult GetPedidosUsuario(Guid usuarioId)
        {
            try
            {
                if (usuarioId == null || usuarioId == Guid.Empty)
                {
                    throw new ApplicationException();
                }

                List<PedidoDTO> pedidosDTO = pedidoAplicacao.BuscarPedidosUsuario(usuarioId);
                List<PedidoModel> pedidosModel = PedidoAdapter.PedidoDTOTOPedidoModel(pedidosDTO);
                return Ok(pedidosModel);
            }
            catch (Exception)
            {
                return BadRequest("Erro ao retornar os pedidos do usuario pesquisado");
            }
        }

        [HttpPost]
        public IActionResult Post(PedidoModel pedidoModel)
        {
            try
            {
                PedidoDTO pedidoDTO = PedidoAdapter.PedidoModelTOPedidoDTO(pedidoModel);
                pedidoAplicacao.AdicionarPedido(pedidoDTO);
                return Ok("Pedido inserido com sucesso");
            }
            catch (Exception)
            {
                return BadRequest("Erro ao inserir o pedido");
            }
        }

        [HttpPut]
        public IActionResult Put(PedidoModel pedidoModel)
        {
            try
            {
                PedidoDTO pedidoDTO = PedidoAdapter.PedidoModelTOPedidoDTO(pedidoModel);
                pedidoAplicacao.AlterarPedido(pedidoDTO);
                return Ok("Pedido atualizado com sucesso");
            }
            catch (Exception)
            {
                return BadRequest("Erro ao atualizar o pedido");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                if (id == null || id == Guid.Empty)
                {
                    throw new ApplicationException();
                }

                pedidoAplicacao.ExcluirPedido(id);
                return Ok("Pedido excluido com sucesso");
            }
            catch (Exception)
            {
                return BadRequest("Erro ao excluir o pedido");
            }
        }

        [HttpDelete("DeletePedidos/{usuarioid}")]
        public IActionResult DeletePedidos(Guid usuarioid)
        {
            try
            {
                if (usuarioid == null || usuarioid == Guid.Empty)
                {
                    throw new ApplicationException();
                }

                pedidoAplicacao.ExcluirPedidos(usuarioid);
                return Ok("Pedidos excluidos com sucesso");
            }
            catch (Exception)
            {
                return BadRequest("Erro ao excluir o pedido");
            }
        }
    }
}
