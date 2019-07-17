using System;
using System.Data;
using System.IO;
using NPOI.XSSF.UserModel;

namespace UTools.DB
{
    public class ExportDb
    {
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="type">0:可销控 1:U订货价目表</param>
        /// <param name="fileAddress"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string ExportDttoExcel(string type,string fileAddress, DataTable dt)
        {
            //流程:1)创建EXCEL导出所需的列,2)从DT内循环读取数据
            var result = "0";
            try
            {
                //声明一个WorkBook
                var xssfWorkbook = new XSSFWorkbook();
                //可销控
                if (type == "0")
                {
                    
                    for (var i = 0; i < 1; i++) //i为EXCEL的Sheet页数ID
                    {
                        //为WorkBook创建work
                        var sheet = xssfWorkbook.CreateSheet("Sheet" + (i + 1));
                        //创建"标题行"
                        var row = sheet.CreateRow(0);
                        for (var j = 0; j < dt.Columns.Count; j++)
                        {
                            //设置列宽度
                            sheet.SetColumnWidth(j, (int)((20 + 0.72) * 256));
                            //创建标题
                            switch (j)
                            {
                                #region SetCellValue
                                case 0:
                                    row.CreateCell(j).SetCellValue("授权对象类型");
                                    break;
                                case 1:
                                    row.CreateCell(j).SetCellValue("授权对象");
                                    break;
                                case 2:
                                    row.CreateCell(j).SetCellValue("授权对象编码");
                                    break;
                                case 3:
                                    row.CreateCell(j).SetCellValue("商品名称");
                                    break;
                                case 4:
                                    row.CreateCell(j).SetCellValue("商品编码");
                                    break;
                                case 5:
                                    row.CreateCell(j).SetCellValue("组织编码");
                                    break;
                                case 6:
                                    row.CreateCell(j).SetCellValue("组织名称");
                                    break;
                                    #endregion
                            }
                        }
                        //获取DT内的行记录步骤
                        for (var j = 0; j < dt.Rows.Count; j++)
                        {
                            //创建行(从第二行开始)
                            row = sheet.CreateRow(j + 1);
                            #region 设置行的第一行的值为自增值
                            //row.CreateCell(0).SetCellValue(j + 1);
                            #endregion
                            //循环获取DT内的列值记录
                            for (var k = 0; k < dt.Columns.Count; k++)
                            {
                                row.CreateCell(k).SetCellValue(Convert.ToString(dt.Rows[j][k]));
                            }
                        }
                    }
                }
                //U订货价目表
                else
                {
                    for (var i = 0; i < 1; i++) //i为EXCEL的Sheet页数ID
                    {
                        //为WorkBook创建work
                        var sheet = xssfWorkbook.CreateSheet("USheet" + (i + 1));
                        //创建"标题行"
                        var row = sheet.CreateRow(0);
                        for (var j = 0; j < dt.Columns.Count; j++)
                        {
                            //设置列宽度
                            sheet.SetColumnWidth(j, (int)((20 + 0.72) * 256));
                            //创建标题
                            switch (j)
                            {
                                #region SetCellValue
                                case 0:
                                    row.CreateCell(j).SetCellValue("物料代码");
                                    break;
                                case 1:
                                    row.CreateCell(j).SetCellValue("物料名称");
                                    break;
                                case 2:
                                    row.CreateCell(j).SetCellValue("客户代码");
                                    break;
                                case 3:
                                    row.CreateCell(j).SetCellValue("单价");
                                    break;
                                case 4:
                                    row.CreateCell(j).SetCellValue("生效日期");
                                    break;
                                case 5:
                                    row.CreateCell(j).SetCellValue("失效日期");
                                    break;
                                case 6:
                                    row.CreateCell(j).SetCellValue("修改日期");
                                    break;
                                case 7:
                                    row.CreateCell(j).SetCellValue("价格类型");
                                    break;
                                case 8:
                                    row.CreateCell(j).SetCellValue("客户分类");
                                    break;
                                case 9:
                                    row.CreateCell(j).SetCellValue("价目表编号");
                                    break;
                                    #endregion
                            }
                        }
                        //获取DT内的行记录步骤
                        for (var j = 0; j < dt.Rows.Count; j++)
                        {
                            //创建行(从第二行开始)
                            row = sheet.CreateRow(j + 1);
                            #region 设置行的第一行的值为自增值
                            //row.CreateCell(0).SetCellValue(j + 1);
                            #endregion
                            //循环获取DT内的列值记录
                            for (var k = 0; k < dt.Columns.Count; k++)
                            {
                                row.CreateCell(k).SetCellValue(Convert.ToString(dt.Rows[j][k]));
                            }
                        }
                    }
                }
                
                //写入数据
                var file = new FileStream(fileAddress, FileMode.Create);
                xssfWorkbook.Write(file);
                file.Close();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
    }
}
