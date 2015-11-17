﻿// ***********************************************************************
// 程序集         : teaCRM.Web
// 作者作者           : Tangyouwei
// 创建时间          : 09-13-2014
//
// 最后修改人: Tangyouwei
// 最后修改时间 : 09-28-2014
// ReSharper disable All 禁止ReSharper显示警告
// ***********************************************************************
// <copyright file="BundleConfig.cs" company="优创科技">
//     Copyright (c) 优创科技. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Web;
using System.Web.Optimization;

/// <summary>
/// The Web namespace.
/// </summary>

namespace teaCRM.Web
{
    /// <summary>
    /// Class BundleConfig.
    /// </summary>
    public class BundleConfig
    {
//        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        /// <summary>
        /// Registers the bundles.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region jquery、json2、bootstrap、jquery.cookie、artDialog、jquery.validate、jquery.poshytip、function、globle 类库1（适用于弹出框里面的表单）

            bundles.Add(new ScriptBundle("~/Themes/default/js/cp.globle.min1.js")
                .Include(
                    "~/Themes/default/lib/jquery/jquery-1.*")
                       .Include("~/Themes/default/lib/json/json2.js")
                .Include("~/Themes/default/lib/bootstrap/js/bootstrap-3.2.0.js")
                .Include("~/Themes/default/lib/cookie/jquery.cookie.js")
                .Include("~/Themes/default/lib/artDialog/dist/dialog-plus-min.js")
                .Include("~/Themes/default/lib/validate/jquery.validate.min.js")
                .Include("~/Themes/default/lib/validate/messages_cn.js")
                .Include("~/Themes/default/lib/validate/jquery.validate.extend.js")
                .Include("~/Themes/default/lib/poshytip/jquery.poshytip.min.js")
                .Include("~/Themes/default/lib/function.js"));

            bundles.Add(
                new StyleBundle("~/Themes/default/css/cp.globle.min1.css")
                    .Include("~/Themes/default/lib/bootstrap/css/bootstrap-3.2.0.css")
                    .Include("~/Themes/default/base/css/globle.css")
                    .Include("~/Themes/default/lib/artDialog/css/ui-dialog.css")
                    .Include("~/Themes/default/lib/poshytip/tip-yellow/tip-yellow.css")
                );

            #endregion

            #region jquery、bootstrap、jquery.cookie、artDialog、jquery.validate、jquery.poshytip、zTree、function、globle 类库2（适用于弹出框里面的表单，有树形）

            bundles.Add(new ScriptBundle("~/Themes/default/js/cp.globle.min2.js")
                .Include(
                    "~/Themes/default/lib/jquery/jquery-1.*")
                .Include("~/Themes/default/lib/bootstrap/js/bootstrap-3.2.0.js")
                .Include("~/Themes/default/lib/cookie/jquery.cookie.js")
                .Include("~/Themes/default/lib/artDialog/dist/dialog-plus-min.js")
                .Include("~/Themes/default/lib/validate/jquery.validate.min.js")
                .Include("~/Themes/default/lib/validate/messages_cn.js")
                .Include("~/Themes/default/lib/validate/jquery.validate.extend.js")
                .Include("~/Themes/default/lib/poshytip/jquery.poshytip.min.js")
                .Include("~/Themes/default/lib/zTree/js/jquery.ztree.all-3.5.min.js")
                .Include("~/Themes/default/lib/zTree/js/jquery.ztree.core-3.5.min.js")
                .Include("~/Themes/default/lib/zTree/js/jquery.ztree.excheck-3.5.min.js")
                .Include("~/Themes/default/lib/zTree/js/jquery.ztree.exedit-3.5.min.js")
                .Include("~/Themes/default/lib/function.js"));

            bundles.Add(
                new StyleBundle("~/Themes/default/css/cp.globle.min2.css")
                    .Include("~/Themes/default/lib/bootstrap/css/bootstrap-3.2.0.css")
                    .Include("~/Themes/default/base/css/globle.css")
                    .Include("~/Themes/default/lib/artDialog/css/ui-dialog.css")
                    .Include("~/Themes/default/lib/poshytip/tip-yellow/tip-yellow.css")
                    .Include("~/Themes/default/lib/zTree/css/zTreeStyle/zTreeStyle.css")
                );

            #endregion

            #region jquery、json2,artDialog、zTree、function、globe、head、foot 类库3（适用于有树形一般页面）

            bundles.Add(new ScriptBundle("~/Themes/default/js/cp.globle.min3.js")
                .Include("~/Themes/default/lib/jquery/jquery-1.*")
                .Include("~/Themes/default/lib/json/json2.js")
                .Include("~/Themes/default/lib/artDialog/dist/dialog-plus-min.js")
                .Include("~/Themes/default/lib/zTree/js/jquery.ztree.all-3.5.min.js")
                .Include("~/Themes/default/lib/bootstrap/js/bootstrap-3.2.0.js")
                .Include("~/Themes/default/lib/jquery.bootgrid/jquery.bootgrid.js")
                .Include("~/Themes/default/lib/function.js")
                );

            bundles.Add(
                new StyleBundle("~/Themes/default/css/cp.globle.min3.css")
                    .Include("~/Themes/default/lib/bootstrap/css/bootstrap-3.2.0.css")
                    .Include("~/Themes/default/base/css/globle.css")
                    .Include("~/Themes/default/base/css/head.css")
                    .Include("~/Themes/default/base/css/foot.css")
                    .Include("~/Themes/default/lib/artDialog/css/ui-dialog.css")
                    .Include("~/Themes/default/lib/zTree/css/zTreeStyle/zTreeStyle.css")
                    .Include("~/Themes/default/lib/jquery.bootgrid/jquery.bootgrid.css")
                );

            #endregion

            #region jquery、artDialog、function、globe 类库4（适用于简单页面）

            bundles.Add(new ScriptBundle("~/Themes/default/js/cp.globle.min4.js")
                .Include("~/Themes/default/lib/jquery/jquery-1.*")
                .Include("~/Themes/default/lib/artDialog/dist/dialog-plus-min.js")
                .Include("~/Themes/default/lib/function.js")
                );

            bundles.Add(
                new StyleBundle("~/Themes/default/css/cp.globle.min4.css")
                    .Include("~/Themes/default/base/css/globle.css")
                    .Include("~/Themes/default/lib/artDialog/css/ui-dialog.css")
                );

            #endregion

            #region jquery、artDialog、function、globe、head、foot 类库5(一般页面、不带验证)

            bundles.Add(new ScriptBundle("~/Themes/default/js/cp.globle.min5.js")
                .Include("~/Themes/default/lib/jquery/jquery-1.*")
                .Include("~/Themes/default/lib/artDialog/dist/dialog-plus-min.js")
                .Include("~/Themes/default/lib/function.js")
                );

            bundles.Add(
                new StyleBundle("~/Themes/default/css/cp.globle.min5.css")
                    .Include("~/Themes/default/base/css/globle.css")
                    .Include("~/Themes/default/base/css/head.css")
                    .Include("~/Themes/default/base/css/foot.css")
                    .Include("~/Themes/default/lib/artDialog/css/ui-dialog.css")
                );

            #endregion

            #region jquery、bootstrap、jquery.cookie、artDialog、function、head、foot 类库6（适用于列表页面，使用bootgrid）

            bundles.Add(new ScriptBundle("~/Themes/default/js/cp.globle.min6.js")
                .Include(
                    "~/Themes/default/lib/jquery/jquery-1.*")
                .Include("~/Themes/default/lib/bootstrap/js/bootstrap-3.2.0.js")
                .Include("~/Themes/default/lib/jquery.bootgrid/jquery.bootgrid.js")
                .Include("~/Themes/default/lib/cookie/jquery.cookie.js")
                .Include("~/Themes/default/lib/artDialog/dist/dialog-plus-min.js")
                .Include("~/Themes/default/lib/function.js")
                );

            bundles.Add(
                new StyleBundle("~/Themes/default/css/cp.globle.min6.css")
                    .Include("~/Themes/default/lib/bootstrap/css/bootstrap-3.2.0.css")
                    .Include("~/Themes/default/base/css/globle.css")
                    .Include("~/Themes/default/base/css/head.css")
                    .Include("~/Themes/default/base/css/foot.css")
                    .Include("~/Themes/default/lib/jquery.bootgrid/jquery.bootgrid.css")
                    .Include("~/Themes/default/lib/artDialog/css/ui-dialog.css")
                );

            #endregion

            #region jquery、jquery.cookie、artDialog、jquery.validate、jquery.poshytip、function、globle 类库7（适用于登陆、注册）

            bundles.Add(new ScriptBundle("~/Themes/default/js/cp.globle.min7.js")
                .Include(
                    "~/Themes/default/lib/jquery/jquery-1.*")
                .Include("~/Themes/default/lib/cookie/jquery.cookie.js")
                .Include("~/Themes/default/lib/artDialog/dist/dialog-plus-min.js")
                .Include("~/Themes/default/lib/validate/jquery.validate.min.js")
                .Include("~/Themes/default/lib/validate/messages_cn.js")
                .Include("~/Themes/default/lib/validate/jquery.validate.extend.js")
                .Include("~/Themes/default/lib/poshytip/jquery.poshytip.min.js")
                .Include("~/Themes/default/lib/function.js"));

            bundles.Add(
                new StyleBundle("~/Themes/default/css/cp.globle.min7.css")
                    .Include("~/Themes/default/base/css/globle.css")
                    .Include("~/Themes/default/lib/artDialog/css/ui-dialog.css")
                    .Include("~/Themes/default/lib/poshytip/tip-yellow/tip-yellow.css")
                );

            #endregion

            #region jquery、bootstrap、jquery.cookie、artDialog、function、head、foot 类库8（适用于列表页面，使用bootgrid、Ztree）

            bundles.Add(new ScriptBundle("~/Themes/default/js/cp.globle.min8.js")
                .Include(
                    "~/Themes/default/lib/jquery/jquery-1.*")
                .Include("~/Themes/default/lib/json/json2.js")
                .Include("~/Themes/default/lib/bootstrap/js/bootstrap-3.2.0.js")
                .Include("~/Themes/default/lib/jquery.bootgrid/jquery.bootgrid.js")
                .Include("~/Themes/default/lib/cookie/jquery.cookie.js")
                .Include("~/Themes/default/lib/artDialog/dist/dialog-plus-min.js")
                .Include("~/Themes/default/lib/zTree/js/jquery.ztree.all-3.5.min.js")
                .Include("~/Themes/default/lib/zTree/js/jquery.ztree.core-3.5.min.js")
                .Include("~/Themes/default/lib/zTree/js/jquery.ztree.excheck-3.5.min.js")
                .Include("~/Themes/default/lib/zTree/js/jquery.ztree.exedit-3.5.min.js")
                .Include("~/Themes/default/lib/function.js")
                );

            bundles.Add(
                new StyleBundle("~/Themes/default/css/cp.globle.min8.css")
                    .Include("~/Themes/default/lib/bootstrap/css/bootstrap-3.2.0.css")
                    .Include("~/Themes/default/base/css/globle.css")
                    .Include("~/Themes/default/base/css/head.css")
                    .Include("~/Themes/default/base/css/foot.css")
                    .Include("~/Themes/default/lib/artDialog/css/ui-dialog.css")
                    .Include("~/Themes/default/lib/jquery.bootgrid/jquery.bootgrid.css")
                    .Include("~/Themes/default/lib/zTree/css/zTreeStyle/zTreeStyle.css")
                );

            #endregion
        }
    }
}