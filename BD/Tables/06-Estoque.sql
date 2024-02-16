CREATE TABLE [dbo].[Estoque] (
    [EstoqueID]             BIGINT   IDENTITY (1, 1) NOT NULL,
    [ProdutoID]             BIGINT   NULL,
    [ProdutorID]            BIGINT   NULL,
    [QuantidadeDisponivel]  BIGINT   NOT NULL,
    [QuantidadeVendida]     BIGINT   NOT NULL,
    [QuantidadeReservada]   BIGINT   NOT NULL,
    [DataUltimaAtualizacao] DATETIME NULL,
    PRIMARY KEY CLUSTERED ([EstoqueID] ASC),
    FOREIGN KEY ([ProdutoID]) REFERENCES [dbo].[Produto] ([ProdutoID]),
    FOREIGN KEY ([ProdutorID]) REFERENCES [dbo].[Pessoa] ([PessoaID])
);

