CREATE TABLE [dbo].[Pessoa] (
    [PessoaID]     BIGINT        IDENTITY (1, 1) NOT NULL,
    [Nome]         VARCHAR (100) NOT NULL,
    [TipoPessoaID] INT           NOT NULL,
    [Documento]    VARCHAR (50)  NOT NULL,
    [CEP]          VARCHAR (50)  NOT NULL,
    [Endereco]     VARCHAR (200) NOT NULL,
    [Numero]       INT           NOT NULL,
    [Complemento]  VARCHAR (100) NULL,
    [Bairro]       VARCHAR (100) NOT NULL,
    [Cidade]       VARCHAR (100) NOT NULL,
    [Estado]       VARCHAR (50)  NOT NULL,
    [Telefone]     VARCHAR (50)  NOT NULL,
    [Email]        VARCHAR (100) NOT NULL,
    [Senha]        VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([PessoaID] ASC)
);