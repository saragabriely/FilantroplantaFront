using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Models
{
    public class Produto
    {
        public long ProdutoID { get; set; }
        public string Descricao { get; set; }
        public decimal ValorPorKG { get; set; }
        public long ProdutorID { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataUltimaAtualizacao { get; set; }


        public Produto() { }

        public Produto(long PessoaID)
        {
            this.ProdutoID = PessoaID;
        }
    }
}
