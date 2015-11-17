﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using teaCRM.Common;
using teaCRM.Dao;
using teaCRM.Entity;
using teaCRM.Entity.Settings;

namespace teaCRM.Service.Settings.Impl
{
    public class AppMakerServiceImpl : IAppMakerService
    {
        public IVAppCompanyDao AppCompany { set; get; }
        public IVMyappCompanyDao MyAppCompany { set; get; }
        public ITFunExpandDao FunExpandDao { set; get; }
        public ITFunFilterDao FunFilterDao { set; get; }
        public ITFunOperatingDao FunOperatingDao { set; get; }

        /// <summary>
        /// 获取当前公司应用信息列表 2014-09-16 14:58:50 By 唐有炜
        /// </summary>
        /// <param name="compNum">企业编号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页的数目</param>
        /// <param name="rowCount">总数</param>
        /// <param name="orders">排序</param>
        /// <param name="predicate">条件</param>
        public IEnumerable<VAppCompany> GetAppLsit(string compNum, int pageIndex, int pageSize, out int rowCount,
            IDictionary<string, teaCRM.Entity.teaCRMEnums.OrderEmum> orders,
            Expression<Func<VAppCompany, bool>> predicate)
        {
            try
            {
                var apps = AppCompany.GetViewListByPage(pageIndex, pageSize, out rowCount, orders,
                    predicate);
                LogHelper.Debug("公司id为" + "的公司获取应用列表成功。");
                return apps;
            }
            catch (Exception ex)
            {
                rowCount = 0;
                LogHelper.Error("公司id为" + "的公司获取应用列表失败。", ex);
                return null;
            }
        }

        #region GetAllMyApps

        /// <summary>
        /// 获取当前公司某个应用的所有模块 14-09018 By 唐有炜
        /// </summary>
        /// <param name="compNum"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        public List<VMyappCompany> GetAllMyApps(string compNum, int appId)
        {
            var myapps = MyAppCompany.GetViewList(m => m.CompNum == compNum && m.AppId == appId);
            return myapps;
        }

        #endregion


        #region  当前公司某个模块的扩展字段列表 14-09-18 By 唐有炜

        /// <summary>
        /// 当前公司某个模块的扩展字段列表 14-09-18 By 唐有炜
        /// </summary>
        /// <param name="compNumm"></param>
        /// <param name="myappId"></param>
        /// <returns></returns>
        public DataTable GetAllMyAppFields(string compNumm, int myappId)
        {
            return FunExpandDao.GetExpandFields(compNumm, myappId);
        }

        #endregion

        #region  当前公司某个模块的视图列表 14-09-18 By 唐有炜

        /// <summary>
        /// 当前公司某个模块的视图列表 14-09-18 By 唐有炜
        /// </summary>
        /// <param name="compNum">企业编号</param>
        /// <param name="myappId">模块id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页的数目</param>
        /// <param name="rowCount">总数</param>
        /// <param name="orders">排序</param>
        /// <param name="predicate">条件</param>
        public IEnumerable<TFunFilter>
            GetAllMyAppViews(string compNum, int myappId, int pageIndex, int pageSize, out int rowCount,
                IDictionary<string, teaCRM.Entity.teaCRMEnums.OrderEmum> orders,
                Expression<Func<TFunFilter, bool>> predicate)
        {
            return FunFilterDao.GetFilterLsit(compNum, myappId, pageIndex, pageSize, out rowCount, orders, predicate);
        }

        #endregion

        #region  前公司某个模块的操作列表 14-09-18 By 唐有炜

        /// <summary>
        /// 当前公司某个模块的操作列表 14-09-18 By 唐有炜
        /// </summary>
        /// <param name="compNum">企业编号</param>
        /// <param name="myappId">模块id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页的数目</param>
        /// <param name="rowCount">总数</param>
        /// <param name="orders">排序</param>
        /// <param name="predicate">条件</param>
        public IEnumerable<TFunOperating>
            GetAllMyAppToolBars(string compNum, int myappId, int pageIndex, int pageSize, out int rowCount,
                IDictionary<string, teaCRM.Entity.teaCRMEnums.OrderEmum> orders,
                Expression<Func<TFunOperating, bool>> predicate)
        {
            return FunOperatingDao.GetOperatingLsit(compNum, myappId, pageIndex, pageSize, out rowCount, orders,
                predicate);
        }

        #endregion


        #region 获取操作

        public TFunExpand GetField(int id)
        {
            return FunExpandDao.GetEntity(e => e.Id == id);
        }

        public TFunFilter GetView(int id)
        {
            return FunFilterDao.GetEntity(f => f.Id == id);
        }



        public TFunOperating GetOperating(int id)
        {
            return FunOperatingDao.GetEntity(o=> o.Id == id);  
        }


        #endregion



        #region 添加操作

        public bool AddField(TFunExpand field)
        {
            return false;
        }

        public bool AddFilter(TFunFilter filter)
        {
            return FunFilterDao.InsertEntity(filter);
        }

        public bool AddOperating(TFunOperating operating)
        {
            return FunOperatingDao.InsertEntity(operating);
        }

        #endregion



        #region 修改操作

        public bool EditField(TFunExpand field)
        {
            return false;
        }

        public bool EditFilter(TFunFilter filter)
        {
            return FunFilterDao.UpadateEntity(filter);
        }

        public bool EditOperating(TFunOperating operating)
        {
            return FunOperatingDao.UpadateEntity(operating);
        }

        #endregion


        #region 删除操作

        public bool DeleteField(string ids)
        {
            return false;
        }

        public bool DeleteFilter(string ids)
        {
            return FunFilterDao.DeleteMoreEntity(ids);
        }

        public bool DeleteOperating(string ids)
        {

            return FunOperatingDao.DeleteMoreEntity(ids);
        }

        #endregion


        /// <summary>
        /// 检测该应用是否安装过
        /// </summary>
        /// <param name="compNum">公司id</param>
        /// <param name="appId">应用id</param>
        /// <param name="appType">应用类型</param>
        /// <returns></returns>
        public bool IsInstalled(string compNum, int appId, int appType)
        {
            return AppCompany.IsInstalled(compNum, appId, appType);
        }


        /// <summary>
        ///安装应用
        /// </summary>
        /// <param name="compNum">公司id</param>
        /// <param name="appId">应用id</param>
        /// <returns></returns>
        public bool Install(string compNum, int appId)
        {
            return AppCompany.Install(compNum, appId);
        }

        ///  <summary>
        /// 卸载应用
        ///  </summary>
        ///  <param name="compNum">公司id</param>
        ///  <param name="appIds">应用id</param>
        /// <param name="isClear">是否清空数据</param>
        /// <returns></returns>
        public bool UnInstall(string compNum, string appIds, bool isClear)
        {
            return AppCompany.UnInstall(compNum, appIds, isClear);
        }
    }
}