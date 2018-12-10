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
                {
                    MessageBox.Show("删除成功");
                    listInfo.Items.Remove(item);
                }                   
            }

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
        #region 部门
        private void btnDepSearch_Click(object sender, EventArgs e)
        {
            listDepartment.Items.Clear();
            //初始一个查询表达式
            Expression<Func<department, bool>> expression = x => x.departmentId == x.departmentId;
            if (cboxDepSearchId.Checked == true)
            {
                expression = LambdaHelper.And(expression, x => x.departmentId == nudDepSearchId.Value);
            }
            if (cboxDepSearchName.Checked == true)
            {
                if (txtDepSearchName.Text != "")
                {
                    expression = LambdaHelper.And(expression, x => x.departmentName == txtDepSearchName.Text);
                }
            }
            department[] deps = dataTools.selectTools.selectDepartment(expression, x => x.departmentId);
            MessageBox.Show("成功查询到" + deps.Length + "条数据");
            for (int count = 0; count < deps.Length; count++)
            {
                ListViewItem item = new ListViewItem();
                item.Text = deps[count].departmentId.ToString();
                item.SubItems.Add(deps[count].departmentName);
                listDepartment.Items.Add(item);
            }
        }
        //加载
        private void btnDepLoad_Click(object sender, EventArgs e)
        {
            if (listDepartment.SelectedItems.Count == 0)
                return;
            ListViewItem item = listDepartment.SelectedItems[0];
            txtDepName.Text = item.SubItems[1].Text;
        }
        //添加
        private void btnDepAdd_Click(object sender, EventArgs e)
        {
            if(txtDepName.Text=="")
            {
                MessageBox.Show("请输入信息");
                return;
            }
            department dep = new department()
            {
                departmentName = txtDepName.Text
            };
            if (dataTools.insertTools.insertDepartment(dep) == false)
            {
                MessageBox.Show("添加失败");
            }
            else
            {
                MessageBox.Show("添加成功");
            }
        }
        //删除
        private void btnDepDelete_Click(object sender, EventArgs e)
        {
            if (listDepartment.SelectedItems.Count == 0)
                return;
            ListViewItem item = listDepartment.SelectedItems[0];
            DialogResult dr = MessageBox.Show("确定要删除该条部门信息吗？\n（此操作会删除该部门在公司中所有的职位信息）",
                                                "删除", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                int id = int.Parse(item.Text);
                if (dataTools.deleteTools.deleteCompanyPositionInfo(id) == false)
                    MessageBox.Show("删除失败");
                else
                {
                    MessageBox.Show("删除成功");
                    listDepartment.Items.Remove(item);
                }

            }
        }
        //修改
        private void btnDepEdit_Click(object sender, EventArgs e)
        {
            if (listDepartment.SelectedItems.Count == 0)
                return;
            ListViewItem item = listDepartment.SelectedItems[0];
            if (txtDepName.Text == "") 
            {
                MessageBox.Show("请输入信息");
                return;
            }
            department dep = new department()
            {
                departmentName = txtDepName.Text
            };
            int id = int.Parse(item.Text);
            if (dataTools.updateTools.updateDepartment(x => x.departmentId == id, dep) == false)
                MessageBox.Show("修改失败");
            else
            {
                MessageBox.Show("修改成功");
                item.SubItems[1].Text = txtDepName.Text;
            }
        }
        #endregion
        #region 职位
        //查询
        private void btnPosSearch_Click(object sender, EventArgs e)
        {
            listPosition.Items.Clear();
            //初始一个查询表达式
            Expression<Func<companyPosition, bool>> expression = x => x.positionId == x.positionId;
            if (cboxPosSearchId.Checked == true)
            {
                expression = LambdaHelper.And(expression, x => x.positionId == nudPosSearchId.Value);
            }
            if (cboxPosSearchName.Checked == true)
            {
                if (txtPosSearchName.Text != "")
                {
                    expression = LambdaHelper.And(expression, x => x.positionName == txtPosSearchName.Text);
                }
            }
            if(cboxPosSearchDepId.Checked == true)
            {
                expression = LambdaHelper.And(expression, x => x.departmentId == nudPosSearchDepId.Value);
            }
            if (cboxPosSearchMoney.Checked == true)
            {
                double tmp = double.Parse(nudPosSearchMoney.Value.ToString());
                expression = LambdaHelper.And(expression, x => x.positionSalary == tmp);
            }
            companyPosition[] pos = dataTools.selectTools.selectCompanyPosition(expression, x => x.positionId);
            MessageBox.Show("成功查询到" + pos.Length + "条数据");
            for (int count = 0; count < pos.Length; count++)
            {
                ListViewItem item = new ListViewItem();
                item.Text = pos[count].positionId.ToString();
                item.SubItems.Add(pos[count].departmentId.ToString());
                item.SubItems.Add(pos[count].positionName);
                item.SubItems.Add(pos[count].positionSalary.ToString());
                listPosition.Items.Add(item);
            }
        }
        //加载
        private void btnPosLoad_Click(object sender, EventArgs e)
        {
            if (listPosition.SelectedItems.Count == 0)
                return;
            ListViewItem item = listPosition.SelectedItems[0];
            nudPosDepId.Value = int.Parse(item.SubItems[1].Text);
            txtPosName.Text = item.SubItems[2].Text;
            nudPosMoney.Value = int.Parse(item.SubItems[3].Text);
        }
        //添加
        private void btnPosAdd_Click(object sender, EventArgs e)
        {
            if (txtPosName.Text == "")
            {
                MessageBox.Show("请输入信息");
                return;
            }
            companyPosition pos = new companyPosition()
            {
                departmentId = int.Parse(nudPosDepId.Value.ToString()),
                positionSalary = int.Parse(nudPosMoney.Value.ToString()),
                positionName = txtPosName.Text
            };
            if (dataTools.insertTools.insertCompanyPosition(pos) == false)
            {
                MessageBox.Show("添加失败");
            }
            else
            {
                MessageBox.Show("添加成功");
            }
        }
        //删除
        private void btnPosDelete_Click(object sender, EventArgs e)
        {
            if (listPosition.SelectedItems.Count == 0)
                return;
            ListViewItem item = listPosition.SelectedItems[0];
            int id = int.Parse(item.Text);
            if (dataTools.deleteTools.deleteCompanyPosition(x => x.positionId == id) == false)
                MessageBox.Show("删除失败");
            else
                MessageBox.Show("删除成功");
            listPosition.Items.Remove(item);
        }
        //修改
        private void btnPosEdit_Click(object sender, EventArgs e)
        {
            if (txtPosName.Text == "")
            {
                MessageBox.Show("请输入信息");
                return;
            }
            if (listPosition.SelectedItems.Count == 0)
                return;
            ListViewItem item = listPosition.SelectedItems[0];
            companyPosition pos = new companyPosition()
            {
                departmentId = int.Parse(nudPosDepId.Value.ToString()),
                positionSalary = int.Parse(nudPosMoney.Value.ToString()),
                positionName = txtPosName.Text
            };
            int id = int.Parse(item.Text);
            if (dataTools.updateTools.updateCompanyPosition(x => x.positionId == id, pos) == false)
                MessageBox.Show("修改失败");
            else
            {
                MessageBox.Show("修改成功");
                item.SubItems[1].Text = pos.departmentId.ToString();
                item.SubItems[2].Text = pos.positionName;
                item.SubItems[3].Text = pos.positionSalary.ToString();
            }
        }
        #endregion
    }
}
