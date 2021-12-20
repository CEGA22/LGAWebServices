using EZWebServices.Models;
using EZWebServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Http;

namespace EZWebServices.Controllers
{
    public class LoginController : ApiController
    {
        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        [Route("api/lga/account/login")]
        public IHttpActionResult AccountLogin(LoginRequest request)
        {
            var loginService = new LoginService();
            return Ok(loginService.AccountLogin(request));
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        [Route("api/lga/account/studentlogin")]
        public IHttpActionResult StudentAccountLogin(StudentLoginRequest studentrequest)
        {
            var loginService = new LoginService();
            return Ok(loginService.StudentAccountLogin(studentrequest));
        }
    }
}