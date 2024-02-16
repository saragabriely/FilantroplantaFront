using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Models.API.Response
{
    public class ResponseBase
    {
        public int StatusRetorno { get; set; }
        public string Erro { get; set; }
        public long ID { get; set; }
    }
}
