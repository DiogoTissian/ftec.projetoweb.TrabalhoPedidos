# ProjetoWeb: API de Pedidos
Projeto desenvolvido para a disciplina de Projetos Web da Uniftec. Período letivo 2/2026

## Objetivo
Desenvolver uma API para a consulta de pedidos, implementando as definições REST, permitindo ser integrada com outras aplicações

## Desenvolvimento
API construída em C# com o framework .NET Core

### Entidades
- Pedido
  
| Cmpo  | Tipo | Descrição |
| ------------- | ------------- | ------------- |
| Id | Guid | Id do pedido |
| UsuarioId  | Guid  | Id do usuário que fez o pedido  |
| ProdutosModel  | Lista de produtos  | Lista de produtos contidos dentro do pedido. Apenas uma abstração dos produtos, com informações essênciais  |
| DataPedido  | DateTime  | Data em que o pedido foi atualizado pela última vez  |
| StatusPedido  | int  | Indica o status do pedido: Pendente pagamento (0), Concluído (1) e Cancelado (-1) |
| ValorTotal | decimal  | Calcula o valor total do pedido com seus itens |


- Produto (Abstração)
  
| Cmpo  | Tipo | Descrição |
| ------------- | ------------- | ------------- |
| Id  | Guid  | Id do produto  |
| PedidoId | Guid | Id do pedido em que ele pertence |
| ProdutoId | Guid | Id do produto na tabela de produtos (consulta microsserviço) |
| Quantidade | int | Quantidade escolhida para o produto |
| Valor | decimal | Valor do produto unitário (consulta em microsserviço) |

### Endpoints
- GET - api/Pedido - Lista todos os pedidos salvos no banco de dados

Resposta:
```
[
  {
    "id": "1b456668-80ed-4000-b7a2-d201fd677af8",
    "usuarioId": "3fa85f64-5717-4562-b3fc-2c963f66af77",
    "produtosModel": [
      {
        "id": "a27aed6a-23b6-4e9e-9772-b343d08b20b7",
        "pedidoId": "1b456668-80ed-4000-b7a2-d201fd677af8",
        "produtoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "quantidade": 2,
        "valor": 100
      },
      {
        "id": "a970f96f-a87e-48ba-9367-274dca9beb5e",
        "pedidoId": "1b456668-80ed-4000-b7a2-d201fd677af8",
        "produtoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "quantidade": 4,
        "valor": 50
      }
    ],
    "dataPedido": "2026-05-11T00:00:00",
    "statusPedido": 1,
    "valorTotal": 400
  }
]
```

- GET - api/Pedido/{id} - Lista o pedido do id especificado

Resposta:
```
{
  "id": "d4e24e9f-5a95-4054-b690-6fbbef901fa7",
  "usuarioId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "produtosModel": [
    {
      "id": "36e225f5-cf18-4bcf-9bd2-2a59d23d2e04",
      "pedidoId": "d4e24e9f-5a95-4054-b690-6fbbef901fa7",
      "produtoId": "3fa85f64-5717-4562-b3fc-2c963f66a775",
      "quantidade": 2,
      "valor": 10
    }
  ],
  "dataPedido": "2026-05-11T00:00:00",
  "statusPedido": 0,
  "valorTotal": 20
}
```

- GET - api/Pedido/GetPedidosUsuario/{usuarioId} - Lista todos os pedidos do usuário especificado

Resposta:
```
[
  {
    "id": "1b456668-80ed-4000-b7a2-d201fd677af8",
    "usuarioId": "3fa85f64-5717-4562-b3fc-2c963f66af77",
    "produtosModel": [
      {
        "id": "a27aed6a-23b6-4e9e-9772-b343d08b20b7",
        "pedidoId": "1b456668-80ed-4000-b7a2-d201fd677af8",
        "produtoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "quantidade": 2,
        "valor": 10
      },
      {
        "id": "a970f96f-a87e-48ba-9367-274dca9beb5e",
        "pedidoId": "1b456668-80ed-4000-b7a2-d201fd677af8",
        "produtoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "quantidade": 4,
        "valor": 5
      }
    ],
    "dataPedido": "2026-05-11T00:00:00",
    "statusPedido": 1,
    "valorTotal": 40
  },
  {
    "id": "6a65417d-4455-4be9-903d-4e5d8e58d95d",
    "usuarioId": "3fa85f64-5717-4562-b3fc-2c963f66af77",
    "produtosModel": [
      {
        "id": "254b18e2-2409-494e-a3a1-4fa5aa0f1952",
        "pedidoId": "6a65417d-4455-4be9-903d-4e5d8e58d95d",
        "produtoId": "3fa85f64-5717-4562-b3fc-2c963f66a775",
        "quantidade": 100,
        "valor": 1
      }
    ],
    "dataPedido": "2026-05-11T00:00:00",
    "statusPedido": 0,
    "valorTotal": 100
  }
]
```

- POST - api/Pedido - Insere um pedido no banco de dados

Envio:
```
{
  "usuarioId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "produtosModel": [
    {
      "produtoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "quantidade": 1
    }
  ]
}
```

Resposta:
```
Pedido inserido com sucesso
```

- PUT - api/Pedido - Atualiza um pedido no banco de dados

Envio:
Obs.: O campo "id" é o id do pedido no banco de dados. Nessa atualização, podemos colocar os novos pedidos atualizados, ou não
```
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "statusPedido": 1,
  "produtosModel": [
    {
      "produtoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "quantidade": 2
    }
  ]
}

Ou atualizando somente o status do pedido

{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "statusPedido": 1
}
```

Resposta:
```
Pedido atualizado com sucesso
```

- DELETE - api/Pedido/{id} - Deleta um pedido especificado no banco de dados

Resposta:
```
Pedido excluido com sucesso
```

- DELETE - api/Pedido/DeletePedidos/{usuarioid} - Deleta todos os pedidos de um usuário especificado

Resposta:
```
Pedidos excluidos com sucesso
```

### Banco de dados
- PostgreSQL
- Scripts de criação de tabelas necessárias:
```
CREATE TABLE public.pedido (
	id varchar NOT NULL,
	usuarioid varchar NULL,
	datapedido date NULL
  statuspedido integer null
);

CREATE TABLE public.produto_pedido (
  id varchar NULL,
	pedidoid varchar NOT NULL,
	produtoid varchar NULL,
	quantidade integer NULL
);
```
