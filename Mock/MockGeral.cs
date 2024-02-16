using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filantroplanta.Models;

namespace Filantroplanta.Mock
{
    public class MockGeral
    {
        public Pessoa MockProdutor()
        {
            return new Filantroplanta.Models.Pessoa
            {
                PessoaID = 1,
                Nome = "Produtor 01",
                TipoPessoaID = Pessoa.Produtor,
                Documento = "49019812000178",
                Email = "produtor@gmail.com",
                Senha = "1234",
                Telefone = "11912345678",
                Endereco = "Av. Caminho do Mar",
                Numero = 10,
                Complemento = "Galpão 01",
                Bairro = "São Bernardo do Campo",
                Cidade = "São Paulo",
                CEP = "01200-003",
                Estado = "SP"
            };
        }

        public Pessoa MockRestaurante()
        {
            return new Pessoa
            {
                PessoaID = 2,
                Nome = "Restaurante 01",
                TipoPessoaID = Pessoa.Restaurante,
                Documento = "21886764000104",
                Email = "r@gmail.com",
                Senha = "1234",
                Telefone = "11912348888",
                Endereco = "Av. Paulista",
                Numero = 10,
                Complemento = "",
                Bairro = "Bela Vista",
                Cidade = "São Paulo",
                CEP = "31220-003",
                Estado = "SP"
            };
        }
        /*
        public Produto MockProduto01()
        {
            return new Produto { ProdutoID = 1, Descricao = "Tomate Cereja", Quantidade = 1000, ValorPorKG = 100 };
        }

        public Produto MockProduto02()
        {
            return new Produto { ProdutoID = 1, Descricao = "Tomate Roma", Quantidade = 1000, ValorPorKG = 100 };
        }

        public List<Produto> MockListaProdutos()
        {
            return new List<Produto>
            {
                MockProduto01(),
                MockProduto02()
            };
        }

        
        public Pedido MockPedidoPendenteAprovacao()
        {
            return
                new Pedido
                {
                    Pedido_ID = 1
                    , ClienteID = MockRestaurante()
                    , mProduto = MockProduto01()
                    , mStatusPedido = new StatusPedido { StatusPedido_ID = StatusPedido.Pendente_Aprovacao }
                    , Quantidade = 10000
                    , ValorPorKG = 10
                    , ValorTotal = 100000
                };
        }

        public Pedido MockPedidoPendenteEnvio()
        {
            return
                new Pedido
                {
                    Pedido_ID = 2
                    , mCliente = MockRestaurante()
                    , mProduto = MockProduto02()
                    , mStatusPedido = new StatusPedido { StatusPedido_ID = StatusPedido.Pendente_Envio }
                    , Quantidade = 10000
                    ,  ValorPorKG = 10
                    , ValorTotal = 100000
                };
        }

        public Pedido MockPedidoFinalizado()
        {
            return
                new Pedido
                {
                    Pedido_ID = 3
                    , mCliente = MockRestaurante()
                    , mProduto = MockProduto01()
                    , mStatusPedido = new StatusPedido { StatusPedido_ID = StatusPedido.Finalizado }
                    , Quantidade = 10000
                    , ValorPorKG = 10
                    , ValorTotal = 100000
                };
        }

        public Pedido MockPedidoCancelado()
        {
            return
                new Pedido
                {
                    Pedido_ID = 4
                    , mCliente = MockRestaurante()
                    , mProduto = MockProduto02()
                    , mStatusPedido = new StatusPedido { StatusPedido_ID = StatusPedido.Cancelado }
                    , Quantidade = 10000
                    , ValorPorKG = 10
                    ,ValorTotal = 100000
                };
        }
        */
    }
}
