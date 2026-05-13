using ftec.projetoweb.TrabalhoPedidos.api.Adapter;
using ftec.projetoweb.TrabalhoPedidos.api.Models;
using ftec.projetoweb.TrabalhoPedidos.Aplicacao;
using ftec.projetoweb.TrabalhoPedidos.Aplicacao.DTO;
using ftec.projetoweb.TrabalhoPedidos.Dominio.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ftec.projetoweb.TrabalhoPedidos.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        PedidoAplicacao pedidoAplicacao;
        string url_api_produto = string.Empty;

        public PedidoController(IConfiguration config)
        {
            pedidoAplicacao = new PedidoAplicacao(config["strConexao"]);
            this.url_api_produto = config["url_api_produto"];
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<PedidoDTO> pedidosDTO = pedidoAplicacao.BuscarPedidos();

                if (pedidosDTO != null && pedidosDTO.Count > 0)
                {
                    List<PedidoModel> pedidosModel = PedidoAdapter.PedidoDTOTOPedidoModel(pedidosDTO);
                    
                    foreach (PedidoModel pedidoModel in pedidosModel)
                    {
                        foreach (ProdutoModel produtoModel in pedidoModel.ProdutosModel)
                        {
                            produtoModel.Preco += 10;

                            pedidoModel.ValorTotal += produtoModel.Quantidade * produtoModel.Preco;

                            /*
                            using HttpClient client = new HttpClient();

                            string url = $"GetPedidosUsuario/{produtoModel.ProdutoId}";

                            HttpResponseMessage response = await client.GetAsync(url);

                            if (response.IsSuccessStatusCode)
                            {
                                var options = new JsonSerializerOptions
                                {
                                    PropertyNameCaseInsensitive = true
                                };
                                string result = await response.Content.ReadAsStringAsync();
                                ProdutoModel produto = JsonSerializer.Deserialize<ProdutoModel>(result, options);

                                produtoModel.Preco = produto.Preco;

                                pedidoModel.ValorTotal += produtoModel.Quantidade * produtoModel.Preco;
                            }
                            else
                            {
                                throw new ApplicationException();
                            }*/
                        }
                    }
                    
                    return Ok(pedidosModel);
                }
                else
                {
                    throw new ApplicationException();
                }
                
            }
            catch (Exception)
            {
                return BadRequest("Erro ao listar os pedidos");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                if (id == null || id == Guid.Empty)
                {
                    throw new ApplicationException();
                }

                PedidoDTO pedidoDTO = pedidoAplicacao.BuscarPedido(id);

                if (pedidoDTO != null)
                {
                    PedidoModel pedidoModel = PedidoAdapter.PedidoDTOTOPedidoModel(pedidoDTO);

                    foreach (ProdutoModel produtoModel in pedidoModel.ProdutosModel)
                    {
                        produtoModel.Preco += 10;

                        pedidoModel.ValorTotal += produtoModel.Quantidade * produtoModel.Preco;
                        /*
                        using HttpClient client = new HttpClient();

                        string url = $"GetPedidosUsuario/{produtoModel.ProdutoId}";

                        HttpResponseMessage response = await client.GetAsync(url);

                        if (response.IsSuccessStatusCode)
                        {
                            var options = new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            };
                            string result = await response.Content.ReadAsStringAsync();
                            ProdutoModel produto = JsonSerializer.Deserialize<ProdutoModel>(result, options);

                            produtoModel.Preco = produto.Preco;

                            pedidoModel.ValorTotal += produtoModel.Quantidade * produtoModel.Preco;
                        }
                        else
                        {
                            throw new ApplicationException();
                        }*/
                    }
                    
                    return Ok(pedidoModel);
                }
                else
                {
                    throw new ApplicationException();
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao retornar o pedido pesquisado");
            }
        }

        [HttpGet("GetPedidosUsuario/{usuarioId}")]
        public async Task<IActionResult> GetPedidosUsuario(Guid usuarioId)
        {
            try
            {
                if (usuarioId == null || usuarioId == Guid.Empty)
                {
                    throw new ApplicationException();
                }

                List<PedidoDTO> pedidosDTO = pedidoAplicacao.BuscarPedidosUsuario(usuarioId);
                
                if (pedidosDTO != null && pedidosDTO.Count > 0)
                {
                    List<PedidoModel> pedidosModel = PedidoAdapter.PedidoDTOTOPedidoModel(pedidosDTO);
                    
                    foreach (PedidoModel pedidoModel in pedidosModel)
                    {
                        foreach (ProdutoModel produtoModel in pedidoModel.ProdutosModel)
                        {
                            produtoModel.Preco += 10;

                            pedidoModel.ValorTotal += produtoModel.Quantidade * produtoModel.Preco;
                            /*
                            using HttpClient client = new HttpClient();

                            string url = $"GetPedidosUsuario/{produtoModel.ProdutoId}";

                            HttpResponseMessage response = await client.GetAsync(url);

                            if (response.IsSuccessStatusCode)
                            {
                                var options = new JsonSerializerOptions
                                {
                                    PropertyNameCaseInsensitive = true
                                };
                                string result = await response.Content.ReadAsStringAsync();
                                ProdutoModel produto = JsonSerializer.Deserialize<ProdutoModel>(result, options);

                                produtoModel.Preco = produto.Preco;

                                pedidoModel.ValorTotal += produtoModel.Quantidade * produtoModel.Preco;
                            }
                            else
                            {
                                throw new ApplicationException();
                            }*/
                        }
                    }
                    
                    return Ok(pedidosModel);
                }
                else
                {
                    throw new ApplicationException();
                }
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

        [HttpPut("AtualizarStatusPedido")]
        public IActionResult PutAtualizarStatusPedido(AtualizacaoPedidoModel atualizacaoPedidoModel)
        {
            try
            {
                AtualizacaoPedidoDTO atualizacaoPedidoDTO = AtualizacaoPedidoAdapter.AtualizacaoPedidoModelTOAtualizacaoPedidoDTO(atualizacaoPedidoModel);
                pedidoAplicacao.AtualizarStatusPedido(atualizacaoPedidoDTO);
                return Ok("Pedido atualizado com sucesso");
            }
            catch (Exception)
            {
                return BadRequest("Erro ao atualizar o pedido");
            }
        }

        [HttpPut("AtualizarEnderecoEntregaPedido")]
        public IActionResult PutAtualizarEnderecoEntregaPedido(AtualizarEnderecoPedidoModel atualizarEnderecoPedidoModel)
        {
            try
            {
                AtualizarEnderecoPedidoDTO atualizarEnderecoPedidoDTO = AtualizarEnderecoPedidoAdapter.AtualizarEnderecoPedidoModelTOAtualizarEnderecoPedidoDTO(atualizarEnderecoPedidoModel);
                pedidoAplicacao.AtualizarEnderecoEntregaPedido(atualizarEnderecoPedidoDTO);
                return Ok("Endereço do pedido atualizado com sucesso");
            }
            catch (Exception)
            {
                return BadRequest("Erro ao atualizar o endereço do pedido");
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

        [HttpDelete("DeleteProdutoPedido/{pedidoId}/{produtoId}")]
        public IActionResult DeleteProdutoPedido(Guid pedidoId, Guid produtoId)
        {
            try
            {
                if (produtoId == null || produtoId == Guid.Empty)
                {
                    throw new ApplicationException();
                }

                pedidoAplicacao.DeleteProdutoPedido(pedidoId, produtoId);
                return Ok("Produto do pedido excluido com sucesso");
            }
            catch (Exception)
            {
                return BadRequest("Erro ao excluir o produto do pedido");
            }
        }
    }
}
