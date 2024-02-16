using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Controle
{
    public class ControleUtil
    {
        public ControleUtil() { }

        public string FormatarDecimal(decimal value) => value.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"));
    }
}
