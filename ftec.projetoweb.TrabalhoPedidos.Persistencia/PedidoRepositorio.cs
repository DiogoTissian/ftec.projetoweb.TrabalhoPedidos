using ftec.projetoweb.TrabalhoPedidos.Dominio.Entidades;
using ftec.projetoweb.TrabalhoPedidos.Dominio.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ftec.projetoweb.TrabalhoPedidos.Persistencia
{
    public class PedidoRepositorio: IPedidoRepositorio
    {
        private string stringConexao = string.Empty;

        public PedidoRepositorio(string strConexao)
        {
            stringConexao = strConexao;
        }

        public void Inserir(Pedido pedido)
        {
            try
            {
                using (var conexao = new NpgsqlConnection(stringConexao))
                {
                    conexao.Open();

                    var sqlCommand = new NpgsqlCommand();
                    sqlCommand.Connection = conexao;

                    Guid pedidoId = Guid.NewGuid();

                    sqlCommand.CommandText = "INSERT INTO pedido (id, usuarioid, datapedido, statuspedido, cependerecoentrega, numeroenderecoentrega) VALUES (@id, @usuarioid, @datapedido, @statuspedido, @cependerecoentrega, @numeroenderecoentrega)";
                    sqlCommand.Parameters.AddWithValue("id", pedidoId);
                    sqlCommand.Parameters.AddWithValue("usuarioid", pedido.UsuarioId);
                    sqlCommand.Parameters.AddWithValue("datapedido", DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("statuspedido", 0);
                    sqlCommand.Parameters.AddWithValue("cependerecoentrega", pedido.CEPEnderecoEntrega);
                    sqlCommand.Parameters.AddWithValue("numeroenderecoentrega", pedido.NumeroEnderecoEntrega);
                    sqlCommand.ExecuteNonQuery();

                    sqlCommand.Parameters.Clear();

                    foreach (Produto produto in pedido.Produtos)
                    {
                        sqlCommand.CommandText = "INSERT INTO produto_pedido (id, pedidoid, produtoid, quantidade) VALUES (@id, @pedidoid, @produtoid, @quantidade)";
                        sqlCommand.Parameters.AddWithValue("id", Guid.NewGuid());
                        sqlCommand.Parameters.AddWithValue("pedidoid", pedidoId);
                        sqlCommand.Parameters.AddWithValue("produtoid", produto.ProdutoId);
                        sqlCommand.Parameters.AddWithValue("quantidade", produto.Quantidade);
                        sqlCommand.ExecuteNonQuery();
                        sqlCommand.Parameters.Clear();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Alterar(Pedido pedido)
        {
            try
            {
                using (var conexao = new NpgsqlConnection(stringConexao))
                {
                    conexao.Open();

                    var sqlCommand = new NpgsqlCommand();
                    sqlCommand.Connection = conexao;

                    sqlCommand.CommandText = "UPDATE pedido SET datapedido = @datapedido, statuspedido = @statuspedido WHERE id = @id";
                    sqlCommand.Parameters.AddWithValue("datapedido", DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("statuspedido", pedido.StatusPedido);
                    sqlCommand.Parameters.AddWithValue("id", pedido.Id.ToString());
                    sqlCommand.ExecuteNonQuery();

                    if (pedido.Produtos.Count > 0)
                    {
                        sqlCommand.Parameters.Clear();

                        sqlCommand.CommandText = "DELETE FROM produto_pedido WHERE pedidoid = @pedidoid";
                        sqlCommand.Parameters.AddWithValue("pedidoid", pedido.Id.ToString());
                        sqlCommand.ExecuteNonQuery();

                        sqlCommand.Parameters.Clear();

                        foreach (Produto produto in pedido.Produtos)
                        {
                            sqlCommand.CommandText = "INSERT INTO produto_pedido (id, pedidoid, produtoid, quantidade) VALUES (@id, @pedidoid, @produtoid, @quantidade)";
                            sqlCommand.Parameters.AddWithValue("id", Guid.NewGuid());
                            sqlCommand.Parameters.AddWithValue("pedidoid", pedido.Id);
                            sqlCommand.Parameters.AddWithValue("produtoid", produto.ProdutoId);
                            sqlCommand.Parameters.AddWithValue("quantidade", produto.Quantidade);
                            sqlCommand.ExecuteNonQuery();
                            sqlCommand.Parameters.Clear();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AtualizarStatusPedido(AtualizacaoPedido atualizacaoPedido)
        {
            try
            {
                using (var conexao = new NpgsqlConnection(stringConexao))
                {
                    conexao.Open();

                    var sqlCommand = new NpgsqlCommand();
                    sqlCommand.Connection = conexao;

                    sqlCommand.CommandText = "UPDATE pedido SET datapedido = @datapedido, statuspedido = @statuspedido WHERE id = @id";
                    sqlCommand.Parameters.AddWithValue("datapedido", DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("statuspedido", atualizacaoPedido.StatusPedido);
                    sqlCommand.Parameters.AddWithValue("id", atualizacaoPedido.PedidoId.ToString());
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AtualizarEnderecoEntregaPedido(AtualizarEnderecoPedido atualizarEnderecoPedido)
        {
            try
            {
                using (var conexao = new NpgsqlConnection(stringConexao))
                {
                    conexao.Open();

                    var sqlCommand = new NpgsqlCommand();
                    sqlCommand.Connection = conexao;

                    sqlCommand.CommandText = "UPDATE pedido SET cependerecoentrega = @cependerecoentrega, numeroenderecoentrega = @numeroenderecoentrega WHERE id = @id";
                    sqlCommand.Parameters.AddWithValue("cependerecoentrega", atualizarEnderecoPedido.CEPEnderecoEntrega);
                    sqlCommand.Parameters.AddWithValue("numeroenderecoentrega", atualizarEnderecoPedido.NumeroEnderecoEntrega);
                    sqlCommand.Parameters.AddWithValue("id", atualizarEnderecoPedido.PedidoId.ToString());
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Ecluir(Guid Id)
        {
            try
            {
                using (var conexao = new NpgsqlConnection(stringConexao))
                {
                    conexao.Open();

                    var sqlCommand = new NpgsqlCommand();
                    sqlCommand.Connection = conexao;

                    sqlCommand.CommandText = "DELETE FROM pedido WHERE id = @id";
                    sqlCommand.Parameters.AddWithValue("id", Id.ToString());
                    sqlCommand.ExecuteNonQuery();

                    sqlCommand.Parameters.Clear();

                    sqlCommand.CommandText = "DELETE FROM produto_pedido WHERE pedidoid = @pedidoid";
                    sqlCommand.Parameters.AddWithValue("pedidoid", Id.ToString());
                    sqlCommand.ExecuteNonQuery();
                }
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
                using (var conexao = new NpgsqlConnection(stringConexao))
                {
                    conexao.Open();

                    var sqlCommand = new NpgsqlCommand();
                    sqlCommand.Connection = conexao;

                    sqlCommand.CommandText = "SELECT id FROM pedido WHERE usuarioid = @usuarioid";
                    sqlCommand.Parameters.AddWithValue("usuarioid", usuarioid.ToString());

                    List<Guid> pedidosUsuarioIds = new List<Guid>();

                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pedidosUsuarioIds.Add(Guid.Parse(reader["id"].ToString()));
                        }
                    }

                    if (pedidosUsuarioIds != null && pedidosUsuarioIds.Count > 0)
                    {
                        sqlCommand.Parameters.Clear();

                        sqlCommand.CommandText = "DELETE FROM pedido WHERE usuarioid = @usuarioid";
                        sqlCommand.Parameters.AddWithValue("usuarioid", usuarioid.ToString());
                        sqlCommand.ExecuteNonQuery();

                        foreach (Guid pedidoId in pedidosUsuarioIds)
                        {
                            sqlCommand.Parameters.Clear();

                            sqlCommand.CommandText = "DELETE FROM produto_pedido WHERE pedidoid = @pedidoid";
                            sqlCommand.Parameters.AddWithValue("pedidoid", pedidoId.ToString());
                            sqlCommand.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        throw new ApplicationException();
                    }
                }
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
                bool excluirPedido = false;

                using (var conexao = new NpgsqlConnection(stringConexao))
                {
                    conexao.Open();

                    var sqlCommand = new NpgsqlCommand();
                    sqlCommand.Connection = conexao;

                    sqlCommand.CommandText = "DELETE FROM produto_pedido WHERE pedidoid = @pedidoid AND produtoid = @produtoid";
                    sqlCommand.Parameters.AddWithValue("pedidoid", pedidoId.ToString());
                    sqlCommand.Parameters.AddWithValue("produtoid", produtoId.ToString());
                    sqlCommand.ExecuteNonQuery();

                    sqlCommand.Parameters.Clear();

                    sqlCommand.CommandText = "SELECT id FROM produto_pedido WHERE pedidoid = @pedidoId";
                    sqlCommand.Parameters.AddWithValue("pedidoId", pedidoId.ToString());

                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        excluirPedido = !reader.Read();
                    }

                    if (excluirPedido)
                    {
                        sqlCommand.Parameters.Clear();
                        sqlCommand.CommandText = "DELETE FROM pedido WHERE id = @pedidoId";
                        sqlCommand.Parameters.AddWithValue("pedidoId", pedidoId.ToString());
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Pedido Procurar(Guid Id)
        {
            try
            {
                Pedido pedido = null;

                using (var conexao = new NpgsqlConnection(stringConexao))
                {
                    conexao.Open();

                    var sqlCommand = new NpgsqlCommand();
                    sqlCommand.Connection = conexao;

                    sqlCommand.CommandText = "SELECT id, usuarioid, datapedido, statuspedido, cependerecoentrega, numeroenderecoentrega FROM pedido WHERE id = @id";
                    sqlCommand.Parameters.AddWithValue("id", Id.ToString());

                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pedido = new Pedido();
                            pedido.Id = Guid.Parse(reader["id"].ToString());
                            pedido.UsuarioId = Guid.Parse(reader["usuarioid"].ToString());
                            pedido.DataPedido = Convert.ToDateTime(reader["datapedido"].ToString());
                            pedido.StatusPedido = Convert.ToInt32(reader["statuspedido"].ToString());
                            pedido.CEPEnderecoEntrega = reader["cependerecoentrega"].ToString();
                            pedido.NumeroEnderecoEntrega = reader["numeroenderecoentrega"].ToString();
                        }
                    }

                    if (pedido != null)
                    {
                        sqlCommand.Parameters.Clear();

                        sqlCommand.CommandText = "SELECT id, pedidoid, produtoid, quantidade FROM produto_pedido WHERE pedidoid = @pedidoid";
                        sqlCommand.Parameters.AddWithValue("pedidoid", Id.ToString());

                        using (var reader = sqlCommand.ExecuteReader())
                        {
                            List<Produto> produtos = new List<Produto>();
                            while (reader.Read())
                            {
                                produtos.Add(new Produto()
                                {
                                    Id = Guid.Parse(reader["id"].ToString()),
                                    PedidoId = Guid.Parse(reader["pedidoid"].ToString()),
                                    ProdutoId = Guid.Parse(reader["produtoid"].ToString()),
                                    Quantidade = Convert.ToInt32(reader["quantidade"].ToString()),
                                });
                            }

                            pedido.Produtos = produtos;
                        }
                    }
                    else
                    {
                        throw new ApplicationException();
                    }
                }

                return pedido;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Pedido> ProcurarTodos()
        {
            try
            {
                List<Pedido> pedidos = new List<Pedido>();

                using (var conexao = new NpgsqlConnection(stringConexao))
                {
                    conexao.Open();

                    var sqlCommand = new NpgsqlCommand();
                    sqlCommand.Connection = conexao;

                    sqlCommand.CommandText = "SELECT id, usuarioid, datapedido, statuspedido, cependerecoentrega, numeroenderecoentrega FROM pedido";

                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pedidos.Add(new Pedido()
                            {
                                Id = Guid.Parse(reader["id"].ToString()),
                                UsuarioId = Guid.Parse(reader["usuarioid"].ToString()),
                                DataPedido = Convert.ToDateTime(reader["datapedido"].ToString()),
                                StatusPedido = Convert.ToInt32(reader["statuspedido"].ToString()),
                                CEPEnderecoEntrega = reader["cependerecoentrega"].ToString(),
                                NumeroEnderecoEntrega = reader["numeroenderecoentrega"].ToString()
                            });
                        }
                    }

                    if (pedidos != null && pedidos.Count > 0)
                    {
                        foreach (Pedido pedido in pedidos)
                        {
                            sqlCommand.Parameters.Clear();

                            sqlCommand.CommandText = "SELECT id, pedidoid, produtoid, quantidade FROM produto_pedido WHERE pedidoid = @pedidoid";
                            sqlCommand.Parameters.AddWithValue("pedidoid", pedido.Id.ToString());

                            using (var reader = sqlCommand.ExecuteReader())
                            {
                                List<Produto> produtos = new List<Produto>();
                                while (reader.Read())
                                {
                                    produtos.Add(new Produto()
                                    {
                                        Id = Guid.Parse(reader["id"].ToString()),
                                        PedidoId = Guid.Parse(reader["pedidoid"].ToString()),
                                        ProdutoId = Guid.Parse(reader["produtoid"].ToString()),
                                        Quantidade = Convert.ToInt32(reader["quantidade"].ToString()),
                                    });
                                }

                                pedido.Produtos = produtos;
                            }
                        }
                    }
                    else
                    {
                        throw new ApplicationException();
                    }
                }

                return pedidos;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public List<Pedido> BuscarPedidosUsuario(Guid usuarioId)
        {
            try
            {
                List<Pedido> pedidos = new List<Pedido>();

                using (var conexao = new NpgsqlConnection(stringConexao))
                {
                    conexao.Open();

                    var sqlCommand = new NpgsqlCommand();
                    sqlCommand.Connection = conexao;

                    sqlCommand.CommandText = "SELECT id, usuarioid, datapedido, statuspedido, cependerecoentrega, numeroenderecoentrega FROM pedido WHERE usuarioid = @usuarioid";
                    sqlCommand.Parameters.AddWithValue("usuarioid", usuarioId.ToString());

                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pedidos.Add(new Pedido()
                            {
                                Id = Guid.Parse(reader["id"].ToString()),
                                UsuarioId = Guid.Parse(reader["usuarioid"].ToString()),
                                DataPedido = Convert.ToDateTime(reader["datapedido"].ToString()),
                                StatusPedido = Convert.ToInt32(reader["statuspedido"].ToString()),
                                CEPEnderecoEntrega = reader["cependerecoentrega"].ToString(),
                                NumeroEnderecoEntrega = reader["numeroenderecoentrega"].ToString()
                            });
                        }
                    }

                    if (pedidos != null && pedidos.Count > 0)
                    {
                        foreach (Pedido pedido in pedidos)
                        {
                            sqlCommand.Parameters.Clear();

                            sqlCommand.CommandText = "SELECT id, pedidoid, produtoid, quantidade FROM produto_pedido WHERE pedidoid = @pedidoid";
                            sqlCommand.Parameters.AddWithValue("pedidoid", pedido.Id.ToString());

                            using (var reader = sqlCommand.ExecuteReader())
                            {
                                List<Produto> produtos = new List<Produto>();
                                while (reader.Read())
                                {
                                    produtos.Add(new Produto()
                                    {
                                        Id = Guid.Parse(reader["id"].ToString()),
                                        PedidoId = Guid.Parse(reader["pedidoid"].ToString()),
                                        ProdutoId = Guid.Parse(reader["produtoid"].ToString()),
                                        Quantidade = Convert.ToInt32(reader["quantidade"].ToString()),
                                    });
                                }

                                pedido.Produtos = produtos;
                            }
                        }
                    }
                    else
                    {
                        throw new ApplicationException();
                    }
                }

                return pedidos;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
