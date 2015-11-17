using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NLite.Data;
using teaCRM.DBContext;
using teaCRM.Entity;

namespace teaCRM.Dao
{
    public interface ITFunFilterDao : ITableDao<TFunFilter>
    {
        #region ��д����չ���� 2014-08-21 14:58:50 By �����


        /// <summary>
        /// ��ȡ��ͼ����ɸѡ��������Ϣ�б� 2014-09-19 14:58:50 By �����
        /// </summary>
        /// <param name="compNum">��ҵ���</param>
        /// <param name="myappId">ģ��id</param>
        /// <param name="pageIndex">ҳ��</param>
        /// <param name="pageSize">ÿҳ����Ŀ</param>
        /// <param name="rowCount">����</param>
        /// <param name="orders">����</param>
        /// <param name="predicate">����</param>
        IEnumerable<TFunFilter> GetFilterLsit(string compNum, int myappId, int pageIndex, int pageSize, out int rowCount,
            IDictionary<string, teaCRM.Entity.teaCRMEnums.OrderEmum> orders,
            Expression<Func<TFunFilter, bool>> predicate);







        bool DeleteMoreEntity(string ids);
        #endregion
    }
}