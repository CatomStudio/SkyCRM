using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using NLite.Data;
using teaCRM.DBContext;
using teaCRM.Entity;

namespace teaCRM.Dao
{
    public interface ITFunExpandDao : ITableDao<TFunExpand>
    {
        #region ��д����չ���� 2014-08-21 14:58:50 By �����

        /// <summary>
        /// ��ѯĳ��ģ�����չ�ֶ�
        /// </summary>
        /// <param name="compNum">��˾���</param>
        /// <param name="myappId">ģ��id</param>
        /// <returns></returns>
        DataTable GetExpandFields(string compNum,int myappId);




        #endregion
    }
}