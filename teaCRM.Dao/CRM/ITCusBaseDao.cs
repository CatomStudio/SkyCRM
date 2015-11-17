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
    public interface ITCusBaseDao : ITableDao<TCusBase>
    {
        #region ��д����չ���� 2014-09-05 14:58:50 By �����



        


        /// <summary>
        /// ��ӿͻ���Ϣ 2014-08-30 14:58:50 By �����
        /// </summary>
        /// <param name="cusBase">�ͻ���Ϣ</param>
        /// <param name="cusCon">����ϵ����Ϣ</param>
        /// <returns></returns>
        bool AddCustomer(TCusBase cusBase, TCusCon cusCon);

        /// <summary>
        /// ɾ���ͻ���Ϣ 2014-08-30 14:58:50 By �����
        /// </summary>
        /// <param name="customerId">�ͻ�id</param>
        /// <returns>ɾ��״̬</returns>
        bool DeleteCustomer(int customerId);

        /// <summary>
        /// �޸Ŀͻ���Ϣ 2014-08-30 14:58:50 By �����
        /// </summary>
        /// <param name="customerId">�ͻ�id</param>
        /// <param name="cusBase">�ͻ���Ϣ</param>
        /// <param name="cusCon">����ϵ����Ϣ</param>
        /// <returns></returns>
        bool EditCustomer(int customerId, TCusBase cusBase, TCusCon cusCon);

        /// <summary>
        /// ��ȡ�ͻ���Ϣ�б�
        /// </summary>
        /// <param name="compNum">��ҵ���</param>
        /// <param name="selectFields">ѡ����ֶ�</param>
        /// <param name="pageIndex">ҳ��</param>
        /// <param name="pageSize">ÿҳ����Ŀ</param>
        /// <param name="strWhere">ɸѡ����</param>
        /// <param name="filedOrder">�����ֶ�</param>
        /// <param name="recordCount">��¼���</param>
        /// <returns>DataTable</returns>
        DataTable GetCustomerLsit(string compNum, string[] selectFields, int pageIndex, int pageSize,
            string strWhere, string filedOrder,
            out int recordCount);

        /// <summary>
        /// ��ȡ�ͻ���Ϣ�б� 2014-08-29 14:58:50 By �����
        /// </summary>
        /// <param name="pageIndex">ҳ��</param>
        /// <param name="pageSize">ÿҳ����Ŀ</param>
        /// <param name="selector">Ҫ��ѯ���ֶ�</param>
        /// <param name="expFields">�洢��չ�ֶ�ֵ���ֶ�</param>
        /// <param name="expSelector">Ҫ��ѯ����չ�ֶ�</param>
        /// <param name="predicate">��ѯ����</param>
        /// <param name="ordering">����</param>
        /// <param name="recordCount">��¼�����</param>
        /// <param name="values">����</param>
        /// <returns>�ͻ���Ϣ�б�</returns>
        List<Dictionary<string, object>> GetCustomerLsit(int pageIndex, int pageSize, string selector, string expFields, string expSelector,
            string predicate, string ordering,
            out int recordCount, params object[] values);



        /// <summary>
        /// Gets the customer.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="expFields">The exp fields.</param>
        /// <param name="expSelector">The exp selector.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="values">The values.</param>
        /// <returns>Dictionary&lt;System.String, System.Object&gt;.</returns>
        Dictionary<string, object> GetCustomer(string selector, string expFields, string expSelector, string predicate,
            params object[] values);


        /// <summary>
        /// ʹ��where sql�����Ŀͻ�״̬(ֻ��������) 2014-09-05 14:58:50 By �����
        /// </summary>
        /// <param name="strSet">Ҫ���µ��ֶ�</param>
        /// <param name="strWhere">����</param>
        /// <returns></returns>
        bool UpdateCustomerStatusByWhere(string strSet, string strWhere);


        /// <summary>
        /// ʹ��LINQ��������TCusBase״̬ 2014-09-05 14:58:50 By �����
        /// </summary>
        /// <param name="fields">Ҫ���µ��ֶΣ�֧���������£�</param>
        /// <param name="predicate">����</param>
        /// <returns></returns>
        bool UpdateTCusBaseStatusByLINQ(Dictionary<string, object> fields,
            Expression<Func<TCusBase, bool>> predicate);


        /// <summary>
        /// ������״̬
        /// </summary>
        /// <param name="cus_ids">id����</param>
        /// <param name="op">������0 1��</param>
        /// <param name="field">�ֶ�</param>
        /// <returns></returns>
        bool UpdateStatusMoreCustomer(string cus_ids, int op, string field);


        #endregion
    }
}