using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;
using Core.Interfaces;
using Core.Entities;
using System.ComponentModel.DataAnnotations;
using Core.DTO;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
       
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthService _authService;
        private readonly IAccountService _accountService;
        private readonly IActionContextAccessor _accessor;

        public HomeController(ILogger<HomeController> logger, IAuthService authService, IAccountService accountService, IActionContextAccessor accessor)
        {
            _logger = logger;
            _authService = authService;
            _accountService = accountService;
            _accessor = accessor;
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var user = _authService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [Authorize(Roles=Role.Admin)]
        [HttpGet]
        [Route("find/{id}")]
        public async Task<IActionResult> FindAccountAsync([RegularExpression("^(36)([0-9]{6})$", ErrorMessage = "Bad Account")]string id)
        {
            var result = await _accountService.FindAccount(id);
            return  Ok(result);

        }


        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        [Route("transfer")]
        public async Task<IActionResult> TransferAsync([FromBody]TransferRequest model)
        {
            model.IpAddress= _accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString();
            var result = await _accountService.Transfer(model);
            return Ok(result);

        }


        [HttpGet]
        [Route("report")]
        public async Task<IActionResult> ReportAsync()
        {
            return Ok(await _accountService.GetReportData());
        }

    }
}
