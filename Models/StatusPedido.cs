using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Models
{
    public class StatusPedido
    {
        public long StatusPedido_ID { get; set; }
        public string Descricao { get; set; }

        public const int Pendente_Aprovacao = 1;
        public const int Pendente_Envio     = 2;
        public const int Finalizado         = 3;
        public const int Cancelado_Produtor = 4;
        public const int Cancelado_Cliente  = 5;

        public const string string_Todos              = "Todos os status";
        public const string string_Pendente_Aprovacao = "Pendente de aprovação";
        public const string string_Pendente_Envio     = "Pendente de envio";
        public const string string_Finalizado         = "Finalizado";
        public const string string_Cancelado_Produtor = "Cancelado pelo produtor";
        public const string string_Cancelado_Cliente  = "Cancelado pelo cliente";
    }
}
