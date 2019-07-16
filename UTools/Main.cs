using System;
using System.Windows.Forms;

namespace UTools
{
    public partial class Main : Form
    {
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
        /// Tab页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tbcontrol_Selecting(object sender, TabControlCancelEventArgs e)
        {
            try
            {
                //

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
        private void Btnsearch_Click(object sender, System.EventArgs e)
        {
            try
            {

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
        private void Btnsear_Click(object sender, System.EventArgs e)
        {
            try
            {

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

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
