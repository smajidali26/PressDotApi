using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using PressDot.Contracts.Request.Customer;
using PressDot.Contracts.Response;
using PressDot.Contracts.Response.Users;
using PressDot.Core.Exceptions;
using PressDot.Facade.Framework.Extensions;
using PressDot.Facade.Infrastructure;
using PressDot.Framework.Attribute;
using PressDot.Service.Infrastructure;

namespace PressDot.Controllers
{
    [Route("api/v1/Account")]
    public class AccountController : BaseController
    {
        #region private

        private readonly IUsersFacade _usersFacade;

        private readonly IUserRoleService _usersRoleService;
        #endregion

        #region ctor

        public AccountController(IUsersFacade usersFacade, IUserRoleService usersRoleService)
        {
            _usersFacade = usersFacade;
            _usersRoleService = usersRoleService;
        }

        #endregion

        #region Methods

        [HttpPost]
        [Route("Register")]
        public IActionResult Create(RegisterUsersRequest request)
        {
            return Ok(_usersFacade.RegisterUser(request));
        }

        //[Authorize]
        [HttpGet]
        [Route("GetRoles")]
        public IActionResult GetRoles()
        {
            return Ok(_usersRoleService.Get().ToModel<UsersRoleResponse>());
        }

        [HttpPost]
        [Route("Authenticate")]
        public IActionResult Authenticate(string username,string password)
        {
            return Ok(_usersFacade.AuthenticateUser(username,password));
        }

        [Authorize]
        [HttpPost]
        [Route("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            return Ok(_usersFacade.ChangePassword(changePasswordRequest.UserId, changePasswordRequest.Password,changePasswordRequest.OldPassword));
        }

        [HttpPost]
        [Route("ChangePasswordByToken")]
        public IActionResult ChangePasswordByToken(ChangePasswordByTokenRequest changePasswordByTokenRequest)
        {
            return Ok(_usersFacade.ChangePassword(changePasswordByTokenRequest.Token, changePasswordByTokenRequest.Password));
        }

        [HttpPost]
        [Route("ActivateAccount")]
        public IActionResult ActivateAccount(string token)
        {
            return Ok(_usersFacade.ActivateAccount(token));
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            return Ok(_usersFacade.ForgotPassword(email));
        }

        #endregion
    }
}
