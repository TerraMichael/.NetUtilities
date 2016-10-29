using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleMVC.Controllers
{
    [RoutePrefix("novasrotas")]
    public class NovasRotasController : Controller
    {
        // GET: NovasRotas
        [Route("index/{nasc:datetime}")]
        public string Index(DateTime nasc)
        {
            return string.Format("Data de nascimento {0:dd/MM/yyyy}", nasc);
        }

        [Route("getdados/{nome}/{idade}")]
        public string GetDados(string nome, int idade = 20)
        {
            return HttpUtility.HtmlEncode(
                    string.Format("Bem-vindo {0} com idade {1}", nome, idade));
        }

        [Route("cotacao/{preco:int:min(20)}")]
        public string GetPreco(int preco)
        {
            return HttpUtility.HtmlEncode(
                    string.Format("O preço do produto é {0}", preco));
        }

        [Route("cliente/{id:int:min(5):max(99)}")]
        public string GetCliente(int id)
        {
            return HttpUtility.HtmlEncode(
                    string.Format("Código do cliente é {0}", id));
        }
    }
}