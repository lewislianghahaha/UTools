using System.Data;
using System.Threading;
using UTools.DB;

namespace UTools
{
    public class TaskLogic
    {
        SearchDb serDb=new SearchDb();
        ExportDb exportDb=new ExportDb();

        private int _taskid;
        private string _type;
        private string _customernumber;
        private string _materialfnumber;
        private string _customername;
        private string _materialname;
        private string _pricetype;
        private DataTable _dt;
        private string _fileAddress;        //文件地址
        private string _importResult = "0"; //返回是否成功的提示信息(注:导入或导出记录时使用)

        private DataTable _resultTable;

        #region Set

            /// <summary>
            /// 中转ID
            /// </summary>
            public int TaskId { set { _taskid = value; } }

            /// <summary>
            /// 0:可销控 1:U订货价目表
            /// </summary>
            public string Type { set { _type = value; } }

            /// <summary>
            /// 客户编号
            /// </summary>
            public string Customernumber { set { _customernumber = value; } }

            /// <summary>
            /// 物料编码
            /// </summary>
            public string Materialnumber { set { _materialfnumber = value; } }

            /// <summary>
            /// 客户名称
            /// </summary>
            public string Customername { set { _customername = value; } }

            /// <summary>
            /// 物料名称
            /// </summary>
            public string Materialname { set { _materialname = value; } }

            /// <summary>
            /// 价格类型-1:全部 0:常规 1:特殊
            /// </summary>
            public string Pricetype { set { _pricetype = value; } }

            /// <summary>
            /// 获取DT（导出时使用）
            /// </summary>
            public DataTable ImportDt { set { _dt = value; } }
            
            /// <summary>
            /// 文件地址
            /// </summary>
            public string FileAddress { set { _fileAddress = value; } }

        #endregion

        #region Get
            /// <summary>
            ///返回DataTable至主窗体
            /// </summary>
            public DataTable RestulTable => _resultTable;

            /// <summary>
            /// 返回导入数据库结果(作用:导入数据库后使用)
            /// </summary>
            public string ImportResult => _importResult;

        #endregion

        /// <summary>
        /// 作功能中转使用
        /// </summary>
        public void StartTask()
        {
            Thread.Sleep(1000);

            switch (_taskid)
            {
                //查询功能
                case 0:
                    SearchSalesDt(_type,_customernumber,_materialfnumber,_customername,_materialname,_pricetype);
                    break;
                //导出功能
                case 1:
                    ExportDtToExcel(_type,_fileAddress,_dt);
                    break;
            }
        }

        /// <summary>
        /// 查询功能
        /// </summary>
        private void SearchSalesDt(string type, string customernumber, string materialfnumber, string customername, string materialname, string pricetype)
        {
            _resultTable = serDb.SearchSalesDt(type, customernumber, materialfnumber, customername, materialname, pricetype);
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fileAddress">导出地址</param>
        /// <param name="dt">导出数据表</param>
        private void ExportDtToExcel(string type,string fileAddress, DataTable dt)
        {
            _importResult = exportDb.ExportDttoExcel(type,fileAddress,dt);
        }

    }
}
