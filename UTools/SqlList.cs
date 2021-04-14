using System.Drawing.Printing;

namespace UTools
{
    public class SqlList
    {
        string _result = string.Empty;

        /// <summary>
        /// 可销控报表使用
        /// </summary>
        /// <param name="customfnumber">客户编码</param>
        /// <param name="materialfnumber">物料编码</param>
        /// <param name="customername">客户名称</param>
        /// <param name="materialname">物料名称</param>
        /// <returns></returns>
        public string Get_SalesList(string customfnumber,string materialfnumber,string customername,string materialname)
        {
            _result = $@"SELECT   '客户' 授权对象类型,t8.FNAME AS 授权对象,t7.FNUMBER AS 授权对象编码,
                                   t12.mc 商品名称,t12.cCode 商品编码,'037214' 组织编码,'雅图高新材料有限公司' 组织名称

                        FROM    AIS20181204095717.dbo.T_SAL_PRICELIST AS t1 
                        INNER JOIN AIS20181204095717.dbo.T_SAL_PRICELIST_L AS t1_1 ON t1.FID = t1_1.FID 
                        LEFT OUTER JOIN AIS20181204095717.dbo.T_SAL_PRICELISTENTRY AS t2 ON t1.FID = t2.FID 
                        LEFT OUTER JOIN AIS20181204095717.dbo.T_SAL_APPLYCUSTOMER AS t3 ON t1.FID = t3.FID 
                        LEFT OUTER JOIN AIS20181204095717.dbo.T_BAS_ASSISTANTDATAENTRY_L AS t4 ON t1.FPRICETYPE = t4.FENTRYID 

                        LEFT OUTER JOIN AIS20181204095717.dbo.T_BD_MATERIAL AS t5 ON t2.FMATERIALID = t5.FMATERIALID 
                        LEFT OUTER JOIN AIS20181204095717.dbo.T_BD_MATERIAL_L AS t6 ON t5.FMATERIALID = t6.FMATERIALID 
                        LEFT OUTER JOIN AIS20181204095717.dbo.T_BD_MATERIALGROUP_L AS t10 ON t5.FMATERIALGROUP = t10.FID 

                        LEFT OUTER JOIN AIS20181204095717.dbo.T_BD_CUSTOMER AS t7 ON t3.FCUSTID = t7.FCUSTID 
                        LEFT OUTER JOIN AIS20181204095717.dbo.T_BD_CUSTOMER_L AS t8 ON t7.FCUSTID = t8.FCUSTID 

                        LEFT OUTER JOIN AIS20181204095717.dbo.T_BD_UNIT_L AS t9 ON t2.FUNITID = t9.FUNITID 
                        LEFT OUTER JOIN AIS20181204095717.dbo.T_BAS_ASSISTANTDATAENTRY_L AS t11 ON t5.F_YTC_ASSISTANT = t11.FENTRYID 

                        INNER JOIN  dbo.uwlda t12 ON t5.FNUMBER=t12.wlbh --t6.FNAME=t12.mc  --U商品档案

                        INNER JOIN  dbo.uAgent t13 ON t7.FNUMBER=t13.cErpCode   --U客户档案

                        WHERE   (GETDATE() BETWEEN t1.FEFFECTIVEDATE AND t2.FEXPRIYDATE) 
                        AND (t1.FDOCUMENTSTATUS = 'C') 
                        AND (t5.FNUMBER NOT LIKE '%CK') 
                        AND (t8.FNAME NOT LIKE '%N') 
                        AND (t7.FNUMBER NOT LIKE 'INT%') 
                        AND (t1.FFORBIDSTATUS = 'A') 
                        AND (t7.FNUMBER NOT LIKE '%.JC') AND (t7.FNUMBER NOT LIKE '%.YC') 
                        AND (t9.FLOCALEID <> 1033) 
                        AND (dbo.GetIncludMAT(t7.FCUSTID, t5.FMATERIALID, t5.FMATERIALGROUP) > 0) 
                        AND (LEN(t6.FNAME) > 0)
                        AND (t7.FNUMBER NOT LIKE '%.YC') /*AND (t7.FNUMBER NOT LIKE '%GY%')*/ 
                        AND (t7.FNUMBER NOT LIKE '%-合%') 
                        --AND (t7.FNUMBER NOT LIKE '%SX%')
                        AND t1.F_YTC_COMBO IN('U订货特殊价','U订货5折常规价')
                        AND t2.FFORBIDSTATUS != 'B'
                        
                        AND (t7.FNUMBER like '%{customfnumber}%' or '{customfnumber}' is null)  --'086.06.311.002'
                        AND (t5.FNUMBER like '%{materialfnumber}%' or '{materialfnumber}' is null) --IN('SP-823-25L-00-00')
                        and (t8.fname like '%{customername}%' or '{customername}' is null) --in ('上海陶森华贸易有限公司')
                        AND (T6.fname like '%{materialname}%' or '{materialname}' is null) --物料名称
                        ORDER BY t8.FNAME,t7.FNUMBER,t12.mc,t12.cCode";

            return _result;
        }

        /// <summary>
        /// U订货价目表
        /// </summary>
        /// <returns></returns>
        public string Get_UPriceList(string materialnumber, string materialname, string pricetype)
        {
            //全部显示
            if (pricetype == "-1")
            {
                _result =
                            $@"SELECT x.物料代码,x.物料名称,x.客户代码,x.单价,x.生效日期,x.失效日期,x.修改日期,x.价格类型,x.客户分类,x.价目表编号
                                FROM (--价目表

                                    SELECT   t3.FNUMBER AS 物料代码, t4.FNAME AS 物料名称, '' AS 客户代码, t2.FPRICE AS 单价, 
                                                    t2.FEFFECTIVEDATE AS 生效日期, t2.FEXPRIYDATE AS 失效日期, t2.F_YTC_DATETIME AS 修改日期, 
                                                    CASE t1.F_YTC_COMBO WHEN 'U订货5折常规价' THEN '常规' ELSE '特殊' END 价格类型, '011' AS 客户分类, 
                                                    t1.FNUMBER 价目表编号
                                    FROM AIS20181204095717.dbo.T_SAL_PRICELIST t1 INNER JOIN
                                    AIS20181204095717.dbo.T_SAL_PRICELISTENTRY t2 ON t1.FID = t2.FID INNER JOIN
                                    AIS20181204095717.dbo.T_BD_MATERIAL t3 ON t2.FMATERIALID = t3.FMATERIALID INNER JOIN
                                    AIS20181204095717.dbo.T_BD_MATERIAL_L t4 ON t3.FMATERIALID = t4.FMATERIALID
                                    WHERE   (GETDATE() BETWEEN t1.FEFFECTIVEDATE AND t2.FEXPRIYDATE) AND (t1.FDOCUMENTSTATUS = 'C') 
                                    AND     (t1.FFORBIDSTATUS = 'A') AND (t2.FFORBIDSTATUS !='B') AND (t3.FNUMBER NOT LIKE '%CK') AND t1.F_YTC_combo = 'U订货5折常规价' 
                                    AND     LEN(t4.FNAME) > 0

                                    UNION ALL

                                    SELECT   t3.FNUMBER AS 物料代码, t7.FNAME 物料名称, t5.FNUMBER AS 客户代码, t2.FPRICE AS 单价, 
                                                    t2.FEFFECTIVEDATE AS 生效日期, t2.FEXPRIYDATE AS 失效日期, t2.F_YTC_DATETIME AS 修改日期, 
                                                    CASE t1.F_YTC_COMBO WHEN 'U订货常规价' THEN '常规' ELSE '特殊' END 价格类型, '' AS 客户分类, 
                                                    t1.FNUMBER 价目表编号
                                    FROM AIS20181204095717.dbo.T_SAL_PRICELIST t1 INNER JOIN
                                    AIS20181204095717.dbo.T_SAL_PRICELISTENTRY t2 ON t1.FID = t2.FID INNER JOIN
                                    AIS20181204095717.dbo.T_BD_MATERIAL t3 ON t2.FMATERIALID = t3.FMATERIALID INNER JOIN
                                    AIS20181204095717.dbo.T_SAL_APPLYCUSTOMER AS t4 ON t1.FID = t4.FID INNER JOIN
                                    AIS20181204095717.dbo.T_BD_CUSTOMER AS t5 ON t4.FCUSTID = t5.FCUSTID INNER JOIN
                                                    AIS20181204095717.dbo.T_BD_CUSTOMER_L AS t6 ON t5.FCUSTID = t6.FCUSTID INNER JOIN
                                                    AIS20181204095717.dbo.T_BD_MATERIAL_L t7 ON t3.FMATERIALID = t7.FMATERIALID
                                    WHERE   (GETDATE() BETWEEN t1.FEFFECTIVEDATE AND t2.FEXPRIYDATE) 
                                    AND (t1.FDOCUMENTSTATUS = 'C') 
                                    AND (t1.FFORBIDSTATUS = 'A') 
                                    AND (t2.FFORBIDSTATUS !='B')
                                    AND (t3.FNUMBER NOT LIKE '%CK') 
                                    AND ((t5.FNUMBER NOT LIKE 'INT%') 
                                    AND (t5.FNUMBER NOT LIKE '%.JC')) 
                                    AND (t5.FNUMBER NOT LIKE '%.YC%')
                                    AND (dbo.GetIncludMAT(t5.FCUSTID, t3.FMATERIALID, t3.FMATERIALGROUP) > 0) 
                                    AND LEN(t7.FNAME) > 0 
                                    AND (T5.FNUMBER NOT LIKE '%--合')
                                    AND (t5.FNUMBER NOT LIKE '%--GM')
                                    AND (t6.FNAME NOT LIKE '%----N')
                                    AND t1.F_YTC_combo = 'U订货特殊价'
                                    )x
                        where (x.物料代码 like '%{materialnumber}%' or '{materialnumber}' is null)
                        and (x.物料名称 like '%{materialname}%' or '{materialname}' is null)";
            }
            else
            {
                _result =
                $@"SELECT x.物料代码,x.物料名称,x.客户代码,x.单价,x.生效日期,x.失效日期,x.修改日期,x.价格类型,x.客户分类,x.价目表编号
                   FROM (--价目表

                        SELECT   t3.FNUMBER AS 物料代码, t4.FNAME AS 物料名称, '' AS 客户代码, t2.FPRICE AS 单价, 
                                        t2.FEFFECTIVEDATE AS 生效日期, t2.FEXPRIYDATE AS 失效日期, t2.F_YTC_DATETIME AS 修改日期, 
                                        CASE t1.F_YTC_COMBO WHEN 'U订货5折常规价' THEN '常规' ELSE '特殊' END 价格类型, '011' AS 客户分类, 
                                        t1.FNUMBER 价目表编号
                        FROM AIS20181204095717.dbo.T_SAL_PRICELIST t1 INNER JOIN
                        AIS20181204095717.dbo.T_SAL_PRICELISTENTRY t2 ON t1.FID = t2.FID INNER JOIN
                        AIS20181204095717.dbo.T_BD_MATERIAL t3 ON t2.FMATERIALID = t3.FMATERIALID INNER JOIN
                        AIS20181204095717.dbo.T_BD_MATERIAL_L t4 ON t3.FMATERIALID = t4.FMATERIALID
                        WHERE   (GETDATE() BETWEEN t1.FEFFECTIVEDATE AND t2.FEXPRIYDATE) AND (t1.FDOCUMENTSTATUS = 'C') 
                        AND     (t1.FFORBIDSTATUS = 'A') AND (t2.FFORBIDSTATUS !='B') AND (t3.FNUMBER NOT LIKE '%CK') AND t1.F_YTC_combo = 'U订货5折常规价' 
                        AND     LEN(t4.FNAME) > 0

                        UNION ALL

                        SELECT   t3.FNUMBER AS 物料代码, t7.FNAME 物料名称, t5.FNUMBER AS 客户代码, t2.FPRICE AS 单价, 
                                        t2.FEFFECTIVEDATE AS 生效日期, t2.FEXPRIYDATE AS 失效日期, t2.F_YTC_DATETIME AS 修改日期, 
                                        CASE t1.F_YTC_COMBO WHEN 'U订货常规价' THEN '常规' ELSE '特殊' END 价格类型, '' AS 客户分类, 
                                        t1.FNUMBER 价目表编号
                        FROM AIS20181204095717.dbo.T_SAL_PRICELIST t1 INNER JOIN
                        AIS20181204095717.dbo.T_SAL_PRICELISTENTRY t2 ON t1.FID = t2.FID INNER JOIN
                        AIS20181204095717.dbo.T_BD_MATERIAL t3 ON t2.FMATERIALID = t3.FMATERIALID INNER JOIN
                        AIS20181204095717.dbo.T_SAL_APPLYCUSTOMER AS t4 ON t1.FID = t4.FID INNER JOIN
                        AIS20181204095717.dbo.T_BD_CUSTOMER AS t5 ON t4.FCUSTID = t5.FCUSTID INNER JOIN
                                        AIS20181204095717.dbo.T_BD_CUSTOMER_L AS t6 ON t5.FCUSTID = t6.FCUSTID INNER JOIN
                                        AIS20181204095717.dbo.T_BD_MATERIAL_L t7 ON t3.FMATERIALID = t7.FMATERIALID
                        WHERE   (GETDATE() BETWEEN t1.FEFFECTIVEDATE AND t2.FEXPRIYDATE) 
                        AND (t1.FDOCUMENTSTATUS = 'C') 
                        AND (t1.FFORBIDSTATUS = 'A') 
                        AND (t2.FFORBIDSTATUS !='B')
                        AND (t3.FNUMBER NOT LIKE '%CK') 
                        AND ((t5.FNUMBER NOT LIKE 'INT%') 
                        AND (t5.FNUMBER NOT LIKE '%.JC')) 
                        AND (t5.FNUMBER NOT LIKE '%.YC%')
                        AND (dbo.GetIncludMAT(t5.FCUSTID, t3.FMATERIALID, t3.FMATERIALGROUP) > 0) 
                        AND LEN(t7.FNAME) > 0 
                        AND (T5.FNUMBER NOT LIKE '%--合')
                        AND (t5.FNUMBER NOT LIKE '%--GM')
                        AND (t6.FNAME NOT LIKE '%----N')
                        AND t1.F_YTC_combo = 'U订货特殊价'
                        )x
            where (x.物料代码 like '%{materialnumber}%' or '{materialnumber}' is null)
            and (x.物料名称 like '%{materialname}%' or '{materialname}' is null)
            and x.价格类型=CASE '{pricetype}' WHEN '0' THEN '常规' ELSE '特殊' END";
            }
            
            return _result;
        }

    }
}
