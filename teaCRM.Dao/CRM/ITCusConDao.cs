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
    public interface ITCusConDao : ITableDao<TCusCon>
    {
        #region ��д����չ���� 2014-08-21 14:58:50 By �����

        /// <summary>
        /// ��ȡ��ϵ����Ϣ�б�
        /// </summary>
        /// <param name="compNum">��ҵ���</param>
        /// <param name="selectFields">ѡ����ֶ�</param>
        /// <param name="pageIndex">ҳ��</param>
        /// <param name="pageSize">ÿҳ����Ŀ</param>
        /// <param name="strWhere">ɸѡ����</param>
        /// <param name="filedOrder">�����ֶ�</param>
        /// <param name="recordCount">��¼����</param>
        /// <returns>DataTable</returns>
        DataTable GetContactLsit(string compNum, string[] selectFields, int pageIndex, int pageSize,
            string strWhere, string filedOrder, out int recordCount);


        /// <summary>
        /// ʹ��LINQ��������TCusCon�ֶ� 2014-09-05 14:58:50 By ����쿣�ע�⣬�ֶ�������Ҫһһ��Ӧ
        /// </summary>
        /// <param name="fields">Ҫ���µ��ֶΣ�֧���������£�</param>
        /// <param name="predicates">��������</param>
        /// <returns><c>true</c>����״̬</returns>
        bool UpdateTCusConFieldsByLINQ(List<KeyValuePair<string, object>> fields,
            List<Expression<Func<TCusCon, bool>>> predicates);




        /// <summary>
        /// ������״̬
        /// </summary>
        /// <param name="con_ids">The con_ids.</param>
        /// <param name="op">������0 1��</param>
        /// <param name="field">�ֶ�</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool UpdateStatusMoreContact(string con_ids, int op, string field);



        #endregion
    }
}