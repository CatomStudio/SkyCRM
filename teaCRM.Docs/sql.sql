--�ͻ���ͼ
SELECT * FROM t_cus_base AS cb INNER JOIN teacrm.t_cus_expvalue_10000 AS ce ON cb.id=ce.cus_id;

--����ͻ�����
INSERT  INTO `t_cus_base`(`cus_no`,`comp_num`,`cus_name`,`cus_sname`,`cus_lastid`,`cus_tel`,`cus_city`,`cus_address`,`cus_note`,`con_id`,`user_id`,`con_team`,`con_is_pub`,`con_back`) VALUES ('1',NULL,'ccc','֣���Ŵ��Ƽ�������޹�˾',NULL,'13893882883','����ʡ֣����','����������·��300��·��(��������)1��¥3��Ԫ3¥����','1',1,1,NULL,1,1);

--����ͻ���չ��

insert  into `t_cus_expvalue_10000`(`cus_id`,`exp_is_marry`,`exp_evaluate`,`exp_nation`,`exp_email`,`exp_age`) values (1,'1',NULL,'�й�','cyutyw@126.com',1);