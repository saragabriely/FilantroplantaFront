using Filantroplanta.Views.Componentizacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Controle
{
    public class ControleComponentizacao
    {
        public string NomeBotaoSalvar   = "botaoSalvar";
        public string NomeBotaoCancelar = "botaoCancelar";
        public string NomeBotaoExcluirRecusar = "btnExcluirRecusar";
        public BotaoSalvar btnSalvar = new BotaoSalvar();
        public BotaoCancelar btnCancelar = new BotaoCancelar();
        public BotaoExcluirRecusar btnExcluirRecusar = new BotaoExcluirRecusar();
        public ControleComponentizacao() { }
    }
}
