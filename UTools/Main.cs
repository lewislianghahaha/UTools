using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using RD.UI;

namespace UTools
{
    public partial class Main : Form
    {
        Load load=new Load();
        TaskLogic task=new TaskLogic();

        public Main()
        {
            InitializeComponent();
            OnRegisterEvents();
        }

        private void OnRegisterEvents()
        {
            tbcontrol.Selecting += Tbcontrol_Selecting;
            btnsearch.Click += Btnsearch_Click;
            btnsear.Click += Btnsear_Click;
            tmExport.Click += TmExport_Click;
        }

        /// <summary>
        /// 初始化价格类型
        /// </summary>
        private void OnShowTypeList()
        {
            var dt = new DataTable();

            //创建表头
            for (var i = 0; i < 2; i++)
            {
                var dc = new DataColumn();
                switch (i)
                {
                    case 0:
                        dc.ColumnName = "Id";
                        break;
                    case 1:
                        dc.ColumnName = "Name";
                        break;
                }
                dt.Columns.Add(dc);
            }

            //创建行内容
            for (var j = 0; j < 3; j++)
            {
                var dr = dt.NewRow();

                switch (j)
                {
                    case 0:
                        dr[0] = "-1";
                        dr[1] = "全部";
                        break;
                    case 1:
                        dr[0] = "0";
                        dr[1] = "常规";
                        break;
                    case 2:
                        dr[0] = "1";
                        dr[1] = "特殊";
                        break;
                }
                dt.Rows.Add(dr);
            }

            comtype.DataSource = dt;
            comtype.DisplayMember = "Name"; //设置显示值
            comtype.ValueMember = "Id";    //设置默认值内码
        }

        /// <summary>
        /// Tab页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tbcontrol_Selecting(object sender, TabControlCancelEventArgs e)
        {
            try
            {
                if (tbcontrol.SelectedTab.Name == "tabPage1")
                {
                    //清空U订货价目表表头
                    txtmatnumber.Text = "";
                    txtmatname.Text = "";

                    //清空U订货价目表表体
                    if (gvdtlu.Rows.Count > 0)
                    {
                        var dt = (DataTable) gvdtlu.DataSource;
                        dt.Columns.Clear();
                        dt.Rows.Clear();
                        gvdtlu.DataSource = dt;
                    }
                }
                //当选择的TAB页面为"U订货价目表"时，初始化相关值
                else if (tbcontrol.SelectedTab.Name == "tabPage2")
                {
                    //初始化下拉列表
                    OnShowTypeList();
                    //清空可销控表头
                    txtcustomername.Text = "";
                    txtmaterialnumber.Text = "";
                    txtcustomernumber.Text = "";

                    //清空可销控表体
                    if (gvdtl.Rows.Count > 0)
                    {
                        var dt = (DataTable) gvdtl.DataSource;
                        dt.Columns.Clear();
                        dt.Rows.Clear();
                        gvdtl.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 可销控报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                //当选择的TAB页面为"可销控"时,才执行以下操作
                if (tbcontrol.SelectedTab.Name == "tabPage1")
                {
                    //获取客户编号
                    var customernumber = txtcustomernumber.Text;
                    //获取客户名称
                    var customername = txtcustomername.Text;
                    //获取物料编码
                    var materialnumber = txtmaterialnumber.Text;

                    task.TaskId = 0;
                    task.Type = "0";  //0:可销控 1:U订货价目表
                    task.Customernumber = customernumber;
                    task.Materialnumber = materialnumber;
                    task.Customername = customername;

                    //使用子线程工作(作用:通过调用子线程进行控制Load窗体的关闭情况)
                    new Thread(Start).Start();
                    load.StartPosition = FormStartPosition.CenterScreen;
                    load.ShowDialog();

                    gvdtl.DataSource = task.RestulTable;
                    label7.Text = $"查询出总行数:{gvdtl.Rows.Count}";
                    if (gvdtl.Rows.Count == 0) throw new Exception("没有查询到相关记录,请在U订货同步平台查询‘U商品档案’及‘U客户档案’是否已下载相关记录");

                    //当查询有值后提示是否导出
                    var clickMessage = $"数据已获取,是否需要导出至Excel?";
                    if (MessageBox.Show(clickMessage, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==DialogResult.Yes)
                    {
                        Exportdt("0");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// U订货价目表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btnsear_Click(object sender, EventArgs e)
        {
            try
            {
                //当选择的TAB页面为"U订货价目表"时,才执行以下操作
                if (tbcontrol.SelectedTab.Name == "tabPage2")
                {
                    //物料代码
                    var materialnumber = txtmatnumber.Text;
                    //物料名称
                    var materialname = txtmatname.Text;
                    //价格类型
                    var dvfactorylist = (DataRowView)comtype.Items[comtype.SelectedIndex];
                    var id = Convert.ToString(dvfactorylist["Id"]);

                    task.TaskId = 0;
                    task.Type = "1";    //0:可销控 1:U订货价目表
                    task.Materialnumber = materialnumber;
                    task.Materialname = materialname;
                    task.Pricetype = id;

                    //使用子线程工作(作用:通过调用子线程进行控制Load窗体的关闭情况)
                    new Thread(Start).Start();
                    load.StartPosition = FormStartPosition.CenterScreen;
                    load.ShowDialog();

                    gvdtlu.DataSource = task.RestulTable;
                    label8.Text = $"查询出总行数:{gvdtlu.Rows.Count}";
                    if (gvdtlu.Rows.Count == 0) throw new Exception("没有查询到相关记录,请在U订货同步平台查询‘U商品档案’及‘U客户档案’是否已下载相关记录");

                    //当查询有值后提示是否导出
                    var clickMessage = $"数据已获取,是否需要导出至Excel?";
                    if (MessageBox.Show(clickMessage, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        Exportdt("1");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 导出EXCEL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmExport_Click(object sender, EventArgs e)
        {
            try
            {
                Exportdt(tbcontrol.SelectedTab.Name == "tabPage1" ? "0" : "1");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 导出
        /// </summary>
        private void Exportdt(string type)
        {
            try
            {
                var saveFileDialog = new SaveFileDialog { Filter = "Xlsx文件|*.xlsx" };
                if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
                var fileAdd = saveFileDialog.FileName;

                //可销控
                task.TaskId = 1;
                task.FileAddress = fileAdd;
                task.Type = type;
                task.ImportDt = type == "0" ? (DataTable)gvdtl.DataSource : (DataTable)gvdtlu.DataSource;

                //使用子线程工作(作用:通过调用子线程进行控制Load窗体的关闭情况)
                new Thread(Start).Start();
                load.StartPosition = FormStartPosition.CenterScreen;
                load.ShowDialog();

                var result = task.ImportResult;
                switch (result)
                {
                    case "0":
                        MessageBox.Show($"导出成功!可从EXCEL中查阅导出效果", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    default:
                        throw (new Exception(result));
                }

                //可销控清空
                if (type == "0")
                {
                    txtcustomername.Text = "";
                    txtmaterialnumber.Text = "";
                    txtcustomernumber.Text = "";

                    var dt = (DataTable)gvdtl.DataSource;
                    dt.Columns.Clear();
                    dt.Rows.Clear();
                    gvdtl.DataSource = dt;
                }
                //价目表清空
                else
                {
                    txtmatnumber.Text = "";
                    txtmatname.Text = "";

                    var dt = (DataTable)gvdtlu.DataSource;
                    dt.Columns.Clear();
                    dt.Rows.Clear();
                    gvdtlu.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///子线程使用(重:用于监视功能调用情况,当完成时进行关闭LoadForm)
        /// </summary>
        private void Start()
        {
            task.StartTask();

            //当完成后将Form2子窗体关闭
            this.Invoke((ThreadStart)(() => {
                load.Close();
            }));
        }



    }
}
