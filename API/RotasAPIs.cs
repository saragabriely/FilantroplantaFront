using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.API
{
    public class RotasAPIs
    {
        public RotasAPIs() { }

        public string URL_LOGIN = "https://azfunc-backend.azurewebsites.net/api/AzFunc_Logon?code=F-aJPrEKkwKPvIuXmYSEbiYAAx3Ny4MuZAos2uuqZ4J6AzFu052jmA==";
        public string URL_CADASTRO_USUARIO = "https://azfunc-backend.azurewebsites.net/api/AzFunc_CadastroUser?code=jOTlX8Q966hHKSvp6Hjirg8m76P8e0mPDAa_Qo9GWPiUAzFuOzDvvQ==";
        public string URL_CADASTRO_PRODUTO = "https://azfunc-backend.azurewebsites.net/api/AzFunc_Produtos?code=O-AJorhlnOXCA08DeGWmtdc2CRRgwqmbc34odkugLGrWAzFus38Qyg==";
        public string URL_CADASTRO_PEDIDO = "https://azfunc-backend.azurewebsites.net/api/AzFunc_Pedido?code=pi4JFTtKtjeqUMQbG_E4w5wFk3TXhO7ZqCfeh_DoQVwjAzFuuVUnHQ==";
        public string URL_CADASTRO_ESTOQUE = "https://azfunc-backend.azurewebsites.net/api/AzFunc_Estoque?code=QueLWUg3sQZNB9h8HoYn4G_kMkQzpDFLgnuKeAmMUN0-AzFuyo3N1g==";
        public string URL_CADASTRO_CARRINHO = "https://azfunc-backend.azurewebsites.net/api/AzFunc_Carrinho?code=uFF52k4TiNth5qHIz-YNRlncbcmADI8QP_fr-MOKPBp4AzFu86fADQ==";

        // URLs usadas para testar as functions locais
        //public string URL_LOGIN = "http://10.0.2.2:7071/api/AzFunc_Logon";
        //public string URL_CADASTRO_USUARIO = "http://10.0.2.2:7071/api/AzFunc_CadastroUser";
        //public string URL_CADASTRO_PRODUTO = "http://10.0.2.2:7071/api/AzFunc_Produtos";
        //public string URL_CADASTRO_ESTOQUE = "http://10.0.2.2:7071/api/AzFunc_Estoque";
        //public string URL_CADASTRO_PEDIDO  = "http://10.0.2.2:7071/api/AzFunc_Pedido";
        //public string URL_CADASTRO_CARRINHO = "http://10.0.2.2:7071/api/AzFunc_Carrinho";
    }
}
