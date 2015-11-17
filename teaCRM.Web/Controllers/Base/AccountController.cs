﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using teaCRM.Common;
using teaCRM.Entity;
using teaCRM.Service;
using teaCRM.Service.Impl;
using teaCRM.Web.Filters;

namespace teaCRM.Web.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// Web层注入账户Service接口 2014-08-26 14:58:50 By 唐有炜
        /// </summary>
        public IAccountService AccountService { set; get; }

        #region 账户验证

        //
        // GET: /Account/ValidateAccount/ 账户验证 
        public string ValidateAccount(string validate_action, string userName, string userPassword = null)
        {
            userName = HttpUtility.UrlDecode(userName);
            ResponseMessage rmsg = null;
            switch (validate_action)
            {
                case  "login"://登陆
                    rmsg = AccountService.ValidateAccount("login", "normal",
                userName, userPassword);
                    break;
                case "public_register"://注册
                    rmsg = AccountService.ValidateAccount("register", "normal",
              userName+"@10000");
                    break;
            }
        
            return rmsg.Status.ToString().ToLower();
        }


        //
        // GET: /Account/ValidateAccount/ 账户验证 
        public string ValidatePhone(string validate_action, string userPhone, string userPassword = null)
        {
            userPhone = HttpUtility.UrlDecode(userPhone);
            ResponseMessage rmsg = null;
            switch (validate_action)
            {
                case "login"://登陆
                    rmsg = AccountService.ValidateAccount("login", "normal",
                userPhone, userPassword);
                    break;
                case "public_register"://注册
                    rmsg = AccountService.ValidateAccount("register", "normal",
              userPhone);
                    break;
            }

            return rmsg.Status.ToString().ToLower();
        }

        #endregion

        #region 登录

        //
        // GET: /Account/Login 默认登录页面 2014-08-26 14:58:50 By 唐有炜
        [HttpGet]
        [AutoLogin]
        public ActionResult Login()
        {




//            LogHelper.Info("登录页面被打开。");
            return View();
        }

        //
        // GET: /Account/Login 登录提交
        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            ResponseMessage rmsg;
            try
            {
                LogHelper.Info("来自" + HttpUtility.UrlDecode(fc["clientPlace"].ToString()) + "的" +
                               fc["userName"].ToString() + "正在登录...");
                rmsg = AccountService.Login(System.Web.HttpContext.Current, fc["type"].ToString(),
                    fc["accountType"].ToString(), fc["userName"].ToString(), fc["userPassword"].ToString(),
                    fc["remember"].ToString(),
                    fc["clientIp"].ToString(), HttpUtility.UrlDecode(fc["clientPlace"].ToString()),
                    fc["clientTime"].ToString());
            }
            catch (Exception ex)
            {
                LogHelper.Error("登录异常," + ex.Message);
                rmsg = new ResponseMessage() {Status = false, Msg = "登录异常，请联系管理员！"};
            }

            return Json(rmsg);
        }

        #region 自动提示

        //
        // GET: /Account/UserNameAuto/ 自动提示
        public ActionResult UserNameAuto(string query)
        {
            string[] emails = new string[]
            {
                "10000", "126.com", "163.com", "yeah.net", "sina.com", "sina.cn", "qq.com", "vip.qq.com", "sohu.com",
                "live.com", "msn.cn", "gmail.com"
            };
            List<KeyValue> results = new List<KeyValue>();
            if (String.IsNullOrEmpty(query))
            {
                results.Add(new KeyValue() {value = "暂无结果", data = "0"});
                return Json(results, JsonRequestBehavior.AllowGet);
            }

            for (int i = 0; i < emails.Length; i++)
            {
                var email = emails[i];
                KeyValue item = new KeyValue();
                if (query.Contains("@")) //有@才提示
                {
                    string query2 = query.Split('@')[1];
                    if (email.StartsWith(query2))
                    {
                        item.value = query.Split('@')[0] + "@" + email.Trim();
                        item.data = i.ToString();
                    }
                    results.Add(item);
                }
//                else
//                {
//                    item.value = query + "@" + email.Trim();
//                    item.data = i.ToString();
//                }
            }
            results = Utils.RemoveEmptyList(results);
            AutoStruct autoStruct = new AutoStruct();
            autoStruct.query = "Unit";
            autoStruct.suggestions = results;
            return Json(autoStruct, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 返回结果
        /// </summary>
        private class AutoStruct
        {
            public string query { set; get; }
            public List<KeyValue> suggestions { set; get; }
        }

   
        #endregion

        #endregion

        #region 平台注册（开户）

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View("Register");
        }


        //
        // GET: /Account/EmailRegister

        public ActionResult EmailRegister()
        {
            return View("EmailRegister");
        }

        //
        // GET: /Account/PhoneRegister

        public ActionResult PhoneRegister()
        {
            return View("PhoneRegister");
        }

        #endregion

        #region  公共注册

        //
        // GET: /Account/PublicRegister

        public ActionResult PublicRegister()
        {
            return View("PublicRegister");
        }

        //
        // Post: /Account/PublicRegister
        [HttpPost]
        public ActionResult PublicRegister(FormCollection fc)
        {
            //IAccountService AccountService = new teaCRM.Service.Impl.AccountServiceImpl();
            ResponseMessage rmsg = AccountService.PublicRegister(System.Web.HttpContext.Current, fc["userName"],
                fc["phone"], fc["userPassword"],
                HttpUtility.UrlDecode(fc["userTname"]));
            return Json(rmsg);
        }

        #endregion

        #region 退出

        public ActionResult Logout()
        {
            Session.Remove(teaCRMKeys.SESSION_USER_COMPANY_INFO_ID);
            Session.Remove(teaCRMKeys.SESSION_USER_COMPANY_INFO_NUM);
            return View("Login");
        }

        #endregion
    }
}