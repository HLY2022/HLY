// ***********************************************************************
// Assembly         : HLY.Mvc
// Author           : yubaolee
// Created          : 07-11-2016
//
// Last Modified By : yubaolee
// Last Modified On : 07-19-2016
// Contact : www.cnblogs.com/yubaolee
// File: BaseController.cs
// ***********************************************************************

using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using HLY.App.Interface;

namespace HLY.Mvc.Controllers
{
    public class BaseController : Controller
    {
        protected Response Result = new Response();
        protected IAuth _authUtil;

        public BaseController(IAuth authUtil)
        {
            _authUtil = authUtil;
        }

    }
}