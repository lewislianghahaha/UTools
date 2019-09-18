using System;
using System.Data;
using System.Data.SqlClient;

namespace UTools.DB
{
    public class SearchDb
    {
        SqlList sqlList=new SqlList();
        DtList dtList=new DtList();

        /// <summary>
        /// 创建连接对像
        /// </summary>
        /// <returns></returns>
        public SqlConnection GetConn()
        {
            var conn = new Conn();
            var sqlcon = new SqlConnection(conn.GetConnectionString());
            return sqlcon;
        }

        /// <summary>
        /// 读取相关记录()
        /// </summary>
        /// <param name="type">0:可销控 1:U订货价目表</param>
        /// <param name="customernumber">客户编号</param>
        /// <param name="materialfnumber">物料编码</param>
        /// <param name="customername">客户名称</param>
        /// <param name="materialname">物料名称</param>
        /// <param name="pricetype">价格类型-1:全部 0:常规 1:特殊</param>
        /// <returns></returns>
        public DataTable SearchSalesDt(string type,string customernumber,string materialfnumber,string customername,string materialname,string pricetype)
        {
            DataTable resultdt;
            var dt = new DataTable();

            try
            {
                //根据TYPE获取对应的临时表
                var tempdt = type == "0" ? dtList.Get_SalesListdt() : dtList.Get_UPriceListDt();

                var sqlscript = type == "0" ? sqlList.Get_SalesList(customernumber, materialfnumber, customername,materialname) : 
                                            sqlList.Get_UPriceList(materialfnumber, materialname, pricetype);

                var sqlDataAdapter=new SqlDataAdapter(sqlscript,GetConn());
                sqlDataAdapter.Fill(dt);
                resultdt = dt.Rows.Count == 0 ? tempdt : dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return resultdt;
        }
    }
}
