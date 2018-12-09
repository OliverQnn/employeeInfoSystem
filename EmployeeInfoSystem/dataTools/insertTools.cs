using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EmployeeInfoSystem.Model;

namespace EmployeeInfoSystem.dataTools
{
    class insertTools
    {
        /// <summary>
        /// 往职工信息表中插入数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns>插入数据后的对象</returns>
        public static bool insertEmployeeInfo(employeeInfo info)
        {
            try
            {
                using (employeeInfoSystemEntities db = new employeeInfoSystemEntities())
                {
                    db.employeeInfo.Add(info);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 往公司部门表中插入数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns>插入数据后的对象</returns>
        public static bool insertDepartment(department info)
        {
            try
            {
                using (employeeInfoSystemEntities db = new employeeInfoSystemEntities())
                {
                    db.department.Add(info);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 往公司职务表中插入数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns>插入数据后的对象</returns>
        public static bool insertCompanyPosition(companyPosition info)
        {
            try
            {
                using (employeeInfoSystemEntities db = new employeeInfoSystemEntities())
                {
                    db.companyPosition.Add(info);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 往公司职务表中插入数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns>插入数据后的对象</returns>
        public static bool insertEmployeePerformance(employeePerformance info)
        {
            try
            {
                using (employeeInfoSystemEntities db = new employeeInfoSystemEntities())
                {
                    db.employeePerformance.Add(info);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
