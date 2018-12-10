using EmployeeInfoSystem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeInfoSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }
        #region 员工信息
        //查询员工信息
        private void btnInfoSearch_Click(object sender, EventArgs e)
        {
            //清空
            listInfo.Items.Clear();
            //初始一个查询表达式
            Expression<Func<employeeInfo, bool>> expression = x => x.employeeId == x.employeeId;
            //如果某个查询项选中的话 加入查询表达式
            if (cboxInfoId.Checked == true)
            {
                //nud的Value不可能为空 所以直接查询即可
                expression = LambdaHelper.And<employeeInfo>(expression, x => x.employeeId == nudInfoSearchId.Value);
            }
            //姓名
            if (cboxInfoName.Checked == true)
            {
                //name的值可能为空 所以要判断
                if (txtInfoSearchName.Text != "")
                {
                    expression = LambdaHelper.And<employeeInfo>(expression, x => x.employeeName == txtInfoSearchName.Text);
                }
            }
            //电话
            if (cboxInfoTel.Checked == true)
            {
                if (txtInfoSearchTel.Text != "")
                {
                    expression = LambdaHelper.And<employeeInfo>(expression, x => x.employeePhone == txtInfoSearchTel.Text);
                }
            }
            //身份证
            if (cboxInfoIdCard.Checked == true)
            {
                if (txtInfoSearchIdCard.Text != "")
                {
                    expression = LambdaHelper.And<employeeInfo>(expression, x => x.employeeIdentity == txtInfoSearchIdCard.Text);
                }
            }
            //性别
            if (cboxInfoSex.Checked == true)
            {
                if (rbtnInfoSearchMale.Checked == true)
                {
                    expression = LambdaHelper.And<employeeInfo>(expression, x => x.employeeSex == 1);
                }
                else
                {
                    expression = LambdaHelper.And<employeeInfo>(expression, x => x.employeeSex == 0);
                }
            }
            //生日
            if (cboxInfoBirth.Checked == true)
            {
                DateTime birth = new DateTime(int.Parse(nudInfoSearchYear.Value.ToString()), 
                    int.Parse(nudInfoSearchMonth.Value.ToString()), int.Parse(nudInfoSearchDay.Value.ToString()));
                expression = LambdaHelper.And<employeeInfo>(expression, x => x.employeeBir == birth);
            }
            employeeInfo[] info = dataTools.selectTools.selectEmployeeInfo(expression, x => x.employeeId);
            MessageBox.Show("成功查询到" + info.Length + "条数据");
            for(int count = 0; count < info.Length; count++)
            {
                ListViewItem item = new ListViewItem();
                item.Text = info[count].employeeId.ToString();
                item.SubItems.Add(info[count].employeeName);
                if (info[count].employeeSex == 0)
                    item.SubItems.Add("女");
                else
                    item.SubItems.Add("男");
                item.SubItems.Add(info[count].employeePhone);
                DateTime birth = (DateTime)info[count].employeeBir;
                item.SubItems.Add(birth.ToString("D"));
                item.SubItems.Add(info[count].employeeIdentity);
                listInfo.Items.Add(item);
            }
            
        }
        //加载
        private void btnInfoLoad_Click(object sender, EventArgs e)
        {
            if (listInfo.SelectedItems.Count == 0)
                return;
            ListViewItem item = listInfo.SelectedItems[0];
            txtInfoName.Text = item.SubItems[1].Text;
            if (item.SubItems[2].Text == "男")
                rbtnInfoMale.Checked = true;
            else
                rbtnInfoFemale.Checked = true;
            txtInfoTel.Text = item.SubItems[3].Text;
            int[] birth = Tools.GetBirthFromString(item.SubItems[4].Text);
            nudInfoYear.Value = birth[0];
            nudInfoMonth.Value = birth[1];
            nudInfoDay.Value = birth[2];
            txtInfoIdCard.Text = item.SubItems[5].Text;
        }

        //添加
        private void btnInfoAdd_Click(object sender, EventArgs e)
        {
            if(txtInfoName.Text==""||txtInfoTel.Text==""||txtInfoIdCard.Text=="")
            {
                MessageBox.Show("请输入信息");
                return;
            }
            DateTime birth = new DateTime(int.Parse(nudInfoYear.Value.ToString()),
                    int.Parse(nudInfoMonth.Value.ToString()), int.Parse(nudInfoDay.Value.ToString()));
            int sex;
            if (rbtnInfoMale.Checked == true)
                sex = 1;
            else
                sex = 0;
            employeeInfo employee = new employeeInfo()
            {
                employeeName = txtInfoName.Text,
                employeePhone = txtInfoTel.Text,
                employeeIdentity = txtInfoIdCard.Text,
                employeeBir = birth,
                employeeSex = sex,
            };
            if (dataTools.insertTools.insertEmployeeInfo(employee) == false)
            {
                MessageBox.Show("添加失败");
            }
            else
            {
                MessageBox.Show("添加成功");
            }
        }
        //删除
        private void btnInfoDelete_Click(object sender, EventArgs e)
        {
            if (listInfo.SelectedItems.Count == 0)
                return;
            ListViewItem item = listInfo.SelectedItems[0];
            DialogResult dr = MessageBox.Show("确定要删除该条员工信息吗？\n（此操作会删除该员工在公司中所有信息）",
                "删除", MessageBoxButtons.OKCancel);
            if(dr == DialogResult.OK)
            {
                if (dataTools.deleteTools.deleteEmployee(int.Parse(item.Text)) == false)
                    MessageBox.Show("删除失败");
                else
                    MessageBox.Show("删除成功");
            }
            listInfo.Items.Remove(item);
        }
        //修改
        private void btnInfoEdit_Click(object sender, EventArgs e)
        {
            if (listInfo.SelectedItems.Count == 0)
                return;
            ListViewItem item = listInfo.SelectedItems[0];
            if (txtInfoName.Text == "" || txtInfoTel.Text == "" || txtInfoIdCard.Text == "")
            {
                MessageBox.Show("请输入信息");
                return;
            }
            DateTime birth = new DateTime(int.Parse(nudInfoYear.Value.ToString()),
                    int.Parse(nudInfoMonth.Value.ToString()), int.Parse(nudInfoDay.Value.ToString()));
            int sex;
            if (rbtnInfoMale.Checked == true)
                sex = 1;
            else
                sex = 0;
            employeeInfo employee = new employeeInfo()
            {
                employeeName = txtInfoName.Text,
                employeePhone = txtInfoTel.Text,
                employeeIdentity = txtInfoIdCard.Text,
                employeeBir = birth,
                employeeSex = sex,
            };
            int id = int.Parse(item.Text);
            if (dataTools.updateTools.updateEmployeeInfo(x => x.employeeId == id, employee) == false)
                MessageBox.Show("修改失败");
            else
            {
                MessageBox.Show("修改成功");
                item.SubItems[1].Text = txtInfoName.Text;
                item.SubItems[2].Text = Tools.ReturnSex(sex);
                item.SubItems[3].Text = txtInfoTel.Text;
                item.SubItems[4].Text = birth.ToString("D");
                item.SubItems[5].Text = txtInfoIdCard.Text;
            }

        }
        #endregion


        #region 公司就职信息
        /// <summary>
        /// 加载公司就职信息选中项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPerLoad_Click(object sender, EventArgs e)
        {
            try
            {
                if (Performance.SelectedItems.Count == 0)
                {
                    return;
                }
                ListViewItem item = Performance.SelectedItems[0];
                nudPerId.Value = Convert.ToInt32(item.SubItems[0].Text);
                //根据职工ID找到职工姓名
                employeeInfo employee = dataTools.selectTools.selectEmployeeInfo(u => u.employeeId == nudPerId.Value, u => u.employeeId)[0];
                nudPerPosId.Value = Convert.ToInt32(item.SubItems[2].Text);
                nudPerPerformance.Value = decimal.Parse(item.SubItems[4].Text);
            }
            catch
            {
                MessageBox.Show("出错了！");
                return;
            }
        }

        /// <summary>
        /// 修改公司就职信息选中项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPerEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Performance.SelectedItems.Count == 0)
                {
                    return;
                }
                ListViewItem item = Performance.SelectedItems[0];

                //先确定数据库中是否有修改后的职工
                employeeInfo[] isEmployee = dataTools.selectTools.selectEmployeeInfo(u => u.employeeId == nudPerId.Value, u => u.employeeId);
                if (isEmployee.Length == 0)
                {
                    MessageBox.Show("没有此员工，请输入正确的信息！");
                    return;
                }
                //确定数据库中是否有修改后的职位
                companyPosition[] isPosition = dataTools.selectTools.selectCompanyPosition(u => u.positionId == nudPerPosId.Value, u => u.positionId);
                if (isPosition.Length == 0)
                {
                    MessageBox.Show("没有此职位，请输入正确的信息！");
                    return;
                }

                employeePerformance performance = new employeePerformance()
                {
                    employeeId = Convert.ToInt32(nudPerId.Value),
                    positionId = Convert.ToInt32(nudPerPosId.Value),
                    performance = Convert.ToDouble(nudPerPerformance.Value),

                };
                int id = Convert.ToInt32(item.SubItems[0].Text);
                if (dataTools.updateTools.updateEmployeePerformance(x => x.employeeId == id, performance) == false)
                {
                    MessageBox.Show("修改失败");
                }
                else
                {
                    MessageBox.Show("修改成功");
                    item.SubItems[0].Text = performance.employeeId.ToString();
                    item.SubItems[1].Text = isEmployee[0].employeeName;
                    item.SubItems[2].Text = isPosition[0].positionId.ToString();
                    item.SubItems[3].Text = isPosition[0].positionName;
                    item.SubItems[4].Text = performance.performance.ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("出错了！" + ex.ToString());
                return;
            }
        }


        /// <summary>
        /// 添加公司就职信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPerAdd_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(nudPerId.Value) == 0 || Convert.ToInt32(nudPerPosId.Value) == 0)
            {
                MessageBox.Show("请输入信息");
                return;
            }

            //先确定数据库中是否有此职工
            employeeInfo[] isEmployee = dataTools.selectTools.selectEmployeeInfo(u => u.employeeId == nudPerId.Value, u => u.employeeId);
            if (isEmployee.Length == 0)
            {
                MessageBox.Show("没有此员工，请输入正确的信息！");
                return;
            }

            //确定数据库中是否有此职位
            companyPosition[] isPosition = dataTools.selectTools.selectCompanyPosition(u => u.positionId == nudPerPosId.Value, u => u.positionId);
            if (isPosition.Length == 0)
            {
                MessageBox.Show("没有此职位，请输入正确的信息！");
                return;
            }

            employeePerformance employee = new employeePerformance()
            {
               employeeId = Convert.ToInt32(nudPerId.Value),
               positionId = Convert.ToInt32(nudPerPosId.Value),
               performance = Convert.ToDouble(nudPerPerformance.Value)
            };
            if (dataTools.insertTools.insertEmployeePerformance(employee) == false)
            {
                MessageBox.Show("添加失败");
            }
            else
            {
                MessageBox.Show("添加成功");
            }
        }

        /// <summary>
        /// 删除选中的公司就职信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPerDelete_Click(object sender, EventArgs e)
        {
            MessageBox.Show("要删除此记录请删除此员工的信息，如果员工在公司就职信息表中没有信息的话，表示此公司没有此员工就职，即没有此员工信息！");
            return;
        }

        /// <summary>
        /// 查询公司就职表中的信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPerSearch_Click(object sender, EventArgs e)
        {
            //清空
            Performance.Items.Clear();
            //初始一个查询表达式
            Expression<Func<employeePerformance, bool>> expression = x => x.employeeId == x.employeeId;
            //如果某个查询项选中的话 加入查询表达式
            //职工Id
            if (cboxPerSearchId.Checked == true)
            {
                if (Convert.ToInt32(nudPerSearchId.Value) != 0)
                {
                    expression = LambdaHelper.And<employeePerformance>(expression, x => x.employeeId == nudPerSearchId.Value);
                }
            }
            //职位Id
            if (cboxPerSearchPosId.Checked == true)
            {
                //name的值可能为空 所以要判断
                if (Convert.ToInt32(nudPosSearchId.Value) != 0)
                {
                    expression = LambdaHelper.And<employeePerformance>(expression, x => x.positionId == nudPosSearchId.Value);
                }
            }
            //业绩
            if (cboxPerSearchPerformance.Checked == true)
            {
                expression = LambdaHelper.And<employeePerformance>(expression, x => x.performance == Convert.ToDouble(nudPerSearchPerformance.Value));
            }

            employeePerformance[] info = dataTools.selectTools.selectEmployeePerformance(expression, x => x.employeeId);
            MessageBox.Show("成功查询到" + info.Length + "条数据");
            for (int count = 0; count < info.Length; count++)
            {
                ListViewItem item = new ListViewItem();
                item.Text = info[count].employeeId.ToString();
                int eId = info[count].employeeId;
                //根据职工Id找到职工的名字
                string epName = dataTools.selectTools.selectEmployeeInfo(u => u.employeeId == eId, u => u.employeeId)[0].employeeName;
                item.SubItems.Add(epName);
                item.SubItems.Add(info[count].positionId.ToString());
                int pId = info[count].positionId;
                //根据职位ID找到职位名字
                string pName = dataTools.selectTools.selectCompanyPosition(u => u.positionId == pId, u => u.positionId)[0].positionName;                
                item.SubItems.Add(pName);
                item.SubItems.Add(info[count].performance.ToString());
                Performance.Items.Add(item);
            }

        }

        #endregion
    }
}
