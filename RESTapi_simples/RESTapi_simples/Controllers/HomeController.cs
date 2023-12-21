using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Security;
using RESTapi_simples.Models;
using System.Diagnostics;
using LayerDAL.Repository;
using LayerDAL.Entities;


namespace RESTapi_simples.Controllers
{
    public class HomeController : Controller

    { 

            private readonly IUserRepository _repository;
                
            public HomeController(ILogger repository) 
            {
            
                _repository = (IUserRepository?)repository;
            
            }

            public IActionResult Index() 
            {
            
                return View();

            
            }



        public IActionResult Privacy()
        {

            return View();


        }

        public async Task<IActionResult> ShowUser() 
        {

            List<UserInfo> ListUser = await _repository.GetUserInfos();

            return View();


        
        }
        


    }
}
