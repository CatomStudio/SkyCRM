
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NLite.Data;
using teaCRM.DBContext;
using teaCRM.Entity;

namespace teaCRM.Dao
{
    public interface ITFunOperatingDao : ITableDao<TFunOperating>
{

        #region ��д��dao��

        /// <summary>
        /// ��ȡ������Ϣ�б� 2014-09-19 14:58:50 By �����
        /// </summary>
        /// <param name="compNum">��ҵ���</param>
        /// <param name="myappId">ģ��id</param>
        /// <param name="pageIndex">ҳ��</param>
        /// <param name="pageSize">ÿҳ����Ŀ</param>
        /// <param name="rowCount">����</param>
        /// <param name="orders">����</param>
        /// <param name="predicate">����</param>
        IEnumerable<TFunOperating> GetOperatingLsit(string compNum, int myappId, int pageIndex, int pageSize,
            out int rowCount,
            IDictionary<string, teaCRMEnums.OrderEmum> orders,
            Expression<Func<TFunOperating, bool>> predicate);

        #endregion




        #region ����ɾ����ͨ��id����

        bool DeleteMoreEntity(string ids);

        #endregion

}
	   }
