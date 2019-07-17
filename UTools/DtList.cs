using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTools
{
    public class DtList
    {
        /// <summary>
        /// 可销控报表使用
        /// </summary>
        /// <returns></returns>
        public DataTable Get_SalesListdt()
        {
            var dt = new DataTable();
            for (var i = 0; i < 7; i++)
            {
                var dc = new DataColumn();
                switch (i)
                {
                    case 0:
                        dc.ColumnName = "授权对象类型";
                        dc.DataType = Type.GetType("System.String"); 
                        break;
                    case 1:
                        dc.ColumnName = "授权对象";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 2:
                        dc.ColumnName = "授权对象编码";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 3:
                        dc.ColumnName = "商品名称";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 4:
                        dc.ColumnName = "商品编码";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 5:
                        dc.ColumnName = "组织编码";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 6:
                        dc.ColumnName = "组织名称";
                        dc.DataType = Type.GetType("System.String");
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }

        /// <summary>
        /// U订货价目表使用
        /// </summary>
        /// <returns></returns>
        public DataTable Get_UPriceListDt()
        {
            var dt = new DataTable();
            for (var i = 0; i < 10; i++)
            {
                var dc = new DataColumn();
                switch (i)
                {
                    case 0:
                        dc.ColumnName = "物料代码";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 1:
                        dc.ColumnName = "物料名称";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 2:
                        dc.ColumnName = "客户代码";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 3:
                        dc.ColumnName = "单价";
                        dc.DataType = Type.GetType("System.Decimal"); 
                        break;
                    case 4:
                        dc.ColumnName = "生效日期";
                        dc.DataType = Type.GetType("System.DateTime"); 
                        break;
                    case 5:
                        dc.ColumnName = "失效日期";
                        dc.DataType = Type.GetType("System.DateTime");
                        break;
                    case 6:
                        dc.ColumnName = "修改日期";
                        dc.DataType = Type.GetType("System.DateTime");
                        break;
                    case 7:
                        dc.ColumnName = "价格类型";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 8:
                        dc.ColumnName = "客户分类";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 9:
                        dc.ColumnName = "价目表编号";
                        dc.DataType = Type.GetType("System.String");
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }
    }
}
