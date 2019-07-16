using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTools
{
    public class SqlList
    {
        string _result = string.Empty;

        /// <summary>
        /// 可销控报表使用
        /// </summary>
        /// <returns></returns>
        public string Get_SalesList()
        {
            _result = $@"SELECT   '客户' 授权对象类型,t8.FNAME AS 授权对象,t7.FNUMBER AS 授权对象编码,t12.mc 商品名称,t12.cCode 商品编码,'037214' 组织编码,'雅图高新材料有限公司' 组织名称
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
                        AND (t7.FNUMBER NOT LIKE '%SX%')
                        AND t1.F_YTC_COMBO IN('U订货特殊价','U订货5折常规价')
                        AND t2.FFORBIDSTATUS != 'B'
                        
                        --AND t7.FNUMBER='086.06.311.002'
                        --AND t5.FNUMBER IN('SP-823-25L-00-00')
                        and t8.fname in ('上海陶森华贸易有限公司')
                        ORDER BY t8.FNAME,t7.FNUMBER,t12.mc,t12.cCode";

            return _result;
        }
    }
}
