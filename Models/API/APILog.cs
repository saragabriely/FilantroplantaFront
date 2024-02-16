using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Models.API
{
    public class APILog
    {
        public long APILogID { get; set; }
        public string RotaAPI { get; set; }
        public string URL { get; set; }
        public string Requisicao { get; set; }
        public string Retorno { get; set; }
        public DateTime Data { get; set; }
    }
}
