

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NLite.Data;
using teaCRM.DBContext;
using teaCRM.Entity;

namespace teaCRM.Dao
{
public  interface IVAppCompanyDao:IViewDao<VAppCompany>
    {
    #region ��д�Ľӿ�

    /// <summary>
    /// ����Ӧ���Ƿ�װ��
    /// </summary>
    /// <param name="compNum">��˾id</param>
    /// <param name="appId">Ӧ��id</param>
    /// <param name="appType">Ӧ������</param>
    /// <returns></returns>
    bool IsInstalled(string compNum, int appId, int appType);

    /// <summary>
    ///��װӦ��
    /// </summary>
    /// <param name="compNum">��˾id</param>
    /// <param name="appId">Ӧ��id</param>
    /// <returns></returns>
    bool Install(string compNum, int appId);

    ///  <summary>
    /// ж��Ӧ��
    ///  </summary>
    ///  <param name="compNum">��˾id</param>
    ///  <param name="appIds">Ӧ��id</param>
    /// <param name="isClear">�Ƿ��������</param>
    /// <returns></returns>
    bool UnInstall(string compNum, string appIds, bool isClear);

    #endregion

    }
	   }
