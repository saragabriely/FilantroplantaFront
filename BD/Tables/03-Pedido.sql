CREATE TABLE [dbo].[Pedido] (
    [PedidoID]              BIGINT          IDENTITY (1, 1) NOT NULL,
    [StatusPedidoID]        INT             NOT NULL,
    [Quantidade]            BIGINT          NOT NULL,
    [ValorPorKg]            DECIMAL (19, 2) NOT NULL,
    [ValorTotal]            DECIMAL (19, 2) NOT NULL,
    [ProdutoID]             BIGINT          NULL,
    [PessoaID]              BIGINT          NULL,
    [Datacadastro]          DATETIME        NULL,
    [DataUltimaAtualizacao] DATETIME        NULL,
    PRIMARY KEY CLUSTERED ([PedidoID] ASC),
    FOREIGN KEY ([ProdutoID]) REFERENCES [dbo].[Produto] ([ProdutoID]),
    FOREIGN KEY ([PessoaID]) REFERENCES [dbo].[Pessoa] ([PessoaID])
);