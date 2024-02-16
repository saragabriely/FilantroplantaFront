CREATE TABLE [dbo].[Produto] (
    [ProdutoID]             BIGINT          IDENTITY (1, 1) NOT NULL,
    [Descricao]             VARCHAR (200)   NOT NULL,
    [ValorPorKg]            DECIMAL (19, 2) NOT NULL,
    [PessoaID]              BIGINT          NULL,
    [Datacadastro]          DATETIME        NULL,
    [DataUltimaAtualizacao] DATETIME        NULL,
    PRIMARY KEY CLUSTERED ([ProdutoID] ASC),
    FOREIGN KEY ([PessoaID]) REFERENCES [dbo].[Pessoa] ([PessoaID])
);