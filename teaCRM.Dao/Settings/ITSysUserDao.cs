using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using NLite.Data;
using teaCRM.DBContext;
using teaCRM.Entity;

namespace teaCRM.Dao
{
    public interface ITSysUserDao : ITableDao<TSysUser>
    {
        #region ��д����չ���� 2014-08-21 14:58:50 By �����
          #region �û����Ƿ���� 14-09-12 By �����

        /// <summary>
        /// ���ù�˾�µ��˺����Ƿ��ظ�
        /// </summary>
        /// <param name="UserLName"></param>
        /// <param name="compNum"></param>
        /// <returns></returns>
        bool ExistsUser(string UserLName, string compNum);

        #endregion

        #endregion
    }
}