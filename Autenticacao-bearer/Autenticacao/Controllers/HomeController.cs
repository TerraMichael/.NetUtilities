using Autenticacao.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Autenticacao.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            var client = new RestClient("http://localhost:50753");

            var request = new RestRequest("api/security/token", Method.POST);
            request.AddParameter("grant_type", "password");
            request.AddParameter("username", username);
            request.AddParameter("password", password);

            IRestResponse<TokenViewModel> response = client.Execute<TokenViewModel>(request);
            var token = response.Data.access_token;

            if (!string.IsNullOrEmpty(token))
                FormsAuthentication.SetAuthCookie(token, false);

            return RedirectToAction("Index");
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}