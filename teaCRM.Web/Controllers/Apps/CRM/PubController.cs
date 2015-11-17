// ***********************************************************************
// 程序集         : teaCRM.Web
// 作者作者           : Tangyouwei
// 创建时间          : 09-13-2014
//
// 最后修改人: Tangyouwei
// 最后修改时间 : 09-26-2014
// ReSharper disable All 禁止ReSharper显示警告
// ***********************************************************************
// <copyright file="PubController.cs" company="优创科技">
//     Copyright (c) 优创科技. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/// <summary>
/// The CRM namespace.
/// </summary>
using teaCRM.Service;
using teaCRM.Service.CRM;
using teaCRM.Service.Settings;

namespace teaCRM.Web.Controllers.Apps.CRM
{
    /// <summary>
    /// Class PubController.
    /// </summary>
    public class PubController : Controller
    {
        #region  Service注入  14-09-26 By 唐有炜

        /// <summary>
        /// Gets or sets the customer service.
        /// </summary>
        /// <value>The customer service.</value>
        public ICustomerService CustomerService { set; get; }

        /// <summary>
        /// Gets or sets the account service.
        /// </summary>
        /// <value>The account service.</value>
        public IAccountService AccountService { set; get; }

        #endregion


        #region 公海客户首页

        //
        // GET: /Apps/CRM/Pub/

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Index()
        {
            return View("PubIndex");
        }

        #endregion

    }
}
