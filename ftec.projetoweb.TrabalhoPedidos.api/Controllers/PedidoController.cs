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
                                return BadRequest("Buscar Pedidos - Erro ao consultar o preço do produto");
                            }
                        }
                    }
                    
                    return Ok(pedidosModel);
                }
                else
                {
                    return BadRequest("Buscar Pedidos - Erro ao listar os pedidos");
                }
                
            }
            catch (Exception)
            {
                return BadRequest("Buscar Pedidos - Erro ao listar os pedidos");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                if (id != null && id != Guid.Empty)
                {
                    PedidoDTO pedidoDTO = pedidoAplicacao.BuscarPedido(id);

                    if (pedidoDTO != null)
                    {
                        PedidoModel pedidoModel = PedidoAdapter.PedidoDTOTOPedidoModel(pedidoDTO);

                        foreach (ProdutoModel produtoModel in pedidoModel.ProdutosModel)
                        {
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
                                return BadRequest("Buscar Pedidos Específico - Erro ao consultar o preço do produto");
                            }
                        }

                        return Ok(pedidoModel);
                    }
                    else
                    {
                        return BadRequest("Buscar Pedido Específico - Erro ao retornar o pedido pesquisado do banco de dados");
                    }
                }
                else
                {
                    return BadRequest("Buscar Pedido Específico - Erro ao retornar o pedido pesquisado. Id do pedido informado inválido");
                }
            }
            catch (Exception)
            {
                return BadRequest("Buscar Pedido Específico - Erro ao retornar o pedido pesquisado");
            }
        }

        [HttpGet("GetPedidosUsuario/{usuarioId}")]
        public async Task<IActionResult> GetPedidosUsuario(Guid usuarioId)
        {
            try
            {
                if (usuarioId != null && usuarioId != Guid.Empty)
                {
                    List<PedidoDTO> pedidosDTO = pedidoAplicacao.BuscarPedidosUsuario(usuarioId);

                    if (pedidosDTO != null && pedidosDTO.Count > 0)
                    {
                        List<PedidoModel> pedidosModel = PedidoAdapter.PedidoDTOTOPedidoModel(pedidosDTO);

                        foreach (PedidoModel pedidoModel in pedidosModel)
                        {
                            foreach (ProdutoModel produtoModel in pedidoModel.ProdutosModel)
                            {
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
                                    return BadRequest("Buscar Pedidos Usuário - Erro ao consultar o preço do produto");
                                }
                            }
                        }

                        return Ok(pedidosModel);
                    }
                    else
                    {
                        return BadRequest("Buscar Pedidos Usuário - Erro ao retornar os pedidos do usuário do banco de dados");
                    }
                }
                else
                {
                    return BadRequest("Buscar Pedidos Usuário - Id do usuário inválido");
                }
            }
            catch (Exception)
            {
                return BadRequest("Buscar Pedidos Usuário - Erro ao retornar os pedidos do usuario pesquisado");
            }
        }

        [HttpPost]
        public IActionResult Post(PedidoModel pedidoModel)
        {
            try
            {
                if (pedidoModel != null)
                {
                    if (pedidoModel.UsuarioId == null || pedidoModel.UsuarioId == Guid.Empty ||
                        string.IsNullOrEmpty(pedidoModel.CEPEnderecoEntrega) || string.IsNullOrEmpty(pedidoModel.NumeroEnderecoEntrega))
                    {
                        return BadRequest("Inserção Pedido - Erro ao inserir o pedido. Dados recebidos inválidos");
                    }

                    foreach (ProdutoModel produto in pedidoModel.ProdutosModel)
                    {
                        if (produto.ProdutoId == null || produto.ProdutoId == Guid.Empty || produto.Quantidade <= 0)
                        {
                            return BadRequest("Inserção Pedido - Erro ao inserir o pedido. Dados do produto recebidos inválidos");
                        }
                    }

                    PedidoDTO pedidoDTO = PedidoAdapter.PedidoModelTOPedidoDTO(pedidoModel);
                    pedidoAplicacao.AdicionarPedido(pedidoDTO);
                    return Ok("Inserção Pedido - Pedido inserido com sucesso");
                }
                else
                {
                    return BadRequest("Inserção Pedido - Erro ao inserir o pedido. Objeto recebido corrompido");
                }
            }
            catch (Exception)
            {
                return BadRequest("Inserção Pedido - Erro ao inserir o pedido");
            }
        }

        [HttpPut]
        public IActionResult Put(PedidoModel pedidoModel)
        {
            try
            {
                if (pedidoModel != null)
                {
                    if (pedidoModel.Id == null || pedidoModel.Id == Guid.Empty ||
                        pedidoModel.StatusPedido < -1 || pedidoModel.StatusPedido > 1)
                    {
                        return BadRequest("Alteração Pedido - Erro ao atualizar o pedido. Dados recebidos inválidos");
                    }

                    foreach (ProdutoModel produto in pedidoModel.ProdutosModel)
                    {
                        if (produto.ProdutoId == null || produto.ProdutoId == Guid.Empty || produto.Quantidade <= 0)
                        {
                            return BadRequest("Alteração Pedido - Erro ao atualizar o pedido. Dados do produto recebidos inválidos");
                        }
                    }

                    PedidoDTO pedidoDTO = PedidoAdapter.PedidoModelTOPedidoDTO(pedidoModel);
                    pedidoAplicacao.AlterarPedido(pedidoDTO);
                    return Ok("Alteração Pedido - Pedido atualizado com sucesso");
                }
                else
                {
                    return BadRequest("Alteração Pedido - Erro ao atualizar o pedido. Objeto recebido corrompido");
                }
            }
            catch (Exception)
            {
                return BadRequest("Alteração Pedido - Erro ao atualizar o pedido");
            }
        }

        [HttpPut("AtualizarStatusPedido")]
        public IActionResult PutAtualizarStatusPedido(AtualizacaoPedidoModel atualizacaoPedidoModel)
        {
            try
            {
                if (atualizacaoPedidoModel != null)
                {
                    if (atualizacaoPedidoModel.PedidoId == null || atualizacaoPedidoModel.PedidoId == Guid.Empty ||
                        atualizacaoPedidoModel.StatusPedido < -1 || atualizacaoPedidoModel.StatusPedido > 1)
                    {
                        return BadRequest("Atualizar Status Pedido - Erro ao atualizar o status do pedido. Dados recebidos inválidos");
                    }

                    AtualizacaoPedidoDTO atualizacaoPedidoDTO = AtualizacaoPedidoAdapter.AtualizacaoPedidoModelTOAtualizacaoPedidoDTO(atualizacaoPedidoModel);
                    pedidoAplicacao.AtualizarStatusPedido(atualizacaoPedidoDTO);
                    return Ok("Atualizar Status Pedido - Pedido atualizado com sucesso");
                }
                else
                {
                    return BadRequest("Atualizar Status Pedido - Erro ao atualizar o status do pedido. Objeto recebido corrompido");
                }
            }
            catch (Exception)
            {
                return BadRequest("Atualizar Status Pedido - Erro ao atualizar o status do pedido");
            }
        }

        [HttpPut("AtualizarEnderecoEntregaPedido")]
        public IActionResult PutAtualizarEnderecoEntregaPedido(AtualizarEnderecoPedidoModel atualizarEnderecoPedidoModel)
        {
            try
            {
                if (atualizarEnderecoPedidoModel != null)
                {
                    if (atualizarEnderecoPedidoModel.PedidoId == null || atualizarEnderecoPedidoModel.PedidoId == Guid.Empty ||
                        string.IsNullOrEmpty(atualizarEnderecoPedidoModel.CEPEnderecoEntrega) || string.IsNullOrEmpty(atualizarEnderecoPedidoModel.NumeroEnderecoEntrega))
                    {
                        return BadRequest("Atualizar Endereço Pedido - Dados recebidos inválidos");
                    }

                    AtualizarEnderecoPedidoDTO atualizarEnderecoPedidoDTO = AtualizarEnderecoPedidoAdapter.AtualizarEnderecoPedidoModelTOAtualizarEnderecoPedidoDTO(atualizarEnderecoPedidoModel);
                    pedidoAplicacao.AtualizarEnderecoEntregaPedido(atualizarEnderecoPedidoDTO);
                    return Ok("Atualizar Endereço Pedido - Endereço do pedido atualizado com sucesso");
                }
                else
                {
                    return BadRequest("Atualizar Endereço Pedido - Erro ao atualizar o endereço do pedido. Objeto recebido corrompido");
                }
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
                if (id != null && id != Guid.Empty)
                {
                    pedidoAplicacao.ExcluirPedido(id);
                    return Ok("Deletar Pedido Específico - Pedido excluido com sucesso");
                }
                else
                {
                    return BadRequest("Deletar Pedido Específico - Id do pedido inválido");
                }
            }
            catch (Exception)
            {
                return BadRequest("Deletar Pedido Específico - Erro ao excluir o pedido");
            }
        }

        [HttpDelete("DeletePedidos/{usuarioid}")]
        public IActionResult DeletePedidos(Guid usuarioid)
        {
            try
            {
                if (usuarioid != null && usuarioid != Guid.Empty)
                {
                    pedidoAplicacao.ExcluirPedidos(usuarioid);
                    return Ok("Deletar Pedidos Usuário - Pedidos excluidos com sucesso");
                }
                else
                {
                    return BadRequest("Deletar Pedidos Usuário - Id do usuário inválido");
                }
            }
            catch (Exception)
            {
                return BadRequest("Deletar Pedidos Usuário - Erro ao excluir o pedido");
            }
        }

        [HttpDelete("DeleteProdutoPedido/{pedidoId}/{produtoId}")]
        public IActionResult DeleteProdutoPedido(Guid pedidoId, Guid produtoId)
        {
            try
            {
                if (pedidoId != null && pedidoId != Guid.Empty && produtoId != null && produtoId != Guid.Empty)
                {
                    pedidoAplicacao.DeleteProdutoPedido(pedidoId, produtoId);
                    return Ok("Deletar Produto Pedido - Produto do pedido excluido com sucesso");
                }
                else
                {
                    return BadRequest("Deletar Produto Pedido - Id do pedido ou produto inválido");
                }
            }
            catch (Exception)
            {
                return BadRequest("Deletar Produto Pedido - Erro ao excluir o produto do pedido");
            }
        }
    }
}
