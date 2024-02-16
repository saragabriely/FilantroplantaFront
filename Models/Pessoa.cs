using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Models
{
    public class Pessoa
    {
        public long PessoaID { get; set; }
        public string Nome { get; set;}
        public int TipoPessoaID { get; set; }
        public string Documento { get; set; }
        public string CEP { get; set; }
        public string Endereco { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public const int Restaurante = 1;
        public const int Produtor    = 2;

        public Pessoa() { }

        public Pessoa(long PessoaID)
        {
            this.PessoaID = PessoaID;
        }

        public Pessoa(string Nome, int TipoPessoa, string Documento, string CEP, string Endereco, 
            int Numero, string Complemento, string Cidade, string Estado, string Telefone, string Email, string Senha)
        {
            this.Nome          = Nome;
            this.TipoPessoaID  = TipoPessoa;
            this.Documento     = Documento;
            this.CEP           = CEP;
            this.Endereco      = Endereco;   
            this.Numero        = Numero;
            this.Complemento   = Complemento;
            this.Cidade        = Cidade;
            this.Estado        = Estado;
            this.Telefone      = Telefone;
            this.Email         = Email;
            this.Senha         = Senha;
        }
    }
}
